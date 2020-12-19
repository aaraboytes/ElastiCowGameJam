using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IO_ItemBasedActivator : InteractuableObject
{
    [SerializeField] ItemData _necessaryObj;
    [SerializeField] bool _removeItemFromInventory;
    protected PlayerItems playerItems;
    private void Start()
    {
        interactionType = InteractionType.Activable;
    }
    public override void Activate()
    {
        base.Activate();
        if (!playerItems)
            playerItems = FindObjectOfType<PlayerItems>();
        if (playerItems.Contains(_necessaryObj))
            ActivationSuccess();
        else
            ActivationFailure();
    }
    public virtual void ActivationSuccess()
    {
        Debug.Log("Activation success");
        if (_removeItemFromInventory)
            playerItems.Remove(_necessaryObj);
    }
    public virtual void ActivationFailure()
    {
        Debug.Log("Activation failure");
    }
}
