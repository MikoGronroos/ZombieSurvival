using UnityEngine;

[CreateAssetMenu(menuName = "Crafting/Recipe", fileName = "New Recipe")]
public class Recipe : ScriptableObject
{

    [SerializeField] private Item product;
    [SerializeField] private Item[] itemsNeeded;
    [SerializeField] private RecipeCategory recipeCategory;

    public RecipeCategory RecipeCategory { get; private set; }

}
