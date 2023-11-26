using K9.Base.DataAccessLayer.Attributes;
using K9.Base.DataAccessLayer.Models;
using K9.Base.Globalisation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace K9.DataAccessLayer.Models
{
    [AutoGenerateName]
    [Name(ResourceType = typeof(Globalisation.Dictionary), ListName = Globalisation.Strings.Names.OrderProducts, PluralName = Globalisation.Strings.Names.OrderProducts, Name = Globalisation.Strings.Names.OrderProduct)]
    public class OrderProductPackProduct : ObjectBase
    {
        [UIHint("OrderProductPack")]
        [ForeignKey("OrderProductPack")]
        public int OrderProductPackId { get; set; }

        public virtual OrderProductPack OrderProductPack { get; set; }

        [UIHint("Product")]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        [NotMapped]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.AmountLabel)]
        public int Amount { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.AmountCompletedLabel)]
        [Required(ErrorMessageResourceType = typeof(Dictionary), ErrorMessageResourceName = Strings.ErrorMessages.FieldIsRequired)]
        public int AmountCompleted { get; set; }

        public int? GetAmountRemaining() => Amount - AmountCompleted;

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.AmountRemainingLabel)]
        public int? AmountRemaining => GetAmountRemaining();
    }
}
