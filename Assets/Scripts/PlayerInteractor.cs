using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out ITutorial tutorial))
        {
            tutorial.Interact();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out ITutorial tutorial))
        {
            tutorial.Exit();
        }
    }
}