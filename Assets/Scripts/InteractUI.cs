using UnityEngine;

public class InteractUI : MonoBehaviour
{
    [SerializeField]
    private GameObject go;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerInteractor player))
        {
            go.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out PlayerInteractor player))
        {
            go.SetActive(false);
        }
    }
}
