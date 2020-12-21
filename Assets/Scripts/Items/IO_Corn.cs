using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IO_Corn : InteractuableObject
{
    public bool IsStill = false;
    [SerializeField] float _tolerance;
    [SerializeField] int _eatTime;
    private Rigidbody body;
    
    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        interactionType = InteractionType.Grabbable;
        body.AddTorque(Vector3.up * Random.Range(10, 50));
    }
    private void Update()
    {
        IsStill = body.velocity.magnitude <= _tolerance;
    }
    public void Eat()
    {
        _eatTime--;
        if (_eatTime == 0)
            Destroy(gameObject);
    }
}
