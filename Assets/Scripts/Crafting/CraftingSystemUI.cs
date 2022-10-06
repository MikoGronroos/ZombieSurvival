using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSystemUI : MonoBehaviour
{

    [SerializeField] private GameObject categoryButton;
    [SerializeField] private Transform categoryButtonParent;

    [SerializeField] private GameObject recipeButtonPrefab;
    [SerializeField] private Transform recipeButtonParent;

    [SerializeField] private CraftingEventChannel craftingEventChannel;

    private List<GameObject> _drawnRecipes = new List<GameObject>();

    private void Start()
    {
        var amount = Enum.GetNames(typeof(RecipeCategory)).Length;
        for (int i = 0; i < amount; i++)
        {
            GameObject button = Instantiate(categoryButton, categoryButtonParent);
            var categoryFromInt = (RecipeCategory)i;
            if (button.TryGetComponent(out CraftingCategoryButton category))
            {
                category.SetupButton(categoryFromInt.ToString());
            }
            button.GetComponent<Button>().onClick.AddListener(()=> {
                DrawRecipes(categoryFromInt);
            });
        }

        DrawRecipes(0);

    }

    private void DrawRecipes(RecipeCategory category)
    {
        if (_drawnRecipes.Count > 0)
        {
            for (int i = _drawnRecipes.Count - 1; i >= 0; i--)
            {
                Destroy(_drawnRecipes[i]);
                _drawnRecipes.RemoveAt(i);
            }
        }
        List<Recipe> recipes = (List<Recipe>)craftingEventChannel.GetRecipesFromCategory?.Invoke(category);

        if (recipes != null)
        {
            foreach (var recipe in recipes)
            {
                GameObject button = Instantiate(recipeButtonPrefab, recipeButtonParent);
                if (button.TryGetComponent(out RecipeButton recipeButton))
                {
                    recipeButton.SetupButton(recipe.Product.ItemName, recipe.Product.ItemIcon);
                }
                _drawnRecipes.Add(button);
                button.GetComponent<Button>().onClick.AddListener(() => {
                    craftingEventChannel.Craft?.Invoke(recipe);
                });
            }
        }
    }
}
