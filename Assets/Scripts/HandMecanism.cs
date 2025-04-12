using UnityEngine;

public class HandMecanism : MonoBehaviour, Interactor
{
    Outline outline;

    GameObject door;

    private void Start()
    {
        outline = GetComponentInParent<Outline>();
        door = transform.parent.parent.gameObject;

        outline.enabled = false;
    }

    public void DisableOutline()
    {
        outline.enabled = false;
    }

    void EnableOutline()
    {
        outline.enabled = true;
    }

    public void Aiming()
    {
        EnableOutline();
    }

    private void OpenDoor()
    {
        door.SetActive(false);
    }

    public void Interact()
    {
        OpenDoor();
    }
}