﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    public Color rayColor;
    public float sphereSize = 0.1f;
    public List<Transform> path_objs = new List<Transform>();
    Transform[] theArray;


    void OnDrawGizmos()
    {
        Gizmos.color = rayColor;
        theArray = GetComponentsInChildren<Transform>();
        path_objs.Clear();

        foreach (Transform path_obj in theArray)
        {
            if(path_obj != this.transform)
            {
                path_objs.Add(path_obj);
            }
        }

        for (int i = 0; i < path_objs.Count; i++)
        {
            Vector3 position = path_objs[i].position;
            if(i > 0)
            {
                Vector3 previous = path_objs[i - 1].position;
                Gizmos.DrawLine(previous, position);
                Gizmos.DrawWireSphere(position, sphereSize);
            }
        }
    }
}
