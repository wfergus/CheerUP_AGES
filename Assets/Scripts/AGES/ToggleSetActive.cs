using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSetActive : InteractiveObject
{
    [Tooltip("The GameObject to toggle")]
    [SerializeField]
    private GameObject objectToToggle;

    [Tooltip("Can the player interact with this more than once?")]
    [SerializeField]
    private bool isReuseable = true;

    private bool hasBeenUsed = false;

    /// <summary>
    /// Toggles the activeSelf value for the objectToToggle when the player interacts with the object
    /// </summary>
    public override void InteractWith()
    {
        if (isReuseable || !hasBeenUsed)
        {
            objectToToggle.SetActive(!objectToToggle.activeSelf);
            base.InteractWith();
            hasBeenUsed = true;

            if (!isReuseable)
                displayText = string.Empty;

            //if (lightSource != null)
            //{
               
            //}
        }
    }


}
