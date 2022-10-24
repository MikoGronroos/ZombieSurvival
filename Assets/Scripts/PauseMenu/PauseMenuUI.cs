using UnityEngine;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour
{

    [SerializeField] private Button saveButton;
    [SerializeField] private Button loadButton;

    [SerializeField] private SaveEventChannel saveEventChannel;

    private void Awake()
    {
        saveButton.onClick.AddListener(()=> {
            saveEventChannel.Save?.Invoke();
        });

        loadButton.onClick.AddListener(() => {
            saveEventChannel.Load?.Invoke();
        });

    }

}
