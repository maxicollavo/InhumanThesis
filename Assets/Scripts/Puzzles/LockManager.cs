using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockManager : MonoBehaviour
{
    [HideInInspector] public List<bool> LockDone = new List<bool>();
    [HideInInspector] public bool HasWon;

    private int currentStep = 0;

    [SerializeField]
    private int maxLocks;

    [SerializeField]
    private GameObject lockWall;
    [SerializeField]
    private GameObject interactUI;
    [SerializeField]
    private GameObject interactTrigger;

    [SerializeField] AnimacionesPyramid pyramid;

    public static LockManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializeLocks(maxLocks);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void InitializeLocks(int count)
    {
        LockDone = new List<bool>();

        for (int i = 0; i < count; i++)
        {
            LockDone.Add(false);
        }
    }

    public void CheckLock()
    {
        if (HasWon) return;

        int newStep = 0;

        for (int i = 0; i < LockDone.Count; i++)
        {
            if (LockDone[i])
            {
                newStep++;
            }
            else
            {
                break;
            }
        }

        if (newStep != currentStep)
        {
            currentStep = newStep;

            if (currentStep == LockDone.Count)
            {
                StartCoroutine(Win());
            }
        }
    }

    private IEnumerator Win()
    {
        EventManager.Instance.Dispatch(GameEventTypes.OnGameplay, this, EventArgs.Empty);

        HasWon = true;
        lockWall.SetActive(false);

        Destroy(interactUI);
        Destroy(interactTrigger);

        yield return new WaitForSeconds(0.1f);
        pyramid.RestartAnim();
    }
}
