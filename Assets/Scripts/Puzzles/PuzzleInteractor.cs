using System;
using UnityEngine;
using UnityEngine.Playables;

public class PuzzleInteractor : MonoBehaviour, Interactor
{
    public Action<PuzzleInteractor> PuzzleAction;

    [SerializeField] Outline outline;

    private void Start()
    {
        outline.enabled = false;
    }

    public void Interact()
    {
        PuzzleMethod();
    }

    public void PuzzleMethod()
    {
        PuzzleAction?.Invoke(this);
    }

    public void DisableOutline()
    {
        outline.enabled = false;

        UIManager.Instance.ChangeCursor(false);
    }

    void EnableOutline()
    {
        outline.enabled = true;
    }

    public void Aiming()
    {
        EnableOutline();

        UIManager.Instance.ChangeCursor(true);
    }
}
