using K9.Base.DataAccessLayer.Models;
using K9.Base.WebApplication.Config;
using K9.DataAccessLayer.Models;
using K9.Globalisation;
using K9.SharedLibrary.Extensions;
using K9.SharedLibrary.Helpers;
using K9.SharedLibrary.Models;
using K9.WebApplication.Models;
using K9.WebApplication.ViewModels;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace K9.WebApplication.Services
{
    public class MembershipService : IMembershipService
    {
        private readonly ILogger _logger;
        private readonly IAuthentication _authentication;
        private readonly IRepository<MembershipOption> _membershipOptionRepository;
        private readonly IRepository<UserMembership> _userMembershipRepository;
        private readonly IRepository<UserCreditPack> _userCreditPacksRepository;
        private readonly IRepository<User> _usersRepository;
        private readonly IContactService _contactService;
        private readonly IMailer _mailer;
        private readonly IRepository<PromoCode> _promoCodesRepository;
        private readonly IRepository<UserConsultation> _userConsultationsRepository;
        private readonly WebsiteConfiguration _config;
        private readonly UrlHelper _urlHelper;

        public MembershipService(ILogger logger, IAuthentication authentication, IRepository<MembershipOption> membershipOptionRepository, IRepository<UserMembership> userMembershipRepository, IRepository<UserCreditPack> userCreditPacksRepository, IRepository<User> usersRepository, IContactService contactService, IMailer mailer, IOptions<WebsiteConfiguration> config, IRepository<PromoCode> promoCodesRepository, IRepository<UserConsultation> userConsultationsRepository)
        {
            _logger = logger;
            _authentication = authentication;
            _membershipOptionRepository = membershipOptionRepository;
            _userMembershipRepository = userMembershipRepository;
            _userCreditPacksRepository = userCreditPacksRepository;
            _usersRepository = usersRepository;
            _contactService = contactService;
            _mailer = mailer;
            _promoCodesRepository = promoCodesRepository;
            _userConsultationsRepository = userConsultationsRepository;
            _config = config.Value;
            _urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
        }

        public MembershipViewModel GetMembershipViewModel(int? userId = null)
        {
            userId = userId ?? _authentication.CurrentUserId;
            var membershipOptions = _membershipOptionRepository.List();
            var activeUserMembership = GetActiveUserMembership(userId);

            return new MembershipViewModel
            {
                MembershipModels = new List<MembershipModel>(membershipOptions.Select(membershipOption =>
                {
                    var isSubscribed = activeUserMembership != null && activeUserMembership.MembershipOptionId == membershipOption.Id;
                    var isUpgradable = activeUserMembership == null || activeUserMembership.MembershipOption.CanUpgradeTo(membershipOption);

                    return new MembershipModel(_authentication.CurrentUserId, membershipOption, activeUserMembership)
                    {
                        IsSelectable = !isSubscribed && isUpgradable,
                        IsSubscribed = isSubscribed
                    };
                }))
            };
        }

        public List<UserMembership> GetActiveUserMemberships(int? userId = null, bool includeScheduled = false)
        {
            userId = userId ?? _authentication.CurrentUserId;
            var membershipOptions = _membershipOptionRepository.List();
            var userMemberships = _authentication.IsAuthenticated
                ? _userMembershipRepository.Find(_ => _.UserId == userId).ToList().Where(_ => _.IsActive || includeScheduled && _.EndsOn > DateTime.Today).Select(userMembership =>
                {
                    userMembership.MembershipOption = membershipOptions.FirstOrDefault(m => m.Id == userMembership.MembershipOptionId);
                    userMembership.UserConsultations = _userConsultationsRepository.Find(e => e.UserId == userId).ToList();
                    userMembership.NumberOfCreditsLeft = GetNumberOfCreditsLeft(userMembership);

                    return userMembership;
                }).ToList()
                : new List<UserMembership>();
            return userMemberships;
        }

        private int GetNumberOfCreditsLeft(UserMembership userMembership)
        {
            var creditPacks = _userCreditPacksRepository.Find(e => e.UserId == userMembership.UserId);
            var creditPackIds = creditPacks.Select(c => c.Id);
            var numberOfUsedCredits = (int?)0;
            //_userProfileReadingsRepository.Find(e => creditPackIds.Contains(e.UserCreditPackId ?? 0))?.Count() +
            //_userRelationshipCompatibilityReadingsRepository.Find(e => creditPackIds.Contains(e.UserCreditPackId ?? 0))?.Count();
            var totalCredits = creditPacks.Any() ? creditPacks.Sum(e => e.NumberOfCredits) : 0;
            return totalCredits - numberOfUsedCredits ?? 0;
        }

        /// <summary>
        /// Sometimes user memberships can overlap, when upgrading for example. This returns the Active membership.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserMembership GetActiveUserMembership(int? userId = null)
        {
            var activeUserMembership = GetActiveUserMemberships(userId).OrderByDescending(_ => _.MembershipOption.SubscriptionType)
                .FirstOrDefault();

            if (activeUserMembership == null && userId.HasValue)
            {
                CreateFreeMembership(userId.Value);
                activeUserMembership = GetActiveUserMemberships(userId).OrderByDescending(_ => _.MembershipOption.SubscriptionType)
                    .FirstOrDefault();
            }

            return activeUserMembership;
        }

        public MembershipModel GetSwitchMembershipModel(int membershipOptionId)
        {
            var userMemberships = GetActiveUserMemberships();
            if (!userMemberships.Any())
            {
                throw new Exception(Dictionary.SwitchMembershipErrorNotSubscribed);
            }

            var activeUserMembership = GetActiveUserMembership();
            if (activeUserMembership.MembershipOptionId == membershipOptionId)
            {
                throw new Exception(Dictionary.SwitchMembershipErrorAlreadySubscribed);
            }

            var membershipOption = _membershipOptionRepository.Find(membershipOptionId);
            if (!activeUserMembership.MembershipOption.CanUpgradeTo(membershipOption))
            {
                throw new Exception(Dictionary.CannotSwitchMembershipError);
            }

            return new MembershipModel(_authentication.CurrentUserId, membershipOption, activeUserMembership)
            {
                IsSelected = true
            };
        }

        public MembershipModel GetPurchaseMembershipModel(int membershipOptionId)
        {
            var activeUserMembership = GetActiveUserMembership();
            if (activeUserMembership?.MembershipOptionId == membershipOptionId)
            {
                throw new Exception(Dictionary.PurchaseMembershipErrorAlreadySubscribed);
            }

            var membershipOption = _membershipOptionRepository.Find(membershipOptionId);
            var userMemberships = GetActiveUserMemberships();
            if (userMemberships.Any(e => e.MembershipOption.Id != membershipOption.Id))
            {
                throw new Exception(Dictionary.PurchaseMembershipErrorAlreadySubscribedToAnother);
            }

            return new MembershipModel(_authentication.CurrentUserId, membershipOption)
            {
                IsSelected = true
            };
        }

        public MembershipModel GetSwitchMembershipModelBySubscriptionType(MembershipOption.ESubscriptionType subscriptionType)
        {
            var membershipOption = _membershipOptionRepository.Find(e => e.SubscriptionType == subscriptionType).FirstOrDefault();
            return GetSwitchMembershipModel(membershipOption?.Id ?? 0);
        }

        public MembershipModel GetPurchaseMembershipModelBySubscriptionType(MembershipOption.ESubscriptionType subscriptionType)
        {
            var membershipOption = _membershipOptionRepository.Find(e => e.SubscriptionType == subscriptionType).FirstOrDefault();
            return GetPurchaseMembershipModel(membershipOption?.Id ?? 0);
        }

        public void ProcessPurchaseWithPromoCode(int userId, string code)
        {
            var promoCode = _promoCodesRepository.Find(e => e.Code == code).FirstOrDefault();

            if (promoCode == null)
            {
                _logger.Error($"MembershipService => ProcessPurchaseWithPromoCode => Invalid Promo Code");
                throw new Exception("Invalid promo code");
            }

            var subscription = _membershipOptionRepository.Find(e => e.SubscriptionType == promoCode.SubscriptionType).FirstOrDefault();

            if (subscription == null)
            {
                _logger.Error($"MembershipService => ProcessPurchaseWithPromoCode => No subscription of type {promoCode.SubscriptionTypeName} found");
                throw new Exception($"No subscription of type {promoCode.SubscriptionTypeName} found");
            }

            var credits = promoCode.Credits;
            var user = _usersRepository.Find(userId);
            var contact = _contactService.GetOrCreateContact("", user.FullName, user.EmailAddress, user.PhoneNumber);

            ProcessPurchase(new PurchaseModel
            {
                ItemId = subscription.Id,
                ContactId = contact.Id
            }, userId, promoCode);

            ProcessCreditsPurchase(new PurchaseModel
            {
                ItemId = subscription.Id,
                ContactId = contact.Id,
                Quantity = credits
            }, userId, promoCode);
        }

        public void ProcessPurchase(PurchaseModel purchaseModel, int? userId = null, PromoCode promoCode = null)
        {
            try
            {
                var membershipOptionId = purchaseModel.ItemId;
                var membershipOption = _membershipOptionRepository.Find(membershipOptionId);
                if (membershipOption == null)
                {
                    _logger.Error($"MembershipService => ProcessPurchase => No MembershipOption with id {membershipOptionId} was found.");
                    throw new IndexOutOfRangeException("Invalid MembershipOptionId");
                }

                var userMembership = new UserMembership
                {
                    UserId = userId ?? _authentication.CurrentUserId,
                    MembershipOptionId = membershipOptionId,
                    StartsOn = DateTime.Today,
                    EndsOn = membershipOption.IsAnnual ? DateTime.Today.AddYears(1) : DateTime.Today.AddMonths(1),
                    IsAutoRenew = true
                };

                _userMembershipRepository.Create(userMembership);
                userMembership.User = _usersRepository.Find(userId ?? _authentication.CurrentUserId);
                TerminateExistingMemberships(membershipOptionId);

                var contact = _contactService.Find(purchaseModel.ContactId);

                SendEmailToPureAlchemy(userMembership, promoCode);
                SendEmailToCustomer(userMembership, contact, promoCode);
            }
            catch (Exception ex)
            {
                _logger.Error($"MembershipService => ProcessPurchase => Purchase failed: {ex.GetFullErrorMessage()}");
                SendEmailToPureAlchemyAboutFailure(purchaseModel, ex.GetFullErrorMessage());
                throw ex;
            }
        }

        public void ProcessCreditsPurchase(PurchaseModel purchaseModel, int? userId = null, PromoCode promoCode = null)
        {
            try
            {
                var numberOfCredits = purchaseModel.Quantity;
                var creditsModel = new PurchaseCreditsViewModel
                {
                    NumberOfCredits = numberOfCredits
                };

                var userCreditPack = new UserCreditPack
                {
                    UserId = userId ?? _authentication.CurrentUserId,
                    NumberOfCredits = numberOfCredits,
                    TotalPrice = promoCode?.TotalPrice ?? creditsModel.TotalPrice
                };

                _userCreditPacksRepository.Create(userCreditPack);
                userCreditPack.User = _usersRepository.Find(_authentication.CurrentUserId);

                var contact = _contactService.Find(purchaseModel.ContactId);

                SendEmailToPureAlchemy(userCreditPack, promoCode);
                SendEmailToCustomer(userCreditPack, contact, promoCode);
            }
            catch (Exception ex)
            {
                _logger.Error($"MembershipService => ProcessPurchase => Purchase failed: {ex.GetFullErrorMessage()}");
                SendEmailToPureAlchemyAboutFailure(purchaseModel, ex.GetFullErrorMessage());
                throw ex;
            }
        }

        public void ProcessSwitch(int membershipOptionId)
        {
            try
            {
                var membershipOption = _membershipOptionRepository.Find(membershipOptionId);
                if (membershipOption == null)
                {
                    _logger.Error($"MembershipService => ProcessSwitch => No MembershipOption with id {membershipOptionId} was found.");
                    throw new IndexOutOfRangeException("Invalid MembershipOptionId");
                }

                _userMembershipRepository.Create(new UserMembership
                {
                    UserId = _authentication.CurrentUserId,
                    MembershipOptionId = membershipOptionId,
                    StartsOn = DateTime.Today,
                    EndsOn = membershipOption.IsAnnual ? DateTime.Today.AddYears(1) : DateTime.Today.AddMonths(1),
                    IsAutoRenew = true
                });
                TerminateExistingMemberships(membershipOptionId);
            }
            catch (Exception ex)
            {
                _logger.Error($"MembershipService => ProcessSwitch => Switch failed: {ex.GetFullErrorMessage()}");
                throw ex;
            }
        }

        public void CreateFreeMembership(int userId)
        {
            try
            {
                var membershipOption = _membershipOptionRepository.Find(e => e.SubscriptionType == MembershipOption.ESubscriptionType.Free).FirstOrDefault();

                if (membershipOption == null)
                {
                    _logger.Error($"MembershipService => CreateFreeMembership => MembershipOption with Subscription Type {MembershipOption.ESubscriptionType.Free} was not found.");
                    return;
                }

                _userMembershipRepository.Create(new UserMembership
                {
                    UserId = userId,
                    MembershipOptionId = membershipOption.Id,
                    StartsOn = DateTime.Today,
                    EndsOn = DateTime.MaxValue,
                    IsAutoRenew = true
                });
            }
            catch (Exception ex)
            {
                _logger.Error($"MembershipService => CreateFreeMembership => failed: {ex.GetFullErrorMessage()}");
                throw ex;
            }
        }

        private void TerminateExistingMemberships(int activeUserMembershipId)
        {
            var userMemberships = GetActiveUserMemberships();
            var activeUserMembership =
                userMemberships.FirstOrDefault(_ => _.MembershipOptionId == activeUserMembershipId);
            if (activeUserMembership == null)
            {
                _logger.Error($"MembershipService => TerminateExistingMemberships => ActiveMembership cannot be determined or does not exist");
                return;
            }
            foreach (var userMembership in userMemberships.Where(_ => _.MembershipOptionId != activeUserMembershipId))
            {
                userMembership.EndsOn = activeUserMembership.StartsOn;
                userMembership.IsDeactivated = true;
                _userMembershipRepository.Update(userMembership);
            }
        }

        private void SendEmailToPureAlchemy(UserMembership userMembership, PromoCode promoCode)
        {
            var template = Dictionary.MembershipCreatedEmail;
            var title = "We have received a new subscription!";
            _mailer.SendEmail(title, TemplateProcessor.PopulateTemplate(template, new
            {
                Title = title,
                Customer = userMembership.User.FullName,
                CustomerEmail = userMembership.User.EmailAddress,
                SubscriptionType = userMembership.MembershipOption.SubscriptionTypeNameLocal,
                TotalPrice = promoCode?.FormattedPrice ?? userMembership.MembershipOption.FormattedPrice,
                LinkToSummary = _urlHelper.AbsoluteAction("Index", "UserMemberships"),
                Company = _config.CompanyName,
                ImageUrl = _urlHelper.AbsoluteContent(_config.CompanyLogoUrl)
            }), _config.SupportEmailAddress, _config.CompanyName, _config.SupportEmailAddress, _config.CompanyName);
        }

        private void SendEmailToCustomer(UserMembership userMembership, Contact contact, PromoCode promoCode = null)
        {
            var template = Dictionary.NewMembershipThankYouEmail;
            var title = TemplateProcessor.PopulateTemplate(Dictionary.ThankyouForSubscriptionEmailTitle, new
            {
                SubscriptionType = userMembership.MembershipOption.SubscriptionTypeNameLocal
            });
            if (contact != null && !contact.IsUnsubscribed)
            {
                _mailer.SendEmail(title, TemplateProcessor.PopulateTemplate(template, new
                {
                    Title = title,
                    CustomerName = userMembership.User.FirstName,
                    SubscriptionType = userMembership.MembershipOption.SubscriptionTypeNameLocal,
                    TotalPrice = promoCode.FormattedPrice ?? userMembership.MembershipOption.FormattedPrice,
                    EndsOn = userMembership.EndsOn.ToLongDateString(),
                    NumberOfConsultations = userMembership.MembershipOption.NumberOfConsultations,
                    ImageUrl = _urlHelper.AbsoluteContent(_config.CompanyLogoUrl),
                    PrivacyPolicyLink = _urlHelper.AbsoluteAction("PrivacyPolicy", "Home"),
                    UnsubscribeLink = _urlHelper.AbsoluteAction("Unsubscribe", "Account", new { id = contact.Id }),
                    DateTime.Now.Year
                }), userMembership.User.EmailAddress, userMembership.User.FirstName, _config.SupportEmailAddress,
                    _config.CompanyName);
            }
        }

        private void SendEmailToPureAlchemy(UserCreditPack userCreditPack, PromoCode promoCode)
        {
            var template = Dictionary.CreditPackPurchased;
            var title = "We have received a new credit pack purchase!";
            _mailer.SendEmail(title, TemplateProcessor.PopulateTemplate(template, new
            {
                Title = title,
                Customer = userCreditPack.User.FullName,
                CustomerEmail = userCreditPack.User.EmailAddress,
                userCreditPack.NumberOfCredits,
                TotalPrice = promoCode?.FormattedPrice ?? userCreditPack.FormattedPrice,
                LinkToCreditPacks = _urlHelper.AbsoluteAction("Index", "UserCreditPacks"),
                Company = _config.CompanyName,
                ImageUrl = _urlHelper.AbsoluteContent(_config.CompanyLogoUrl)
            }), _config.SupportEmailAddress, _config.CompanyName, _config.SupportEmailAddress, _config.CompanyName);
        }

        private void SendEmailToCustomer(UserCreditPack userCreditPack, Contact contact, PromoCode promoCode = null)
        {
            var template = Dictionary.NewCreditPackThankYouEmail;
            var title = Dictionary.ThankyouForCreditPackPurchaseEmailTitle;
            if (contact != null && !contact.IsUnsubscribed)
            {
                _mailer.SendEmail(title, TemplateProcessor.PopulateTemplate(template, new
                {
                    Title = title,
                    CustomerName = userCreditPack.User.FirstName,
                    NumberOfCreditsPurchased = userCreditPack.NumberOfCredits,
                    TotalPrice = promoCode?.FormattedPrice ?? userCreditPack.FormattedPrice,
                    ImageUrl = _urlHelper.AbsoluteContent(_config.CompanyLogoUrl),
                    PrivacyPolicyLink = _urlHelper.AbsoluteAction("PrivacyPolicy", "Home"),
                    UnsubscribeLink = _urlHelper.AbsoluteAction("Unsubscribe", "Account", new { code = contact.Name }),
                    DateTime.Now.Year
                }), userCreditPack.User.EmailAddress, userCreditPack.User.FirstName, _config.SupportEmailAddress,
                    _config.CompanyName);
            }
        }

        private void SendEmailToPureAlchemyAboutFailure(PurchaseModel purchaseModel, string errorMessage)
        {
            var template = Dictionary.PaymentError;
            var title = "A customer made a successful payment, but an error occurred.";
            _mailer.SendEmail(title, TemplateProcessor.PopulateTemplate(template, new
            {
                Title = title,
                Customer = purchaseModel.CustomerName,
                CustomerEmail = purchaseModel.CustomerEmailAddress,
                ErrorMessage = errorMessage,
                Company = _config.CompanyName,
                ImageUrl = _urlHelper.AbsoluteContent(_config.CompanyLogoUrl)
            }), _config.SupportEmailAddress, _config.CompanyName, _config.SupportEmailAddress, _config.CompanyName);
        }

    }
}