using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObject : MonoBehaviour, Interactor
{
    [SerializeField] Animator anim;

    public void Interact()
    {
        anim.SetTrigger("Interact");
    }
}