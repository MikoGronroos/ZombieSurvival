using UnityEngine;

[CreateAssetMenu(menuName = "InteractionData")]
public class InteractionData : ScriptableObject
{

    public delegate void StartProgressBar(float time, InventoryDelay inventoryDelay);

    public delegate bool CanInteract(Transform target);

    public delegate bool IsInteracting();

    public StartProgressBar StartProgressBarEvent { get; set; }

    public CanInteract CanInteractEvent { get; set; }

    public IsInteracting IsInteractingEvent { get; set; }

}
