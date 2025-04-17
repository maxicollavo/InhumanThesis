using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MovePuzzleManager : MonoBehaviour
{
    [Header("Timeline")]
    [SerializeField] private PlayableDirector winTimeline;

    [HideInInspector]
    public Waypoints wp { get; set; }

    public List<BoxCollider> allColliders;
    public List<BoxCollider> buttonsColliders;
    [HideInInspector]
    public List<bool> OnTargetList;

    [HideInInspector]
    public TouchFigure selectedFigure;
    public Transform cinematicCam;
    public Transform playerCam;

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
            Win();
        }
    }

    void Win()
    {
        Debug.Log("Win");
        foreach (var coll in allColliders)
        {
            Destroy(coll);
        }

        cinematicCam.gameObject.SetActive(true);
        cinematicCam.transform.position = playerCam.transform.position;
        cinematicCam.transform.rotation = playerCam.transform.rotation;

        EventManager.Instance.Dispatch(GameEventTypes.OnCinematic, this, EventArgs.Empty);

        winTimeline.Play();
        StartCoroutine(WaitForTimeline());
    }

    IEnumerator WaitForTimeline()
    {
        yield return new WaitUntil(() => winTimeline.state != PlayState.Playing);
        EventManager.Instance.Dispatch(GameEventTypes.OnGameplay, this, EventArgs.Empty);
    }
}