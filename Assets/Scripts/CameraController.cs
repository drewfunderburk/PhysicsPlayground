﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _maxDistance = 30;
    [SerializeField] private float _sensitivity = 100;
    [SerializeField] private bool _invertY;

    private float _currentDistance = 0;
    private void Update()
    {
        // Rotate
        if (Input.GetMouseButton(1))
        {
            Vector3 angles = transform.eulerAngles;
            Vector2 rotation;
            rotation.y = Input.GetAxis("Mouse X");
            rotation.x = -Input.GetAxis("Mouse Y") * (_invertY ? -1 : 1);

            // Look up and down
            angles.x = Mathf.Clamp(angles.x + rotation.x * _sensitivity, 0, 70);

            // Look left and right
            angles.y += rotation.y * _sensitivity;

            // Set the angles
            transform.eulerAngles = angles;
        }


        // Move
        RaycastHit hitInfo;
        if (Physics.Raycast(_target.position, -transform.forward, out hitInfo, _maxDistance))
        {
            Debug.Log("Test");
            _currentDistance = hitInfo.distance;
        }
        else
        {
            _currentDistance = Mathf.MoveTowards(_currentDistance, _maxDistance, 0.1f);
        }

        transform.position = _target.position + (_currentDistance * -transform.forward);
    }
}