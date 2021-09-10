using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthBehaviour : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 1;
    [SerializeField] private bool _invincible = false;
    [SerializeField] private bool _destroyOnDeath = false;
    [Space]
    [SerializeField] private bool _allowInvincibilityFrames = true;
    [SerializeField] private float _invincibilityFramesDuration = 1f;
    [Space]

    public UnityEvent OnTakeDamage;
    public UnityEvent OnDeath;

    private Renderer[] _meshRenderers;
    private float _health = 1;
    private float _invincibilityTimer = 0f;

    public float Health { get => _health; private set => _health = value; }
    public bool DestroyOnDeath { get => _destroyOnDeath; private set => _destroyOnDeath = value; }
    public bool Invincible { get => _invincible; set => _invincible = value; }
    public float MaxHealth { get => _maxHealth; private set => _maxHealth = value; }

    private void Awake()
    {
        _meshRenderers = GetComponentsInChildren<Renderer>();
    }

    private void Start()
    {
        _health = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        // Don't bother if invincible
        if (Invincible)
            return;

        if (damage > 0)
        {
            OnTakeDamage.Invoke();
            Health -= damage;

            if (Health <= 0)
            {
                Health = 0;

                OnDeath.Invoke();
                if (DestroyOnDeath)
                    Destroy(gameObject);
            }
            else if (_allowInvincibilityFrames)
                StartCoroutine(IFramesCoroutine());
        }
    }

    private IEnumerator IFramesCoroutine()
    {
        // Set invincible
        Invincible = true;

        // While timer is not expired
        while (_invincibilityTimer < _invincibilityFramesDuration)
        {
            // Increment timer
            _invincibilityTimer += Time.deltaTime;

            // Toggle the object to achieve flashing effect
            if (_meshRenderers.Length > 0)
                foreach (Renderer renderer in _meshRenderers)
                    renderer.enabled = !renderer.enabled;

            // Wait for next frame
            yield return null;
        }

        // Set not invincible
        Invincible = false;

        // Turn object on
        foreach (Renderer renderer in _meshRenderers)
            renderer.enabled = true;

        // Reset timer
        _invincibilityTimer = 0f;
    }
}
