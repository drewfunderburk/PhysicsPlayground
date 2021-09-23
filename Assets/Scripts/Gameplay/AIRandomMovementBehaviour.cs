using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CapsuleCollider))]
public class AIRandomMovementBehaviour : MonoBehaviour
{
    [SerializeField] private float _randomRadius = 10f;
    [SerializeField] private float _remainingDistance = 0.5f;
    [Space]
    [SerializeField] private float _explosionForce = 10f;
    [SerializeField] private float _explosionRadius = 20f;
    [Space]
    [SerializeField] private Animator _animator;

    private CapsuleCollider _collider;
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _collider = GetComponent<CapsuleCollider>();
    }

    private void Start()
    {
        // Disable child ragdoll parts
        foreach (Collider collider in GetComponentsInChildren<Collider>())
            collider.enabled = false;
        foreach (Rigidbody rigidbody in GetComponentsInChildren<Rigidbody>())
            rigidbody.isKinematic = true;

        // Enable parent collider
        _collider.enabled = true;
    }

    private void Update()
    {
        _animator.SetFloat("Speed", _agent.speed);

        if (_agent.isActiveAndEnabled && _agent.hasPath && _agent.remainingDistance > _remainingDistance)
            return;

        Vector2 unitCircle2D = Random.insideUnitCircle * _randomRadius;
        Vector3 unitCircle = new Vector3(unitCircle2D.x, 0, unitCircle2D.y);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position + unitCircle, out hit, 2, NavMesh.AllAreas))
            _agent.SetDestination(hit.position);

    }

    public void Ragdoll()
    {

        // Enable child ragdoll parts
        foreach (Collider collider in GetComponentsInChildren<Collider>())
            collider.enabled = true;
        foreach (Rigidbody rigidbody in GetComponentsInChildren<Rigidbody>())
        {
            rigidbody.isKinematic = false;
            rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }

        // Disable parent parts
        _collider.enabled = false;
        _agent.enabled = false;
        _animator.enabled = false;
    }
}
