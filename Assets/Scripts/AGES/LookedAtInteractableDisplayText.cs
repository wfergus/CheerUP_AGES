using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This UI text displays infor aobut the currently looked at IInteractable object.
/// Text should be hidden if player is not currently looking at an interactive element.
/// </summary>
public class LookedAtInteractableDisplayText : MonoBehaviour
{
    private IInteractable lookedAtInteractable;
    private Text displayText;

    private void Awake()
    {
        displayText = GetComponent<Text>();
        UpdateDesplayText();
    }

    private void UpdateDesplayText()
    {
        if (lookedAtInteractable != null)
            displayText.text = lookedAtInteractable.DisplayText;
        else
            displayText.text = string.Empty;
    }

    /// <summary>
    /// event handler for DetectLookedAtInteractable.LookedAtInteractableChanged
    /// </summary>
    /// <param name="newLookedAtInteractable">Reference to the new IInteractabe the  player is looking at</param>
    private void OnLookedAtInteractableChanged(IInteractable newLookedAtInteractable)
    {
        lookedAtInteractable = newLookedAtInteractable;
        UpdateDesplayText();
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
