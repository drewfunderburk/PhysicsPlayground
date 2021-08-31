using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float _baseSpeed = 1;
    [SerializeField] private float _gravityMultiplier = 1;
    [SerializeField] private float _jumpForce = 100;

    private CharacterController _controller;

    private Vector3 _desiredGroundVelocity;
    private Vector3 _desiredAirVelocity;
    private bool _isJumpDesired;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Get movement input
        _desiredGroundVelocity.x = (Input.GetAxisRaw("Horizontal") * transform.right).x;
        _desiredGroundVelocity.y = 0;
        _desiredGroundVelocity.z = (Input.GetAxisRaw("Vertical") * transform.forward).z;

        // Scale movement to desired velocity
        _desiredGroundVelocity.Normalize();
        _desiredGroundVelocity *= _baseSpeed;

        // Get jump input
        _isJumpDesired = Input.GetButtonDown("Jump");

        if (_isJumpDesired)
        {
            _desiredAirVelocity.y = _jumpForce;
            _isJumpDesired = false;
        }

        // Gravity
        _desiredAirVelocity += Physics.gravity * _gravityMultiplier * Time.deltaTime;

        // Move
        _controller.Move((_desiredGroundVelocity + _desiredAirVelocity) * Time.deltaTime);
    }
}
