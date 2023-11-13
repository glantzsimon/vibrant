using K9.Base.DataAccessLayer.Models;
using System.ComponentModel.DataAnnotations;

namespace K9.DataAccessLayer.Models
{
    public abstract class GenoTypeBase : ObjectBase
    {
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.VataDosha)]
        public bool VataDosha { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.PittaDosha)]
        public bool PittaDosha { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.KaphaDosha)]
        public bool KaphaDosha { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.Hunter)]
        public bool Hunter { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.Gatherer)]
        public bool Gatherer { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.Teacher)]
        public bool Teacher { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.Explorer)]
        public bool Explorer { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.Warrior)]
        public bool Warrior { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.Nomad)]
        public bool Nomad { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.Water)]
        public bool Water { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.Earth)]
        public bool Earth { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.Tree)]
        public bool Tree { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.Metal)]
        public bool Metal { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.Fire)]
        public bool Fire { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.Cbs)]
        public bool Cbs { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.Immunity)]
        public bool Immunity { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.NeurologicalHealth)]
        public bool NeurologicalHealth { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.DigestiveHealth)]
        public bool DigestiveHealth { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.CardiovascularHealth)]
        public bool CardioVascularHealth { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.UrologicalHealth)]
        public bool UrologicalHealth { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.HormoneBalance)]
        public bool HormoneBalance { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.AntiInflammatory)]
        public bool AntiInflammatory { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.StressRelief)]
        public bool StressRelief { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.Detoxification)]
        public bool Detoxification { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.DentalHealth)]
        public bool DentalHealth { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.NourishesYin)]
        public bool NourishesYin { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.NourishesYang)]
        public bool NourishesYang { get; set; }
    }
}
