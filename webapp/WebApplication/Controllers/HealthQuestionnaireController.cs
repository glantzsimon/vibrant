using K9.Base.WebApplication.UnitsOfWork;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Models;
using K9.WebApplication.Config;
using K9.WebApplication.Services;
using System.Linq;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace K9.WebApplication.Controllers
{
    [Authorize]
    public class HealthQuestionnaireController : BasePureController
    {
        private readonly IQuestionnaireService _questionnaireService;

        public HealthQuestionnaireController(IControllerPackage<Order> controllerPackage, IOptions<DefaultValuesConfiguration> defaultValues, IMembershipService membershipService, IQuestionnaireService questionnaireService) : base(controllerPackage.Logger, controllerPackage.DataSetsHelper, controllerPackage.Roles, controllerPackage.Authentication, controllerPackage.FileSourceHelper, membershipService)
        {
            _questionnaireService = questionnaireService;
        }

        [Route("health-questionnaire")]
        public ActionResult AnswerHealthQuestionnaire(int? clientId = null)
        {
            HealthQuestionnaire hq;

            if (clientId.HasValue)
            {
                hq = _questionnaireService.GetHealthQuestionnaireForClient(clientId.Value);
            }
            else
            {
                hq = _questionnaireService.GetHealthQuestionnaireForUser(WebSecurity.CurrentUserId);
            }
            
            if (hq == null)
            {
                return HttpNotFound();
            }

            if (order.UserId != WebSecurity.CurrentUserId)
            {
                var user = _usersRepository.Find(WebSecurity.CurrentUserId);
                var client = _clientsRepository.Find(e => e.UserId == user.Id).First();
                if (client.Id != order.ClientId)
                {
                    return HttpForbidden();
                }
            }

            ViewBag.DeviceType = GetDeviceType();

            return View(order);
        }
    }
}

