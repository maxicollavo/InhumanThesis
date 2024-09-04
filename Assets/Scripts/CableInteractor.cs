using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableInteractor : MonoBehaviour, Interactor
{
    [SerializeField] GameObject objectToInteract;

    public void Interact()
    {
        objectToInteract.SetActive(false);
    }
}
