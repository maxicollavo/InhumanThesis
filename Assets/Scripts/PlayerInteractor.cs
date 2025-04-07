using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    private Interactor currentInteractor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Interactor interactor))
        {
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
        if (GameManager.Instance.inputblock) return;

        if (currentInteractor != null && Input.GetKeyDown(KeyCode.E))
        {
            currentInteractor.Interact();
        }
    }
}