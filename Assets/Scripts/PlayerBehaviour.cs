using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float _baseSpeed = 1;
    [SerializeField] private float _gravityMultiplier = 1;
    [SerializeField] private float _jumpHeight = 1;
    [SerializeField] [Range(0, 1)] private float _airControl = 1;

    private CharacterController _controller;
    private Transform _camTransform;

    private Vector3 _desiredVelocity;
    private Vector3 _desiredAirVelocity;
    private bool _isJumpDesired;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _camTransform = Camera.main.gameObject.transform;
    }

    private void Update()
    {
        // Get movement input
        _desiredVelocity.x = Input.GetAxisRaw("Horizontal");
        _desiredVelocity.y = 0;
        _desiredVelocity.z = Input.GetAxisRaw("Vertical");

        // Get camera forward
        Vector3 cameraForward = _camTransform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        // Get camera right
        Vector3 cameraRight = _camTransform.right;

        _desiredVelocity = (_desiredVelocity.x * cameraRight + _desiredVelocity.z * cameraForward);

        // Scale movement to desired velocity
        _desiredVelocity.Normalize();
        _desiredVelocity *= _baseSpeed;

        // Apply air control

        // Get jump input
        _isJumpDesired = Input.GetButton("Jump");

        if (_isJumpDesired && _controller.isGrounded)
        {
            _desiredAirVelocity = transform.up * Mathf.Sqrt(_jumpHeight * -2f * (Physics.gravity.y * _gravityMultiplier));
            //_isJumpDesired = false;
        }

        if (!_isJumpDesired && _controller.isGrounded)
        {
            _desiredAirVelocity.y = 0;
        }


        // Gravity
        _desiredAirVelocity += Physics.gravity * _gravityMultiplier * Time.fixedDeltaTime;

        // Move
        _controller.Move((_desiredVelocity + _desiredAirVelocity) * Time.fixedDeltaTime);
    }
}
