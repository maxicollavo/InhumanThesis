using System;
using UnityEngine;

public class TrailManager : MonoBehaviour
{
    [SerializeField] PatternTracker tracker;
    [SerializeField] ParticleSystem particle;
    [SerializeField] BoxCollider coll;
    [SerializeField] Camera playerCam;
    [SerializeField] Camera cam;

    [Header("On Win")]
    [SerializeField] GameObject openBox;

    public bool OnJeroglific { get; set; }

    private void Start()
    {
        cam.enabled = false;
    }

    void Update()
    {
        if (!OnJeroglific) return;

        if (tracker.isTracking)
        {
            if (!particle.isPlaying)
                ParticleTracking(true);
        }
        else
        {
            if (particle.isPlaying)
            {
                ParticleTracking(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            ReactivateGameplay(false);
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

    public void EnterToJeroglific()
    {
        cam.enabled = true;
        coll.enabled = false;
        OnJeroglific = true;

        EventManager.Instance.Dispatch(GameEventTypes.OnPuzzle, this, EventArgs.Empty);
    }

    public void ReactivateGameplay(bool HasWon)
    {
        if (HasWon)
        {
            Debug.Log("Gano");
            openBox.SetActive(false);
            Destroy(coll.gameObject);
        }
        else
        {
            coll.enabled = true;
        }
        ParticleTracking(false);
        cam.enabled = false;
        OnJeroglific = false;
        EventManager.Instance.Dispatch(GameEventTypes.OnGameplay, this, EventArgs.Empty);
    }
}
