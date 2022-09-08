using UnityEngine;

[CreateAssetMenu(menuName = "InteractionData")]
public class InteractionData : ScriptableObject
{

    public delegate void Interacted(InventoryDelay slot);

    public delegate void StartProgressBar(float time);

    public delegate bool CanInteract(Transform target);

    public Interacted InteractedEvent { get; set; }

    public StartProgressBar StartProgressBarEvent { get; set; }

    public CanInteract CanInteractEvent { get; set; }

}
