using K9.Base.DataAccessLayer.Attributes;
using K9.Base.DataAccessLayer.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using K9.SharedLibrary.Attributes;

namespace K9.DataAccessLayer.Models
{
    [AutoGenerateName]
    [Name(ResourceType = typeof(Globalisation.Dictionary), ListName = Globalisation.Strings.Names.RepCommissions, PluralName = Globalisation.Strings.Names.RepCommissions, Name = Globalisation.Strings.Names.RepCommission)]
    public class RepCommission : ObjectBase
    {
        [UIHint("Client")]
        [Required]
        [ForeignKey("Rep")]
        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.RepLabel)]
        public int RepId { get; set; }

        public virtual Client Rep { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.RepLabel)]
        [LinkedColumn(LinkedTableName = "Client", LinkedColumnName = "FullName", ForeignKey = "RepId")]
        public string RepName { get; set; }
        
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.AmountRedeemedLabel)]
        [DataType(DataType.Currency)]
        [Required]
        public double AmountRedeemed { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.AmountRedeemedLabel)]
        public string FormattedAmountRedeemed => double.Parse(AmountRedeemed.ToString()).ToString("C", CultureInfo.GetCultureInfo("th-TH"));

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.RedeemedOnLabel)]
        [Required]
        public DateTime RedeemedOn { get; set; }
    }
}
