﻿using K9.Base.DataAccessLayer.Attributes;
using K9.Base.DataAccessLayer.Models;
using K9.SharedLibrary.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace K9.DataAccessLayer.Models
{
    [AutoGenerateName]
    [Name(ResourceType = typeof(Globalisation.Dictionary), ListName = Globalisation.Strings.Names.ProtocolProducts, PluralName = Globalisation.Strings.Names.ProtocolProducts, Name = Globalisation.Strings.Names.ProtocolProduct)]
    public class ProtocolActivity : ObjectBase
    {
        [UIHint("Protocol")]
        [ForeignKey("Protocol")]
        public int ProtocolId { get; set; }

        public virtual Protocol Protocol { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.ProtocolLabel)]
        [LinkedColumn(LinkedTableName = "Protocol", LinkedColumnName = "Name")]
        public string ProtocolName { get; set; }

        [UIHint("Activity")]
        [ForeignKey("Activity")]
        public int ActivityId { get; set; }

        public virtual Activity Activity { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.ActivityLabel)]
        [LinkedColumn(LinkedTableName = "Activity", LinkedColumnName = "Name")]
        public string ActivityName { get; set; }
    }
}