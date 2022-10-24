using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Dialogue Node")]
public class DialogueNode : ScriptableObject
{

    [SerializeField] private string nodeSpeaker;
    [SerializeField] private string nodeContents;

}