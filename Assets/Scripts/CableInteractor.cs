using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableInteractor : MonoBehaviour, Interactor
{
    [SerializeField] GameObject objectToInteract;
    public Animation Anim1;
    public Animation Anim2;

    public void Interact()
    {
        objectToInteract.SetActive(false);
    }
}
