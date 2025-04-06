using System.Collections.Generic;
using UnityEngine;

public class LockManager : MonoBehaviour
{
    [HideInInspector] public List<bool> LockDone = new List<bool>();
    [HideInInspector] public bool HasWon;

    private int currentStep = 0;

    [SerializeField]
    private int maxLocks;

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
            Debug.Log($"Paso actualizado. Counter: {currentStep}");

            if (currentStep == LockDone.Count)
            {
                Debug.Log("Se abre la puerta");
                HasWon = true;
            }
        }
    }

}
