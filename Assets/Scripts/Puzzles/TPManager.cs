using System.Collections.Generic;
using UnityEngine;

public class TPManager : MonoBehaviour
{
    [SerializeField] Animator openDoor;
    [SerializeField] Animator openDoor2;

    public List<TPColours> coloursList;

    public List<TPColours> redList;
    public List<TPColours> blueList;
    public List<TPColours> greenList;

    public static TPManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        coloursList = new List<TPColours>();

        redList = new List<TPColours>();
        blueList = new List<TPColours>();
        greenList = new List<TPColours>();
    }

    private void Start()
    {
        Debug.Log(coloursList.Count);
    }

    public void CheckForColours(bool onTeleport, TPColours colour)
    {
        CheckColoursList(onTeleport, colour);

        var redAmount = redList.Count;
        var blueAmount = blueList.Count;
        var greenAmount = greenList.Count;

        if (redAmount == 3 && blueAmount == 2 && greenAmount == 3)
            OpenDoors();
    }

    void CheckColoursList(bool onTeleport, TPColours colour)
    {
        if (onTeleport)
        {
            if (colour == TPColours.Red)
            {
                redList.Add(colour);
            }
            else if (colour == TPColours.Green)
            {
                greenList.Add(colour);
            }
            else if (colour == TPColours.Blue)
            {
                blueList.Add(colour);
            }
        }
        else
        {
            if (colour == TPColours.Red)
            {
                redList.Remove(colour);
            }
            else if (colour == TPColours.Green)
            {
                greenList.Remove(colour);
            }
            else if (colour == TPColours.Blue)
            {
                blueList.Remove(colour);
            }
        }
    }

    void OpenDoors()
    {
        GameManager.Instance.CoroutinesStoper();

        openDoor.SetBool("IsTrue", true);
        openDoor2.SetBool("IsTrue", true);
    }
}

public enum TPColours
{
    Red,
    Blue,
    Green
}
