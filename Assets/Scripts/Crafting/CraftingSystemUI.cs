using System;
using UnityEngine;

public class CraftingSystemUI : MonoBehaviour
{

    [SerializeField] private GameObject categoryButton;
    [SerializeField] private Transform categoryButtonParent;

    private void Start()
    {
        var amount = Enum.GetNames(typeof(RecipeCategory)).Length;
        for (int i = 0; i < amount; i++)
        {
            GameObject button = Instantiate(categoryButton, categoryButtonParent);
            if (button.TryGetComponent(out CraftingCategoryButton category))
            {
                category.SetupButton(((RecipeCategory)i).ToString());
            }
        }

    }

}
