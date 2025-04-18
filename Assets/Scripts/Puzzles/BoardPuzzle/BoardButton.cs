using System;
using UnityEngine;

public class BoardButton : MonoBehaviour
{
    //Va a llamar a un action y enviar la dirección del botón presionado al action
    public Action<Vector2, BoardButton> OnButtonPressed;
    [SerializeField] Vector2 direction;

    [HideInInspector]
    public Outline outline;

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    private void Start()
    {
        outline.enabled = false;
    }

    private void OnMouseDown()
    {
        PressedButton();
    }

    public void PressedButton()
    {
        OnButtonPressed?.Invoke(direction, this);
    }

    public void DisableOutline()
    {
        outline.enabled = false;
    }

    public void EnableOutline()
    {
        outline.enabled = true;
    }
}
