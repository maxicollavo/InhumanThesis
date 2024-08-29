using UnityEngine;

public class SwitchTurnOff : MonoBehaviour, Interactor
{
    [SerializeField] GameObject objectToTurnOff;

    public void Interact()
    {
        objectToTurnOff.SetActive(false);
    }
}
