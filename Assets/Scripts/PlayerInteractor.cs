using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    private Interactor currentInteractor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Interactor interactor))
        {
            Debug.Log("Entro al Trigger");
            currentInteractor = interactor;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Interactor interactor) && interactor == currentInteractor)
        {
            currentInteractor = null;
        }
    }

    private void Update()
    {
        if (currentInteractor != null && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Aprieto la E");
            currentInteractor.Interact();
        }
    }
}