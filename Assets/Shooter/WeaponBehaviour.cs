using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Hitscan weapon. Provides functionality for damage, range, fire rate, shot count, recoil, bullet spread, and effect spawning.
/// </summary>
public class WeaponBehaviour : MonoBehaviour
{
    #region SERIALZED FIELDS
    [Header("Stats")]
    [Tooltip("How much damage to deal to a target")]
    [SerializeField] private float _damage = 1f;

    [Tooltip("How far to allow hits")]
    [SerializeField] private float _range = 100f;

    [Tooltip("Fire rate in RPM")]
    [SerializeField] private float _fireRate = 10f;

    [Tooltip("How many shots should be fired")]
    [SerializeField] [Min(1)] private int _shotCount = 1;

    [Space]
    [Header("Recoil")]
    [Tooltip("Total amount of recoil")]
    [SerializeField] private float _maximumRecoil = 2f;

    [Tooltip("How much recoil to apply per shot")]
    [SerializeField] private float _recoilPerShot = 0.1f;

    [Tooltip("How fast/hard the recoil should be")]
    [SerializeField] private float _recoilSpeed = 1f;

    [Tooltip("How fast the camera should return to its default state")]
    [SerializeField] private float _cameraReturnSpeed = 1f;

    [Tooltip("How long after firing stops before the camera returns to its default position")]
    [SerializeField] private float _cameraReturnDelay = 0.5f;

    [Space]
    [Header("Spread")]
    [Tooltip("Maximum bullet spread")]
    [SerializeField] private float _maximumBulletSpread = 0.1f;

    [Tooltip("Radius around the crosshair to spread bullets")]
    [SerializeField] private float _bulletSpreadRadius = 0f;

    [Tooltip("Spread increase per shot")]
    [SerializeField] private float _bulletSpreadRate = 0.01f;

    [Space]
    [Header("References")]
    [Tooltip("Camera to raycast from")]
    [SerializeField] private Camera _cam;

    [Tooltip("Muzzle flash effect")]
    [SerializeField] private ParticleSystem _muzzleFlash;

    [Tooltip("Hit effect")]
    [SerializeField] private ObjectPool _hitEffect;
    #endregion

    [Space]
    public UnityEvent OnFire;

    private bool _firing = false;
    
    private float _nextFireTime = 0f;
    private float _recoilHeight = 0;
    private float _recoilTimer = 0;

    private void Start()
    {
        // Initialize bullet hole object pool
        _hitEffect.Initialize();
    }

    private void Update()
    {
        // If we are currently firing
        if (_firing)
        {
            // Increment recoil timer
            _recoilTimer += Time.deltaTime;

            // Lerp the camera's rotation up
            Quaternion angle = Quaternion.Euler(Vector3.right * -_recoilHeight);
            _cam.transform.localRotation = Quaternion.Lerp(_cam.transform.localRotation, angle, _recoilSpeed * Time.deltaTime);

            // If we've stopped shooting long enough, set firing false
            if (_recoilTimer > _cameraReturnDelay)
                _firing = false;
        }
        else
        {
            // Aim camera back at 0
            _recoilHeight = 0;
            _cam.transform.localRotation = Quaternion.Lerp(_cam.transform.localRotation, Quaternion.identity, _cameraReturnSpeed * Time.deltaTime);
            
            // Reset bullet spread
            _bulletSpreadRadius = 0;
        }
    }

    public void Fire()
    {
        // Limit fire rate
        if (Time.time < _nextFireTime)
            return;
        _nextFireTime = Time.time + (1f / _fireRate);

        // Invoke OnFire
        OnFire.Invoke();

        // Play muzzle flash
        if (_muzzleFlash != null)
            _muzzleFlash.Play();

        // Recoil
        _recoilTimer = 0;
        _recoilHeight += _recoilPerShot;
        _recoilHeight = Mathf.Min(_recoilHeight, _maximumRecoil);
        _firing = true;

        for (int i = 0; i < _shotCount; i++)
        {
            // Bullet spread
            _bulletSpreadRadius += _bulletSpreadRate;
            _bulletSpreadRadius = Mathf.Min(_bulletSpreadRadius, _maximumBulletSpread);
            Vector3 spread = Random.insideUnitSphere * _bulletSpreadRadius;
            Vector3 targetDirection = _cam.transform.forward + spread;


            // Fire a raycast from the camera
            RaycastHit hit;
            if (Physics.Raycast(_cam.transform.position, targetDirection, out hit, _range))
            {
                // If the target is IDamageable, damage it
                IDamageable target = hit.transform.gameObject.GetComponent<IDamageable>();
                if (target != null)
                    target.TakeDamage(_damage);

                // Place a bullet hole
                if (_hitEffect != null)
                {
                    GameObject obj = _hitEffect.GetNext();
                    obj.transform.position = hit.point;
                    obj.transform.rotation = Quaternion.LookRotation(hit.normal);
                    obj.transform.parent = hit.transform;
                }

                Rigidbody other = hit.transform.gameObject.GetComponent<Rigidbody>();
                if (other != null)
                    other.AddForce(-hit.normal * 10);
            }
        }
    }
}