using K9.Base.DataAccessLayer.Attributes;
using K9.Base.DataAccessLayer.Models;
using K9.Base.Globalisation;
using K9.SharedLibrary.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace K9.DataAccessLayer.Models
{
    [AutoGenerateName]
    [Name(ResourceType = typeof(Globalisation.Dictionary), ListName = Globalisation.Strings.Names.IngredientSubstitutes, PluralName = Globalisation.Strings.Names.IngredientSubstitutes, Name = Globalisation.Strings.Names.IngredientSubstitute)]
    public class IngredientSubstitute : ObjectBase
    {
        [UIHint("Ingredient")]
        [ForeignKey("Ingredient")]
        public int IngredientId { get; set; }

        public virtual Ingredient Ingredient { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.IngredientLabel)]
        [LinkedColumn(LinkedTableName = "Ingredient", LinkedColumnName = "Name")]
        public string IngredientName { get; set; }

        [UIHint("Ingredient")]
        [ForeignKey("SubstituteIngredient")]
        public int SubstituteIngredientId { get; set; }

        public virtual Ingredient SubstituteIngredient { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.IngredientSubstituteLabel)]
        [LinkedColumn(LinkedTableName = "Ingredient", LinkedColumnName = "Name")]
        public string SubstituteIngredientName { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.PriorityLabel)]
        [Required(ErrorMessageResourceType = typeof(Dictionary), ErrorMessageResourceName = Strings.ErrorMessages.FieldIsRequired)]
        public int Priority { get; set; }
    }
}
