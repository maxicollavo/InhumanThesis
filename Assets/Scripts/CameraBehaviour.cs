using System;
using Unity.VisualScripting;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private void Start()
    {
        EventManager.Instance.Register(GameEventTypes.OnCinematic, CursorEnabled);
        EventManager.Instance.Register(GameEventTypes.OnGameplay, CursorDisabled);
    }

    private void OnDestroy()
    {
        EventManager.Instance.Unregister(GameEventTypes.OnCinematic, CursorEnabled);
        EventManager.Instance.Unregister(GameEventTypes.OnGameplay, CursorDisabled);
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
