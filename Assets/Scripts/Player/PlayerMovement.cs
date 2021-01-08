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
        [SerializeField] private SpriteRenderer spriteRenderer;

        private Vector2 _facing;
        private Vector2 _movement;

        private void Start()
        {
            if (InputManager.Instance is null)
                return;

            InputManager.Instance.InputMaster.Player.VerticalMovement.performed += OnVerticalMovement;
            InputManager.Instance.InputMaster.Player.HorizontalMovement.performed += OnHorizontalMovement;
        }

        private void FixedUpdate()
        {
            var newMove = PixelMovementUtility.PixelPerfectClamp(_movement * (moveSpeed * Time.fixedDeltaTime), spriteRenderer.sprite.pixelsPerUnit);
            var oldPosition =
                PixelMovementUtility.PixelPerfectClamp(myRigidbody.position, spriteRenderer.sprite.pixelsPerUnit);
            
            myRigidbody.MovePosition(oldPosition + newMove);
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

            InputManager.Instance.InputMaster.Player.VerticalMovement.performed -= OnVerticalMovement;
            InputManager.Instance.InputMaster.Player.HorizontalMovement.performed -= OnHorizontalMovement;
        }

        private void OnHorizontalMovement(InputAction.CallbackContext context)
        {
            _movement.x = context.ReadValue<float>();
        }

        private void OnVerticalMovement(InputAction.CallbackContext context)
        {
            _movement.y = context.ReadValue<float>();
        }
    }
}