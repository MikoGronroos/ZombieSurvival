using UnityEngine;

[CreateAssetMenu(menuName = "Crafting/Recipe", fileName = "New Recipe")]
public class Recipe : ScriptableObject
{

    [SerializeField] private Item product;
    [SerializeField] private Item[] itemsNeeded;
    [SerializeField] private RecipeCategory recipeCategory;

    public RecipeCategory RecipeCategory { get { return recipeCategory; } }

    public Item Product { get { return product; } }

    public Item[] ItemsNeeded { get { return itemsNeeded; } }

}
