using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WakeupMaterialBehaviour : MonoBehaviour
{
    public Material AwakeMaterial = null;
    public Material AsleepMaterial = null;

    private Rigidbody _rigidbody;
    private MeshRenderer _renderer;

    private bool _materialIsAwake = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<MeshRenderer>();
    }

    private void FixedUpdate()
    {
        // Set material to asleep if rigidbody is asleep
        if (_materialIsAwake && _rigidbody.IsSleeping() && AsleepMaterial)
        {
            _renderer.material = AsleepMaterial;
            _materialIsAwake = false;
            Debug.Log("AsleepMaterial");
        }
        // Set material to awake if rigidbody is awake
        else if (!_materialIsAwake && !_rigidbody.IsSleeping() && AwakeMaterial)
        {
            _renderer.material = AwakeMaterial;
            _materialIsAwake = true;
            Debug.Log("AwakeMaterial");
        }
    }
}
