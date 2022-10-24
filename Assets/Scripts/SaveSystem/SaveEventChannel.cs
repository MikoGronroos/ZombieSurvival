using System;
using UnityEngine;

[CreateAssetMenu(menuName = "EventChannels/Save Event Channel")]
public class SaveEventChannel : ScriptableObject
{

    public Action Save { get; set; }

    public Action Load { get; set; }
}
