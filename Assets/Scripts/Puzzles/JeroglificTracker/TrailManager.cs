using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrailManager : MonoBehaviour
{
    [Header("Start Settings")]
    public bool CanStart;
    [SerializeField] StatueManager statueManager;
    [SerializeField] List<GameObject> nodesToEnable;

    [Header("Nodes")]
    [SerializeField] List<GameObject> actionNodes; //Referencia a su action
    [SerializeField] List<GameObject> validNodes; //Los nodos que tengo que tocar
    private List<GameObject> currentPath = new List<GameObject>(); //Los nodos que toco
    [SerializeField] private LayerMask nodeLayerMask;

    [Header("Particle")]
    [SerializeField] ParticleSystem particle;

    [Header("Tracker")]
    public bool isTracking { get; private set; }
    [SerializeField] GameObject tracker;

    [Header("Settings")]
    [SerializeField] Camera puzzleCam;
    [SerializeField] PuzzleInteractor interactor;
    private bool previousState = false;
    public bool OnPuzzle { get; private set; }
    [SerializeField] BoxCollider interactorCollider;

    [Header("On Win")]
    public Action<TrailManager> JeroglificAction;
    private bool HasWon;

    private void Start()
    {
        interactor.PuzzleAction += OnPuzzleMethod;
        statueManager.StatueManagerAction += OnStatueFinish;
        puzzleCam.enabled = false;
    }

    private void OnStatueFinish(StatueManager manager)
    {
        CanStart = true;

        ActivateNodes();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            BackToGameplay();
        }

        if (HasWon || !CanStart) return;

        bool currentState = OnPuzzle;

        if (currentState != previousState)
        {
            tracker.SetActive(currentState);
            previousState = currentState;
        }

        if (!currentState) return;
        Debug.Log(currentState);

        if (Input.GetMouseButtonDown(0))
        {
            currentPath.Clear();
            isTracking = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isTracking = false;
            RestartTracking();
        }

        if (isTracking)
        {
            TrackMouse();
        }

        ParticleTracking(isTracking);
    }

    void ActivateNodes()
    {
        foreach (var n in nodesToEnable)
        {
            n.SetActive(true);
        }
    }

    void OnPuzzleMethod(PuzzleInteractor interactor)
    {
        interactor.DisableOutline();
        puzzleCam.enabled = true;
        interactorCollider.enabled = false;
        OnPuzzle = true;
        EventManager.Instance.Dispatch(GameEventTypes.OnPuzzle, this, EventArgs.Empty);
    }

    public void BackToGameplay()
    {
        puzzleCam.enabled = false;
        OnPuzzle = false;
        EventManager.Instance.Dispatch(GameEventTypes.OnGameplay, this, EventArgs.Empty);
        if (HasWon) return;

        interactorCollider.enabled = true;
    }

    void TrackMouse()
    {
        Ray ray = puzzleCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, nodeLayerMask))
        {
            GameObject hitObj = hit.collider.gameObject;

            CheckIfValid(hitObj);
        }
        else
        {
            RestartTracking();
        }
    }

    void RestartTracking()
    {
        isTracking = false;
        currentPath.Clear();
        Debug.Log(currentPath.Count);
    }

    void CheckIfValid(GameObject node)
    {
        if (validNodes.Contains(node) && !currentPath.Contains(node))
        {
            Debug.Log("Agrega nodo");
            AddNode(node);
            CheckWin();
        }
        {
            Debug.Log(currentPath.Count);
        }
    }

    void CheckWin()
    {
        if (currentPath.Count == validNodes.Count)
        {
            isTracking = false;
            Win();
        }
    }

    void Win()
    {
        JeroglificAction?.Invoke(this);
        HasWon = true;
        BackToGameplay();
    }

    void AddNode(GameObject node)
    {
        currentPath.Add(node);
    }

    private void ParticleTracking(bool isTracking)
    {
        if (isTracking)
        {
            if (!particle.isPlaying)
            {
                particle.Play();
            }
        }
        else
        {
            if (particle.isPlaying)
            {
                particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            }
        }
    }
}