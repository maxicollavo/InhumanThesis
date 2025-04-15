using System.Collections.Generic;
using UnityEngine;

public class MovePuzzleManager : MonoBehaviour
{
    [HideInInspector]
    public Waypoints wp { get; set; }

    public List<BoxCollider> buttonsColliders;
    public List<bool> OnTargetList;

    public TouchFigure selectedFigure;

    public static MovePuzzleManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        selectedFigure = null;

        SetButtonsEnabled(false);

        OnTargetList = new List<bool>(new bool[4]);
    }

    public void GetFigure(TouchFigure target, Waypoints wayp)
    {
        selectedFigure = target;
        wp = wayp;
    }

    public void SetButtonsEnabled(bool state)
    {
        foreach (var coll in buttonsColliders)
            coll.enabled = state;
    }

    public void CheckPosition(Waypoints wp)
    {
        int wpIndex = wp.index;

        if (wp.IsTarget && selectedFigure.index == wpIndex)
        {
            selectedFigure.OnPositionWinner = true;
            OnTargetList[wpIndex - 1] = true;
        }
        else
        {
            selectedFigure.OnPositionWinner = false;

            if (wpIndex - 1 >= 0 && wpIndex - 1 < OnTargetList.Count)
            {
                OnTargetList[wpIndex - 1] = false;
            }

        }

        CheckWin();
    }


    void CheckWin()
    {
        bool allPositionsWon = true;
        foreach (bool position in OnTargetList)
        {
            if (!position)
            {
                allPositionsWon = false;
                break;
            }
        }

        if (allPositionsWon)
        {
            Debug.Log("¡Has ganado!");
        }
    }
}