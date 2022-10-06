using UnityEngine;
using TMPro;

public class CraftingCategoryButton : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI categoryNameText;

    public void SetupButton(string name)
    {
        categoryNameText.text = name;
    }

}
