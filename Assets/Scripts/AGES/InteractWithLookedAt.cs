using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// detects when the player pressing the interact button while looking at IInteractive,
/// and then calls IInteractive's InteractWith() mentod.
/// </summary>
public class InteractWithLookedAt : MonoBehaviour
{
    private IInteractable lookedAtInteractable;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact") && lookedAtInteractable != null)
        {
            Debug.Log("pressed interact button");
            lookedAtInteractable.InteractWith();
        }
    }

    /// <summary>
    /// event handler for DetectLookedAtInteractable.LookedAtInteractableChanged
    /// </summary>
    /// <param name="newLookedAtInteractable">Reference to the new IInteractabe the  player is looking at</param>
    private void OnLookedAtInteractableChanged(IInteractable newLookedAtInteractable)
    {
        lookedAtInteractable = newLookedAtInteractable;
    }

     #region Event subscription / unsubscriptoion
    private void OnEnable()
    {
        DetectLookedAtInteractive.LookedAtInteractableChanged += OnLookedAtInteractableChanged;
    }

    private void OnDisable()
    {
        DetectLookedAtInteractive.LookedAtInteractableChanged -= OnLookedAtInteractableChanged;
    }
    #endregion
}
