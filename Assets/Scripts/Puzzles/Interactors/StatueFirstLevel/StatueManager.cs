using System;
using UnityEngine;

public class StatueManager : MonoBehaviour
{
    [SerializeField] StatueInteractor statueInteractor;
    [SerializeField] HeadInteractor headInteractor;

    [SerializeField] BoxCollider headColl;
    [SerializeField] BoxCollider headColl2;
    [SerializeField] GameObject blueLight;
    [SerializeField] MeshRenderer headMesh;
    [SerializeField] Animator anim;

    public Action<StatueManager> StatueManagerAction;

    private void Start()
    {
        statueInteractor.InteractorAction += OnStatueInteract;
        headInteractor.HeadAction += OnHeadMethod;
    }

    private void OnStatueInteract(StatueInteractor interactor)
    {
        StatueInteract();
    }

    private void OnHeadMethod(HeadInteractor interactor)
    {
        HeadInteract();
    }

    private void StatueInteract()
    {
        anim.SetTrigger("Interact");
        headColl.enabled = true;
    }

    private void HeadInteract()
    {
        blueLight.SetActive(true);
        headColl.enabled = false;
        headColl2.enabled = false;
        headMesh.enabled = false;

        SendActionToJeroglific();
    }

    void SendActionToJeroglific()
    {
        StatueManagerAction?.Invoke(this);
    }
}