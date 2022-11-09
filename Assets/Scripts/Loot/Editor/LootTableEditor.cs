using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LootTable))]
public class LootTableEditor : Editor
{

    LootTable selection;
    float previousValue;

    private void OnEnable()
    {
        selection = (LootTable)target;

    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        for (int k = 0; k < selection.CurrentLootTable.Length; k++)
        {
            previousValue = selection.CurrentLootTable[k].ChanceOfSpawning;
            GUILayout.Label("This is a text that makes space");
            selection.CurrentLootTable[k].ChanceOfSpawning = Mathf.Clamp(GUILayout.HorizontalSlider(selection.CurrentLootTable[k].ChanceOfSpawning, 0, 1), 0, 1);
            if (previousValue != selection.CurrentLootTable[k].ChanceOfSpawning)
            {
                AdjustSliders(k);
            }
        }
    }

    private void AdjustSliders(int ignoreIndex)
    {
        int amountOfSliders = selection.CurrentLootTable.Length - 1;

        float valueToGiveToAll = previousValue - selection.CurrentLootTable[ignoreIndex].ChanceOfSpawning;

        float valueToGiveToOne = valueToGiveToAll / amountOfSliders;

        for (int i = 0; i < selection.CurrentLootTable.Length; i++)
        {

            if (i == ignoreIndex) continue;

            selection.CurrentLootTable[i].ChanceOfSpawning = Mathf.Clamp(selection.CurrentLootTable[i].ChanceOfSpawning + valueToGiveToOne, 0, 1);

        }

    }
}
