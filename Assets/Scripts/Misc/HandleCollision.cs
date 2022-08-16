using UnityEngine;

public class HandleCollision : MonoBehaviour
{

    public delegate void TriggerEnter(Collider collider);
    public event TriggerEnter TriggerEnterEvent;

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            TriggerEnterEvent?.Invoke(other);
        }

    }

}
