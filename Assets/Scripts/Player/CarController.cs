using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private WheelCollider _frontLeftWheelCollider;
    [SerializeField] private WheelCollider _frontRightWheelCollider;
    [SerializeField] private WheelCollider _rearLeftWheelCollider;
    [SerializeField] private WheelCollider _rearRightWheelCollider;
    [Space]
    [SerializeField] private Transform _frontLeftWheelTransform;
    [SerializeField] private Transform _frontRightWheelTransform;
    [SerializeField] private Transform _rearLeftWheelTransform;
    [SerializeField] private Transform _rearRightWheelTransform;
    [Space]
    [SerializeField] private float _maxSteeringAngle = 30f;
    [SerializeField] private float _motorForce = 50;

    private float _horizontalInput;
    private float _verticalInput;
    private float _steeringAngle;


    private void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerate();
        UpdateWheelPoses();
    }

    private void GetInput()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
    }

    private void Steer()
    {
        _steeringAngle = _maxSteeringAngle * _horizontalInput;
        _frontLeftWheelCollider.steerAngle = _steeringAngle;
        _frontRightWheelCollider.steerAngle = _steeringAngle;
    }

    private void Accelerate()
    {
        _rearRightWheelCollider.motorTorque = _motorForce * _verticalInput;
        _rearLeftWheelCollider.motorTorque = _motorForce * _verticalInput;
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(_frontLeftWheelCollider, _frontLeftWheelTransform);
        UpdateWheelPose(_frontRightWheelCollider, _frontRightWheelTransform);
        UpdateWheelPose(_rearLeftWheelCollider, _rearLeftWheelTransform);
        UpdateWheelPose(_rearRightWheelCollider, _rearRightWheelTransform);
    }

    private void UpdateWheelPose(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 position = wheelTransform.position;
        Quaternion rotation = wheelTransform.rotation;

        wheelCollider.GetWorldPose(out position, out rotation);

        wheelTransform.position = position;
        wheelTransform.rotation = rotation;
    }
}
