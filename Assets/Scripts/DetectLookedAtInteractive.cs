using System;
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

    //Event raised when the player looks at a different IInteracable
    public static event Action<IInteractable> LookedAtInteractableChanged;

    public IInteractable LookedAtInteractable
    {
        get { return lookedAtInteractable; }
        protected set
        {
            bool isInterableChanged = value != lookedAtInteractable;
            if (isInterableChanged)
            {
                lookedAtInteractable = value;
                LookedAtInteractableChanged?.Invoke(lookedAtInteractable);
            }
        }
    }

    private IInteractable lookedAtInteractable;

    private void FixedUpdate()
    {
       LookedAtInteractable = GetLookedAtInteractable();
    }

    /// <summary>
    /// Raycasts forward from the camera to look for IInteractable
    /// </summary>
    /// <returns>The first IInteractable detected, or null if none are found</returns>
    private IInteractable GetLookedAtInteractable()
    {
        //Debug.DrawRay(raycastOrigin.position, raycastOrigin.forward * raycastMaxRange, Color.red);
        RaycastHit hitInfo;
        bool objectWasDetected = Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out hitInfo, raycastMaxRange);

        IInteractable interactable = null;
        LookedAtInteractable = interactable;

        if (objectWasDetected)
            interactable = hitInfo.collider.GetComponent<IInteractable>();
        
        return interactable;
    }
}
