using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Extensions;
using K9.SharedLibrary.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace K9.WebApplication.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly ILogger _logger;
        private readonly IRepository<Ingredient> _ingredientsRepository;

        public IngredientService(ILogger logger, IRepository<Ingredient> ingredientsRepository)
        {
            _logger = logger;
            _ingredientsRepository = ingredientsRepository;
        }

        public Ingredient Find(int id)
        {
            var ingredient = _ingredientsRepository.Find(id);
            return ingredient;
        }

        public Ingredient FindNext(int id)
        {
            var ingredient = _ingredientsRepository.Find(e => e.Id > id).OrderBy(e => e.Id).FirstOrDefault() ?? _ingredientsRepository.GetQuery("SELECT TOP 1 * FROM [Ingredient] ORDER BY [Id]").FirstOrDefault();
            return ingredient;
        }

        public Ingredient FindPrevious(int id)
        {
            var ingredient = _ingredientsRepository.Find(e => e.Id < id).OrderByDescending(e => e.Id).FirstOrDefault() ?? _ingredientsRepository.GetQuery("SELECT TOP 1 * FROM [Ingredient] ORDER BY [Id] DESC").FirstOrDefault();

            return ingredient;
        }

        public Ingredient Find(string seoFriendlyId)
        {
            var ingredient = _ingredientsRepository.Find(e => e.SeoFriendlyId == seoFriendlyId).FirstOrDefault();
            return ingredient;
        }

        public Ingredient DuplicateIngredient(int id)
        {
            var ingredient = Find(id);
            if (ingredient == null)
            {
                return null;
            }

            var newIngredient = new Ingredient();
            ingredient.MapTo(newIngredient);
            newIngredient.Id = 0;
            var newIngredientName = $"{ingredient.Name} Copy";
            newIngredient.Name = newIngredientName;

            _ingredientsRepository.Create(newIngredient);
            newIngredient = Find(newIngredientName);
            if (newIngredient == null)
            {
                throw new Exception("Error duplicating ingredient");
            }

            return ingredient;
        }

        public List<Ingredient> List()
        {
            var ingredients = _ingredientsRepository.List().Where(e => !e.IsDeleted).OrderBy(e => e.Name).ToList();
            return ingredients;
        }
    }
}