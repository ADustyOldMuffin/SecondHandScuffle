using System;
using Managers;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private Rigidbody2D myRigidbody;

        private Vector2 _movement;

        private void Awake()
        {
            if (InputManager.Instance is null)
                return;

            InputManager.Instance.InputMaster.Player.VerticalMovement.performed +=
                context => _movement.y = context.ReadValue<float>();

            InputManager.Instance.InputMaster.Player.HorizontalMovement.performed +=
                context => _movement.x = context.ReadValue<float>();
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
    }
}