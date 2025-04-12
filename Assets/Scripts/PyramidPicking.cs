using UnityEngine;

public class PyramidPicking : MonoBehaviour, Interactor
{
    [SerializeField] Outline outline;

    [SerializeField] GameObject handPyramid;
    [SerializeField] GameObject grabbedPyramid;
    [SerializeField] GameObject door;

    private void Start()
    {
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

    private void GrabPyramid()
    {
        handPyramid.SetActive(true);
        grabbedPyramid.SetActive(false);
        door.SetActive(false);
    }

    public void Interact()
    {
        GrabPyramid();
    }
}
