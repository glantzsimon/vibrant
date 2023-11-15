using K9.Base.WebApplication.Config;
using K9.Base.WebApplication.ViewModels;
using K9.DataAccessLayer.Models;
using K9.Globalisation;
using K9.SharedLibrary.Extensions;
using K9.SharedLibrary.Helpers;
using K9.SharedLibrary.Models;
using K9.WebApplication.Config;
using K9.WebApplication.Models;
using K9.WebApplication.Services;
using NLog;
using System;
using System.Web.Mvc;
using K9.WebApplication.Packages;

namespace K9.WebApplication.Controllers
{
    public class SupportController : BasePureController
    {
        private readonly ILogger _logger;
        private readonly IMailer _mailer;
        private readonly IDonationService _donationService;
        private readonly IClientService _clientService;
        private readonly IRecaptchaService _recaptchaService;
        private readonly RecaptchaConfiguration _recaptchaConfig;
        private readonly WebsiteConfiguration _config;
        private readonly UrlHelper _urlHelper;

        public SupportController(ILogger logger, IDataSetsHelper dataSetsHelper, IRoles roles, IMailer mailer, IOptions<WebsiteConfiguration> config, IAuthentication authentication, IFileSourceHelper fileSourceHelper, IOptions<StripeConfiguration> stripeConfig, IDonationService donationService, IMembershipService membershipService, IClientService clientService, IOptions<RecaptchaConfiguration> recaptchaConfig, IRecaptchaService recaptchaService, IPureControllerPackage pureControllerPackage)
            : base(logger, dataSetsHelper, roles, authentication, fileSourceHelper, pureControllerPackage)
        {
            _logger = logger;
            _mailer = mailer;
            _donationService = donationService;
            _clientService = clientService;
            _recaptchaService = recaptchaService;
            _recaptchaConfig = recaptchaConfig.Value;
            _config = config.Value;
            _urlHelper = new UrlHelper(System.Web.HttpContext.Current.Request.RequestContext);
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.RecaptchaSiteKey = _recaptchaConfig.RecaptchaSiteKey;
            return View("ContactUs");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ContactUs(ContactUsViewModel model)
        {
            var encodedResponse = Request.Form[RecaptchaResult.ResponseFormVariable];
            var isCaptchaValid = _recaptchaService.Validate(encodedResponse);

            if (!isCaptchaValid)
            {
                ModelState.AddModelError("", Dictionary.InvalidRecaptcha);
                return View("ContactUs", model);
            }

            try
            {
                _mailer.SendEmail(
                    model.Subject,
                    model.Body,
                    _config.SupportEmailAddress,
                    _config.CompanyName,
                    model.EmailAddress,
                    model.Name);

                var client = _clientService.GetOrCreateClient("", model.Name, model.EmailAddress);
                SendEmailToClient(client);

                return RedirectToAction("ContactUsSuccess");
            }
            catch (Exception ex)
            {
                _logger.Error(ex.GetFullErrorMessage());
                return View("FriendlyError");
            }
        }

        public ActionResult ContactUsSuccess()
        {
            return View();
        }
        
        [Route("donate")]
        public ActionResult DonateStart()
        {
            return View(new Donation
            {
                DonationAmount = 10,
                DonationDescription = Dictionary.DonationToPureAlchemy
            });
        }   

        [Route("donate")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Donate(Donation donation)
        {
            return View(donation);
        }

        [HttpPost]
        public ActionResult ProcessDonation(PurchaseModel purchaseModel)
        {
            try
            {
                var client = _clientService.Find(purchaseModel.ClientId);

                _donationService.CreateDonation(new Donation
                {
                    Currency = purchaseModel.Currency,
                    Customer = purchaseModel.CustomerName,
                    CustomerEmail = purchaseModel.CustomerEmailAddress,
                    DonationDescription = purchaseModel.Description,
                    DonatedOn = DateTime.Now,
                    DonationAmount = purchaseModel.Amount
                }, client);

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                _logger.Error($"SupportController => ProcessDonation => Error: {ex.GetFullErrorMessage()}");
                return Json(new { success = false, error = ex.Message });
            }
        }

        [Route("donate/success")]
        public ActionResult DonationSuccess(string sessionId)
        {
            return View();
        }

        [Route("donate/cancel/success")]
        public ActionResult DonationCancelSuccess()
        {
            return View();
        }

        public override string GetObjectName()
        {
            throw new NotImplementedException();
        }

        private void SendEmailToClient(Client client)
        {
            var template = Dictionary.SupportQuery;
            var title = Dictionary.EmailThankYouTitle;
            if (client != null && !client.IsUnsubscribed)
            {
                _mailer.SendEmail(title, TemplateProcessor.PopulateTemplate(template, new
                {
                    Title = title,
                    FirstName = client.GetFirstName(),
                    PrivacyPolicyLink = _urlHelper.AbsoluteAction("PrivacyPolicy", "Home"),
                    UnsubscribeLink = _urlHelper.AbsoluteAction("Unsubscribe", "Account", new { code = client.Name }),
                    DateTime.Now.Year
                }), client.EmailAddress, client.GetFirstName(), _config.SupportEmailAddress,
                    _config.CompanyName);
            }
        }
    }
}
