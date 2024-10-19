using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class CodeInteractor : MonoBehaviour, Interactor
{
    [HideInInspector]
    public bool isLit;
    private Renderer torchRenderer;
    public Shader shader1;
    public Shader shader2;
    public ParticleSystem FireParticle;
    public TextMeshProUGUI numberText;

    #region Sounds
    [SerializeField] AudioSource torchSound;
    public AudioSource fireSound;
    #endregion Sounds

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

        if (isLit)
        {
            torchSound.Play();
            fireSound.Play();
        }
        else
        {
            torchSound.Play();
            fireSound.Stop();
        }


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

    public IEnumerable<bool> GetTorchesLitGenerator()
    {
        yield return isLit;
    }
}