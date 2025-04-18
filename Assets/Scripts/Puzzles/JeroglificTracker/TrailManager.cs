using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrailManager : MonoBehaviour
{
    [Header("Nodes")]
    public Node[] nodes;
    public List<Node> validNodes;

    [Header("Particle")]
    [SerializeField] ParticleSystem particle;

    [Header("Tracker")]
    private List<Node> currentPath = new List<Node>();
    public bool isTracking { get; private set; }

    [Header("Settings")]
    private bool OnJeroglific;
    [SerializeField] Camera trackCam;
    [SerializeField] Camera playerCam;

    private void Start()
    {
        foreach (var n in nodes)
        {
            n.OnNodeTouched += GetNodeTouched;
        }
    }

    void GetNodeTouched(Node node)
    {
        CheckIfValid(node);
    }

    void CheckIfValid(Node node)
    {
        if (validNodes.Contains(node) && !currentPath.Contains(node))
        {
            currentPath.Add(node);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentPath.Clear();
            isTracking = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isTracking = false;
            CheckPattern();
        }

        if (isTracking)
        {
            TrackMouse();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            //ReactivateGameplay(false);
        }
    }

    void CheckPattern()
    {
        if (currentPath.Count == validNodes.Count)
        {
            isTracking = false;
            Debug.Log("Gane");
        }
        else
        {
            Debug.Log("Patrón incompleto.");
        }
    }

    void TrackMouse()
    {
        Ray ray = trackCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            Node hitNode = hit.collider.GetComponent<Node>();

            if (validNodes.Contains(hitNode))
            {
                if (!currentPath.Contains(hitNode))
                {
                    currentPath.Add(hitNode);
                    Debug.Log($"Nodo válido agregado: {hitNode.name}");

                    if (currentPath.Count == validNodes.Count)
                    {
                        isTracking = false;
                        CheckPattern();
                    }
                }
            }
            else
            {
                Debug.Log($"Nodo inválido tocado: {hitNode.name}. Reiniciando patrón.");
                currentPath.Clear();
                isTracking = false;
            }
        }
    }

    private void ParticleTracking(bool IsTracking)
    {
        if (IsTracking)
            particle.Play();
        else
        {
            particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            particle.Clear();
        }
    }

    //public void EnterToJeroglific()
    //{
    //    cam.enabled = true;
    //    coll.enabled = false;
    //    OnJeroglific = true;

    //    EventManager.Instance.Dispatch(GameEventTypes.OnPuzzle, this, EventArgs.Empty);
    //}

    //public void ReactivateGameplay(bool HasWon)
    //{
    //    if (HasWon)
    //    {
    //        Debug.Log("Gano");
    //    }
    //    else
    //    {
    //    }
    //    ParticleTracking(false);
    //    cam.enabled = false;
    //    OnJeroglific = false;
    //    EventManager.Instance.Dispatch(GameEventTypes.OnGameplay, this, EventArgs.Empty);
    //}
}
