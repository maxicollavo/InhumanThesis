using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CodeInteractor : MonoBehaviour, Interactor
{
    [HideInInspector]
    public bool isLit;
    private Renderer torchRenderer;
    public Shader shader1;
    public Shader shader2;
    public ParticleSystem FireParticle;
    private void Start()
    {
        FireParticle.Pause();
    }
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
            //torchRenderer.material.shader = shader1;
          torchRenderer.material.color = Color.red;
            FireParticle.Play();
        }
        else
        {
           // torchRenderer.material.shader = shader2;
            torchRenderer.material.color = Color.white;
            FireParticle.Stop();
        }
    }

}
