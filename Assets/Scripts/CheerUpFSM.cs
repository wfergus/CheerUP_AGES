using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheerUpFSM : FSM
{
    /// <summary>
    /// enum for checkpoints? or boolians? if player character hits a chieckpoint should it flip a bool
    /// or should it change state. a bool would require multiple if statements and checks in order to 
    /// decide where the character is sthrough progression. a state could send them straight to a common
    /// series of actions due to progression.
    /// OR
    /// on collision events? collision areas that only apear based on previous collions. 
    /// </summary>
   
    public enum FSMState
    {
        None,
        Patrol,
        Move,
        Interact,
        Sleep,
    }

    public FSMState curState;

    //this interaction timer needs to be a property in the waypoint objects to let the a.i. know how long to stay before changing state
    public float interactTimer;

    // We overwrite the deprecated built-in `rigidbody` variable.
    new public Rigidbody rigidbody;

    public override void Initialize()
    {
        curState = FSMState.Patrol;
        elapsedTime = 0.0f;
        interactTimer = 0.0f;

        //Get the list of points
        pointList = GameObject.FindGameObjectsWithTag("Waypoint");

        //Get the target enemy(Player)
        GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
        playerTransform = objPlayer.transform;

        GameObject objAI = GameObject.FindGameObjectWithTag("ai");

        // Get the rigidbody
        rigidbody = GetComponent<Rigidbody>();

        if (!playerTransform)
            print("Player doesn't exist.. Please add one with Tag named 'Player'");

    }
    public override void FSMUpdate()
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
        curState = FSMState.Patrol;
    }
    public void UpdateMoveState()
    {
        curState = FSMState.Move;
        Debug.Log("Switch to Move State");

        // if the A.I. reaches the destPos, interact
        if (Vector3.Distance(transform.position, destPos) == 0.0f)
        {
            UpdateInteractState();
        }
    }
    public void UpdateInteractState()
    {
        curState = FSMState.Interact;
        elapsedTime = 0.0f;

        if (elapsedTime >= interactTimer)
        {
            Debug.Log("interacting");
            UpdateSleepState();
        }

    }
    public void UpdateSleepState()
    {
        curState = FSMState.Interact;
    }
}

