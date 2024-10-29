using System.Collections.Generic;
using UnityEngine;

public class TPManager : MonoBehaviour
{
    [SerializeField] Animator openDoor;
    [SerializeField] Animator openDoor2;

    [HideInInspector]
    public bool stageOneDone;
    [HideInInspector]
    public bool stageTwoDone;
    [HideInInspector]
    public bool stageThreeDone;

    public AudioSource winBell;

    public int spotCounter;

    public List<TPColours> colorList;
    public List<BoxCollider> objList;
    public List<Rigidbody> rbList;

    public static TPManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void StageCompleted(int stage, Rigidbody rb)
    {
        foreach (var obj in objList)
        {
            obj.enabled = false;
        }
        foreach (var rigid in rbList)
        {
            rigid.constraints = RigidbodyConstraints.FreezePosition;
        }

        if (stage == 1)
        {
            stageOneDone = true;
        }
        else if (stage == 2)
        {
            stageTwoDone = true;
        }
        else if (stage == 3)
        {
            stageThreeDone = true;
        }

        colorList.Clear();
        objList.Clear();
        rbList.Clear();
    }

    public void ColorChecker(BoxCollider obj, Rigidbody rb)
    {
        objList.Add(obj);
        rbList.Add(rb);

        if (spotCounter == 2)
        {
            if (!stageOneDone)
            {
                if (colorList[0] == TPColours.Yellow && colorList[1] == TPColours.Blue || colorList[0] == TPColours.Blue && colorList[1] == TPColours.Yellow)
                {
                    StageCompleted(1, rb);
                }
            }
            else if (!stageTwoDone)
            {
                if (colorList[0] == TPColours.Blue && colorList[1] == TPColours.Red || colorList[0] == TPColours.Red && colorList[1] == TPColours.Blue)
                {
                    StageCompleted(2, rb);
                }
            }
            else if (!stageThreeDone)
            {
                if (colorList[0] == TPColours.Yellow && colorList[1] == TPColours.Red || colorList[0] == TPColours.Red && colorList[1] == TPColours.Yellow)
                {
                    StageCompleted(3, rb);
                }
            }

            if (stageThreeDone)
            {
                OpenDoors();
            }
        }
    }

    void OpenDoors()
    {
        GameManager.Instance.CoroutinesStoper();
        GameManager.Instance.colorButton.enabled = true;
        winBell.Play();
        openDoor.SetBool("IsTrue", true);
        openDoor2.SetBool("IsTrue", true);
    }
}

public enum TPColours
{
    Red,
    Blue,
    Yellow
}
