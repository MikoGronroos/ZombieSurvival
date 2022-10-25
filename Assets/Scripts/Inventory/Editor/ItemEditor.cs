using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Item), true)]
public class ItemEditor : Editor
{

    private Item _target;

    private void OnEnable()
    {
        _target = (Item)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Generate Id")){
            _target.GenerateId();
        }
    }
}
