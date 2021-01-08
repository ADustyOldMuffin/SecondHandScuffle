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
        [SerializeField] protected float fireRate = .5f;
        
        [FormerlySerializedAs("_currentCooldown")] [SerializeField] protected float currentCooldown = 0.0f;

        [SerializeField] protected AnimancerComponent animancer;
        [SerializeField] protected DirectionalAnimationSet idles;
        [SerializeField] protected DirectionalAnimationSet attacks;

        protected Vector2 Facing;
        protected bool IsAttacking;

        public WeaponType type;
        public string weaponName;
        public Sprite weaponIcon;
        [SerializeField]  public Dialogue[] objectDialogue;

        protected virtual void Awake()
        {
            if (InputManager.Instance is null)
                return;
            
            InputManager.Instance.InputMaster.Player.Attack.performed += OnAttackAction;
            Facing = Vector2.down;
            SetAnimation();
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

        protected virtual void OnDestroy()
        {
            if (InputManager.Instance is null)
                return;
            
            InputManager.Instance.InputMaster.Player.Attack.performed -= OnAttackAction;
        }

        protected virtual void FixedUpdate()
        {
            if(IsAttacking)
                OnAttack();
            
            if (currentCooldown >= 0.0f)
                currentCooldown -= Time.fixedDeltaTime;
            else
            {
                if (currentCooldown < 0)
                    currentCooldown = 0;
            }
        }

        private void SetAnimation()
        {
            var set = IsAttacking ? attacks : idles;
            var clip = set.GetClip(Facing);
            animancer.Play(clip);
        }

        protected virtual void OnAttackAction(InputAction.CallbackContext context)
        {
            var newValue = context.ReadValue<Vector2>();

            if (newValue == Vector2.zero)
            {
                IsAttacking = false;
            }
            else
            {
                IsAttacking = true;
                Facing = newValue;
            }
            
            SetAnimation();
        }

        protected virtual void OnAttack()
        {
            throw new NotImplementedException();
        }
    }
}