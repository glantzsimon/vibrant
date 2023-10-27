using K9.DataAccessLayer.Enums;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Extensions;
using K9.SharedLibrary.Helpers;
using K9.SharedLibrary.Models;
using K9.WebApplication.Models;
using K9.WebApplication.Services;
using NLog;
using System;
using System.Web.Mvc;

namespace K9.WebApplication.Controllers
{
    public class ConsultationController : BasePureController
    {
        private readonly ILogger _logger;
        private readonly IConsultationService _consultationService;
        private readonly IClientService _clientService;

        public ConsultationController(ILogger logger, IDataSetsHelper dataSetsHelper, IRoles roles, IAuthentication authentication, IFileSourceHelper fileSourceHelper, IMembershipService membershipService,  IConsultationService consultationService, IClientService clientService)
            : base(logger, dataSetsHelper, roles, authentication, fileSourceHelper, membershipService)
        {
            _logger = logger;
            _consultationService = consultationService;
            _clientService = clientService;
        }
        
        [Route("book-consultation")]
        public ActionResult BookConsultationStart()
        {
            return View(new Consultation
            {
                ConsultationDuration = EConsultationDuration.OneHour
            });
        }   

        [Route("book-consultation")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BookConsultation(Consultation consultation)
        {
            return View(consultation);
        }

        [HttpPost]
        public ActionResult ProcessConsultation(PurchaseModel purchaseModel)
        {
            try
            {
                var client = _clientService.Find(purchaseModel.ClientId);

                _consultationService.CreateConsultation(new Consultation
                {
                    ConsultationDuration = (EConsultationDuration)purchaseModel.Quantity,
                    ClientId = purchaseModel.ClientId
                }, client);

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                _logger.Error($"SupportController => ProcessConsultation => Error: {ex.GetFullErrorMessage()}");
                return Json(new { success = false, error = ex.Message });
            }
        }

        public ActionResult BookConsultationSuccess()
        {
            return View();
        }
        
        public override string GetObjectName()
        {
            return string.Empty;
        }

    }
}
