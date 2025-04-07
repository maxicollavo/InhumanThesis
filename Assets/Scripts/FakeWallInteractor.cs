using System;
using System.Collections;
using UnityEngine;

public class FakeWallInteractor : MonoBehaviour, Interactor
{
    [SerializeField] private Animator wallAnim;
    [SerializeField] private Animator columnAnim;

    [SerializeField] private GameObject interactableUI;
    [SerializeField] private GameObject interactableTrigger;

    [SerializeField] private CinematicCamera cinematicCamera;

    private BoxCollider boxCollider;
    private bool HasDone;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    public void Interact()
    {
        if (HasDone) return;
        
        Destroy(interactableUI);
        Destroy(interactableTrigger);
        boxCollider.enabled = false;

        Mecanism();
    }

    private void Mecanism()
    {
        EventManager.Instance.Dispatch(GameEventTypes.OnCinematic, this, EventArgs.Empty);
        HasDone = true;
        wallAnim.SetTrigger("Interact");
    }

    public IEnumerator OnCinematicMethod()
    {
        cinematicCamera.RotateCamera(0);
        yield return new WaitForSeconds(1f);
        columnAnim.SetTrigger("Interact");
    }
}