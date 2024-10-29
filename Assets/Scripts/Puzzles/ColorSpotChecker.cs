using UnityEngine;

public class ColorSpotChecker : MonoBehaviour
{
    public bool canReceive = true;
    public bool isLast;
    public int spot;
    public Transform stationPos;

    public void TeleportCube(GameObject cube, BoxCollider tp, Rigidbody rb)
    {
        canReceive = false;
        cube.transform.position = stationPos.position;
        var objectTP = cube.GetComponent<ObjectTP>();
        objectTP.actualSpot = spot;
        TPManager.Instance.colorList.Add(objectTP.color);
        TPManager.Instance.spotCounter++;
        TPManager.Instance.ColorChecker(tp, rb);
    }

    public void CanReceiveBoolChange()
    {
        canReceive = true;
    }
}
