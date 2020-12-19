using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IO_AnimatorTrigger : InteractuableObject
{
    [SerializeField] Animator _animator;
    [SerializeField] string _triggerName;
    private void Start()
    {
        interactionType = InteractionType.Activable;
    }
    public override void Activate()
    {
        base.Activate();
        _animator.SetTrigger(_triggerName);
    }
}
