using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainPanelUI : MonoBehaviour
{

    [SerializeField] private PanelButton[] panelButtons;

    private void Awake()
    {
        foreach (PanelButton button in panelButtons)
        {
            button.panelButton.onClick.AddListener(()=> {
                button.pressedEvent?.Invoke();
            });
        }
    }

}

[System.Serializable]
public class PanelButton
{
    public string name;
    public Button panelButton;
    public UnityEvent pressedEvent;
}