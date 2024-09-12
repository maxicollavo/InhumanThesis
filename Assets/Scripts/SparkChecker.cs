using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkChecker : MonoBehaviour
{
    [SerializeField] CableInteractor ci;

    public void Checker()
    {
        ci.AnimFinish();
    }
}
