using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailPuzzle : MonoBehaviour
{
    public List<Transform> posInRail = new List<Transform>();

    public int posCounter;

    public void GoForward()
    {
        if (posCounter == 4) return;

        posCounter++;

        var newPos = posInRail[posCounter].position;

        transform.position = newPos;
    }

    public void GoBackwards()
    {
        if (posCounter == 0) return;

        posCounter--;

        var newPos = posInRail[posCounter].position;

        transform.position = newPos;
    }
}
