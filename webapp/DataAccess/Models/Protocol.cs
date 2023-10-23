using K9.Base.DataAccessLayer.Attributes;
using K9.Base.DataAccessLayer.Models;
using K9.Base.Globalisation;
using K9.DataAccessLayer.Enums;
using K9.SharedLibrary.Attributes;
using K9.SharedLibrary.Enums;
using K9.SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;

namespace K9.DataAccessLayer.Models
{
    [Name(ResourceType = typeof(Globalisation.Dictionary), ListName = Globalisation.Strings.Names.Protocols, PluralName = Globalisation.Strings.Names.Protocols, Name = Globalisation.Strings.Names.Protocol)]
    public class Protocol : ObjectBase
    {
        public Guid ExternalId { get; set; }

        [NotMapped]
        [UIHint("Protocol")]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.ProtocolLabel)]
        public int ProtocolId => Id;

        [UIHint("ProtocolType")]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.ProtocolTypeLabel)]
        public EProtocolType Type { get; set; }

        [UIHint("Frequency")]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.FrequencyLabel)]
        public EFrequency Frequency { get; set; }

        [UIHint("Period")]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.PeriodLabel)]
        public EPeriod Period { get; set; }

        [UIHint("Every")]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.PeriodValueLabel)]
        public int PeriodValue { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.NumberOfPeriodsOffLabel)]
        public int NumberOfPeriodsOff { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.NumberOfPeriodsOnLabel)]
        public int NumberOfPeriodsOn { get; set; }

        [UIHint("Contact")]
        [ForeignKey("Contact")]
        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.CustomerLabel)]
        public int? ContactId { get; set; }

        public virtual Contact Contact { get; set; }

        [LinkedColumn(LinkedTableName = "Contact", LinkedColumnName = "FullName")]
        public string ContactName { get; set; }

        [UIHint("ProtocolDuration")]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.DurationLabel)]
        public EProtocolDuration Duration { get; set; } = EProtocolDuration.ThreeMonths;

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.DaysDurationLabel)]
        public int DaysDuration => GetDaysDuration();

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.CustomDurationLabel)]
        public int CustomDaysDuration { get; set; }

        public virtual IEnumerable<ProtocolActivity> ProtocolActivities { get; set; }

        [NotMapped]
        public List<ProtocolActivity> Activities { get; set; }

        public virtual IEnumerable<ProtocolDietaryRecommendation> ProtocolDietaryRecommendations { get; set; }

        [NotMapped]
        public List<ProtocolDietaryRecommendation> DietaryRecommendations { get; set; }

        public virtual IEnumerable<ProtocolProduct> ProtocolProducts { get; set; }

        public ProtocolProduct GetProtocolProductByProductId(int productId) =>
            Products?.FirstOrDefault(e => e.ProductId == productId);

        public ProtocolProductPack GetProtocolProductPackByProductId(int productId) =>
            ProductPacks?.FirstOrDefault(e => e.ProductPack.Products.Select(p => p.ProductId).Contains(productId));

        public ProductPackProduct GetProtocolProductPackProductByProductId(int productId) =>
            GetProtocolProductPackByProductId(productId)?.ProductPack.Products.FirstOrDefault(e => e.ProductId == productId);

        [NotMapped]
        public List<ProtocolProduct> Products { get; set; }

        public virtual IEnumerable<ProtocolProductPack> ProtocolProductPacks { get; set; }

        public List<Product> GetAllProducts() => Products?.Select(e => e?.Product).ToList()
                                                .Concat(ProductPacks?
                                                .SelectMany(e => e.ProductPack.Products.Select(p => p.Product)))
                                                .Distinct().ToList();

        [NotMapped]
        public List<ProtocolProductPack> ProductPacks { get; set; }

        public virtual IEnumerable<ProtocolSection> ProtocolProtocolSections { get; set; }

        [NotMapped]
        public List<ProtocolSection> ProtocolSections { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.ShortDescriptionLabel)]
        [Required(ErrorMessageResourceType = typeof(Dictionary), ErrorMessageResourceName = Strings.ErrorMessages.FieldIsRequired)]
        [StringLength(int.MaxValue)]
        [DataType(DataType.Html)]
        [AllowHtml]
        public string ShortDescription { get; set; }

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.BodyLabel)]
        [Required(ErrorMessageResourceType = typeof(Dictionary), ErrorMessageResourceName = Strings.ErrorMessages.FieldIsRequired)]
        [StringLength(int.MaxValue)]
        [DataType(DataType.Html)]
        [AllowHtml]
        public string Body { get; set; }

        [FileSourceInfo("upload/protocols", Filter = EFilesSourceFilter.Images)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Names.UploadImages)]
        public FileSource ImageFileSource { get; set; }

        [FileSourceInfo("upload/protocols", Filter = EFilesSourceFilter.Videos)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Names.UploadVideo)]
        public FileSource VideoFileSource { get; set; }

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.SeoFriendlyIdLabel)]
        public string SeoFriendlyId { get; set; }

        [StringLength(512)]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.VideoUrlLabel)]
        public string VideoUrl { get; set; }

        public int GetPeriodLength()
        {
            switch (Frequency)
            {
                case EFrequency.Fortnightly:
                    return 14;

                case EFrequency.Monthly:
                    return 28;

                case EFrequency.Daily:
                    return 7;
            }

            return 0;
        }

        public int GetNumberOfPeriodsPerDuration()
        {
            var numberOfPeriodsPerDuration = DaysDuration / GetPeriodLength();
            if (numberOfPeriodsPerDuration <= 0)
            {
                throw new Exception(Globalisation.Dictionary.ProtocolDurationIsTooShort);
            }
            return numberOfPeriodsPerDuration;
        }

        public int GetNumberOfDaysOnPerDuration()
        {
            var numberOfPeriodsPerDuration = GetNumberOfPeriodsPerDuration();
            switch (Frequency)
            {
                case EFrequency.Daily:
                    return numberOfPeriodsPerDuration * (GetPeriodLength() - NumberOfPeriodsOff);

                case EFrequency.Fortnightly:
                case EFrequency.Monthly:
                    return numberOfPeriodsPerDuration * (NumberOfPeriodsOn);

                default:
                    return 0;
            }
        }

        private int GetDaysDuration()
        {
            switch (Duration)
            {
                case EProtocolDuration.Custom:
                    return CustomDaysDuration;

                default:
                    return (int)Duration;
            }
        }
    }
}
