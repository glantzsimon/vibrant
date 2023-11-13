using K9.DataAccessLayer.Enums;
using System;

namespace K9.DataAccessLayer.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class QuestionCategoryAttribute : Attribute
    {
        public EQuestionCategory Category { get; set; }
    }
}