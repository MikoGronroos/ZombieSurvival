using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem : MonoBehaviour
{

    [SerializeField] private SaveEventChannel saveEventChannel;

    private void OnEnable()
    {
        saveEventChannel.Save += Save;
        saveEventChannel.Load += Load;
    }

    private void OnDisable()
    {
        saveEventChannel.Save -= Save;
        saveEventChannel.Load -= Load;
    }

    private string _savePath => $"{Application.persistentDataPath}/gamestate.txt";

    [ContextMenu("Save")]
    private void Save()
    {
        var state = LoadFile();
        CaptureState(state);
        SaveFile(state);
    }

    [ContextMenu("Load")]
    private void Load()
    {
        var state = LoadFile();
        RestoreState(state);
    }

    private Dictionary<string, object> LoadFile()
    {
        if (!File.Exists(_savePath))
        {
            return new Dictionary<string, object>();
        }
        using (FileStream stream = File.Open(_savePath, FileMode.Open))
        {
            var formatter = new BinaryFormatter();
            return (Dictionary<string, object>)formatter.Deserialize(stream);
        }
    }

    private void SaveFile(object state)
    {
        using (var stream = File.Open(_savePath, FileMode.Create))
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, state);
        }
    }

    private void CaptureState(Dictionary<string, object> state)
    {
        foreach (var saveable in FindObjectsOfType<SaveableEntity>())
        {
            state[saveable.Id] = saveable.CaptureState();
        }
    }

    private void RestoreState(Dictionary<string, object> state)
    {
        foreach (var saveable in FindObjectsOfType<SaveableEntity>())
        {
            if (state.TryGetValue(saveable.Id, out object value))
            {
                saveable.RestoreState(value);
            }
        }
    }

}
