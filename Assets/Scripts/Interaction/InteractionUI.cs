using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI interactionAvailableText;
    [SerializeField] private GameObject generalInteractionDelayImageBG;
    [SerializeField] private InventoryDelay generalInteractionDelayImage;

    [SerializeField] private UserInterfaceChannel userInterfaceChannel;
    [SerializeField] private InteractionData interactionData;

    private void OnEnable()
    {
        userInterfaceChannel.ToggleMouseOnTopOfInteractionUI += ToggleInteractionUI;
        userInterfaceChannel.ToggleGeneralInteractionDelayUI += ToggleGeneralInteractionDelayUI;
    }

    private void OnDisable()
    {
        userInterfaceChannel.ToggleMouseOnTopOfInteractionUI -= ToggleInteractionUI;
        userInterfaceChannel.ToggleGeneralInteractionDelayUI -= ToggleGeneralInteractionDelayUI;
    }

    private void ToggleInteractionUI(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {
        bool toggleValue = (bool)args["value"];
        if (toggleValue)
        {
            string description = (string)args["description"];
            interactionAvailableText.text = description;
        }

        interactionAvailableText.enabled = toggleValue;

    }

    private void ToggleGeneralInteractionDelayUI(bool value)
    {
        interactionData.InteractedEvent?.Invoke(generalInteractionDelayImage);
        generalInteractionDelayImageBG.SetActive(value);
    }

}
