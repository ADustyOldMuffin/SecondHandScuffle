using System;
using System.Numerics;
using Animancer;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

namespace Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private DirectionalAnimationSet idles, moving;
        [SerializeField] private AnimancerComponent animancer;

        public Vector2 Facing { get; private set; } = Vector2.down;

        private bool _isMoving = false;

        private void Awake()
        {
            if (InputManager.Instance is null)
                return;
            
            InputManager.Instance.InputMaster.Player.Look.performed += OnLook;
            
        }

        private void Start()
        {
            if (EventBus.Instance is null)
                return;
            
            EventBus.Instance.PlayerFacingDirectionChange(Facing);
            SetAnimation();
        }

        private void OnEnable()
        {
            if (InputManager.Instance is null)
                return;
            
            InputManager.Instance.InputMaster.Player.Look.Enable();
        }

        private void OnDisable()
        {
            if (InputManager.Instance is null)
                return;
            
            InputManager.Instance.InputMaster.Player.Look.Disable();
            InputManager.Instance.InputMaster.Player.Look.performed -= OnLook;
        }

        private void Update()
        {
            var movement = InputManager.Instance.InputMaster.Player.Movement.ReadValue<Vector2>();

            if (movement == Vector2.zero && _isMoving)
            {
                _isMoving = false;
                SetAnimation();
            }
            else if (movement != Vector2.zero && !_isMoving)
            {
                _isMoving = true;
                SetAnimation();
            }

            var look = InputManager.Instance.InputMaster.Player.Look.ReadValue<Vector2>();

            if (look == Vector2.zero)
                return;

            if (look == Facing)
                return;

            Facing = look;
            SetAnimation();
            EventBus.Instance.PlayerFacingDirectionChange(Facing);
        }

        private void OnLook(InputAction.CallbackContext context)
        {
            var newDirection = InputManager.Instance.InputMaster.Player.Look.ReadValue<Vector2>();
            //Debug.Log(newDirection);

            // This means that the player taken a finger off a button, but we want to know if they're still pressing one.
            if (newDirection == Vector2.zero)
                newDirection = InputManager.Instance.InputMaster.Player.Look.ReadValue<Vector2>();

            if (newDirection == Vector2.zero)
                return;

            Facing = newDirection;
            EventBus.Instance.PlayerFacingDirectionChange(Facing);
            SetAnimation();
        }

        private void SetAnimation()
        {
            var set = _isMoving ? moving : idles;
            var clip = set.GetClip(Facing);
            animancer.Play(clip);
        }
    }
}