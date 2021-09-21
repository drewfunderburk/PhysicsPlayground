using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCameraBehaviour : MonoBehaviour
{
    [SerializeField] private bool _zeroOutX = false;
    private Camera _camera;

    void Awake()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        if (_zeroOutX)
        {
            Vector3 rotation = _camera.transform.eulerAngles;
            rotation.x = 0;
            transform.rotation = Quaternion.Euler(rotation);
        }
        else
            transform.rotation = _camera.transform.rotation;
    }
}