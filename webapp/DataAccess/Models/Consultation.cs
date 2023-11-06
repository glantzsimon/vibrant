using K9.Base.DataAccessLayer.Attributes;
using K9.Base.DataAccessLayer.Models;
using K9.DataAccessLayer.Enums;
using K9.Globalisation;
using K9.SharedLibrary.Attributes;
using K9.SharedLibrary.Extensions;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using K9.DataAccessLayer.Helpers;

namespace K9.DataAccessLayer.Models
{
    [AutoGenerateName]
    [Name(ResourceType = typeof(Dictionary), ListName = Strings.Names.Consultations, PluralName = Strings.Names.Consultations, Name = Strings.Names.Consultation)]
    public class Consultation : ObjectBase
    {

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.ClientLabel)]
        [UIHint("Client")]
        [Required]
        [ForeignKey("Client")]
        public int ClientId { get; set; }
        
        [UIHint("ConsultationDuration")]
        [Required]
        [Display(ResourceType = typeof(Dictionary),
            Name = Strings.Labels.ConsultationDurationLabel)]
        public EConsultationDuration ConsultationDuration { get; set; } = EConsultationDuration.OneHour;

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.CompletedOnLabel)]
        public DateTime? CompletedOn { get; set; }

        public virtual Client Client { get; set; }

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.ClientLabel)]
        [LinkedColumn(LinkedTableName = "Client", LinkedColumnName = "FullName")]
        public string ClientName { get; set; }

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.TotalPriceLabel)]
        [DataType(DataType.Currency)]
        public double GetPrice() => GetPricePerDuration();

        [UIHint("InternationalCurrency")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.TotalPriceLabel)]
        [DataType(DataType.Currency)]
        public double InternationalPrice => GetPrice().ToInternationalPrice();

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.TotalPriceLabel)] public string FormattedPrice => GetPrice().ToString("C0", CultureInfo.GetCultureInfo("en-US"));

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.DurationLabel)]
        public TimeSpan GetDuration() => new TimeSpan((int) ConsultationDuration, 0, 0);

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.DurationLabel)] public string DurationDescription => ConsultationDuration.GetAttribute<EnumDescriptionAttribute>().GetDescription();

        private double GetPricePerDuration()
        {
            if (ConsultationDuration == EConsultationDuration.OneHour)
            {
                return 2200;
            }

            if (ConsultationDuration == EConsultationDuration.TwoHours)
            {
                return 3500;
            }

            return 0;
        }

    }
}
