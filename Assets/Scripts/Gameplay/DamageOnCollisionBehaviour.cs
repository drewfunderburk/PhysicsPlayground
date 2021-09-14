using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Damage an object on collision
/// 
/// Depends on IDamageable
/// </summary>
public class DamageOnCollisionBehaviour : MonoBehaviour
{
    private enum DamageResult
    {
        NOTHING,
        DESTROY,
        DISABLE
    }

    [Tooltip("How much damage to deal to the collided object")]
    [SerializeField] private float _damage = 1;

    [Tooltip("What layers this object should check when looking to deal damage")]
    [SerializeField] private LayerMask _damageMask = ~0;

    [Space]
    [Tooltip("What should happen to this object after it deals damage?")]
    [SerializeField] private DamageResult _damageResult = DamageResult.NOTHING;

    [Space]
    public UnityEvent OnDamage;

    private void OnCollisionEnter(Collision collision)
    {
        // If the other GameObject's layer is not in _whatToDamage, return
        if (((1 << collision.gameObject.layer) & _damageMask) == 0)
            return;

        // If it can be damaged, damage it
        if (collision.gameObject.TryGetComponent(out IDamageable other))
        {
            other.TakeDamage(_damage);
            OnDamage.Invoke();

            switch (_damageResult)
            {
                case DamageResult.NOTHING:
                    break;
                case DamageResult.DESTROY:
                    Destroy(gameObject);
                    break;
                case DamageResult.DISABLE:
                    gameObject.SetActive(false);
                    break;
                default:
                    break;
            }
        }
    }
}