using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{

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

}
