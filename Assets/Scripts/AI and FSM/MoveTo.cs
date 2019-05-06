using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//simple waypoint movement
public class MoveTo : MonoBehaviour
{
    //place to set target destination
    public Transform target;

    void Start()
    {
        // gets nav mesh component 
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        //moves agent to target position
        agent.destination = target.position;
    }

}
