using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IO_Ubbers : InteractuableObject
{
    [SerializeField] UbberDetector _ubbersDetector;
    private void Start()
    {
        interactionType = InteractionType.Activable;
    }
    public override void Activate()
    {
        base.Activate();
        _ubbersDetector.FillBucket();
    }
}
