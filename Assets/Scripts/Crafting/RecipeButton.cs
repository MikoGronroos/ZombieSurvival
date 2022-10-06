using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class RecipeButton : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Image recipeIconImage;

    public void SetupButton(string name, Sprite icon)
    {
        recipeNameText.text = name;
        recipeIconImage.sprite = icon;
    }

}
