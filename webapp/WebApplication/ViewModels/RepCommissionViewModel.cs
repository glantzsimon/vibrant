using System.Collections.Generic;
using K9.DataAccessLayer.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace K9.WebApplication.ViewModels
{
    public class RepCommissionViewModel
    {
        [UIHint("Contact")]
        [ForeignKey("Rep")]
        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.RepLabel)]
        public int RepId { get; set; }

        public virtual Contact Rep { get; set; }

        public List<RepCommission> RepCommissions { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.AmountRedeemableLabel)]
        [DataType(DataType.Currency)]
        public double AmountRedeemable { get; set; }

        public bool IsRedeemable => AmountRedeemable > 3300;

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.PriceLabel)]
        public string FormattedAmountRedeemable => double.Parse(AmountRedeemable.ToString()).ToString("C", CultureInfo.GetCultureInfo("th-TH"));

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.TotalAmountRedeemedLabel)]
        [DataType(DataType.Currency)]
        public double AmountRedeemed { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.TotalAmountRedeemedLabel)]
        public string FormattedAmountRedeemed => double.Parse(AmountRedeemable.ToString()).ToString("C", CultureInfo.GetCultureInfo("th-TH"));

    }
}