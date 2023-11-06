﻿using K9.Base.DataAccessLayer.Models;
using K9.Base.Globalisation;
using K9.Base.WebApplication.Config;
using K9.Base.WebApplication.Enums;
using K9.Base.WebApplication.Extensions;
using K9.Base.WebApplication.Filters;
using K9.Base.WebApplication.Models;
using K9.Base.WebApplication.Options;
using K9.Base.WebApplication.Services;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Authentication;
using K9.SharedLibrary.Extensions;
using K9.SharedLibrary.Helpers;
using K9.SharedLibrary.Models;
using K9.WebApplication.Config;
using K9.WebApplication.Helpers;
using K9.WebApplication.Models;
using K9.WebApplication.Services;
using K9.WebApplication.ViewModels;
using NLog;
using System;
using System.Linq;
using System.Web.Mvc;
using WebMatrix.WebData;
using Client = K9.DataAccessLayer.Models.Client;
using UserRole = K9.Base.DataAccessLayer.Models.UserRole;

namespace K9.WebApplication.Controllers
{
    public partial class AccountController : BasePureController
    {
        private readonly IRepository<User> _userRepository;
        private readonly ILogger _logger;
        private readonly IAccountService _accountService;
        private readonly IAuthentication _authentication;
        private readonly IFacebookService _facebookService;
        private readonly IMembershipService _membershipService;
        private readonly IClientService _clientService;
        private readonly IUserService _userService;
        private readonly IRepository<PromoCode> _promoCodesRepository;
        private readonly IRecaptchaService _recaptchaService;
        private readonly IRepository<UserProtocol> _userProtocolsRepository;
        private readonly IRepository<Protocol> _protocolsRepository;
        private readonly IRepository<UserRole> _userRolesRepository;
        private readonly IRepository<Role> _rolesRepository;
        private readonly RecaptchaConfiguration _recaptchaConfig;

        public AccountController(IRepository<User> userRepository, ILogger logger, IMailer mailer, IOptions<WebsiteConfiguration> websiteConfig, IDataSetsHelper dataSetsHelper, IRoles roles, IAccountService accountService, IAuthentication authentication, IFileSourceHelper fileSourceHelper, IFacebookService facebookService, IMembershipService membershipService, IClientService clientService, IUserService userService, IRepository<PromoCode> promoCodesRepository, IOptions<RecaptchaConfiguration> recaptchaConfig, IRecaptchaService recaptchaService, IRepository<UserProtocol> userProtocolsRepository, IRepository<Protocol> protocolsRepository, IRepository<UserRole> userRolesRepository, IRepository<Role> rolesRepository)
            : base(logger, dataSetsHelper, roles, authentication, fileSourceHelper, membershipService)
        {
            _userRepository = userRepository;
            _logger = logger;
            _accountService = accountService;
            _authentication = authentication;
            _facebookService = facebookService;
            _membershipService = membershipService;
            _clientService = clientService;
            _userService = userService;
            _promoCodesRepository = promoCodesRepository;
            _recaptchaService = recaptchaService;
            _userProtocolsRepository = userProtocolsRepository;
            _protocolsRepository = protocolsRepository;
            _userRolesRepository = userRolesRepository;
            _rolesRepository = rolesRepository;
            _recaptchaConfig = recaptchaConfig.Value;

            websiteConfig.Value.RegistrationEmailTemplateText = Globalisation.Dictionary.WelcomeEmail;
            websiteConfig.Value.PasswordResetEmailTemplateText = Globalisation.Dictionary.PasswordResetEmail;
        }

        #region Membership

