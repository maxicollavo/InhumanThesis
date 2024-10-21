using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailInteractor : MonoBehaviour, Interactor
{
    [SerializeField] bool isForward;

    [SerializeField] RailPuzzle railPuzzle;

    public void Interact()
    {
        if (isForward)
        {
            railPuzzle.GoForward();
        }
        else
        {
            railPuzzle.GoBackwards();
        }
    }
}
