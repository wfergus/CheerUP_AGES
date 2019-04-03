using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class InteractiveObject : MonoBehaviour, IInteractable
{
    [SerializeField]
    protected string displayText = nameof(InteractiveObject);

    public string DisplayText => displayText;

    private AudioSource audioSource;
    //protected Light lightSource;

    private void Awake()
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
