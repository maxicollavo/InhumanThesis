using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookActivator : MonoBehaviour, Interactor
{
    public List<HookBehaviour> hooks = new List<HookBehaviour>();
    [SerializeField] AudioSource audio;

    BoxCollider boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    public void Interact()
    {
        foreach (var hook in hooks)
        {
            hook.Mover();
            hook.WinChecker();
        }
        audio.Play();

        StartCoroutine(BoxCollToggle());
    }

    IEnumerator BoxCollToggle()
    {
        boxCollider.enabled = false;
        yield return new WaitForSeconds(2.7f);
        boxCollider.enabled = true;
    }
}
