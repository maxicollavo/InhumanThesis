using System.Collections.Generic;
using System;
using UnityEngine;

public class LockInteractor : MonoBehaviour, Interactor
{
    [SerializeField] List<GameObject> gameObj;
    [SerializeField] List<GameObject> lockGOs;

    private bool OnLock;

    private void Start()
    {
        EventManager.Instance.Register(GameEventTypes.OnPuzzle, LockEnabled);
        EventManager.Instance.Register(GameEventTypes.OnGameplay, LockDisabled);
    }

    private void OnDestroy()
    {
        EventManager.Instance.Unregister(GameEventTypes.OnPuzzle, LockEnabled);
        EventManager.Instance.Unregister(GameEventTypes.OnGameplay, LockDisabled);
    }

    void LockEnabled(object sender, EventArgs e)
    {
        foreach (var obj in gameObj)
        {
            obj.SetActive(false);
        }

        foreach (var go in lockGOs)
        {
            go.SetActive(true);
        }
    }

    void LockDisabled(object sender, EventArgs e)
    {
        foreach(var obj in gameObj)
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

        if (OnLock)
        {
            EventManager.Instance.Dispatch(GameEventTypes.OnPuzzle, this, EventArgs.Empty);
        }
        else
        {
            EventManager.Instance.Dispatch(GameEventTypes.OnGameplay, this, EventArgs.Empty);
        }
    }

    public void DisableOutline()
    {
        throw new NotImplementedException();
    }

    public void Aiming()
    {
        throw new NotImplementedException();
    }
}
