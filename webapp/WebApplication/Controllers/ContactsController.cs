using K9.Base.WebApplication.Controllers;
using K9.Base.WebApplication.Extensions;
using K9.Base.WebApplication.Filters;
using K9.Base.WebApplication.UnitsOfWork;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Authentication;
using K9.SharedLibrary.Extensions;
using K9.SharedLibrary.Models;
using K9.WebApplication.Services;
using NLog;
using System;
using System.Linq;
using System.Web.Mvc;
using K9.Base.DataAccessLayer.Models;
using K9.WebApplication.Models;
using WebMatrix.WebData;

namespace K9.WebApplication.Controllers
{
    [Authorize]
    [RequirePermissions(Role = RoleNames.Administrators)]
    public class ContactsController : BaseController<Contact>
    {
        private readonly IRepository<Donation> _donationRepository;
        private readonly ILogger _logger;
        private readonly IMailChimpService _mailChimpService;
        private readonly IRepository<Country> _countriesRepository;

        public ContactsController(IControllerPackage<Contact> controllerPackage, IRepository<Donation> donationRepository, ILogger logger, IMailChimpService mailChimpService, IRepository<Country> countriesRepository) : base(controllerPackage)
        {
            _donationRepository = donationRepository;
            _logger = logger;
            _mailChimpService = mailChimpService;
            _countriesRepository = countriesRepository;
        }

        public ActionResult ImportContactsFromDonations()
        {
            var existing = Repository.List();

            var contactsToImport = _donationRepository.Find(c => !string.IsNullOrEmpty(c.CustomerEmail) && existing.All(e => e.EmailAddress != c.CustomerEmail))
                .Select(e => new Contact
                {
                    FullName = e.GetCustomerName(),
                    EmailAddress = e.CustomerEmail
                }).ToList();

            Repository.CreateBatch(contactsToImport);

            return RedirectToAction("Index");
        }

        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult SignUpToNewsLetter()
        {
            return View(new Contact());
        }

        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult ViewContactAddressLabel(int id)
        {
            return ViewContactsAddressLabels(id);
        }

        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult ViewContactsAddressLabels(params int[] ids)
        {
            var recipients = Repository.Find(e => ids.Contains(e.Id)).ToList();
            foreach (var contact in recipients)
            {
                contact.Country = _countriesRepository.Find(contact.CountryId ?? 0);
            }

            var sender = Repository.Find(1);
            sender.Country = _countriesRepository.Find(sender.CountryId ?? 0);

            return View("ViewContactAddressLabels", new AddressLabelViewModel(recipients)
            {
                Sender = sender
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult SignUpToNewsLetter(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Repository.Exists(_ => _.EmailAddress == contact.EmailAddress))
                    {
                        ModelState.AddModelError("EmailAddress", K9.Globalisation.Dictionary.DuplicateContactError);
                    }
                    else
                    {
                        Repository.Create(contact);
                        return RedirectToAction("SignUpSuccess");
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.GetFullErrorMessage());
                    ModelState.AddErrorMessageFromException<Contact>(ex, contact);
                }
            }

            return View("", contact);
        }

        public ActionResult SignUpSuccess()
        {
            return View();
        }

        public ActionResult AddAllContactsToMailChimp()
        {
            _mailChimpService.AddAllContacts();
            return RedirectToAction("MailChimpImportSuccess");
        }

        public ActionResult MailChimpImportSuccess()
        {
            return View();
        }

        public ActionResult ReviewCommission(int id)
        {
            return RedirectToAction("Review", "RepCommissions", new { repId = id });
        }
    }
}
