using K9.Base.DataAccessLayer.Attributes;
using K9.DataAccessLayer.Enums;
using K9.SharedLibrary.Extensions;
using System;

namespace K9.DataAccessLayer.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class NineStarKiEnumMetaDataAttribute : Attribute
    {
        public Type ResourceType { get; set; }
        public string Name { get; set; }
        public ENineStarKiElement Element { get; set; }
    
        public string GetDescription()
        {
            return ResourceType.GetValueFromResource(Name);
        }
        
        public string GetElement()
        {
            var attr = Element.GetAttribute<EnumDescriptionAttribute>();
            return attr.GetDescription();
        }
        
        private string GetEnergytNumberAndName(ENineStarKiEnergy energy)
        {
            return $"{(int)energy} {energy.GetAttribute<NineStarKiEnumMetaDataAttribute>().Name}";
        }
    }

}