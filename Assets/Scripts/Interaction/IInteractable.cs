using System;
using UnityEngine;

public interface IInteractable
{

    public float GetInteractionTime();

    public int GetInteractionNumber();

    public void Interact();

    public string GetDescription();

}
