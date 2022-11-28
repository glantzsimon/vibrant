using K9.Base.DataAccessLayer.Attributes;
using K9.Base.DataAccessLayer.Models;
using K9.DataAccessLayer.Enums;
using K9.SharedLibrary.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace K9.DataAccessLayer.Models
{
    [AutoGenerateName]
    [Name(ResourceType = typeof(Globalisation.Dictionary), ListName = Globalisation.Strings.Names.Sales, PluralName = Globalisation.Strings.Names.Sales, Name = Globalisation.Strings.Names.Sale)]
    public class Sale : ObjectBase
    {
        [UIHint("User")]
        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        [LinkedColumn(LinkedTableName = "User", LinkedColumnName = "Username")]
        public string UserName { get; set; }

        [UIHint("SaleType")]
        [Required]
        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.IngredientTypeLabel)]
        public ESaleType SaleType { get; set; }

        [UIHint("Contact")]
        [ForeignKey("Contact")]
        public int ContactId { get; set; }

        public virtual Contact Contact { get; set; }
    
        public virtual IEnumerable<SaleItem> SaleItems { get; set; }
    }
}
