using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="EventChannels/Crafting Event Channel")]
public class CraftingEventChannel : ScriptableObject
{

    public delegate IEnumerable<Recipe> RecipeDictionaryDelegate(RecipeCategory category);

    public RecipeDictionaryDelegate GetRecipesFromCategory { get; set; }

}