using K9.Base.WebApplication.Filters;
using K9.Base.WebApplication.UnitsOfWork;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Models;
using K9.WebApplication.Config;
using K9.WebApplication.Services;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace K9.WebApplication.Controllers
{
    [Authorize]
    [RequirePermissions(Role = Constants.Constants.ClientUser)]
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
            
            return View(hq);
        }


        [Route("health-questionnaire")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AnswerHealthQuestionnaire(HealthQuestionnaire model)
        {
            _questionnaireService.Save(model);

            if (!model.DateOfBirth.HasValue)
            {
                ModelState.AddModelError(nameof(HealthQuestionnaire.DateOfBirth), Base.Globalisation.Dictionary.FieldIsRequired);
            }

            if (!model.Gender.HasValue)
            {
                ModelState.AddModelError(nameof(HealthQuestionnaire.Gender), Base.Globalisation.Dictionary.FieldIsRequired);
            }

            if (!model.BloodGroup.HasValue)
            {
                ModelState.AddModelError(nameof(HealthQuestionnaire.BloodGroup), Base.Globalisation.Dictionary.FieldIsRequired);
            }

            if (!model.RhesusFactor.HasValue)
            {
                ModelState.AddModelError(nameof(HealthQuestionnaire.RhesusFactor), Base.Globalisation.Dictionary.FieldIsRequired);
            }
            
            if (!model.SensitivityToMedications.HasValue)
            {
                ModelState.AddModelError(nameof(HealthQuestionnaire.SensitivityToMedications), Base.Globalisation.Dictionary.FieldIsRequired);
            }

            if (!model.SensitiveToCaffeine.HasValue)
            {
                ModelState.AddModelError(nameof(HealthQuestionnaire.SensitiveToCaffeine), Base.Globalisation.Dictionary.FieldIsRequired);
            }

            if (!model.CaffeineAffectsSleep.HasValue)
            {
                ModelState.AddModelError(nameof(HealthQuestionnaire.CaffeineAffectsSleep), Base.Globalisation.Dictionary.FieldIsRequired);
            }
            
            if (!model.SensitiveToMold.HasValue)
            {
                ModelState.AddModelError(nameof(HealthQuestionnaire.SensitiveToMold), Base.Globalisation.Dictionary.FieldIsRequired);
            }
            
            if (!model.SensitiveToEnvironmentalChemicals.HasValue)
            {
                ModelState.AddModelError(nameof(HealthQuestionnaire.SensitiveToEnvironmentalChemicals), Base.Globalisation.Dictionary.FieldIsRequired);
            }

            if (model.IsComplete())
            {
                return RedirectToAction("QuestionnaireCompletedSuccess");
            }

            return View(model);
        }

        [Route("health-questionnaire/success")]
        public ActionResult QuestionnaireCompletedSuccess()
        {
            return View();
        }
    }
}

