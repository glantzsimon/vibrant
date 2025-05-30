using K9.Base.WebApplication.Config;
using K9.DataAccessLayer.Models;
using K9.Globalisation;
using K9.SharedLibrary.Extensions;
using K9.SharedLibrary.Helpers;
using K9.SharedLibrary.Models;
using NLog;
using System;
using System.Web;
using System.Web.Mvc;
using K9.WebApplication.Helpers;

namespace K9.WebApplication.Services
{
    public class ConsultationService : IConsultationService
    {
        private readonly IRepository<Consultation> _consultationRepository;
        private readonly ILogger _logger;
        private readonly IMailer _mailer;
        private readonly IAuthentication _authentication;
        private readonly IRepository<UserConsultation> _userConsultationRepository;
        private readonly WebsiteConfiguration _config;
        private readonly UrlHelper _urlHelper;

        public ConsultationService(IRepository<Consultation> consultationRepository, ILogger logger, IMailer mailer, IOptions<WebsiteConfiguration> config, IAuthentication authentication, IRepository<UserConsultation> userConsultationRepository)
        {
            _consultationRepository = consultationRepository;
            _logger = logger;
            _mailer = mailer;
            _authentication = authentication;
            _userConsultationRepository = userConsultationRepository;
            _config = config.Value;
            _urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
        }
        
        public void CreateConsultation(Consultation consultation, Client client)
        {
            try
            {
                _consultationRepository.Create(consultation);

                if (_authentication.IsAuthenticated)
                {
                    _userConsultationRepository.Create(new UserConsultation
                    {
                        UserId = Current.UserId,
                        ConsultationId = consultation.Id
                    });
                }

                SendEmailToPureAlchemy(consultation, client);
                SendEmailToCustomer(consultation, client);
            }
            catch (Exception ex)    
            {
                _logger.Error($"ConsultationService => CreateConsultation => {ex.GetFullErrorMessage()}");
            }
        }

        private void SendEmailToPureAlchemy(Consultation consultation, Client client)
        {
            var template = Dictionary.ConsultationBookedEmail;
            var title = "We have received a consultation booking!";
            _mailer.SendEmail(title, TemplateProcessor.PopulateTemplate(template, new
            {
                Title = title,
                ClientName = client.FullName,
                ClientEmail = client.EmailAddress,
                client.PhoneNumber,
                Duration = consultation.DurationDescription,
                Price = consultation.FormattedPrice,
                Company = _config.CompanyName,
                ImageUrl = _urlHelper.AbsoluteContent(_config.CompanyLogoUrl)
            }), _config.SupportEmailAddress, _config.CompanyName, _config.SupportEmailAddress, _config.CompanyName);
        }

        private void SendEmailToCustomer(Consultation consultation, Client client)
        {
            var template = Dictionary.ConsultationBookedThankYouEmail;
            var title = Dictionary.ThankyouForBookingConsultationEmailTitle;
            _mailer.SendEmail(title, TemplateProcessor.PopulateTemplate(template, new
            {
                Title = title,
                FirstName = client.GetFirstName(),
                Duration = consultation.DurationDescription,
                ImageUrl = _urlHelper.AbsoluteContent(_config.CompanyLogoUrl),
                PrivacyPolicyLink = _urlHelper.AbsoluteAction("PrivacyPolicy", "Home"),
                UnsubscribeLink = _urlHelper.AbsoluteAction("Unsubscribe", "Account", new { code = client.Name }),
                DateTime.Now.Year
            }), client.EmailAddress, client.FullName, _config.SupportEmailAddress, _config.CompanyName);
        }
    }
}