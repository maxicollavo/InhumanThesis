using UnityEngine;
using UnityEngine.SceneManagement;

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

        if (other.gameObject.CompareTag("Finisher"))
        {
            SceneManager.LoadScene("EndDemoScene");
        }
    }
}