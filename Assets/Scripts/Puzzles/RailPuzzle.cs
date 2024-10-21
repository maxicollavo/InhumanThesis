using System.Collections.Generic;
using UnityEngine;

public class RailPuzzle : MonoBehaviour
{
    public List<Transform> posInRail = new List<Transform>();

    public int posCounter;

    [HideInInspector] public bool state;

    [SerializeField] int winPos;
    [SerializeField] int railNum;

    [SerializeField] RailManager manager;

    private void Awake()
    {
        GameManager.Instance.rail.Add(state);
    }

    public void GoForward()
    {
        if (posCounter == 4) return;

        posCounter++;

        var newPos = posInRail[posCounter].position;

        transform.position = newPos;

        CheckPos();
    }

    public void GoBackwards()
    {
        if (posCounter == 0) return;

        posCounter--;

        var newPos = posInRail[posCounter].position;

        transform.position = newPos;

        CheckPos();
    }
    private void CheckPos()
    {
        if (posCounter == winPos)
            GameManager.Instance.rail[railNum] = true;
        else
            GameManager.Instance.rail[railNum] = false;

        manager.CheckWin();
    }
}
