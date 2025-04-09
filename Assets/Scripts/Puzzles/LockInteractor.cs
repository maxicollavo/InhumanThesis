using System.Collections.Generic;
using System;
using UnityEngine;

public class LockInteractor : MonoBehaviour, Interactor
{
    [SerializeField] List<GameObject> gameObj;
    [SerializeField] List<GameObject> lockGOs;

    [HideInInspector]
    public bool OnLock;

    [SerializeField] Outline outline;

    private void Start()
    {
        EventManager.Instance.Register(GameEventTypes.OnPuzzle, LockEnabled);

        outline.enabled = false;
    }

    private void OnDestroy()
    {
        EventManager.Instance.Unregister(GameEventTypes.OnPuzzle, LockEnabled);
    }

    void LockEnabled(object sender, EventArgs e)
    {
        DisableOutline();

        foreach (var obj in gameObj)
        {
            obj.SetActive(false);
        }

        foreach (var go in lockGOs)
        {
            go.SetActive(true);
        }
    }

    public void LockDisabled()
    {
        OnLock = !OnLock;
        EventManager.Instance.Dispatch(GameEventTypes.OnGameplay, this, EventArgs.Empty);

        foreach (var obj in gameObj)
        {
            obj.SetActive(true);
        }

        foreach (var go in lockGOs)
        {
            go.SetActive(false);
        }
    }

    public void Interact()
    {
        OnLock = !OnLock;

        EventManager.Instance.Dispatch(GameEventTypes.OnPuzzle, this, EventArgs.Empty);
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
