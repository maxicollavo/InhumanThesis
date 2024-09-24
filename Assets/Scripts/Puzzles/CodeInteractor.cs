using UnityEngine;
using TMPro;

public class CodeInteractor : MonoBehaviour, Interactor
{
    [HideInInspector]
    public bool isLit;
    private Renderer torchRenderer;
    public Shader shader1;
    public Shader shader2;
    public ParticleSystem FireParticle;
    public TextMeshProUGUI numberText;
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
            numberText.color = Color.green;
            FireParticle.Play();
        }
        else
        {
            torchRenderer.material.shader = shader1;
            numberText.color = Color.red;
            FireParticle.Stop();
        }
    }

}
