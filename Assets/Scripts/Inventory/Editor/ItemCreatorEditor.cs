using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class ItemCreatorEditor : EditorWindow
{

    string[] options = {
        "Ammunition",
        "Clothing",
        "Consumable",
        "Health",
        "Resource",
        "Tools",
        "Weapons"
    };

    string[] scriptableObjectOptions =
    {
        "Equipment",
        "Resource",
        "Clothing",
        "Consumable"
    };

    int index = 0;
    int scriptIndex = 0;
    string itemName = "";
    Sprite itemIcon;
    EquipmentType equipmentType;

    int consumableValuesSize;
    ConsumableValue[] consumableValues;

    int maxStackSize = 0;
    float itemWeight = 0;

    GameObject equipmentPrefab = null;

    Item creationResult = null;

    [MenuItem("Window/Item Creator")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(ItemCreatorEditor));
    }

    private void OnGUI()
    {

        index = EditorGUILayout.Popup(
            "Item save folder",
            index,
            options
        );

        scriptIndex = EditorGUILayout.Popup(
            "Item type",
            scriptIndex,
            scriptableObjectOptions
        );

        itemName = EditorGUILayout.TextField("Item name", itemName);

        itemIcon = (Sprite)EditorGUILayout.ObjectField("Item icon", itemIcon, typeof(Sprite), false);

        maxStackSize = EditorGUILayout.IntField("Max stack size", maxStackSize);

        itemWeight = EditorGUILayout.FloatField("Item weight", itemWeight);

        if (scriptIndex == 0) //Item tool or weapon
        {
            equipmentPrefab = (GameObject)EditorGUILayout.ObjectField(equipmentPrefab, typeof(GameObject), true);
            equipmentType = (EquipmentType)EditorGUILayout.EnumPopup("Equipment type", equipmentType);
        }
        else if (scriptIndex == 2) //Item clothing
        {
            equipmentPrefab = (GameObject)EditorGUILayout.ObjectField(equipmentPrefab, typeof(GameObject), true);
            equipmentType = (EquipmentType)EditorGUILayout.EnumPopup("Equipment type", equipmentType);
        }

        if (GUILayout.Button("Create item"))
        {
            switch (scriptIndex)
            {
                case 0:
                    creationResult = ScriptableObject.CreateInstance<ItemEquipment>();
                    var temp = creationResult as ItemEquipment;
                    temp.EquipmentPrefab = equipmentPrefab;
                    temp.Type = equipmentType;
                    break;
                case 1:
                    creationResult = ScriptableObject.CreateInstance<ItemResource>();
                    break;
                case 2:
                    creationResult = ScriptableObject.CreateInstance<ItemClothing>();
                    var clothingTemp = creationResult as ItemClothing;
                    clothingTemp.EquipmentPrefab = equipmentPrefab;
                    clothingTemp.Type = equipmentType;
                    break;
                case 3:
                    creationResult = ScriptableObject.CreateInstance<ItemConsumable>();
                    break;
            }

            creationResult.GenerateId();
            creationResult.ItemIcon = itemIcon;
            creationResult.ItemName = itemName;
            creationResult.Weight = itemWeight;
            creationResult.MaxStackSize = maxStackSize;

            AssetDatabase.CreateAsset(creationResult, $"Assets/ScriptableObjects/Resources/Items/{options[index]}/{itemName}.asset");
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = creationResult;

            Debug.Log($"Created item: {itemName}");
        }

    }

}
