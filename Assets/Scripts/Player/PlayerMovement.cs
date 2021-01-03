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
        public Vector2 Facing { get; private set; } = Vector2.down;

        private Vector2 _movement;

        private void Awake()
        {
            if (InputManager.Instance is null)
                return;

            InputManager.Instance.InputMaster.Player.VerticalMovement.performed += OnVerticalMovement;
            InputManager.Instance.InputMaster.Player.HorizontalMovement.performed += OnHorizontalMovement;
            InputManager.Instance.InputMaster.Player.VerticalLook.performed += OnHorizontalLook;
            InputManager.Instance.InputMaster.Player.HorizontalLook.performed += OnVerticalLook;
            
            UpdateMovementAnimation();
        }

        private void OnDestroy()
        {
            if (InputManager.Instance is null)
                return;
            
            animancer.Stop();
            InputManager.Instance.InputMaster.Player.VerticalMovement.performed -= OnVerticalMovement;
            InputManager.Instance.InputMaster.Player.HorizontalMovement.performed -= OnHorizontalMovement;
            InputManager.Instance.InputMaster.Player.VerticalLook.performed -= OnHorizontalLook;
            InputManager.Instance.InputMaster.Player.HorizontalLook.performed -= OnVerticalLook;
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
            InputManager.Instance.InputMaster.Player.VerticalLook.Enable();
            InputManager.Instance.InputMaster.Player.HorizontalLook.Enable();
        }

        private void OnDisable()
        {
            if (InputManager.Instance is null)
                return;
            
            InputManager.Instance.InputMaster.Player.VerticalMovement.Disable();
            InputManager.Instance.InputMaster.Player.HorizontalMovement.Disable();
            InputManager.Instance.InputMaster.Player.VerticalLook.Disable();
            InputManager.Instance.InputMaster.Player.HorizontalLook.Disable();
        }

        private void Play(DirectionalAnimationSet animationSet)
        {
            var clip = animationSet.GetClip(Facing);
            animancer.Play(clip);
        }
        
        private void UpdateMovementAnimation()
        {
            Play(_movement != Vector2.zero ? moving : idles);
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

        private void OnHorizontalLook(InputAction.CallbackContext context)
        {
            Facing = new Vector2(0, context.ReadValue<float>());
            UpdateMovementAnimation();
        }

        private void OnVerticalLook(InputAction.CallbackContext context)
        {
            Facing = new Vector2(context.ReadValue<float>(), 0);
            UpdateMovementAnimation();
        }
    }
}