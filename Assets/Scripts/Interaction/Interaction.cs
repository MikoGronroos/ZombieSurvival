using System.Collections.Generic;
using UnityEngine;
using Finark.Utils;
using System;

public class Interaction : MonoBehaviour
{

    [SerializeField] private UserInterfaceChannel userInterfaceChannel;
    [SerializeField] private InteractionData interactionData;

    private Camera _cam;

    private void Awake()
    {
        _cam = Camera.main;
    }

    private void Update()
    {

        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hit, 100))
        {
            if (hit.transform.TryGetComponent(out IInteractable interactable))
            {

                if (MyUtils.IsPointerOverUI()) return;

                if (!interactionData.CanInteractEvent(hit.transform)) return;

                userInterfaceChannel.ToggleMouseOnTopOfInteractionUI?.Invoke(new Dictionary<string, object> { { "value", true }, { "description", interactable.GetDescription() } });

                if (InputSystem.Instance.IsInteracting)
                {
                    interactable.Interact();
                }
            }
            else
            {
                userInterfaceChannel.ToggleMouseOnTopOfInteractionUI?.Invoke(new Dictionary<string, object> { { "value", false } });
            }
        }
    }

}
