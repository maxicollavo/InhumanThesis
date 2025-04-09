using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class FakeWallInteractor : MonoBehaviour, Interactor
{
    [SerializeField] private PlayableDirector timelineDirector;

    private BoxCollider boxCollider;
    private bool HasDone;

    [SerializeField] Outline outline;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();

        outline.enabled = false;
    }

    public void Interact()
    {
        if (HasDone) return;

        Mecanism();
    }

    private void Mecanism()
    {
        HasDone = true;
        EventManager.Instance.Dispatch(GameEventTypes.OnCinematic, this, EventArgs.Empty);
        DisableOutline();
        boxCollider.enabled = false;
        timelineDirector.Play();
    }

    public void DisableOutline()
    {
        outline.enabled = false;
    }

    void EnableOutline()
    {
        outline.enabled = true;
    }

    public void Aiming()
    {
        EnableOutline();
    }
}