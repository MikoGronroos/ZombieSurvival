using UnityEngine;

public class Toggle : MonoBehaviour
{

    public void ToggleGameObject()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

}
