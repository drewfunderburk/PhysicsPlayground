using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleObjectSpawnerBehaviour : MonoBehaviour
{
    [Tooltip("Object to spawn")]
    [SerializeField] private GameObject _object;
    [Space]
    [Tooltip("How many spawns per second to allow")]
    [SerializeField] private float _spawnRate = 1;
    [Tooltip("Should objects be spawned on cooldown?")]
    [SerializeField] private bool _spawnContinuously = false;

    private float _nextFireTime;

    private void Update()
    {
        if (_spawnContinuously)
            Spawn();
    }

    public void Spawn()
    {
        if (Time.time < _nextFireTime)
            return;

        _nextFireTime = Time.time + (1 / _spawnRate);

        Instantiate(_object, transform.position, transform.rotation);
    }
}