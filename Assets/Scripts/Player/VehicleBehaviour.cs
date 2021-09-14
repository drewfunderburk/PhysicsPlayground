using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleBehaviour : MonoBehaviour
{
    public HingeJoint frontLeft;
    public HingeJoint frontRight;
    public HingeJoint rearLeft;
    public HingeJoint rearRight;

    private JointMotor _frontLeftMotor;
    private JointMotor _frontRightMotor;
    private JointMotor _rearLeftMotor;
    private JointMotor _rearRightMotor;

    public float topSpeed = 500;

    private void Start()
    {
        _frontLeftMotor = frontLeft.motor;
        _frontRightMotor = frontRight.motor;
        _rearLeftMotor = rearLeft.motor;
        _rearRightMotor = rearRight.motor;
    }

    private void Update()
    {
        float forward = Input.GetAxis("Vertical");
        float sideways = Input.GetAxis("Horizontal");

        _frontLeftMotor.targetVelocity = topSpeed * Mathf.Max(forward, sideways);
        _frontRightMotor.targetVelocity = topSpeed * Mathf.Max(-forward, -sideways);
        _rearLeftMotor.targetVelocity = topSpeed * Mathf.Max(forward, sideways);
        _rearRightMotor.targetVelocity = topSpeed * Mathf.Max(-forward, -sideways);
    }
}
