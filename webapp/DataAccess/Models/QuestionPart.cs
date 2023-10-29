using K9.Base.DataAccessLayer.Attributes;
using K9.Base.DataAccessLayer.Models;
using K9.Globalisation;
using System.ComponentModel.DataAnnotations;

namespace K9.DataAccessLayer.Models
{
    [AutoGenerateName]
    [Name(ResourceType = typeof(Dictionary), ListName = Strings.Names.Questions, PluralName = Strings.Names.Questions, Name = Strings.Names.Question)]
    public class QuestionPart : ObjectBase
    {
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.TextLabel)]
        [Required(ErrorMessageResourceType = typeof(Base.Globalisation.Dictionary), ErrorMessageResourceName = K9.Base.Globalisation.Strings.ErrorMessages.FieldIsRequired)]
        public string Text { get; set; }
    }
}
