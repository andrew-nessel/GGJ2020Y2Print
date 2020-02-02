using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkBaseMovement : MonoBehaviour
{
    public Transform waypoint1;
    public Transform waypoint2;
    public bool currentWaypoint;
    public float maxSpeed;
    public float minSpeed;
    public float currentSpeed;
    public Vector3 newPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(currentWaypoint){
            if(transform.position.z >= waypoint1.position.z){
                newPosition = transform.position;
                newPosition.z = transform.position.z - currentSpeed;
                transform.position = newPosition;
            }else{
                currentWaypoint = !currentWaypoint;
                currentSpeed = Random.Range(minSpeed, maxSpeed);
            }
        }else{
            if(transform.position.z <= waypoint2.position.z){
                newPosition = transform.position;
                newPosition.z = transform.position.z + currentSpeed;
                transform.position = newPosition;
            }else{
                currentWaypoint = !currentWaypoint;
                currentSpeed = Random.Range(minSpeed, maxSpeed);
            }
        }

    }
}
