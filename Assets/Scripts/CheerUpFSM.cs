using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheerUpFSM : FSM
{
    public enum FSMState
    {
        None,
        Patrol,
        Move,
        Interact,
        Sleep,
    }

    public FSMState curState;

    // We overwrite the deprecated built-in `rigidbody` variable.
    new private Rigidbody rigidbody;

    protected override void Initialize()
    {
        curState = FSMState.Patrol;
        elapsedTime = 0.0f;

        //Get the list of points
        pointList = GameObject.FindGameObjectsWithTag("WandarPoint");

        //Get the target enemy(Player)
        GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
        playerTransform = objPlayer.transform;

        // Get the rigidbody
        rigidbody = GetComponent<Rigidbody>();

        if (!playerTransform)
            print("Player doesn't exist.. Please add one with Tag named 'Player'");

    }
    protected override void FSMUpdate()
    {
        switch (curState)
        {
            case FSMState.Patrol: UpdatePatrolState(); break;
            case FSMState.Move: UpdateMoveState(); break;
            case FSMState.Interact: UpdateInteractState(); break;
            case FSMState.Sleep: UpdateSleepState(); break;
        }

        //Update the time
        elapsedTime += Time.deltaTime;

    }
    public void UpdatePatrolState()
    {

    }
    protected void UpdateMoveState()
    {
        // if the A.I. reaches the destPos, interact
        if (Vector3.Distance(transform.position, destPos) == 0.0f)
        {
            UpdateInteractState();
        }
        // if player touches sensor
        else if (playerTransform.position)
        {
            print("Switch to Move Position");
            curState = FSMState.Move;
        }
        else if ()
        {

        }
    }
    public void UpdateInteractState()
    {

    }
    public void UpdateSleepState()
    {

    }
}
}
