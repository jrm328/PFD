using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainController : MonoBehaviour
{
    public bool canGo = true;
    public Route route;
    public float speed = 8f;
    public float rotationSpeed = 3f;
    public float reachDistance = 1f;

    public int number;
    private int currentPointID;
    private int currentLookPointID;

    void Start()
    {
        currentPointID = number;
        currentLookPointID = route.path_objs.Count - 2;

        transform.position = route.path_objs[currentPointID].position;
    }

    void FixedUpdate()
    {
        if (canGo)
        {
            float distance = Vector3.Distance(route.path_objs[currentPointID].position, transform.position);
            transform.position = Vector3.MoveTowards(transform.position, route.path_objs[currentPointID].position, Time.deltaTime * speed);

            //float distanceLook = Vector3.Distance(route.path_objs[currentPointID].position, lookTransform.position);
            //lookTransform.position = Vector3.MoveTowards(lookTransform.position, route.path_objs[currentLookPointID].position, Time.deltaTime * speed);

            //var rotation = Quaternion.LookRotation(route.path_objs[currentLookPointID].position - lookTransform.position);
            //lookTransform.rotation = Quaternion.Slerp(lookTransform.rotation, rotation, Time.deltaTime * rotationSpeed);

            var rotation = Quaternion.LookRotation(route.path_objs[currentPointID].position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

            if (distance <= reachDistance)
            {
                currentPointID++;
                currentLookPointID = currentPointID - 2;

                if (currentLookPointID < 0)
                {
                    currentLookPointID = route.path_objs.Count - 2;
                }
            }

            if (currentPointID >= route.path_objs.Count)
            {
                currentPointID = 0;
            }



        }
        //else
        //{
        //    float distance = Vector3.Distance(linePlacePos, transform.position);
        //    transform.position = Vector3.MoveTowards(transform.position, linePlacePos, Time.deltaTime * transitionSpeedBase * transitionSpeed);

        //    var rotation = Quaternion.LookRotation(linePlacePos - transform.position);
        //    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeedBase * rotationSpeed);
        //}

    }
}
