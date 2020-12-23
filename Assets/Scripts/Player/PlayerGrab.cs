using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerGrab : MonoBehaviour
{
    public static UnityAction OnObjectGrabbed;
    public static UnityAction OnObjectDropped;
    public static InteractuableObject GrabbedObject;

    [SerializeField] GameObject _grabPoint;
    [SerializeField] PlayerItems _items;

    private bool grabOpportunity;
    private Rigidbody grabbedBody;
    private SpringJoint joint;
    private void Start()
    {
        PlayerSight.OnSightObject += GrabOpportunity;
        PlayerSight.OnSightStop += RemoveGrabOpportunity;
    }
    private void OnDestroy()
    {
        PlayerSight.OnSightObject = null;
        PlayerSight.OnSightStop = null;
    }
    private void Update()
    {
        if (grabOpportunity)
        {
            if (Input.GetButtonDown("Fire1") && PlayerSight.InSightObject)
            {
                InteractuableObject interactuableObject = PlayerSight.InSightObject;
                GrabbedObject = interactuableObject;
                switch (interactuableObject.interactionType)
                {
                    case InteractuableObject.InteractionType.Activable:
                        interactuableObject.Activate();
                        break;
                    case InteractuableObject.InteractionType.Grabbable:
                        grabbedBody = interactuableObject.GetComponent<Rigidbody>();
                        _grabPoint.transform.position = grabbedBody.transform.position;
                        joint = _grabPoint.AddComponent<SpringJoint>();
                        joint.spring = 100;
                        joint.connectedBody = grabbedBody;
                        joint.autoConfigureConnectedAnchor = false;
                        joint.anchor = Vector3.zero;
                        OnObjectGrabbed?.Invoke();
                        break;
                    case InteractuableObject.InteractionType.Pickable:
                        ItemData data = interactuableObject.Pick();
                        _items.AddItem(data);
                        break;
                    default:
                        break;
                }
            }
        }
        if (Input.GetButtonUp("Fire1") && joint)
        {
            if(GrabbedObject)
                OnObjectDropped?.Invoke();
            Destroy(joint);
            GrabbedObject = null;
        }
    }
    private void GrabOpportunity() => grabOpportunity = true;
    private void RemoveGrabOpportunity() => grabOpportunity = false;
}
