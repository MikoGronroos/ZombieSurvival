using System;
using UnityEngine;

[CreateAssetMenu(menuName = "InteractionData")]
public class InteractionData : ScriptableObject
{

    public delegate void Interacted(InventorySlotUI slot);

    public delegate void UpdateProgressBar(float current, float max);

    public Interacted InteractedEvent { get; set; }

    public UpdateProgressBar UpdateProgressBarEvent { get; set; }

}
