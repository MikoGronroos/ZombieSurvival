using System;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{

    public float InteractionTime { get; set; }



    public string GetDescription()
    {
        return "Open the door.";
    }

    public float GetInteractionTime()
    {
        return 0f;
    }

    public void Interact()
    {
        gameObject.SetActive(false);
    }

}
