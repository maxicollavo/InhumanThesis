using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Closer"))
        {
            var doorCloser = other.GetComponent<DoorCloser>();

            if (doorCloser != null)
                doorCloser.Close();
        }
    }
}
