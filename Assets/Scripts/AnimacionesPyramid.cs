using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionesPyramid : MonoBehaviour
{
    [SerializeField] ParticleSystem chispas;
    [SerializeField] GameObject top;

    void Close()
    {
        Instantiate(chispas, top.transform);
    }
}
