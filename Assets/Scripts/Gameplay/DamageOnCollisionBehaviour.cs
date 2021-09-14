using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Damage an object on collision
/// 
/// Depends on IDamageable
/// </summary>
public class DamageOnCollisionBehaviour : MonoBehaviour
{
    [Tooltip("How much damage to deal to the collided object")]
    [SerializeField] private float _damage = 1;

    [Tooltip("What layers this object should check when looking to deal damage")]
    [SerializeField] private LayerMask _damageMask = ~0;

    private void OnCollisionEnter(Collision collision)
    {
        // If the other GameObject's layer is in _whatToDamage
        if (((1 << collision.gameObject.layer) & _damageMask) != 0)
        {
            // Get a reference to the other object's HealthBehaviour
            IDamageable otherHealth = collision.gameObject.GetComponent<IDamageable>();

            // If it has a HealthBehaviour, damage it
            if (otherHealth != null)
                otherHealth.TakeDamage(_damage);
        }
    }
}