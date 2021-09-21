using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HingeJoint))]
public class HammerSwingBehaviour : MonoBehaviour
{
    private HingeJoint _hinge;
    private float _torque = 0;
    private JointMotor _motor;
    private float _previousAngle;

    private void Awake()
    {
        _hinge = GetComponent<HingeJoint>();
        _motor = _hinge.motor;
        _torque = _motor.targetVelocity;
        _previousAngle = _hinge.angle;
    }

    private void Update()
    {
        if (_hinge.angle - _previousAngle > 0)
            _motor.targetVelocity = _torque;
        else
            _motor.targetVelocity = -_torque;

        _hinge.motor = _motor;
        _previousAngle = _hinge.angle;
    }
}
