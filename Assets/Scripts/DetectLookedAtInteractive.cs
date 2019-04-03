using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Detects Ingteractive elements that the player is lookins at.
/// </summary>
public class DetectLookedAtInteractive : MonoBehaviour
{
    [Tooltip("Starting point of raycast used to detect interactives.")]
    [SerializeField]
    private Transform raycastOrigin;

    [Tooltip("How far from the raycast origin that you can search for interactive elements.")]
    [SerializeField]
    private float raycastMaxRange = 5.0f;

    private void FixedUpdate()
    {
        //Debug.DrawRay(raycastOrigin.position, raycastOrigin.forward * raycastMaxRange, Color.red);
        RaycastHit hitInfo;
        bool objectWasDetected = Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out hitInfo, raycastMaxRange);

        if (objectWasDetected)
        {
            Debug.Log("Player is looking at: " + hitInfo.collider.gameObject.name);
        }
    }
}
