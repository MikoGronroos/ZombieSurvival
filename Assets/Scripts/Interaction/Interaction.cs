using System.Collections.Generic;
using UnityEngine;
using Finark.Utils;
using System;
using System.Collections;

public class Interaction : MonoBehaviour
{

    [SerializeField] private UserInterfaceChannel userInterfaceChannel;
    [SerializeField] private InteractionData interactionData;

    [SerializeField] private bool isInteracting;

    private Camera _cam;

    private void Awake()
    {
        _cam = Camera.main;
    }

    private void OnEnable()
    {
        interactionData.IsInteractingEvent += GetIsInteracting;
    }

    private void OnDisable()
    {
        interactionData.IsInteractingEvent -= GetIsInteracting;
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
                    StartCoroutine(Interacting(interactable));
                }
            }
            else
            {
                userInterfaceChannel.ToggleMouseOnTopOfInteractionUI?.Invoke(new Dictionary<string, object> { { "value", false } });
            }
        }
    }

    private IEnumerator Interacting(IInteractable interactable)
    {
        isInteracting = true;
        userInterfaceChannel.ToggleGeneralInteractionDelayUI?.Invoke(true);
        Timer timer = new Timer(interactable.GetInteractionTime(), null);
        interactionData.StartProgressBarEvent?.Invoke(interactable.GetInteractionTime());
        while (timer.CurrentTime < timer.MaxTime)
        {
            timer.Tick();
            yield return null;
        }
        userInterfaceChannel.ToggleGeneralInteractionDelayUI?.Invoke(false);
        interactable.Interact();
        isInteracting = false;
    }

    private bool GetIsInteracting()
    {
        return isInteracting;
    }

}
