using System.Collections;
using UnityEngine;

public class FakeWallInteractor : MonoBehaviour, Interactor
{
    [SerializeField] private Animator wallAnim;
    [SerializeField] private Animator columnAnim;

    public void Interact()
    {
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