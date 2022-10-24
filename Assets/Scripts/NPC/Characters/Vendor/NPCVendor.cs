using UnityEngine;

public class NPCVendor : MonoBehaviour, IInteractable
{

    [SerializeField] private string npcName;

    public string GetDescription()
    {
        return $"Talk to {npcName}.";
    }

    public float GetInteractionTime()
    {
        return 0;
    }

    public void Interact()
    {
    }
}
