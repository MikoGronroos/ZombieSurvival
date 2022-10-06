using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{

    [SerializeField] private CraftingEventChannel craftingEventChannel;
    [SerializeField] private InventoryChannel inventoryChannel;

    private Dictionary<RecipeCategory, List<Recipe>> _recipes = new Dictionary<RecipeCategory, List<Recipe>>();

    private void Awake()
    {
        Recipe[] recipes = Resources.LoadAll<Recipe>("Recipes/");
        
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
        craftingEventChannel.Craft += CraftItem;
    }

    private void OnDisable()
    {
        craftingEventChannel.GetRecipesFromCategory -= GetRecipesFromCategory;
        craftingEventChannel.Craft -= CraftItem;
    }

    private IEnumerable<Recipe> GetRecipesFromCategory(RecipeCategory category)
    {
        if (_recipes.ContainsKey(category))
        {
            return _recipes[category];
        }
        return null;
    }

    private void CraftItem(Recipe recipe)
    {
        inventoryChannel.AddAmountOfItems(recipe.Product, 1);
    }

}
