using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class FireIfInRangeBehaviour : ProjectileLauncher
{
    [SerializeField] private float _targetRadius = 20f;

    public float FireRate = 1f;

    private SphereCollider _collider;
    private float _nextFireTime = 0f;
    private bool _inRange = false;

    public float TargetRadius 
    { 
        get => _targetRadius;
        set
        {
            _targetRadius = value;
            if (_collider)
                _collider.radius = value;
        }
    }

    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
    }

    private void Start()
    {
        _collider.isTrigger = true;
        _collider.radius = TargetRadius;
    }

    private void Update()
    {
        if (_inRange)
        {
            if (Time.time < _nextFireTime)
                return;
            _nextFireTime = Time.time + (1f / FireRate);

            LaunchProjectile();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            _inRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            _inRange = false;
    }

    private void OnValidate()
    {
        _collider = GetComponent<SphereCollider>();
        _collider.radius = TargetRadius;
    }
}
