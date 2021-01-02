using System;
using Managers;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Weapons
{
    public abstract class BaseWeapon : MonoBehaviour
    {
        
        [SerializeField] protected GameObject projectile;
        [SerializeField] protected Transform spawnPoint;
        [SerializeField] protected float fireRate = .5f;
        
        [SerializeField] protected float _currentCooldown = 0.0f;

        protected virtual void Awake()
        {
            if (InputManager.Instance is null)
                return;
            
            InputManager.Instance.InputMaster.Player.Attack.performed += OnAttack;
        }

        protected virtual void OnEnable()
        {
            if (InputManager.Instance is null)
                return;
            
            InputManager.Instance.InputMaster.Player.Attack.Enable();
        }

        protected virtual void OnDisable()
        {
            if (InputManager.Instance is null)
                return;
            
            InputManager.Instance.InputMaster.Player.Attack.Disable();
        }

        protected void OnDestroy()
        {
            if (InputManager.Instance is null)
                return;
            
            InputManager.Instance.InputMaster.Player.Attack.performed -= OnAttack;
        }

        protected virtual void FixedUpdate()
        {
            if (_currentCooldown >= 0.0f)
                _currentCooldown -= Time.fixedDeltaTime;
            else
            {
                if (_currentCooldown < 0)
                    _currentCooldown = 0;
            }
        }
        
        protected virtual void OnAttack(InputAction.CallbackContext _)
        {
            if (_currentCooldown > 0.0f)
                return;
        }
    }
}