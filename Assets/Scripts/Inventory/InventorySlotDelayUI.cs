using UnityEngine;

public class InventorySlotDelayUI : MonoBehaviour
{

    [SerializeField] private InteractionData interactionData;
    [SerializeField] private InventorySlotUI inventorySlot;

    private void OnEnable()
    {
        interactionData.InteractedEvent += InteractedListener;
        interactionData.UpdateProgressBarEvent += UpdateLoadingBarState;
    }

    private void OnDisable()
    {
        interactionData.InteractedEvent -= InteractedListener;
        interactionData.UpdateProgressBarEvent -= UpdateLoadingBarState;
    }

    private void InteractedListener(InventorySlotUI slot)
    {
        inventorySlot = slot;
    }

    public void UpdateLoadingBarState(float current, float max)
    {
        if (inventorySlot.ItemLoadingBar == null)
        {
            return;
        }
        inventorySlot.ItemLoadingBar.fillAmount = current / max;
    }

}
