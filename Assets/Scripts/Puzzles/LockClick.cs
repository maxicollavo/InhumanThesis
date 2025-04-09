using System.Collections.Generic;
using UnityEngine;

public class LockClick : MonoBehaviour
{
    [SerializeField] List<Material> mats;
    private Renderer myRenderer;
    private int currentIndex = 0;

    private LockSystem lockSystem;

    private void Awake()
    {
        myRenderer = GetComponent<Renderer>();
        if (mats.Count > 0)
        {
            myRenderer.material = mats[currentIndex];
        }

        lockSystem = GetComponent<LockSystem>();
    }

    private void OnMouseDown()
    {
        ChangeColor();
        UpdateLockStatus();
    }

    private void UpdateLockStatus()
    {
        lockSystem.SendLock();
    }

    private void ChangeColor()
    {
        if (mats.Count == 0) return;

        currentIndex = (currentIndex + 1) % mats.Count;
        myRenderer.material = mats[currentIndex];
    }
}