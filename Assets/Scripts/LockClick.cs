using System.Collections.Generic;
using UnityEngine;

public class LockClick : MonoBehaviour
{
    [SerializeField] List<Material> mats;
    private Renderer renderer;
    private int currentIndex = 0;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        if (mats.Count > 0)
        {
            renderer.material = mats[currentIndex];
        }
    }

    private void OnMouseDown()
    {
        ChangeColor();
    }

    private void ChangeColor()
    {
        if (mats.Count == 0) return;

        currentIndex = (currentIndex + 1) % mats.Count;
        renderer.material = mats[currentIndex];
    }
}