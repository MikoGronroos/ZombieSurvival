using System;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{

    [SerializeField] private float openingTime;
    [SerializeField] private InteractionNumber interactionNumber;

    public string GetDescription()
    {
        return "Open the door.";
    }

    public float GetInteractionTime()
    {
        return openingTime;
    }

    public void Interact()
    {
        gameObject.SetActive(false);
    }

    public int GetInteractionNumber()
    {
        return (int)interactionNumber;
    }

}
