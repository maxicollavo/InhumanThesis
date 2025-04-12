using System.Collections.Generic;
using System;
using UnityEngine;

public class LockInteractor : MonoBehaviour, Interactor
{
    [SerializeField] GameObject gameObj;
    [SerializeField] GameObject lockGOs;

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
        OnPuzzleMethod();
    }

    public void LockDisabled()
    {
        OnGameplayMethod();
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

    void OnPuzzleMethod()
    {
        DisableOutline();
        gameObj.SetActive(false);
        lockGOs.SetActive(true);
        GameManager.Instance.lockInt = this;
    }

    void OnGameplayMethod()
    {
        OnLock = !OnLock;
        EventManager.Instance.Dispatch(GameEventTypes.OnGameplay, this, EventArgs.Empty);
        gameObj.SetActive(true);
        lockGOs.SetActive(false);
        GameManager.Instance.lockInt = null;
    }

    public void Aiming()
    {
        EnableOutline();
    }
}
