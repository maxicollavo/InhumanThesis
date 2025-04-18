using System;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Action<Node> OnNodeTouched;
    [HideInInspector]
    public bool IsTouched;

    public void NodeTouched()
    {
        OnNodeTouched?.Invoke(this);
    }
}
