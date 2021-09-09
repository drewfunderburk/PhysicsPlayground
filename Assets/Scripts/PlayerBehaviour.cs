﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float _baseSpeed = 1;
    [SerializeField] private float _gravityMultiplier = 1;
    [SerializeField] private float _jumpHeight = 1;
    [SerializeField] [Range(0, 1)] private float _airControl = 1;
    [SerializeField] private bool _faceWithCamera = false;
    [SerializeField] private Animator _animator;

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
        float inputRight = Input.GetAxisRaw("Horizontal");
        float inputForward = Input.GetAxisRaw("Vertical");

        // Get camera forward
        Vector3 cameraForward = _camTransform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        // Get camera right
        Vector3 cameraRight = _camTransform.right;

        // Set velocity relative to camera
        _desiredVelocity = (inputRight * cameraRight + inputForward * cameraForward);

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

        float animatorSpeed = 0;

        // Update animations
        if (_faceWithCamera)
        {
            transform.forward = cameraForward;
            animatorSpeed = inputForward;
            _animator.SetFloat("Direction", inputRight);
        }
        else if (_desiredVelocity != Vector3.zero)
        {
            transform.forward = _desiredVelocity.normalized;
            animatorSpeed = _desiredVelocity.magnitude / _baseSpeed;
        }

        _animator.SetFloat("Speed", animatorSpeed);

        _animator.SetBool("Jump", !_controller.isGrounded);

        // Move
        _controller.Move((_desiredVelocity + _desiredAirVelocity) * Time.fixedDeltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Finish"))
        {
            _controller.enabled = false;
            _animator.enabled = false;

            Rigidbody[] rbs = GetComponentsInChildren<Rigidbody>();

            foreach (Rigidbody rb in rbs)
                rb.AddExplosionForce(500, hit.point, 50);

            this.enabled = false;
        }
    }
}
