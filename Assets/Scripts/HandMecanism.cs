using System.Collections;
using UnityEngine;

public class HandMecanism : MonoBehaviour, Interactor
{
    Outline outline;

    GameObject door;

    [SerializeField] TutorialTriggers tutorialTrigger;
    [SerializeField] GameObject textTutorial;

    private void Start()
    {
        outline = GetComponentInParent<Outline>();
        door = transform.parent.parent.gameObject;

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

    private void OpenDoor()
    {
        StartCoroutine(DestroyTrigger());
        UIManager.Instance.ChangeCursor(false);
    }

    private IEnumerator DestroyTrigger()
    {
        Destroy(textTutorial);
        yield return new WaitForSeconds(0.1f);
        Destroy(tutorialTrigger.gameObject);
        door.SetActive(false);
    }

    public void Interact()
    {
        OpenDoor();
    }
}