using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public Dictionary<int, Waypoints> neighbors = new Dictionary<int, Waypoints>();

    public Waypoints waypointUp;
    public Waypoints waypointDown;
    public Waypoints waypointLeft;
    public Waypoints waypointRight;

    public bool IsUsing = false;
    public bool IsTarget;
    public int index;

    void Awake()
    {
        if (waypointUp != null) neighbors[1] = waypointUp;
        if (waypointDown != null) neighbors[2] = waypointDown;
        if (waypointLeft != null) neighbors[4] = waypointLeft;
        if (waypointRight != null) neighbors[5] = waypointRight;
    }
}