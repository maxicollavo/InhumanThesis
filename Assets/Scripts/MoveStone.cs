using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStone : MonoBehaviour
{
    public Transform stonePos;

    void Start()
    {
        transform.position = stonePos.position - new Vector3(0, 5.5f, 0f);
    }
}
