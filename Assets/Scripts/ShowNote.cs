using System.Collections;
using UnityEngine;

public class ShowNote : MonoBehaviour, Interactor
{
    [SerializeField]
    GameObject noteToShow;
    bool isShowing;
    public GameObject HWPGO;
    [SerializeField] Animator stoneAnim;

    public bool isOpener;
    WaitForSeconds wfs = new WaitForSeconds(3f);

    BoxCollider noteCollider;
    public OpenHallwayDoor door;

    private void Start()
    {
        noteCollider = GetComponent<BoxCollider>();
    }
    private void Update()
    {
       if (HWPGO.activeInHierarchy) 
       {
           stoneAnim.SetBool("IsTrue", true);
       }
    }
    public void Interact()
    {
        if (isShowing) return;

        if (isOpener)
            StartCoroutine(ShowNoteAndOpenCoroutine());
        else
            StartCoroutine(ShowNoteCoroutine());
    }

    private IEnumerator ShowNoteAndOpenCoroutine()
    {
        noteCollider.enabled = false;
        isShowing = true;
        noteToShow.SetActive(isShowing);
        yield return wfs;
        isShowing = false;
        noteToShow.SetActive(isShowing);

        door.Interact();
    }

    private IEnumerator ShowNoteCoroutine()
    {
        isShowing = true;
        noteToShow.SetActive(isShowing);
        yield return wfs;
        isShowing = false;
        noteToShow.SetActive(isShowing);
    }
}