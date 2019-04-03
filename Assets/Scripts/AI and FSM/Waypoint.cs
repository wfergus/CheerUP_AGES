using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Waypoint : MonoBehaviour
{
    public float timeLength;
    public CheerUpFSM CheerUpFSM;
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("hit wp");
            //CheerUpFSM.destPos = ;//next interact position
            CheerUpFSM.interactTimer = timeLength;// waypoint needs a property called timeLength. associated with the next interactions length
            CheerUpFSM.UpdateMoveState();

        }
    }
}
