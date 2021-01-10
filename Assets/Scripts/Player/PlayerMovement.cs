using Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 3f, tweenTime = 0.25f;
        [SerializeField] private Rigidbody2D myRigidbody;
        [SerializeField] private SpriteRenderer spriteRenderer;

        public Vector2 Movement { get; private set; }

        private void Start()
        {
            if (InputManager.Instance is null)
                return;

            InputManager.Instance.InputMaster.Player.VerticalMovement.performed += OnVerticalMovement;
            InputManager.Instance.InputMaster.Player.HorizontalMovement.performed += OnHorizontalMovement;
            EventBus.Instance.OnPlayerPushRequest += OnPlayerPushRequest;
        }

        private void OnPlayerPushRequest(Vector2 direction, float amount)
        {
            // We don't want to move if we're already moving.
            if (LeanTween.isTweening(gameObject))
                return;
            
            var movement = (Vector2)transform.position + (direction * amount);
            LeanTween.move(gameObject, movement, tweenTime).setEase(LeanTweenType.easeOutBack);
        }

        private void FixedUpdate()
        {
            var newMove = PixelMovementUtility.PixelPerfectClamp(Movement * (moveSpeed * Time.fixedDeltaTime), spriteRenderer.sprite.pixelsPerUnit);
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
            EventBus.Instance.OnPlayerPushRequest -= OnPlayerPushRequest;
        }

        private void OnHorizontalMovement(InputAction.CallbackContext context)
        {
            Movement = new Vector2(context.ReadValue<float>(), 0);
        }

        private void OnVerticalMovement(InputAction.CallbackContext context)
        {
            Movement = new Vector2(0,context.ReadValue<float>());
        }
    }
}