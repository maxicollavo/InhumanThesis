using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeInteractor : MonoBehaviour, Interactor
{
    [HideInInspector]
    public bool isLit;
    private Renderer torchRenderer;

    private void Awake()
    {
        torchRenderer = GetComponent<Renderer>();
    }

    public void Interact()
    {
        isLit = !isLit;

        UpdateTorchState();

        GameManager.Instance.GetAllTorchs();
    }

    private void UpdateTorchState()
    {
        if (isLit)
        {
            torchRenderer.material.color = Color.red;
        }
        else
        {
            torchRenderer.material.color = Color.white;
        }
    }
}
