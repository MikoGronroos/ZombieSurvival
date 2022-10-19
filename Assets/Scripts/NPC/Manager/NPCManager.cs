using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour, ISaveable
{

    [SerializeField] private GameObject[] npcs;

    public object CaptureState()
    {

        List<NPCData> data = new List<NPCData>();

        foreach (var npc in npcs)
        {
            data.Add(new NPCData{
                posX = npc.transform.position.x,
                posY = npc.transform.position.y,
                posZ = npc.transform.position.z,
                rotX = npc.transform.rotation.x,
                rotY = npc.transform.rotation.y,
                rotZ = npc.transform.rotation.z,
                prefabName = ""
            });
        }

        return new SaveData { 
            data = data
        };
    }

    public void RestoreState(object state)
    {
        var data = (SaveData)state;
        foreach (var item in data.data)
        {
            npcs[0].transform.position = new Vector3(item.posX, item.posY, item.posZ);
        }
    }

    [System.Serializable]
    struct SaveData
    {
        public List<NPCData> data;
    }

    [System.Serializable]
    struct NPCData
    {
        public float posX, posY, posZ;
        public float rotX, rotY, rotZ;
        public string prefabName;
    }

}
