using System.Collections;
using UnityEngine;

public class DissolverController : MonoBehaviour
{
    [SerializeField] float dissolverRate = 0.0125f;
    [SerializeField] float refreshRate = 0.025f;
    [SerializeField] Renderer MeshRender;
    [SerializeField] Material[] Materials;
    private bool isDissolving = false;
    private bool isRestoring = false;

    void Start()
    {
        if (MeshRender != null)
            Materials = MeshRender.materials;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isDissolving && !isRestoring)
        {
            StartCoroutine(DissolveCo());
        }
        else if (Input.GetKeyDown(KeyCode.K) && !isDissolving && !isRestoring)
        {
            StartCoroutine(RestoreCo());
        }
    }

    public IEnumerator DissolveCo()
    {
        if (Materials.Length > 0)
        {
            isDissolving = true;
            float counter = 0;
            while (counter < 1)
            {
                counter += dissolverRate;
                for (int i = 0; i < Materials.Length; i++)
                {
                    Materials[i].SetFloat("_DissolverAmount", counter);
                }
                yield return new WaitForSeconds(refreshRate);
            }
            isDissolving = false;
        }
    }

    public IEnumerator RestoreCo()
    {
        if (Materials.Length > 0)
        {
            isRestoring = true;
            float counter = 1;
            while (counter > 0)
            {
                counter -= dissolverRate;
                for (int i = 0; i < Materials.Length; i++)
                {
                    Materials[i].SetFloat("_DissolverAmount", counter);
                }
                yield return new WaitForSeconds(refreshRate);
            }
            isRestoring = false;
        }
    }

    public void SetDissolveAmount(float amount)
    {
        for (int i = 0; i < Materials.Length; i++)
        {
            Materials[i].SetFloat("_DissolverAmount", amount);
        }
    }
}