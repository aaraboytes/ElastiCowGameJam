using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerSight : MonoBehaviour
{
    public static InteractuableObject InSightObject;
    public static UnityAction OnSightObject;
    public static UnityAction OnSightStop;

    [SerializeField] Transform _eyes;
    [SerializeField] LayerMask _interactuableMask;

    private bool OnSight = false;

    private void OnDestroy()
    {
        OnSightObject = null;
        OnSightStop = null;
    }
    private void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(_eyes.position, _eyes.forward);
        if(Physics.Raycast(ray,out hit,3,_interactuableMask))
        {
            InteractuableObject lastInteract = InSightObject;
            InSightObject = hit.collider.GetComponent<InteractuableObject>();
            if (lastInteract != InSightObject && InSightObject)
            {
                if (!OnSight)
                {
                    OnSightObject?.Invoke();
                    OnSight = true;
                }
            }
        }
        else
        {
            if (OnSight)
            {
                OnSight = false;
                OnSightStop?.Invoke();
            }
            InSightObject = null;
        }
    }
}
