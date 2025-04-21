using System;
using System.Collections;
using UnityEngine;

public class Torch : MonoBehaviour, Interactor
{
    public Action<Torch, int> TorchAction;
    public bool IsUpsideDown => isUpsideDown;

    public int index;
    Animator anim;
    bool isUpsideDown;
    Outline outline;
    MeshCollider coll;

    private void Awake()
    {
        anim = transform.parent.GetComponent<Animator>();
        outline = GetComponent<Outline>();
        coll = GetComponent<MeshCollider>();
    }

    private void Start()
    {
        outline.enabled = false;
    }

    void EnableOutline()
    {
        outline.enabled = true;
    }

    public void Aiming()
    {
        EnableOutline();

        UIManager.Instance.ChangeCursor(true);
    }

    public void DisableOutline()
    {
        outline.enabled = false;

        UIManager.Instance.ChangeCursor(false);
    }

    public void Interact()
    {
        DisableOutline();
        coll.enabled = false;
        isUpsideDown = !isUpsideDown;
        SendAction();

        float animDuration = anim.runtimeAnimatorController.animationClips[0].length;

        if (isUpsideDown)
        {
            anim.Play("Interact", 0, 0f);
            StartCoroutine(WaitForAnimationEnd(animDuration));
        }
        else
        {
            anim.Play("Interact_Reverse", 0, 0f);
            StartCoroutine(WaitForAnimationEnd(animDuration));
        }
    }

    void SendAction()
    {
        TorchAction?.Invoke(this, index);
    }

    private IEnumerator WaitForAnimationEnd(float duration)
    {
        yield return new WaitForSeconds(duration);
        coll.enabled = true;
    }
}