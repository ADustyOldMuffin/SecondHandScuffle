using System;
using System.Numerics;
using Animancer;
using Constants;
using Managers;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Vector2 = UnityEngine.Vector2;

namespace Weapons
{
    public abstract class BaseWeapon : MonoBehaviour
    {
        
        [SerializeField] protected GameObject projectile;
        [SerializeField] protected Transform spawnPoint;
        [SerializeField] protected float fireRate = 0.5f, knockBackDistance = 0.1f, currentCooldown = 0.0f;
        [SerializeField] protected AnimancerComponent animancer;
        [SerializeField] protected DirectionalAnimationSet idles, attacks;
        
        protected Vector2 Facing;

        public WeaponType type;
        public string weaponName;
        public Sprite weaponIcon;
        
        protected abstract void OnAttackActionStarted(InputAction.CallbackContext context);
        protected abstract void OnAttackActionCancelled(InputAction.CallbackContext context);
        protected abstract void OnAttackAction(InputAction.CallbackContext context);

        protected virtual void Awake()
        {
            if (InputManager.Instance is null)
                return;
            
            InputManager.Instance.InputMaster.Player.Attack.started += OnAttackActionStarted;
            InputManager.Instance.InputMaster.Player.Attack.performed += OnAttackAction;
            InputManager.Instance.InputMaster.Player.Attack.canceled += OnAttackActionCancelled;
            Facing = LevelManager.Instance.GetCurrentPlayerFacingDirection();
            
            EventBus.Instance.OnFacingDirectionChange += OnFacingDirectionChange;
        }

        private void Start()
        {
            SetIdleAnimation();
        }

        private void OnFacingDirectionChange(Vector2 newDirection)
        {
            Facing = newDirection;
            SetIdleAnimation();
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
            
            InputManager.Instance.InputMaster.Player.Attack.started -= OnAttackActionStarted;
            InputManager.Instance.InputMaster.Player.Attack.performed -= OnAttackAction;
            InputManager.Instance.InputMaster.Player.Attack.canceled -= OnAttackActionCancelled;
        }
        
        private void SetIdleAnimation()
        {
            var clip = idles.GetClip(Facing);
            animancer.Play(clip);
        }
    }
}