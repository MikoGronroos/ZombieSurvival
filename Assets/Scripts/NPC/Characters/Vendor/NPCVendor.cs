using UnityEngine;

public class NPCVendor : MonoBehaviour, IInteractable
{

    [SerializeField] private string npcName;
    [SerializeField] private float interactionTime;
    [SerializeField] private InteractionNumber interactionNumber;

    public string GetDescription()
    {
        return $"Talk to {npcName}.";
    }

    public float GetInteractionTime()
    {
        return interactionTime;
    }

    public void Interact()
    {
    }

    public int GetInteractionNumber()
    {
        return (int)interactionNumber;
    }

}
