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
        torchRenderer.material.shader = shader1;
    }

    private void Awake()
    {
        torchRenderer = transform.parent.GetComponent<Renderer>();
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
            torchRenderer.material.shader = shader2;
            FireParticle.Play();
        }
        else
        {
            torchRenderer.material.shader = shader1;
            FireParticle.Stop();
        }
    }

}
