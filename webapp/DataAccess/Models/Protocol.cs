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
using System.Linq;
using System.Web.Mvc;
using K9.DataAccessLayer.Enums;

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

        [UIHint("Frequency")]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.FrequencyLabel)]
        public EFrequency Frequency { get; set; }

        [UIHint("Period")]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.PeriodLabel)]
        public EPeriod Period { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.PeriodValueLabel)]
        public int PeriodValue { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.NumberOfPeriodsOffLabel)]
        public int NumberOfPeriodsOff { get; set; }

        [UIHint("Contact")]
        [ForeignKey("Contact")]
        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.CustomerLabel)]
        public int? ContactId { get; set; }

        public virtual Contact Contact { get; set; }

        [LinkedColumn(LinkedTableName = "Contact", LinkedColumnName = "FullName")]
        public string ContactName { get; set; }

        public virtual IEnumerable<ProtocolActivity> ProtocolActivities { get; set; }

        [NotMapped]
        public List<ProtocolActivity> Activities { get; set; }

        public virtual IEnumerable<ProtocolProduct> ProtocolProducts { get; set; }

        [NotMapped]
        public List<ProtocolProduct> Products { get; set; }

        public virtual IEnumerable<ProtocolProductPack> ProtocolProductPacks { get; set; }

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
        
        public bool CheckSchedule(DayOfWeek dayofWeek)
        {
            if (Frequency == EFrequency.Daily)
            {
                if (NumberOfPeriodsOff >= 7)
                {
                    return false;
                }

                switch (dayofWeek)
                {
                    case DayOfWeek.Monday:
                        return true;

                    case DayOfWeek.Tuesday:
                        return NumberOfPeriodsOff <= 2;

                    case DayOfWeek.Wednesday:
                        return new []{4, 3, 1, 0}.Contains(NumberOfPeriodsOff);

                    case DayOfWeek.Thursday:
                        return new []{5, 2, 1, 0}.Contains(NumberOfPeriodsOff);

                    case DayOfWeek.Friday:
                        return NumberOfPeriodsOff <= 4;

                    case DayOfWeek.Saturday:
                        return NumberOfPeriodsOff <= 3;

                    case DayOfWeek.Sunday:
                        return NumberOfPeriodsOff == 0;
                }
            }

            return false;
        }
    }
}
