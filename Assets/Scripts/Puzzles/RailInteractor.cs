using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailInteractor : MonoBehaviour, Interactor
{
    [SerializeField] bool isForward;

    [SerializeField] RailPuzzle statue;
    [SerializeField] AudioSource statueSound;

   

    public void Interact()
    {
        if (isForward)
        {
            statue.GoForward();
            statueSound.Play();
        }
        else
        {
            statue.GoBackwards();
            statueSound.Play();
        }
    }
}
