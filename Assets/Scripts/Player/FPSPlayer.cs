using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSPlayer : MonoBehaviour
{
    [SerializeField] bool _invertY = false;
    [SerializeField] float _movementSpeed;
    [SerializeField] float _runSpeed;
    [SerializeField] float _jumpHeight;
    [SerializeField] float _gravityScale;
    [SerializeField] Vector2 _sensibility;
    [SerializeField] Camera _playerCamera;

    private float camVerticalAngle;
    private CharacterController character;
    private Vector3 movInput;
    private Vector3 rotInput;
    private void Awake()
    {
        character = GetComponent<CharacterController>();
    }
    private void Start()
    {
        Cursor.visible = false;
    }
    private void Update()
    {
        Move();
        Look();
    }
    private void Move()
    {
        if (character.isGrounded)
        {
            movInput = Input.GetAxis("Horizontal") * Vector3.right + Input.GetAxis("Vertical") * Vector3.forward;
            movInput = Vector3.ClampMagnitude(movInput, 1);

            if (Input.GetButton("Sprint"))
                movInput *= _runSpeed;
            else
                movInput *= _movementSpeed;

            movInput = transform.TransformDirection(movInput);

            if (Input.GetButtonDown("Jump"))
            {
                movInput.y = Mathf.Sqrt(_jumpHeight * -2f * _gravityScale);
            }
        }

        movInput.y += _gravityScale * Time.deltaTime;
        character.Move(movInput * Time.deltaTime);
    }
    private void Look()
    {
        rotInput.x = Input.GetAxis("Mouse X") * _sensibility.x * Time.deltaTime;
        rotInput.y = Input.GetAxis("Mouse Y") * _sensibility.y * Time.deltaTime;
        camVerticalAngle += rotInput.y * (_invertY?1:-1);
        camVerticalAngle = Mathf.Clamp(camVerticalAngle, -70f, 70f);
        _playerCamera.transform.localRotation = Quaternion.Euler(camVerticalAngle, 0, 0);
        transform.Rotate(Vector3.up * rotInput.x);
    }
}
