using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Destroys gameObject after a specified delay
/// </summary>
public class DestroySelfBehaviour : MonoBehaviour
{
    [Tooltip("Delay in seconds after which to destroy this object")]
    [SerializeField] private float _delay = 1f;

    private void Start()
    {
        StartCoroutine(DestroySelfCoroutine());
    }

    private IEnumerator DestroySelfCoroutine()
    {
        yield return new WaitForSeconds(_delay);
        Destroy(gameObject);
    }
}