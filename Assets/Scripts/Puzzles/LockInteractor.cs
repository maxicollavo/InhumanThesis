using System.Collections.Generic;
using System;
using UnityEngine;

public class LockInteractor : MonoBehaviour, Interactor
{
    [SerializeField] GameObject playerCam;
    [SerializeField] GameObject lockCam;

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
        playerCam.SetActive(false);
        lockCam.SetActive(true);
        GameManager.Instance.lockInt = this;
    }

    void OnGameplayMethod()
    {
        OnLock = !OnLock;
        EventManager.Instance.Dispatch(GameEventTypes.OnGameplay, this, EventArgs.Empty);
        playerCam.SetActive(true);
        lockCam.SetActive(false);
        GameManager.Instance.lockInt = null;
    }

    public void Aiming()
    {
        EnableOutline();
    }
}
