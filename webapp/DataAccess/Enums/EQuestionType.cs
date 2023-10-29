using K9.Base.DataAccessLayer.Attributes;

namespace K9.DataAccessLayer.Enums
{
    public enum EQuestionType
    {
        [EnumDescription(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.AminoAcid)]
        Text = 1,
        Numerical = 2,
        Date = 3,
        Scale = 4,
        MultipleChoice = 5
    }
}
