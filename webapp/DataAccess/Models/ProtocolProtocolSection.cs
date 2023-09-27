using K9.Base.DataAccessLayer.Attributes;
using K9.Base.DataAccessLayer.Models;
using K9.SharedLibrary.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace K9.DataAccessLayer.Models
{
    [AutoGenerateName]
    [Name(ResourceType = typeof(Globalisation.Dictionary), ListName = Globalisation.Strings.Names.ProtocolProtocolSections, PluralName = Globalisation.Strings.Names.ProtocolProtocolSections, Name = Globalisation.Strings.Names.ProtocolProtocolSection)]
    public class ProtocolProtocolSection : ObjectBase
    {
        [UIHint("Protocol")]
        [ForeignKey("Protocol")]
        public int ProtocolId { get; set; }

        public virtual Protocol Protocol { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.ProtocolLabel)]
        [LinkedColumn(LinkedTableName = "Protocol", LinkedColumnName = "Name")]
        public string ProtocolName { get; set; }

        [UIHint("ProtocolSection")]
        [ForeignKey("ProtocolSection")]
        public int ProtocolSectionId { get; set; }

        public virtual ProtocolSection ProtocolSection { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.ProtocolSectionLabel)]
        [LinkedColumn(LinkedTableName = "ProtocolSection", LinkedColumnName = "Name")]
        public string ProtocolSectionName { get; set; }

        public virtual IEnumerable<ProtocolProtocolSectionProduct> ProtocolProtocolSectionProducts { get; set; }

        [NotMapped]
        public List<ProtocolProtocolSectionProduct> ProtocolSectionProducts { get; set; }
    }
}
