using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public string GetDescription()
    {
        return "Open the door.";
    }

    public void Interact()
    {
        gameObject.SetActive(false);
    }

}
