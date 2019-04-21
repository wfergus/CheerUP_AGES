using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class InteractiveObject : MonoBehaviour, IInteractable
{
    [SerializeField]
    protected string displayText = nameof(InteractiveObject);

    public virtual string DisplayText => displayText;

    protected AudioSource audioSource;
    //protected Light lightSource;

    protected virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        //lightSource = lightSource?.GetComponent<Light>();
    }

    public virtual void InteractWith()
    {
        try
        {
            audioSource.Play();
        }
        catch (System.Exception)
        {
            throw new System.Exception("Missing audio source: Interactable Object requires an audio source.");
        }
        Debug.Log("Player has interacted with: " + gameObject.name);
    }
}
