using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour, IInteractable
{
    [SerializeField]
    private string displayText;

    public string DisplayText => displayText;

    public void InteractWith()
    {
        Debug.Log("Player has interacted with: " + gameObject.name);
    }
}
