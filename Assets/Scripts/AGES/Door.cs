using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Door : InteractiveObject
{
    [Tooltip("Check this box to lock the door")]
    [SerializeField]
    private bool isLocked;

    [Tooltip("Text that displays whne the player looks at a locked door")]
    [SerializeField]
    private string lockedDisplayText = "Locked";

    [Tooltip("Play this audio clip when the player interacts with a locked door without having the key")]
    [SerializeField]
    private AudioClip lockedAudioClip;

    [Tooltip("The audio clip that plays when the door opens")]
    [SerializeField]
    private AudioClip openAudioClip;

    public override string DisplayText => isLocked ? lockedDisplayText : base.DisplayText;

    private Animator animator;
    private bool isOpen = false;
    /// <summary>
    /// uses constructor to initialize display text
    /// </summary>
    public Door()
    {
        displayText = nameof(Door);
    }

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        
    }

    public override void InteractWith()
    {
        if (!isOpen)
        {
            if (!isLocked)
            {
                audioSource.clip = openAudioClip;
                animator.SetBool("shouldOpen", true);
                displayText = string.Empty;
                isOpen = true;
            }
            else
                audioSource.clip = lockedAudioClip;

            base.InteractWith(); //plays sound effect
        }
    }
}
