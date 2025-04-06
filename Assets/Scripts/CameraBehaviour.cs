using System;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private void Start()
    {
        EventManager.Instance.Register(GameEventTypes.OnCinematic, CursorEnabled);
        EventManager.Instance.Register(GameEventTypes.OnGameplay, CursorDisabled);
        EventManager.Instance.Register(GameEventTypes.OnPuzzle, CursorDisabled);
    }

    private void OnDestroy()
    {
        EventManager.Instance.Unregister(GameEventTypes.OnCinematic, CursorEnabled);
        EventManager.Instance.Unregister(GameEventTypes.OnGameplay, CursorDisabled);
        EventManager.Instance.Unregister(GameEventTypes.OnPuzzle, CursorDisabled);
    }

    void CursorEnabled(object sender, EventArgs e)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void CursorDisabled(object sender, EventArgs e)
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
}
