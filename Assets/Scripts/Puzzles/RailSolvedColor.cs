using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailSolvedColor : MonoBehaviour
{
    public Material newColor;

    public void Interactor()
    {
        Renderer objectRenderer = GetComponent<Renderer>();
        objectRenderer.material = newColor;
    }
}