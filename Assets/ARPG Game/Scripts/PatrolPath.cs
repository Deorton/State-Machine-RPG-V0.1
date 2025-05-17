using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPath : MonoBehaviour
{
    [SerializeField] private float waypointRadius = 0.3f;
    void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.DrawSphere(GetWaypoint(i), waypointRadius);
            Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(GetNextIndex(i)));

        }
    }

    public Vector3 GetWaypoint(int i)
    {
        return transform.GetChild(i).position;
    }
    
    public int GetNextIndex(int i)
    {
        if (i + 1 >= transform.childCount)
        {
            return 0;
        }
        else
        {
            return i + 1;
        }
    }
}
