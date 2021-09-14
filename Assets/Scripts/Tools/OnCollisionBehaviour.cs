using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnCollisionBehaviour : MonoBehaviour
{
    [SerializeField] private LayerMask _collisionMask = ~0;
    
    [Space]
    public UnityEvent OnEnter;
    public UnityEvent OnStay;
    public UnityEvent OnExit;

    private void OnCollisionEnter(Collision collision)
    {
        // If the other GameObject's layer is not in _collisionMask, return
        if (((1 << collision.gameObject.layer) & _collisionMask) == 0)
            return;

        OnEnter.Invoke();
    }

    private void OnCollisionStay(Collision collision)
    {
        // If the other GameObject's layer is not in _collisionMask, return
        if (((1 << collision.gameObject.layer) & _collisionMask) == 0)
            return;

        OnStay.Invoke();
    }

    private void OnCollisionExit(Collision collision)
    {
        // If the other GameObject's layer is not in _collisionMask, return
        if (((1 << collision.gameObject.layer) & _collisionMask) == 0)
            return;

        OnExit.Invoke();
    }
}
