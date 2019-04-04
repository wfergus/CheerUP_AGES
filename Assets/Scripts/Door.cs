using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Door : InteractiveObject
{
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
            animator.SetBool("shouldOpen", true);
            base.InteractWith();
            displayText = string.Empty;
            isOpen = true;
        }
    }
}
