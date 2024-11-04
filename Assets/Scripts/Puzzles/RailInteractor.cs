using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailInteractor : MonoBehaviour, Interactor
{
    [SerializeField] bool isForward;

    [SerializeField] RailPuzzle statue;

    public void Interact()
    {
        if (isForward)
        {
            statue.GoForward();
        }
        else
        {
            statue.GoBackwards();
        }
    }
}
