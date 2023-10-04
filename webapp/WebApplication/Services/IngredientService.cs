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
        private readonly IRepository<IngredientSubstitute> _ingredientSubstituesRepository;

        public IngredientService(ILogger logger, IRepository<Ingredient> ingredientsRepository, IRepository<IngredientSubstitute> ingredientSubstituesRepository)
        {
            _logger = logger;
            _ingredientsRepository = ingredientsRepository;
            _ingredientSubstituesRepository = ingredientSubstituesRepository;
        }

        public Ingredient Find(int id)
        {
            var ingredient = _ingredientsRepository.Find(id);
            if (ingredient != null)
            {
                ingredient = GetFullIngredient(ingredient);
            }
            return ingredient;
        }

        public Ingredient FindNext(int id)
        {
            var ingredient = _ingredientsRepository.Find(e => e.Id > id).OrderBy(e => e.Id).FirstOrDefault() ?? _ingredientsRepository.GetQuery("SELECT TOP 1 * FROM [Ingredient] ORDER BY [Id]").FirstOrDefault();
            if (ingredient != null)
            {
                ingredient = GetFullIngredient(ingredient);
            }
            return ingredient;
        }

        public Ingredient FindPrevious(int id)
        {
            var ingredient = _ingredientsRepository.Find(e => e.Id < id).OrderByDescending(e => e.Id).FirstOrDefault() ?? _ingredientsRepository.GetQuery("SELECT TOP 1 * FROM [Ingredient] ORDER BY [Id] DESC").FirstOrDefault();
            if (ingredient != null)
            {
                ingredient = GetFullIngredient(ingredient);
            }
            return ingredient;
        }

        public Ingredient Find(string seoFriendlyId)
        {
            var ingredient = _ingredientsRepository.Find(e => e.SeoFriendlyId == seoFriendlyId).FirstOrDefault();
            if (ingredient != null)
            {
                ingredient = GetFullIngredient(ingredient);
            }
            return ingredient;
        }

        public Ingredient GetFullIngredient(Ingredient ingredient)
        {
            var ingredientSubstitutes = _ingredientSubstituesRepository.Find(e => e.IngredientId == ingredient.Id)
                .OrderByDescending(e => e.Priority).ToList();

            foreach (var ingredientSubstitute in ingredientSubstitutes)
            {
                ingredientSubstitute.Ingredient = ingredient;
                ingredientSubstitute.SubstituteIngredient =_ingredientsRepository.Find(e => e.Id == ingredientSubstitute.IngredientId).FirstOrDefault();
            }

            ingredient.IngredientSubstitutes = ingredientSubstitutes;
            ingredient.Substitutes = ingredientSubstitutes;

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

        public List<Ingredient> List(bool retrieveFullIngredient = false)
        {
            var ingredients = _ingredientsRepository.List().Where(e => !e.IsDeleted).OrderBy(e => e.Name).ToList();
            
            if (retrieveFullIngredient)
            {
                var fullIngredients = new List<Ingredient>();
                foreach (var ingredient in ingredients)
                {
                    fullIngredients.Add(GetFullIngredient(ingredient));
                }

                return fullIngredients;
            }

            return ingredients;
        }
    }
}