using K9.Base.DataAccessLayer.Attributes;
using K9.Base.DataAccessLayer.Models;
using K9.Base.Globalisation;
using K9.SharedLibrary.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace K9.DataAccessLayer.Models
{
    [AutoGenerateName]
    [Name(ResourceType = typeof(Globalisation.Dictionary), ListName = Globalisation.Strings.Names.ProductPackProducts, PluralName = Globalisation.Strings.Names.ProductPackProducts, Name = Globalisation.Strings.Names.ProductPackProduct)]
    public class ProductPackProduct : ObjectBase
	{
        [UIHint("Product")]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

	    [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.ProductLabel)]
	    [LinkedColumn(LinkedTableName = "Product", LinkedColumnName = "Title")]
	    public string ProductName { get; set; }

	    [UIHint("ProductPack")]
	    [ForeignKey("ProductPack")]
	    public int ProductPackId { get; set; }

	    public virtual ProductPack ProductPack { get; set; }

	    [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.ProductPackLabel)]
	    [LinkedColumn(LinkedTableName = "ProductPack", LinkedColumnName = "Name")]
	    public string ProductPackName { get; set; }
        
	    [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.AmountLabel)]
	    [Required(ErrorMessageResourceType = typeof(Dictionary), ErrorMessageResourceName = Strings.ErrorMessages.FieldIsRequired)]
	    public int Amount { get; set; }

	    [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.PriceLabel)]
	    [DataType(DataType.Currency)]
	    public double TotalPrice => Amount * Product?.Price ?? 0;

	    [NotMapped]
	    [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.AmountRequiredLabel)]
	    public int AmountRequired { get; set; }
	}
}
