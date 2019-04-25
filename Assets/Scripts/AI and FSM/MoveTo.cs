using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
