using K9.Base.DataAccessLayer.Models;
using K9.Base.WebApplication.Controllers;
using K9.Base.WebApplication.Extensions;
using K9.Base.WebApplication.Filters;
using K9.Base.WebApplication.UnitsOfWork;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Authentication;
using K9.SharedLibrary.Extensions;
using K9.SharedLibrary.Models;
using K9.WebApplication.Models;
using K9.WebApplication.Services;
using NLog;
using System;
using System.Linq;
using System.Web.Mvc;

namespace K9.WebApplication.Controllers
{
    [Authorize]
    [RequirePermissions(Role = RoleNames.Administrators)]
    public class ClientsController : BaseController<Client>
    {
        private readonly IRepository<Donation> _donationRepository;
        private readonly ILogger _logger;
        private readonly IMailChimpService _mailChimpService;
        private readonly IRepository<Country> _countriesRepository;

        public ClientsController(IControllerPackage<Client> controllerPackage, IRepository<Donation> donationRepository, ILogger logger, IMailChimpService mailChimpService, IRepository<Country> countriesRepository) : base(controllerPackage)
        {
            _donationRepository = donationRepository;
            _logger = logger;
            _mailChimpService = mailChimpService;
            _countriesRepository = countriesRepository;
        }

        public ActionResult ImportClientsFromDonations()
        {
            var existing = Repository.List();

            var customersToImport = _donationRepository.Find(c => !string.IsNullOrEmpty(c.CustomerEmail) && existing.All(e => e.EmailAddress != c.CustomerEmail))
                .Select(e => new Client
                {
                    FullName = e.GetCustomerName(),
                    EmailAddress = e.CustomerEmail
                }).ToList();

            Repository.CreateBatch(customersToImport);

            return RedirectToAction("Index");
        }

        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult SignUpToNewsLetter()
        {
            return View(new Client());
        }

        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult ViewClientAddressLabel(int id)
        {
            return ViewClientsAddressLabels(id);
        }

        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult ViewClientsAddressLabels(params int[] ids)
        {
            var recipients = Repository.Find(e => ids.Contains(e.Id)).ToList();
            foreach (var recipient in recipients)
            {
                recipient.Country = _countriesRepository.Find(recipient.CountryId ?? 0);
            }

            var sender = Repository.Find(1);
            sender.Country = _countriesRepository.Find(sender.CountryId ?? 0);

            return View("ViewClientAddressLabels", new AddressLabelViewModel(recipients)
            {
                Sender = sender
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult SignUpToNewsLetter(Client client)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Repository.Exists(_ => _.EmailAddress == client.EmailAddress))
                    {
                        ModelState.AddModelError("EmailAddress", K9.Globalisation.Dictionary.DuplicateClientError);
                    }
                    else
                    {
                        Repository.Create(client);
                        return RedirectToAction("SignUpSuccess");
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.GetFullErrorMessage());
                    ModelState.AddErrorMessageFromException<Client>(ex, client);
                }
            }

            return View("", client);
        }

        public ActionResult SignUpSuccess()
        {
            return View();
        }

        public ActionResult AddAllClientsToMailChimp()
        {
            _mailChimpService.AddAllClients();
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
