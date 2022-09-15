using System;
using UnityEngine;

public interface IInteractable
{

    public float GetInteractionTime();

    public void Interact();

    public string GetDescription();

}
