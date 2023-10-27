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

namespace K9.DataAccessLayer.Models
{
    [AutoGenerateName]
    [Name(ResourceType = typeof(Dictionary), ListName = Strings.Names.HealthQuestionnaires, PluralName = Strings.Names.HealthQuestionnaires, Name = Strings.Names.HealthQuestionnaire)]
    public class HealthQuestionnaire : ObjectBase
    {

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.CompletedOnLabel)]
        public DateTime? CompletedOn { get; set; }

        public virtual Client Client { get; set; }

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.ClientLabel)]
        [LinkedColumn(LinkedTableName = "Client", LinkedColumnName = "FullName")]
        public string ClientName { get; set; }

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.TotalPriceLabel)]
        [DataType(DataType.Currency)]
        public double GetPrice() => GetPricePerDuration();

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.TotalPriceLabel)] public string FormattedPrice => GetPrice().ToString("C0", CultureInfo.GetCultureInfo("en-US"));

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.DurationLabel)]
        public TimeSpan GetDuration() => new TimeSpan((int) ConsultationDuration, 0, 0);

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.DurationLabel)] public string DurationDescription => ConsultationDuration.GetAttribute<EnumDescriptionAttribute>().GetDescription();

        private double GetPricePerDuration()
        {
            if (ConsultationDuration == EConsultationDuration.OneHour)
            {
                return 17;
            }

            if (ConsultationDuration == EConsultationDuration.TwoHours)
            {
                return 27;
            }

            return 0;
        }

    }
}
