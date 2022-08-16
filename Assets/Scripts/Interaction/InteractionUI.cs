using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI interactionAvailableText;

    [SerializeField] private UserInterfaceChannel userInterfaceChannel;

    private void OnEnable()
    {
        userInterfaceChannel.ToggleMouseOnTopOfInteractionUI += ToggleInteractionUI;
    }

    private void OnDisable()
    {
        userInterfaceChannel.ToggleMouseOnTopOfInteractionUI -= ToggleInteractionUI;
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

}
