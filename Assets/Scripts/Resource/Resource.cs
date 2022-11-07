using UnityEngine;

public class Resource : MonoBehaviour, IInteractable
{

    [SerializeField] private string resourceName;
    [SerializeField] private float gatherTime;

    [SerializeField] private Item resourceItem;

    [SerializeField] private int minBaseResourceYield;
    [SerializeField] private int maxBaseResourceYield;

    [SerializeField] private InteractionNumber interactionNumber;

    [SerializeField] private InventoryChannel inventoryChannel;

    public string GetDescription()
    {
        return $"Gather {resourceName}.";
    }

    public float GetInteractionTime()
    {
        return gatherTime;
    }

    public void Interact()
    {
        inventoryChannel.AddAmountOfItems?.Invoke(resourceItem, Random.Range(minBaseResourceYield, maxBaseResourceYield), ()=> {
            Destroy(gameObject);
        });
    }

    public int GetInteractionNumber()
    {
        return (int)interactionNumber;
    }

}
