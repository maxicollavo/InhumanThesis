using System.Collections.Generic;
using UnityEngine;

public class ObjectTP : MonoBehaviour, ITeleportable
{
    private bool onStation;

    [SerializeField] Transform spawnPoint;

    [SerializeField] ColorSpotChecker spotChecker;
    [SerializeField] List<ColorSpotChecker> checkerList = new List<ColorSpotChecker>();

    public int actualSpot;

    public TPColours color;

    public void Interact()
    {
        onStation = !onStation;

        if (onStation)
        {
            spotChecker.Check(this.gameObject);
        }
        else
        {
            transform.position = spawnPoint.position;
            checkerList[actualSpot].CanReceiveBoolChange();
            actualSpot = 0;
        }
    }
}