        public ActionResult Login(string returnUrl, string retrieveLast = null)
        {
            if (WebSecurity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            TempData["ReturnUrl"] = returnUrl;
            TempData["RetrieveLast"] = retrieveLast;

            return View(CookieService.GetLoginCookie());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserAccount.LoginModel model)
        {
            if (ModelState.IsValid)
            {
                switch (_accountService.Login(model.UserName, model.Password, model.RememberMe))
                {
                    case ELoginResult.Success:
                        if (model.RememberMe)
                        {
                            CookieService.SetUsernameCookie(model.UserName, model.Password);
                        }

                        var user = _userRepository.Find(e => e.Username == model.UserName).First();
                        if (!user.IsActivated)
                        {
                            _accountService.Logout();
                            ModelState.AddModelError("", Dictionary.AccountNotActivatedError);
                            break;
                        }

                        if (TempData["ReturnUrl"] != null)
                        {
                            return Redirect(TempData["ReturnUrl"].ToString());
                        }
                        return RedirectToAction("MyAccount", "Account");

                    case ELoginResult.AccountLocked:
                        return RedirectToAction("AccountLocked");

                    case ELoginResult.AccountNotActivated:
                        ModelState.AddModelError("", Dictionary.AccountNotActivatedError);
                        break;

                    default:
                        ModelState.AddModelError("", Dictionary.UsernamePasswordIncorrectError);
                        break;
                }
            }
            else
            {
                ModelState.AddModelError("", Dictionary.UsernamePasswordIncorrectError);
            }

            return View(model);
        }

        public ActionResult Facebook()
        {
            return Redirect(_facebookService.GetLoginUrl().AbsoluteUri);
        }

        public ActionResult FacebookCallback(string code)
        {
            var result = _facebookService.Authenticate(code);
            if (result.IsSuccess)
            {
                var user = result.Data as User;
                var regResult = _accountService.RegisterOrLoginAuth(new UserAccount.RegisterModel
                {
                    UserName = user.Username,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    BirthDate = user.BirthDate,
                    EmailAddress = user.EmailAddress
                });

                user.Id = _userRepository.Find(e => e.Username == user.Username).FirstOrDefault()?.Id ?? 0;

                if (user.Id > 0)
                {
                    _membershipService.CreateFreeMembership(user.Id);
                }

                if (regResult.IsSuccess)
                {
                    return RedirectToAction("RegisterPromoCode", "Account");
                }
                result.Errors.AddRange(regResult.Errors);
            }

            foreach (var registrationError in result.Errors)
            {
                if (registrationError.Exception != null && registrationError.Exception.IsDuplicateIndexError())
                {
                    var duplicateUser = registrationError.Data.MapTo<User>();
                    var serviceError = registrationError.Exception.GetServiceErrorFromException(duplicateUser);
                    ModelState.AddModelError("", serviceError.ErrorMessage);
                }
                else
                {
                    ModelState.AddModelError(registrationError.FieldName, registrationError.ErrorMessage);
                }
            }

            return View("Login", new UserAccount.LoginModel());
        }

        [Authorize]
        public ActionResult RegisterPromoCode(string username)
        {
            return View(new RegisterPromoCodeModel
            {
                Username = username
            });
        }

        [HttpPost]
        [Authorize]
        public ActionResult RegisterPromoCode(RegisterPromoCodeModel model)
        {
            try
            {
                if (_userService.CheckIfPromoCodeIsUsed(model.PromoCode))
                {
                    ModelState.AddModelError("PromoCode", Globalisation.Dictionary.PromoCodeInUse);
                    return View(model);
                };
                
                var user = _userRepository.Find(e => e.Username == model.Username).First();

                try
                {  
                    _userService.UsePromoCode(user.Id, model.PromoCode);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("PromoCode", e.Message);
                }

                _membershipService.ProcessPurchaseWithPromoCode(user.Id, model.PromoCode);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("PromoCode", e.Message);
                return View(model);
            }
        }

        public ActionResult AccountLocked()
        {
            return View();
        }

        [Authorize]
        public ActionResult LogOff()
        {
            _accountService.Logout();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register(string promoCode = null)
        {
            ViewBag.RecaptchaSiteKey = _recaptchaConfig.RecaptchaSiteKey;

            if (WebSecurity.IsAuthenticated)
            {
                WebSecurity.Logout();
            }

            if (promoCode != null)
            {
                try
                {
                    if (_userService.CheckIfPromoCodeIsUsed(promoCode))
                    {
                        ModelState.AddModelError("PromoCode", Globalisation.Dictionary.PromoCodeInUse);
                    };
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("PromoCode", e.Message);
                }
            }

            return View(new RegisterViewModel
            {
                RegisterModel = new UserAccount.RegisterModel
                {
                    Gender = Methods.GetRandomGender(),
                    BirthDate = DateTime.Today.AddYears(-27)
                },
                PromoCode = promoCode
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            ViewBag.RecaptchaSiteKey = _recaptchaConfig.RecaptchaSiteKey;

            var encodedResponse = Request.Form[RecaptchaResult.ResponseFormVariable];
            var isCaptchaValid = _recaptchaService.Validate(encodedResponse);

            if (!isCaptchaValid)
            {
                ModelState.AddModelError("", Globalisation.Dictionary.InvalidRecaptcha);
                return View(model);
            }

            if (_authentication.IsAuthenticated)
            {
                _authentication.Logout();
            }

            if (ModelState.IsValid)
            {
                var result = _accountService.Register(model.RegisterModel);

                if (result.IsSuccess)
                {
                    var user = _userRepository.Find(e => e.Username == model.RegisterModel.UserName).FirstOrDefault();

                    if (!string.IsNullOrEmpty(model.PromoCode))
                    {
                        if (user?.Id > 0)
                        {
                            try
                            {
                                _userService.UsePromoCode(user.Id, model.PromoCode);
                            }
                            catch (Exception e)
                            {
                                ModelState.AddModelError("PromoCode", e.Message);
                            }

                            _membershipService.ProcessPurchaseWithPromoCode(user.Id, model.PromoCode);
                        }
                        else
                        {
                            _logger.Error("AccountController => Register => Promo code used but UserId is 0");
                            return RedirectToAction("AccountCreated", "Account", new { additionalError = Globalisation.Dictionary.PromoCodeNotUsed });
                        }
                    }

                    // Add to client users
                    var clientUserRole = _rolesRepository.Find(e => e.Name == Constants.Constants.ClientUser).FirstOrDefault();
                    var userRole = new UserRole
                    {
                        UserId = user.Id,
                        RoleId = clientUserRole.Id
                    };
                    _userRolesRepository.Create(userRole);

                    // Create client record
                    _clientService.GetOrCreateClient("", user.FullName, user.EmailAddress, user.PhoneNumber, user.Id);

                    return RedirectToAction("AccountCreated", "Account");
                }

                foreach (var registrationError in result.Errors)
                {
                    if (registrationError.Exception != null && registrationError.Exception.IsDuplicateIndexError())
                    {
                        var user = registrationError.Data.MapTo<User>();
                        var serviceError = registrationError.Exception.GetServiceErrorFromException(user);
                        ModelState.AddModelError("", serviceError.ErrorMessage);
                    }
                    else
                    {
                        ModelState.AddModelError(registrationError.FieldName, registrationError.ErrorMessage);
                    }
                }
            }

            return View(model);
        }

        [Authorize]
        public ActionResult UpdatePassword()
        {
            return View();
        }

        [Authorize]
        public ActionResult UpdatePasswordSuccess()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdatePassword(UserAccount.LocalPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _accountService.UpdatePassword(model);

                if (result.IsSuccess)
                {
                    return RedirectToAction("UpdatePasswordSuccess", "Account");
                }

                foreach (var registrationError in result.Errors)
                {
                    ModelState.AddModelError(registrationError.FieldName, registrationError.ErrorMessage);
                }
            }

            return View(model);
        }

        [Authorize]
        public ActionResult MyAccount()
        {
            var user = _userRepository.Find(u => u.Username == User.Identity.Name).FirstOrDefault();
            var userProtocols = _userProtocolsRepository.Find(e => e.UserId == user.Id);
            var protocols = _protocolsRepository.Find(e => userProtocols.Select(u => u.ProtocolId).Contains(e.Id))
                .ToList();

            return View(new MyAccountViewModel
            {
                User = user,
                Membership = _membershipService.GetActiveUserMembership(user?.Id),
                Protocols = protocols
            });
        }

        [Authorize]
        [HttpGet]
        public ActionResult UpdateAccount()
        {
            return RedirectToAction("MyAccount");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateAccount(User model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _userRepository.Update(model);
                    ViewBag.IsPopupAlert = true;
                    ViewBag.AlertOptions = new AlertOptions
                    {
                        AlertType = EAlertType.Success,
                        Message = Dictionary.Success,
                        OtherMessage = Dictionary.AccountUpdatedSuccess
                    };
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.GetFullErrorMessage());
                    ModelState.AddModelError("", Dictionary.FriendlyErrorMessage);
                }
            }

            return View("MyAccount", new MyAccountViewModel
            {
                User = model,
                Membership = _membershipService.GetActiveUserMembership(model.Id)
            });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAccount(ConfirmDeleteAccountModel model)
        {
            try
            {
                if (_accountService.DeleteAccount(model.UserId).IsSuccess)
                {
                    return RedirectToAction("DeleteAccountSuccess");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.GetFullErrorMessage());
            }

            return RedirectToAction("DeleteAccountFailed");
        }

        [Route("promocodes/email")]
        [Authorize]
        public ActionResult EmailPromoCode(int id)
        {
            var promoCode = _promoCodesRepository.Find(id);
            var model = new EmailPromoCodeViewModel
            {
                PromoCode = promoCode
            };
            return View(model);
        }

        [Route("promocodes/email")]
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmailPromoCode(EmailPromoCodeViewModel model)
        {
            try
            {
                _userService.SendPromoCode(model);
            }
            catch (Exception e)
            {
                _logger.Error($"AccountController => EmailPromocode => Error: {e.GetFullErrorMessage()}");
                throw;
            }

            return RedirectToAction("PromoCodeEmailSent");
        }

        public ActionResult PromoCodeEmailSent()
        {
            return View();
        }

        public ActionResult ConfirmDeleteAccount(int id)
        {
            var user = _userRepository.Find(id);
            if (user == null || user.Username != _authentication.CurrentUserName)
            {
                return HttpNotFound();
            }
            return View(new ConfirmDeleteAccountModel { UserId = id });
        }

        public ActionResult DeleteAccountSuccess()
        {
            return View();
        }

        public ActionResult DeleteAccountFailed()
        {
            return View();
        }

        #endregion


        #region Password Reset

        public ActionResult PasswordResetEmailSent()
        {
            return View();
        }

        public ActionResult PasswordResetRequest()
        {
            if (WebSecurity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PasswordResetRequest(UserAccount.PasswordResetRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _accountService.PasswordResetRequest(model);
                if (result.IsSuccess)
                {
                    return RedirectToAction("PasswordResetEmailSent", "Account", new { userName = model.UserName, result.Data });
                }

                return RedirectToAction("ResetPasswordFailed");
            }

            return View(model);
        }

        public ActionResult ResetPassword(string username, string token)
        {
            if (!_accountService.ConfirmUserFromToken(username, token))
            {
                return RedirectToAction("ResetPasswordFailed");
            }

            var model = new UserAccount.ResetPasswordModel
            {
                UserName = username,
                Token = token
            };

            return View(model);
        }

        public ActionResult ResetPasswordFailed()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(UserAccount.ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _accountService.ResetPassword(model);
                if (result.IsSuccess)
                {
                    return RedirectToAction("ResetPasswordSuccess");
                }

                foreach (var registrationError in result.Errors)
                {
                    ModelState.AddModelError(registrationError.FieldName, registrationError.ErrorMessage);
                }
            }

            return View(model);
        }

        [Authorize]
        public ActionResult ResetPasswordSuccess()
        {
            return View();
        }

        #endregion


        #region Account Activation

        [AllowAnonymous]
        public ActionResult AccountCreated(string userName, string additionalError = "")
        {
            ViewBag.AdditionalError = additionalError;
            return View();
        }

        [AllowAnonymous]
        public ActionResult AccountActivated(string userName)
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult AccountActivationFailed()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult AccountAlreadyActivated()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult AccountDeactivated()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ActivateAccount(string userName, string token)
        {
            var result = _accountService.ActivateAccount(userName, token);

            switch (result.Result)
            {
                case EActivateAccountResult.Success:
                    _membershipService.CreateFreeMembership(result.User.Id);
                    return RedirectToAction("AccountActivated", "Account", new { userName });

                case EActivateAccountResult.AlreadyActivated:
                    return RedirectToAction("AccountAlreadyActivated", "Account");

                default:
                    return RedirectToAction("AccountActivationFailed", "Account");
            }
        }

        [RequirePermissions(Permission = Permissions.Edit)]
        public ActionResult ActivateUserAccount(int userId)
        {
            var result = _accountService.ActivateAccount(userId);

            switch (result.Result)
            {
                case EActivateAccountResult.Success:
                    var user = _userRepository.Find(userId);
                    return RedirectToAction("AccountActivated", "Account", new { userName = user.Username });

                case EActivateAccountResult.AlreadyActivated:
                    return RedirectToAction("AccountAlreadyActivated", "Account");

                default:
                    return RedirectToAction("AccountActivationFailed", "Account");
            }
        }

        [RequirePermissions(Permission = Permissions.Edit)]
        public ActionResult DeactivateUserAccount(int userId)
        {
            var user = _userRepository.Find(userId);
            user.IsDeleted = true;
            _userRepository.Update(user);
            return RedirectToAction("AccountAlreadyActivated", "Account");
        }

        [Route("unsubscribe")]
        public ActionResult Unsubscribe(string code)
        {
            if (_clientService.Unsubscribe(code))
            {
                return View("UnsubscribeSuccess");
            }

            return View("UnsubscribeFailed");
        }

        #endregion


        #region Helpers

        public override string GetObjectName()
        {
            return typeof(User).Name;
        }

        #endregion

    }
}