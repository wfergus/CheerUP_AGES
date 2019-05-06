using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheerUpFSM : FSM
{   
    public enum FSMState
    {
        Patrol,
        Walk,
        Flee
    }

    public FSMState curState;

    //this interaction timer needs to be a property in the waypoint objects to let the a.i. know how long to stay before changing state
    public float interactTimer;

    // We overwrite the deprecated built-in `rigidbody` variable.
    new public Rigidbody rigidbody;

    public float curSpeed;

    public override void Initialize()
    {
        curState = FSMState.Patrol;
        elapsedTime = 0.0f;
        curSpeed = 10f;
        

        //Get the list of points
        pointList = GameObject.FindGameObjectsWithTag("Waypoint");

        //Get the target enemy(Player)
        GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
        playerTransform = objPlayer.transform;

        GameObject agent = GameObject.FindGameObjectWithTag("Agent");


        if (!playerTransform)
            print("Player doesn't exist.. Please add one with Tag named 'Player'");
    }

    public override void FSMUpdate()
    {
        switch (curState)
        {
            case FSMState.Patrol: UpdatePatrolState(); break;
            case FSMState.Walk: UpdateWalkState(); break;
            case FSMState.Flee: UpdateFleeState(); break;
        }
        //Update the time
        elapsedTime += Time.deltaTime;
    }

    public void UpdatePatrolState()
    {
        curSpeed = 5f;
        curState = FSMState.Patrol;

        Debug.Log("Switch to Patrol State");
    }

    public void UpdateWalkState()
    {
        curSpeed = 5f;
        curState = FSMState.Walk;
            
        Debug.Log("Switch to Walk State");
    }

    public void UpdateFleeState()
    {
        curSpeed = 10f;
        curState = FSMState.Flee;
        elapsedTime = 0.0f;

        Debug.Log("Switch to Flee State");

        if (elapsedTime >= 5)
            UpdatePatrolState();
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UpdateFleeState();
        }
    }
}

