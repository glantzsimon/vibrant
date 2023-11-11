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
    [Route("genetic-profile")]
    public class HealthQuestionnaireController : BasePureController
    {
        private readonly IQuestionnaireService _questionnaireService;

        public HealthQuestionnaireController(IControllerPackage<Order> controllerPackage, IOptions<DefaultValuesConfiguration> defaultValues, IMembershipService membershipService, IQuestionnaireService questionnaireService) : base(controllerPackage.Logger, controllerPackage.DataSetsHelper, controllerPackage.Roles, controllerPackage.Authentication, controllerPackage.FileSourceHelper, membershipService)
        {
            _questionnaireService = questionnaireService;
        }

        [Route("genetic-profile/questionnaire")]
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


        [Route("genetic-profile/questionnaire")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AnswerHealthQuestionnaire(HealthQuestionnaire model)
        {
            _questionnaireService.Save(model);

            #region Personal Details

            if (!model.DateOfBirth.HasValue)
            {
                ModelState.AddModelError(nameof(HealthQuestionnaire.DateOfBirth), Base.Globalisation.Dictionary.FieldIsRequired);
            }

            if (!model.Gender.HasValue)
            {
                ModelState.AddModelError(nameof(HealthQuestionnaire.Gender), Base.Globalisation.Dictionary.FieldIsRequired);
            }

            #endregion

            #region Blood

            if (!model.BloodGroup.HasValue)
            {
                ModelState.AddModelError(nameof(HealthQuestionnaire.BloodGroup), Base.Globalisation.Dictionary.FieldIsRequired);
            }

            if (!model.RhesusFactor.HasValue)
            {
                ModelState.AddModelError(nameof(HealthQuestionnaire.RhesusFactor), Base.Globalisation.Dictionary.FieldIsRequired);
            }

            #endregion

            #region Acetylation
            
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

            #endregion 

            #region Biometrics

            if (model.StandingHeight <= 0)
            {
                ModelState.AddModelError(nameof(HealthQuestionnaire.StandingHeight), Base.Globalisation.Dictionary.FieldIsRequired);
            }

            if (model.SittingHeight <= 0)
            {
                ModelState.AddModelError(nameof(HealthQuestionnaire.SittingHeight), Base.Globalisation.Dictionary.FieldIsRequired);
            }

            if (model.ChairHeight <= 0)
            {
                ModelState.AddModelError(nameof(HealthQuestionnaire.ChairHeight), Base.Globalisation.Dictionary.FieldIsRequired);
            }
            
            if (model.LowerLegLength <= 0)
            {
                ModelState.AddModelError(nameof(HealthQuestionnaire.LowerLegLength), Base.Globalisation.Dictionary.FieldIsRequired);
            }

            if (model.UpperLegLength <= 0)
            {
                ModelState.AddModelError(nameof(HealthQuestionnaire.UpperLegLength), Base.Globalisation.Dictionary.FieldIsRequired);
            }

            if (model.IndexFingerLengthLeft <= 0)
            {
                ModelState.AddModelError(nameof(HealthQuestionnaire.IndexFingerLengthLeft), Base.Globalisation.Dictionary.FieldIsRequired);
            }

            if (model.IndexFingerLengthRight <= 0)
            {
                ModelState.AddModelError(nameof(HealthQuestionnaire.IndexFingerLengthRight), Base.Globalisation.Dictionary.FieldIsRequired);
            }
            
            if (model.RingFingerLengthLeft <= 0)
            {
                ModelState.AddModelError(nameof(HealthQuestionnaire.RingFingerLengthLeft), Base.Globalisation.Dictionary.FieldIsRequired);
            }

            if (model.RingFingerLengthRight <= 0)
            {
                ModelState.AddModelError(nameof(HealthQuestionnaire.RingFingerLengthRight), Base.Globalisation.Dictionary.FieldIsRequired);
            }

            if (!model.SpaceBetweenThighs.HasValue)
            {
                ModelState.AddModelError(nameof(HealthQuestionnaire.SpaceBetweenThighs), Base.Globalisation.Dictionary.FieldIsRequired);
            }
            
            if (model.WaistSize <= 0)
            {
                ModelState.AddModelError(nameof(HealthQuestionnaire.WaistSize), Base.Globalisation.Dictionary.FieldIsRequired);
            }
            
            if (model.HipSize <= 0)
            {
                ModelState.AddModelError(nameof(HealthQuestionnaire.HipSize), Base.Globalisation.Dictionary.FieldIsRequired);
            } 
            
            if (model.HeadWidth <= 0)
            {
                ModelState.AddModelError(nameof(HealthQuestionnaire.HeadWidth), Base.Globalisation.Dictionary.FieldIsRequired);
            } 
            
            if (model.HeadLength <= 0)
            {
                ModelState.AddModelError(nameof(HealthQuestionnaire.HeadLength), Base.Globalisation.Dictionary.FieldIsRequired);
            }
            
            if (!model.GonialAngle.HasValue)
            {
                ModelState.AddModelError(nameof(HealthQuestionnaire.GonialAngle), Base.Globalisation.Dictionary.FieldIsRequired);
            }
            
            if (!model.TendonsAndSinewsVisible.HasValue)
            {
                ModelState.AddModelError(nameof(HealthQuestionnaire.TendonsAndSinewsVisible), Base.Globalisation.Dictionary.FieldIsRequired);
            }
            
            if (!model.WristsAndAnklesLookPadded.HasValue)
            {
                ModelState.AddModelError(nameof(HealthQuestionnaire.WristsAndAnklesLookPadded), Base.Globalisation.Dictionary.FieldIsRequired);
            }
            
            if (!model.GainsMuscleEasily.HasValue)
            {
                ModelState.AddModelError(nameof(HealthQuestionnaire.GainsMuscleEasily), Base.Globalisation.Dictionary.FieldIsRequired);
            }
            
            if (!model.SomatoType.HasValue && !model.WristCircumference.HasValue)
            {
                ModelState.AddModelError(nameof(HealthQuestionnaire.SomatoType), Base.Globalisation.Dictionary.FieldIsRequired);
                ModelState.AddModelError(nameof(HealthQuestionnaire.WristCircumference), Base.Globalisation.Dictionary.FieldIsRequired);
            }
            
            if (model.StandingHeight <= 0)
            {
                ModelState.AddModelError(nameof(HealthQuestionnaire.StandingHeight), Base.Globalisation.Dictionary.FieldIsRequired);
            }

            #endregion

            #region Dermatoglyphics

            if (!model.LeftThumprintType.HasValue)
            {
                ModelState.AddModelError(nameof(HealthQuestionnaire.LeftThumprintType), Base.Globalisation.Dictionary.FieldIsRequired);
            }

            if (!model.LeftIndexFingerprintType.HasValue)
            {
                ModelState.AddModelError(nameof(HealthQuestionnaire.LeftIndexFingerprintType), Base.Globalisation.Dictionary.FieldIsRequired);
            }

            if (!model.LeftMiddleFingerprintType.HasValue)
            {
                ModelState.AddModelError(nameof(HealthQuestionnaire.LeftMiddleFingerprintType), Base.Globalisation.Dictionary.FieldIsRequired);
            }

            if (!model.LeftRingFingerprintType.HasValue)
            {
                ModelState.AddModelError(nameof(HealthQuestionnaire.LeftRingFingerprintType), Base.Globalisation.Dictionary.FieldIsRequired);
            }

            if (!model.LeftLittleFingerprintType.HasValue)
            {
                ModelState.AddModelError(nameof(HealthQuestionnaire.LeftLittleFingerprintType), Base.Globalisation.Dictionary.FieldIsRequired);
            }

            if (!model.RightThumprintType.HasValue)
            {
                ModelState.AddModelError(nameof(HealthQuestionnaire.RightThumprintType), Base.Globalisation.Dictionary.FieldIsRequired);
            }

            if (!model.RightIndexFingerprintType.HasValue)
            {
                ModelState.AddModelError(nameof(HealthQuestionnaire.RightIndexFingerprintType), Base.Globalisation.Dictionary.FieldIsRequired);
            }

            if (!model.RightMiddleFingerprintType.HasValue)
            {
                ModelState.AddModelError(nameof(HealthQuestionnaire.RightMiddleFingerprintType), Base.Globalisation.Dictionary.FieldIsRequired);
            }

            if (!model.RightRingFingerprintType.HasValue)
            {
                ModelState.AddModelError(nameof(HealthQuestionnaire.RightRingFingerprintType), Base.Globalisation.Dictionary.FieldIsRequired);
            }
            
            #endregion


            #region Dentition

            if (!model.IncisorShovelling.HasValue)
            {
                ModelState.AddModelError(nameof(HealthQuestionnaire.IncisorShovelling), Base.Globalisation.Dictionary.FieldIsRequired);
            }
            
            #endregion


            if (model.IsComplete())
            {
                return RedirectToAction("QuestionnaireCompletedSuccess");
            }

            return View(model);
        }

        [Route("genetic-profile/questionnaire/success")]
        public ActionResult QuestionnaireCompletedSuccess()
        {
            return View();
        }
    }
}

