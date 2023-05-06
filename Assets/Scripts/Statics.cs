using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Statics
{
    public static int CurrentWaypoint = 1; // Текущый свободный waypoint. Начинается с 1, потому что GetComponentsInChildren<Transform>() начинается с родителя.
}
