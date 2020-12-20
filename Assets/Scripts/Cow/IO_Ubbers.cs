using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IO_Ubbers : InteractuableObject
{
    [SerializeField] Cow _cow;
    [SerializeField] UbberDetector _ubbersDetector;
    private void Start()
    {
        interactionType = InteractionType.Activable;
    }
    public override void Activate()
    {
        base.Activate();
        if (_cow.IsFake)
        {
            _cow.TransitionToElasticow();
        }
        else
            _ubbersDetector.FillBucket();
    }
}
