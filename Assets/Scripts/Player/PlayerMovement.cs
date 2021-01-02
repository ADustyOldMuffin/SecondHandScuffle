using System;
using Animancer;
using Managers;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private Rigidbody2D myRigidbody;
        [SerializeField] private AnimancerComponent animancer;
        [SerializeField] private DirectionalAnimationSet idles;
        [SerializeField] private DirectionalAnimationSet moving;
        [SerializeField] private Vector2 facing = Vector2.down;

        private Vector2 _movement;

        private void Awake()
        {
            if (InputManager.Instance is null)
                return;

            InputManager.Instance.InputMaster.Player.VerticalMovement.performed +=
                context =>
                {
                    _movement.y = context.ReadValue<float>();
                    UpdateMovementAnimation();
                };

            InputManager.Instance.InputMaster.Player.HorizontalMovement.performed +=
                context =>
                {
                    _movement.x = context.ReadValue<float>();
                    UpdateMovementAnimation();
                };
            
            UpdateMovementAnimation();
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
            var clip = animationSet.GetClip(facing);
            animancer.Play(clip);
        }
        
        private void UpdateMovementAnimation()
        {
            if (_movement != Vector2.zero)
            {
                facing = _movement;
                Play(moving);
            }
            else
            {
                Play(idles);
            }
        }
    }
}