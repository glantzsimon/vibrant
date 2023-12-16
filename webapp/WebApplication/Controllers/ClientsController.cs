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
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using K9.WebApplication.ViewModels;

namespace K9.WebApplication.Controllers
{
    [Authorize]
    [RequirePermissions(Role = RoleNames.Administrators)]
    public class ClientsController : BaseController<Client>
    {
        private readonly IRepository<Donation> _donationRepository;
        private readonly ILogger _logger;
        private readonly IMailChimpService _mailChimpService;
        private readonly IClientService _clientService;
        private readonly IRepository<Country> _countriesRepository;
        private readonly IOrderService _ordersService;

        public ClientsController(IControllerPackage<Client> controllerPackage, IRepository<Donation> donationRepository, ILogger logger, IMailChimpService mailChimpService, IClientService clientService, IRepository<Country> countriesRepository, IOrderService ordersService) : base(controllerPackage)
        {
            _donationRepository = donationRepository;
            _logger = logger;
            _mailChimpService = mailChimpService;
            _clientService = clientService;
            _countriesRepository = countriesRepository;
            _ordersService = ordersService;
        }

        [Authorize]
        public ActionResult ClientAccount(int id)
        {
            var clientRecord = _clientService.Find(id);
            if (clientRecord != null && clientRecord.UserId.HasValue)
            {
                return RedirectToAction("MyAccount", "Account", new { userId = clientRecord.UserId });
            }

            return HttpNotFound();
        }

        public ActionResult EditForbiddenFoods(int id = 0)
        {
            return RedirectToAction("EditForbiddenFoodItemsClient", "ClientForbiddenFoods", new { id });
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
        public ActionResult ViewClientAddressLabelsForAllOrders()
        {
            var allOrders = _ordersService.List(true).Where(e => !e.IsOnHold && !e.IsLocalDelivery).ToList();
            var ordersViewModel = new OrdersReviewViewModel(allOrders);
            return ViewClientsAddressLabels(ordersViewModel.GetIncompleteOrders().Select(e => e.ClientId ?? 0).ToArray());
        }

        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult ViewClientsAddressLabels(params int[] ids)
        {
            var recipients = ids != null
                ? Repository.Find(e => ids.Contains(e.Id)).ToList()
                : new List<Client>();

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

        public ActionResult GeneticProfileTestOverviewForClient(int? id = null)
        {
            return RedirectToAction("GeneticProfileTestOverview", "HealthQuestionnaire", new {clientId = id});
        }

        public ActionResult GeneticProfileTestForClient(int? id = null)
        {
            return RedirectToAction("GeneticProfileTest", "HealthQuestionnaire", new {clientId = id});
        }
    }
}
