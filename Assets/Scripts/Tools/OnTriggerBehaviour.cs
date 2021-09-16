using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerBehaviour : MonoBehaviour
{
    [SerializeField] private LayerMask _collisionMask = ~0;
    
    [Space]
    public UnityEvent OnEnter;
    public UnityEvent OnStay;
    public UnityEvent OnExit;

    private void OnTriggerEnter(Collider collider)
    {
        // If the other GameObject's layer is not in _collisionMask, return
        if (((1 << collider.gameObject.layer) & _collisionMask) == 0)
            return;

        OnEnter.Invoke();
    }

    private void OnTriggerStay(Collider collider)
    {
        // If the other GameObject's layer is not in _collisionMask, return
        if (((1 << collider.gameObject.layer) & _collisionMask) == 0)
            return;

        OnStay.Invoke();
    }

    private void OnTriggerExit(Collider collider)
    {
        // If the other GameObject's layer is not in _collisionMask, return
        if (((1 << collider.gameObject.layer) & _collisionMask) == 0)
            return;

        OnExit.Invoke();
    }
}
