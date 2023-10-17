using K9.DataAccessLayer.Enums;
using K9.SharedLibrary.Models;

namespace K9.DataAccessLayer.Interfaces
{
    public interface ICategorisable : IObjectBase
    {
        ECategory Category { get; set; }
        int ItemCode { get; set; }
    }
}