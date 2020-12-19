using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IO_MilkBucket : InteractuableObject
{
    public bool IsFilled => filled;
    [SerializeField] Animator _animator;
    
    private bool filled = false;
    private void Start()
    {
        interactionType = InteractionType.Grabbable;
    }
    public void Fill()
    {
        filled = true;
        _animator.SetBool("Filled", true);
    }
    public void Throw()
    {
        filled = false;
        _animator.SetBool("Filled", false);
    }
}
