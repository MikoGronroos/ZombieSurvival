using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{

    [SerializeField] private CraftingEventChannel craftingEventChannel;

    private Dictionary<RecipeCategory, List<Recipe>> _recipes = new Dictionary<RecipeCategory, List<Recipe>>();

    private void Awake()
    {
        Object[] recipes = Resources.LoadAll("Recipes", typeof(Recipe));

        foreach (Recipe recipe in recipes)
        {
            if (!_recipes.ContainsKey(recipe.RecipeCategory))
            {
                _recipes.Add(recipe.RecipeCategory, new List<Recipe>());
            }
            _recipes[recipe.RecipeCategory].Add(recipe);
        }

    }

    private void OnEnable()
    {
        craftingEventChannel.GetRecipesFromCategory += GetRecipesFromCategory;
    }

    private void OnDisable()
    {
        craftingEventChannel.GetRecipesFromCategory -= GetRecipesFromCategory;
    }

    private IEnumerable<Recipe> GetRecipesFromCategory(RecipeCategory category)
    {
        if (_recipes.ContainsKey(category))
        {
            return _recipes[category];
        }
        return null;
    }

}
