using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [Space]
    [SerializeField] private float _startDistance = 5;
    [SerializeField] private float _minDistance = 2;
    [SerializeField] private float _maxDistance = 20;
    [SerializeField] private float _scrollSensitivity = 1;
    [SerializeField] [Range(0, 1)] private float _cameraZoomSpeed = 0.2f;
    [SerializeField] private bool _invertScrollY = false;
    [SerializeField] private LayerMask _layerMask = ~0;
    [Space]
    [SerializeField] private float _sensitivity = 5;
    [SerializeField] private bool _invertCameraY = false;

    private float _currentDistance = 0;
    private float _targetDistance = 0;

    private void Start()
    {
        _currentDistance = _startDistance;
        _targetDistance = _startDistance;
        transform.position = _target.position + (_currentDistance * -transform.forward);
    }

    private void Update()
    {
        // Rotate
        if (Input.GetMouseButton(1))
        {
            Vector3 angles = transform.eulerAngles;
            Vector2 rotation;
            rotation.y = Input.GetAxis("Mouse X");
            rotation.x = -Input.GetAxis("Mouse Y") * (_invertCameraY ? -1 : 1);

            // Look up and down
            angles.x = Mathf.Clamp(angles.x + rotation.x * _sensitivity, 0, 70);

            // Look left and right
            angles.y += rotation.y * _sensitivity;

            // Set the angles
            transform.eulerAngles = angles;
        }

        // Mouse scroll
        float mouseScroll = -Input.mouseScrollDelta.y;
        _targetDistance += mouseScroll * _scrollSensitivity;
        _targetDistance = Mathf.Clamp(_targetDistance, _minDistance, _maxDistance);

        // Move
        RaycastHit hitInfo;
        if (Physics.Raycast(_target.position, -transform.forward, out hitInfo, _maxDistance, _layerMask))
        {
            _currentDistance = hitInfo.distance;
        }
        else
        {
            _currentDistance = Mathf.MoveTowards(_currentDistance, _targetDistance, _cameraZoomSpeed);
        }
        
        transform.position = _target.position + (_currentDistance * -transform.forward);
    }
}