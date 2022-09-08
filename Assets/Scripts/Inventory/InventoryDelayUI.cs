using System;
using System.Collections;
using UnityEngine;

public class InventoryDelayUI : MonoBehaviour
{

    [SerializeField] private InteractionData interactionData;
    [SerializeField] private InventoryDelay inventorySlot;

    private void OnEnable()
    {
        interactionData.InteractedEvent += InteractedListener;
        interactionData.StartProgressBarEvent += StartProgressBar;
    }

    private void OnDisable()
    {
        interactionData.InteractedEvent -= InteractedListener;
        interactionData.StartProgressBarEvent -= StartProgressBar;
    }


    private void InteractedListener(InventoryDelay slot)
    {
        inventorySlot = slot;
    }

    private void StartProgressBar(float time)
    {
        StartCoroutine(DelayCoroutine(inventorySlot, time));
    }

    private void UpdateLoadingBarState(InventoryDelay slot, float current, float max)
    {
        if (slot.ItemLoadingBar == null)
        {
            return;
        }
        slot.ItemLoadingBar.fillAmount = current / max;
    }

    private IEnumerator DelayCoroutine(InventoryDelay slot, float time)
    {
        Timer timer = new Timer(time, null);
        while (timer.CurrentTime <= timer.MaxTime)
        {
            timer.Tick();
            UpdateLoadingBarState(slot, timer.CurrentTime, timer.MaxTime);
            yield return null;
        }
    }

}
