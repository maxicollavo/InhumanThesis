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

    public void Interact()
    {
        Destroy(interactableUI);
        Destroy(interactableTrigger);

        MecanismCoroutine();
    }

    private void MecanismCoroutine()
    {
        EventManager.Instance.Dispatch(GameEventTypes.OnCinematic, this, EventArgs.Empty);
        wallAnim.SetTrigger("Interact");
    }

    public IEnumerator OnCinematicMethod()
    {
        cinematicCamera.RotateCamera(0);
        yield return new WaitForSeconds(1f);
        columnAnim.SetTrigger("Interact");
    }
}