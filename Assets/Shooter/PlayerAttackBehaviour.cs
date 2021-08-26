using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Provides functionality for InputSystem driven attacks.
/// </summary>
public class PlayerAttackBehaviour : MonoBehaviour
{
    [Tooltip("Weapon to be fired")]
    [SerializeField] private WeaponBehaviour _weapon;

    private bool _firing = false;

    private void Update()
    {
        // Tell the weapon to fire if we're firing
        if (_firing)
            _weapon.Fire();
    }

    /// <summary>
    /// Performs attack actions upon receiving input
    /// </summary>
    public void OnAttack(InputAction.CallbackContext value)
    {
        // When the button is pressed, start firing
        if (value.started)
            _firing = true;

        // When released, stop firing
        if (value.canceled)
            _firing = false;
    }
}
