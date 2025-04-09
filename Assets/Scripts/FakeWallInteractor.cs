using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class FakeWallInteractor : MonoBehaviour, Interactor
{
    [SerializeField] private PlayableDirector timelineDirector;

    [SerializeField] private GameObject interactableUI;
    [SerializeField] private GameObject interactableTrigger;

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
        timelineDirector.Play();
    }
}