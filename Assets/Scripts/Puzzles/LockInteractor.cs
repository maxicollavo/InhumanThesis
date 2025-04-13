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
        EventManager.Instance.Register(GameEventTypes.OnGameplay, LockDisabled);

        outline.enabled = false;
    }

    private void OnDestroy()
    {
        EventManager.Instance.Unregister(GameEventTypes.OnGameplay, LockDisabled);
    }

    public void LockDisabled(object o, EventArgs e)
    {
        OnGameplayMethod();
    }

    public void Interact()
    {
        OnPuzzleMethod();
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
        EventManager.Instance.Dispatch(GameEventTypes.OnPuzzle, this, EventArgs.Empty);
        OnLock = true;
        DisableOutline();

        lockCam.SetActive(true);
        playerCam.SetActive(false);

        GameManager.Instance.lockInt = this;
    }

    void OnGameplayMethod()
    {
        if (!OnLock) return;
        Debug.Log("On gameplay");

        OnLock = false;

        playerCam.SetActive(true);
        lockCam.SetActive(false);

        GameManager.Instance.lockInt = null;
    }

    public void Aiming()
    {
        EnableOutline();
    }
}