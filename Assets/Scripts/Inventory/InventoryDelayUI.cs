using System;
using System.Collections;
using UnityEngine;

public class InventoryDelayUI : MonoBehaviour
{

    [SerializeField] private InteractionData interactionData;

    private void OnEnable()
    {
        interactionData.StartProgressBarEvent += StartProgressBar;
    }

    private void OnDisable()
    {
        interactionData.StartProgressBarEvent -= StartProgressBar;
    }

    private void StartProgressBar(float time, InventoryDelay inventoryDelay)
    {
        StartCoroutine(DelayCoroutine(inventoryDelay, time));
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
