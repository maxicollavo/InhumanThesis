using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowNote : MonoBehaviour, Interactor
{
    [SerializeField]
    GameObject noteToShow;
    bool isShowing;
    WaitForSeconds wfs = new WaitForSeconds(5f);

    public void Interact()
    {
        if (isShowing) return;
        StartCoroutine(ShowNoteCoroutine());
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