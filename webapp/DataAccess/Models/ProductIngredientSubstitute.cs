using K9.Base.DataAccessLayer.Attributes;
using K9.Base.DataAccessLayer.Models;
using K9.Base.Globalisation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace K9.DataAccessLayer.Models
{
    [AutoGenerateName]
    [Name(ResourceType = typeof(Globalisation.Dictionary), ListName = Globalisation.Strings.Names.ProductIngredientSubstitutes, PluralName = Globalisation.Strings.Names.ProductIngredientSubstitutes, Name = Globalisation.Strings.Names.ProductIngredientSubstitute)]
    public class ProductIngredientSubstitute : ObjectBase
    {
        [ForeignKey("ProductIngredient")]
        public int ProductIngredientId { get; set; }

        public virtual ProductIngredient ProductIngredient { get; set; }
        
        [ForeignKey("SubstituteIngredient")]
        public int SubstituteIngredientId { get; set; }

        public virtual Ingredient SubstituteIngredient { get; set; }
        
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.PriorityLabel)]
        [Required(ErrorMessageResourceType = typeof(Dictionary), ErrorMessageResourceName = Strings.ErrorMessages.FieldIsRequired)]
        public int Priority { get; set; }
    }
}
