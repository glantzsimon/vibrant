using K9.DataAccessLayer.Interfaces;
using System.Collections.Generic;

namespace K9.WebApplication.Services
{
    public interface ICategorisableService
    {
        int GetItemCode(ICategorisable model, List<ICategorisable> items);
    }
}