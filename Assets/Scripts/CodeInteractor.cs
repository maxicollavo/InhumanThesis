using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeInteractor : MonoBehaviour, Interactor
{
    [HideInInspector]
    public bool isLit;
    private Renderer torchRenderer;
    [HideInInspector]
    public int torchIndex;

    private void Awake()
    {
        torchRenderer = GetComponent<Renderer>();

        if (GameManager.Instance != null && GameManager.Instance.antorchas != null)
        {
            GameManager.Instance.antorchas.Add(gameObject);
            torchIndex = GameManager.Instance.antorchas.Count - 1;

            UpdateTorchState();
        }
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
