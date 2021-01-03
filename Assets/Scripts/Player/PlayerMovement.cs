using System;
using Animancer;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private Rigidbody2D myRigidbody;
        [SerializeField] private AnimancerComponent animancer;
        [SerializeField] private DirectionalAnimationSet idles;
        [SerializeField] private DirectionalAnimationSet moving;

        private Vector2 _facing;
        private Vector2 _movement;

        private void Awake()
        {
            if (InputManager.Instance is null)
                return;

            InputManager.Instance.InputMaster.Player.VerticalMovement.performed += OnVerticalMovement;
            InputManager.Instance.InputMaster.Player.HorizontalMovement.performed += OnHorizontalMovement;
            LevelManager.OnPlayerDirectionChange += OnPlayerDirectionChanged;
            
            UpdateMovementAnimation();
        }

        private void OnDestroy()
        {
            if (InputManager.Instance is null)
                return;
            
            animancer.Stop();
            InputManager.Instance.InputMaster.Player.VerticalMovement.performed -= OnVerticalMovement;
            InputManager.Instance.InputMaster.Player.HorizontalMovement.performed -= OnHorizontalMovement;
            LevelManager.OnPlayerDirectionChange -= OnPlayerDirectionChanged;
        }

        private void FixedUpdate()
        {
            myRigidbody.MovePosition(myRigidbody.position + _movement * (moveSpeed * Time.fixedDeltaTime));
        }

        private void OnEnable()
        {
            if (InputManager.Instance is null)
                return;
            
            InputManager.Instance.InputMaster.Player.VerticalMovement.Enable();
            InputManager.Instance.InputMaster.Player.HorizontalMovement.Enable();
        }

        private void OnDisable()
        {
            if (InputManager.Instance is null)
                return;
            
            InputManager.Instance.InputMaster.Player.VerticalMovement.Disable();
            InputManager.Instance.InputMaster.Player.HorizontalMovement.Disable();
        }

        private void Play(DirectionalAnimationSet animationSet)
        {
            var clip = animationSet.GetClip(_facing);
            animancer.Play(clip);
        }
        
        private void UpdateMovementAnimation()
        {
            Play(_movement != Vector2.zero ? moving : idles);
        }

        private void OnPlayerDirectionChanged(Vector2 direction)
        {
            _facing = direction;
            UpdateMovementAnimation();
        }

        private void OnHorizontalMovement(InputAction.CallbackContext context)
        {
            _movement.x = context.ReadValue<float>();
            UpdateMovementAnimation();
        }

        private void OnVerticalMovement(InputAction.CallbackContext context)
        {
            _movement.y = context.ReadValue<float>();
            UpdateMovementAnimation();
        }
    }
}