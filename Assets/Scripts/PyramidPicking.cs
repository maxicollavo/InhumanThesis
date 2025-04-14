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

        UIManager.Instance.ChangeCursor(false);
    }

    void EnableOutline()
    {
        outline.enabled = true;
    }

    public void Aiming()
    {
        EnableOutline();

        UIManager.Instance.ChangeCursor(true);
    }

    private void GrabPyramid()
    {
        handPyramid.SetActive(true);
        grabbedPyramid.SetActive(false);
        door.SetActive(false);

        UIManager.Instance.ChangeCursor(false);
    }

    public void Interact()
    {
        GrabPyramid();
    }
}
