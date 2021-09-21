using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMoveBehaviour : MonoBehaviour
{
    [SerializeField] private Vector3 _movementPerSecond;
    [SerializeField] private bool _worldSpace = false;

    private void Update()
    {
        if (_worldSpace)
        {
            transform.position += _movementPerSecond * Time.deltaTime;
        }
        else
        {
            Vector3 right = transform.right * _movementPerSecond.x;
            Vector3 up = transform.up * _movementPerSecond.y;
            Vector3 forward = transform.forward * _movementPerSecond.z;
            Vector3 movement = right + up + forward;

            transform.position += movement * Time.deltaTime;
        }
    }
}
