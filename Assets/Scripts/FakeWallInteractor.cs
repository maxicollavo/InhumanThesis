using System.Collections;
using UnityEngine;

public class FakeWallInteractor : MonoBehaviour, Interactor
{
    [SerializeField] private Animator wallAnim;
    [SerializeField] private Animator columnAnim;

    [SerializeField] private GameObject interactableUI;
    [SerializeField] private GameObject interactableTrigger;

    public void Interact()
    {
        Destroy(interactableUI);
        Destroy(interactableTrigger);
        StartCoroutine(MecanismCoroutine());
    }

    private IEnumerator MecanismCoroutine()
    {
        Debug.Log("Corrutina");
        wallAnim.SetTrigger("Interact");
        yield return new WaitForSeconds(2f);
        columnAnim.SetTrigger("Interact");
    }
}