using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
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
        PlayerSight.OnSightObject -= GrabOpportunity;
        PlayerSight.OnSightStop -= RemoveGrabOpportunity;
    }
    private void Update()
    {
        if (grabOpportunity)
        {
            if (Input.GetButtonDown("Fire1") && PlayerSight.InSightObject)
            {
                InteractuableObject interactuableObject = PlayerSight.InSightObject;
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
            Destroy(joint);
        }
    }
    private void GrabOpportunity() => grabOpportunity = true;
    private void RemoveGrabOpportunity() => grabOpportunity = false;
}
