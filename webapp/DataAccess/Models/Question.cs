using K9.Base.DataAccessLayer.Attributes;
using K9.Base.DataAccessLayer.Models;
using K9.Globalisation;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace K9.DataAccessLayer.Models
{
    [AutoGenerateName]
    [Name(ResourceType = typeof(Dictionary), ListName = Strings.Names.Questions, PluralName = Strings.Names.Questions, Name = Strings.Names.Question)]
    public class Question : ObjectBase
    {
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.ShortDescriptionLabel)]
        [Required(ErrorMessageResourceType = typeof(Base.Globalisation.Dictionary), ErrorMessageResourceName = K9.Base.Globalisation.Strings.ErrorMessages.FieldIsRequired)]
        public string Title { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.ShortDescriptionLabel)]
        [Required(ErrorMessageResourceType = typeof(Base.Globalisation.Dictionary), ErrorMessageResourceName = K9.Base.Globalisation.Strings.ErrorMessages.FieldIsRequired)]
        [StringLength(444)]
        [DataType(DataType.Html)]
        [AllowHtml]
        public string ShortDescription { get; set; }
    }
}
