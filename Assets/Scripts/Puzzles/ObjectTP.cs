using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTP : MonoBehaviour, ITeleportable
{
    private bool onStation = true;

    [SerializeField] Transform spawnPoint;

    [SerializeField] List<ColorSpotChecker> checkerList = new List<ColorSpotChecker>();

    public int actualSpot;

    public TPColours color;

    private BoxCollider objBC;
    private Rigidbody objRB;

    private void Start()
    {
        objBC = GetComponent<BoxCollider>();
        objRB = GetComponent<Rigidbody>();
    }

    public void Interact()
    {
        if (onStation)
        {
            var moved = PutCubeToSpot(this.gameObject);

            if (moved)
            {
                onStation = !onStation;
            }
        }
        else
        {
            transform.position = spawnPoint.position;
            onStation = !onStation;
            checkerList.Find(x => x.spot == actualSpot).CanReceiveBoolChange();
            TPManager.Instance.colorList.RemoveAt(actualSpot); 
            actualSpot = 0;
            TPManager.Instance.spotCounter--;
        }
    }

    public bool PutCubeToSpot(GameObject cube)
    {
        foreach (var spot in checkerList)
        {
            if (spot.canReceive)
            {
                spot.TeleportCube(cube, objBC, objRB);
                return true;
            }

            if (spot.spot == 2 && !TPManager.Instance.stageOneDone)
            {
                return false;
            }
            else if (spot.spot == 4 && !TPManager.Instance.stageTwoDone)
            {
                return false;
            }
            else if (spot.spot == 6 && !TPManager.Instance.stageThreeDone)
            {
                return false;
            }
        }

        return default;
    }
}