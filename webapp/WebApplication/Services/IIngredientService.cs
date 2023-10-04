using K9.DataAccessLayer.Models;
using System.Collections.Generic;

namespace K9.WebApplication.Services
{
    public interface IIngredientService
    {
        Ingredient Find(int id);
        Ingredient FindPrevious(int id);
        Ingredient FindNext(int id);
        Ingredient Find(string seoFriendlyId);
        Ingredient DuplicateIngredient(int id);
        Ingredient GetFullIngredient(Ingredient ingredient);
        Ingredient FindWithSubstitutesSelectList(int id);
        List<Ingredient> List(bool retrieveFullIngredient = false);
    }
}