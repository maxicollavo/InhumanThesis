using UnityEngine;

public class ColorSpotChecker : MonoBehaviour
{
    [HideInInspector]
    public bool canReceive = true;
    public bool isLast;
    public int spot;
    public Transform stationPos;
    public Transform nextStationPos;

    public void Check(GameObject cube)
    {
        if (canReceive)
        {
            canReceive = false;
            cube.transform.position = stationPos.position;
            var objectTP = cube.GetComponent<ObjectTP>();
            objectTP.actualSpot = spot;
            TPManager.Instance.colorList.Add(objectTP.color);
        }
        else
        {
            if (!isLast)
            {
                cube.transform.position = nextStationPos.position;
                var objectTP = cube.GetComponent<ObjectTP>();
                TPManager.Instance.colorList.Add(objectTP.color);
            }
        }

        TPManager.Instance.ColorChecker();
    }

    public void CanReceiveBoolChange()
    {
        canReceive = true;
    }
}
