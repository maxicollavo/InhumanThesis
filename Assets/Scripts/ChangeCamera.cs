using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour, Interactor
{
    [SerializeField] List<GameObject> mainCams;
    [SerializeField] GameObject changeCam;

    //[SerializeField] GameObject bodyLight;
    [SerializeField] GameObject crosshair;

    private bool OnInteractor;

    private void Start()
    {
        EventManager.Instance.Register(GameEventTypes.OnCinematic, LockEnabled);
        EventManager.Instance.Register(GameEventTypes.OnGameplay, LockDisabled);
    }

    private void OnDestroy()
    {
        EventManager.Instance.Unregister(GameEventTypes.OnCinematic, LockEnabled);
        EventManager.Instance.Unregister(GameEventTypes.OnGameplay, LockDisabled);
    }

    void LockEnabled(object sender, EventArgs e)
    {
        Debug.Log("LockEnabled");
        GameManager.Instance.canMove = false;

        foreach (var cam in mainCams)
        {
            cam.SetActive(false);
        }

        changeCam.SetActive(true);
        //bodyLight.SetActive(true);
        crosshair.SetActive(false);
    }

    void LockDisabled(object sender, EventArgs e)
    {
        Debug.Log("LockDisabled");
        GameManager.Instance.canMove = true;

        foreach (var cam in mainCams)
        {
            cam.SetActive(true);
        }

        changeCam.SetActive(false);
        //bodyLight.SetActive(false);
        crosshair.SetActive(true);
    }

    public void Interact()
    {
        OnInteractor = !OnInteractor;

        if (OnInteractor)
        {
            EventManager.Instance.Dispatch(GameEventTypes.OnCinematic, this, EventArgs.Empty);
        }
        else
        {
            EventManager.Instance.Dispatch(GameEventTypes.OnGameplay, this, EventArgs.Empty);
        }
    }
}