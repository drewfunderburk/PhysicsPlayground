using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
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
    [SerializeField] private float _idleBrakingForce = 100f;
    [SerializeField] [Range(0.0001f, 1)] private float _driftStiffness = 0.5f;
    [SerializeField] [Range(0, 1)] private float _driftSmoothDampTime = 0.1f;
    [SerializeField] private bool _frontWheelDrive = false;
    [Space]
    [SerializeField] private bool _resetAfterFall = true;
    [SerializeField] private float _resetY = -10;

    public bool IsBeingControlled = false;

    private Rigidbody _rigidbody;

    private float _horizontalInput;
    private float _verticalInput;
    private bool _brakeInput;
    private float _steeringAngle;
    private History<Vector3> _positionHistory;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _positionHistory = new History<Vector3>(50);
        _positionHistory.Enqueue(transform.position);
    }

    private void OnDrawGizmos()
    {
        if (_positionHistory == null || _positionHistory.Count == 0)
            return;

        Gizmos.color = Color.green;
        foreach (Vector3 position in _positionHistory.ToArray())
        {
            Gizmos.DrawWireSphere(position, 0.1f);
        }
    }

    private void FixedUpdate()
    {
        if (IsBeingControlled)
            GetInput();
        Steer();
        Accelerate();
        Drift();
        UpdateWheelPoses();

        // Save last position where all 4 wheels were grounded
        if (_frontRightWheelCollider.isGrounded &&
            _frontLeftWheelCollider.isGrounded &&
            _rearRightWheelCollider.isGrounded &&
            _rearLeftWheelCollider.isGrounded)
            _positionHistory.Enqueue(transform.position);

        if (_resetAfterFall && transform.position.y < _resetY)
        {
            transform.position = _positionHistory.Dequeue();
            transform.rotation = Quaternion.identity;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }
    }

    private void GetInput()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
        _brakeInput = Input.GetButton("Jump");
    }

    private void Steer()
    {
        _steeringAngle = _maxSteeringAngle * _horizontalInput;
        _frontLeftWheelCollider.steerAngle = _steeringAngle;
        _frontRightWheelCollider.steerAngle = _steeringAngle;
    }

    private void Accelerate()
    {
        if (_frontWheelDrive)
        {
            _frontRightWheelCollider.motorTorque = _motorForce * _verticalInput;
            _frontLeftWheelCollider.motorTorque = _motorForce * _verticalInput;
        }
        else
        {
            _rearRightWheelCollider.motorTorque = _motorForce * _verticalInput;
            _rearLeftWheelCollider.motorTorque = _motorForce * _verticalInput;
        }

        _frontLeftWheelCollider.brakeTorque = (1 - Mathf.Abs(_verticalInput)) * _idleBrakingForce;
        _frontRightWheelCollider.brakeTorque = (1 - Mathf.Abs(_verticalInput)) * _idleBrakingForce;
        _rearLeftWheelCollider.brakeTorque = (1 - Mathf.Abs(_verticalInput)) * _idleBrakingForce;
        _rearRightWheelCollider.brakeTorque = (1 - Mathf.Abs(_verticalInput)) * _idleBrakingForce;
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

    private void Drift()
    {
        WheelFrictionCurve sidewaysFriction = _rearLeftWheelCollider.sidewaysFriction;
        float vel = 0;
        sidewaysFriction.stiffness = Mathf.SmoothDamp(sidewaysFriction.stiffness, (_brakeInput ? _driftStiffness : 1), ref vel, _driftSmoothDampTime);
        _rearLeftWheelCollider.sidewaysFriction = sidewaysFriction;
        _rearRightWheelCollider.sidewaysFriction = sidewaysFriction;
    }

    public void ResetCar()
    {
        // Position and rotation
        transform.position = _positionHistory.Dequeue();
        transform.rotation = Quaternion.identity;

        // Velocity
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }
}
