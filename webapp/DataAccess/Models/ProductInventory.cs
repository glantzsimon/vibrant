using K9.Base.DataAccessLayer.Attributes;
using K9.Base.DataAccessLayer.Models;
using K9.Base.Globalisation;
using K9.SharedLibrary.Attributes;
using K9.SharedLibrary.Enums;
using K9.SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace K9.DataAccessLayer.Models
{
    [AutoGenerateName]
    [Name(ResourceType = typeof(Globalisation.Dictionary), ListName = Globalisation.Strings.Names.ProductInventory, PluralName = Globalisation.Strings.Names.ProductInventory, Name = Globalisation.Strings.Names.ProductInventoryItem)]
    public class ProductInventory : ObjectBase
	{
	    [ForeignKey("Product")]
	    public int? ProductId { get; set; }

	    public virtual Product Product { get; set; }

	    [LinkedColumn(LinkedTableName = "Product", LinkedColumnName = "Name")]
	    public string ProductName { get; set; }
        
	    [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.QuantityInStockLabel)]
	    public int QuantityInStock { get; set; }

	    [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.InStockLabel)]
	    public bool IsInStock => QuantityInStock > 0;

	    [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.AmountPerProductLabel)]
		public int AmountPerProduct { get; set; }
        
		[Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.NotesLabel)]
		[Required(ErrorMessageResourceType = typeof(Dictionary), ErrorMessageResourceName = Strings.ErrorMessages.FieldIsRequired)]
		[StringLength(int.MaxValue)]
		[DataType(DataType.Html)]
		[AllowHtml]
		public string Notes { get; set; }
        
	}
}
