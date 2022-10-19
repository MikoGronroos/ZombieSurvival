using System.Collections.Generic;
using UnityEngine;
using Finark.Utils;
using System.Collections;
using System;

public class Interaction : MonoBehaviour
{

    [SerializeField] private UserInterfaceChannel userInterfaceChannel;
    [SerializeField] private InteractionData interactionData;
    [SerializeField] private InputEventChannel inputEventChannel;

    [SerializeField] private bool isInteracting;

    private Camera _cam;
    private IInteractable _interactable;

    private void Awake()
    {
        _cam = Camera.main;
    }

    private void OnEnable()
    {
        interactionData.IsInteractingEvent += GetIsInteracting;
        inputEventChannel.IsInteracting += IsInteractingListener;
    }

    private void OnDisable()
    {
        interactionData.IsInteractingEvent -= GetIsInteracting;
        inputEventChannel.IsInteracting -= IsInteractingListener;
    }

    private void IsInteractingListener()
    {
        if (_interactable != null)
        {
            StartCoroutine(Interacting(_interactable));
        }
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

                _interactable = interactable;
                userInterfaceChannel.ToggleMouseOnTopOfInteractionUI?.Invoke(new Dictionary<string, object> { { "value", true }, { "description", interactable.GetDescription() } });
            }
            else
            {
                _interactable = null;
                userInterfaceChannel.ToggleMouseOnTopOfInteractionUI?.Invoke(new Dictionary<string, object> { { "value", false } });
            }
        }
    }

    private IEnumerator Interacting(IInteractable interactable)
    {
        if (interactable.GetInteractionTime() > 0)
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
        }
        interactable.Interact();
        isInteracting = false;
    }

    private bool GetIsInteracting()
    {
        return isInteracting;
    }

}
