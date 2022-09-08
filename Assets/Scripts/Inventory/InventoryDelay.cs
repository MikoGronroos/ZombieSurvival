using UnityEngine;
using UnityEngine.UI;

public class InventoryDelay : MonoBehaviour
{
    [SerializeField] private Image itemLoadingBar;

    public Image ItemLoadingBar { get { return itemLoadingBar; } private set { } }
}