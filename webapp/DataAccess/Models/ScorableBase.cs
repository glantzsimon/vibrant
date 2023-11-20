using K9.Base.DataAccessLayer.Models;
using K9.SharedLibrary.Models;

namespace K9.DataAccessLayer.Models
{
    public abstract class ScorableBase : ObjectBase, IObjectBase, IScorableBase
    {
        public int Score { get; set; }
        
        public int RelativeScore { get; set; }

        public string GetRelativeScoreHtml()
        {
            if (RelativeScore > 90)
            {
                return "<i class=\"fa fa-heart\"</i><i class=\"fa fa-heart\"</i>";
            }

            if (RelativeScore > 80)
            {
                return "<i class=\"fa fa-heart\"</i>";
            }

            if (RelativeScore > 70)
            {
                return "<i class=\"fa fa-heart-o\"</i>";
            }

            return "";
        }
        
    }
}
