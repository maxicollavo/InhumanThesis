using System;
using UnityEngine;

public class MovePuzzleInteractor : MonoBehaviour, Interactor
{
    Outline outline;
    [SerializeField] Camera puzzleCam;

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    private void Start()
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

        UIManager.Instance.ChangeCursor(true);
    }

    public void DisableOutline()
    {
        outline.enabled = false;

        UIManager.Instance.ChangeCursor(false);
    }

    public void Interact()
    {
        EventManager.Instance.Dispatch(GameEventTypes.OnPuzzle, this, EventArgs.Empty);
        puzzleCam.enabled = true;
        DisableOutline();
    }
}