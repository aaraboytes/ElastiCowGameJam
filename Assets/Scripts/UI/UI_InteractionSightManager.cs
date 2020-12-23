using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_InteractionSightManager : MonoBehaviour
{
    [SerializeField] UI_InteractionDescription _description;
    private Vector3 targetPos;
    private Camera camera;
    private bool objectInHands = false;
    private void Start()
    {
        PlayerSight.OnSightObject += DescribeObject;
        PlayerSight.OnSightStop += StopDescription;
        PlayerGrab.OnObjectDropped += EnableDescription;
        PlayerGrab.OnObjectGrabbed += DisableDescription;
        camera = Camera.main;
    }
    private void OnDestroy()
    {
        PlayerSight.OnSightObject -= DescribeObject;
        PlayerSight.OnSightStop -= StopDescription;
        PlayerGrab.OnObjectDropped -= EnableDescription;
        PlayerGrab.OnObjectGrabbed -= DisableDescription;
    }
    private void Update()
    {
        if(targetPos!=Vector3.zero && _description.gameObject.activeInHierarchy)
        {
            _description.transform.position = camera.WorldToScreenPoint(targetPos);
        }
    }
    private void DisableDescription()
    {
        objectInHands = true;
        _description.gameObject.SetActive(false);
    }
    private void DescribeObject()
    {
        if (!objectInHands)
        {
            InteractuableObject interactuableObject = PlayerSight.InSightObject;
            targetPos = interactuableObject.transform.position;
            _description.Write(interactuableObject.Name, interactuableObject.interactionMessage);
            _description.gameObject.SetActive(true);
        }
    }
    private void EnableDescription()
    {
        objectInHands = false;
    }
    private void StopDescription()
    {
        _description.gameObject.SetActive(false);
    }
}
