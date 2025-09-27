using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(CharacterController))]
public class VaSteR_PlayerController : MonoBehaviour
{
    [SerializeField] private Transform headTransform;
    
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float  gravity;
    [SerializeField] private float jumpHeight;

    [FormerlySerializedAs("mouseSensivity")] [SerializeField] private float mouseSensivityX;
    [FormerlySerializedAs("mouseSensivity")] [SerializeField] private float mouseSensivityY;
    [SerializeField] private float minPitch;
    [SerializeField] private float maxPitch;

    private CharacterController _controller;

    private float _pitch = 0f;

    private Vector3 _verticalVelocity;
    

    private InputAction _moveAction;
    private InputAction _lookAction;
    private InputAction _jumpAction;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        var playerInput = GetComponent<PlayerInput>();
        if (playerInput != null)
        {
            _moveAction = playerInput.actions["Move"];
            _lookAction = playerInput.actions["Look"];
            _jumpAction = playerInput.actions["Jump"];
        }
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleGravity();
        HandleJump();
        HandleMovement();
        HandleLook();
    }

    private void HandleGravity()
    {
        if (_controller.isGrounded && _verticalVelocity.y < 0) _verticalVelocity.y = -2f;
        _verticalVelocity.y += gravity * Time.deltaTime;
        _controller.Move(_verticalVelocity * Time.deltaTime);
    }

    private void HandleMovement()
    {
        Vector2 moveInput = _moveAction.ReadValue<Vector2>();
        Vector3 move = transform.forward * moveInput.y + transform.right * moveInput.x;
        _controller.Move(move * (moveSpeed * Time.deltaTime));
    }

    private void HandleLook()
    {
        Vector2 lookInput = _lookAction.ReadValue<Vector2>();
        transform.Rotate(Vector3.up, lookInput.x * mouseSensivityX * Time.deltaTime);
        _pitch -= lookInput.y * mouseSensivityY * Time.deltaTime;
        _pitch = Mathf.Clamp(_pitch, minPitch, maxPitch);
        headTransform.localRotation = Quaternion.Euler(_pitch, 0 ,0);
    }

    

    private void HandleJump()
    {
        if (_jumpAction.triggered && _controller.isGrounded) _verticalVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
}
