using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKiller : MonoBehaviour
{
    [SerializeField] float _turnSpeed;

    [Header("Player components")]
    [SerializeField] Camera _eyes;
    [SerializeField] FPSPlayer _fps;
    [SerializeField] PlayerWhistle _whistle;
    [SerializeField] PlayerSight _sight;
    [SerializeField] PlayerGrab _grab;
    [SerializeField] PlayerItems _items;

    [Header("Elasticow")]
    [SerializeField] Transform _head;

    private bool killingStarted = true;

    private void Update()
    {
        if (killingStarted)
        {
            _eyes.transform.rotation = Quaternion.Lerp(_eyes.transform.rotation, Quaternion.LookRotation(_head.position - _eyes.transform.position), _turnSpeed * Time.deltaTime);
        }
    }
    public void Kill()
    {
        _fps.enabled = _whistle.enabled = _sight.enabled = _grab.enabled = _items.enabled = false;
        killingStarted = true;
    }
}
