using System.Collections.Generic;
using UnityEngine;

public class RailPuzzle : MonoBehaviour
{
    public List<Transform> posInRail = new List<Transform>();

    public int posCounter;

    [HideInInspector] public bool state;

    [SerializeField] int winPos;
    [SerializeField] int railNum;

    private Transform childTransform;

    [SerializeField] AudioSource statueSound;

    [SerializeField] RailManager manager;

    private void Awake()
    {
        childTransform = transform.GetChild(0);
        GameManager.Instance.rail.Add(state);
    }

    public void GoForward()
    {
        if (posCounter == 4)
        {
            //Sonido de NO SE PUEDE
            return;
        }

        statueSound.Play();
        posCounter++;

        var newPos = posInRail[posCounter].position;

        childTransform.position = newPos;

        CheckPos();
    }

    public void GoBackwards()
    {
        if (posCounter == 0) return;

        statueSound.Play();
        posCounter--;

        var newPos = posInRail[posCounter].position;

        childTransform.position = newPos;

        CheckPos();
    }

    private void CheckPos()
    {
        if (posCounter == winPos)
        {
            GameManager.Instance.rail[railNum] = true;
        }
        else
        {
            GameManager.Instance.rail[railNum] = false;
        }

        manager.CheckWin();
    }
}
