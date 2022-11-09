using UnityEngine;

public class SlotClickedMenuButton : MonoBehaviour
{

    [SerializeField] private SlotMenuType slotMenuType;

    public SlotMenuType SlotMenuType { get { return slotMenuType; } }

    public void ToggleGameObject(bool value)
    {
        gameObject.SetActive(value);
    }

}
