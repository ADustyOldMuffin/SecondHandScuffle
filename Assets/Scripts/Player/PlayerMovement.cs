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
            Movement = InputManager.Instance.InputMaster.Player.Movement.ReadValue<Vector2>();
            Movement = Vector2Int.RoundToInt(Movement);

            Debug.Log(Movement);
            
            var newMove = PixelMovementUtility.PixelPerfectClamp(Movement * (moveSpeed * Time.fixedDeltaTime), spriteRenderer.sprite.pixelsPerUnit);
            var oldPosition =
                PixelMovementUtility.PixelPerfectClamp(myRigidbody.position, spriteRenderer.sprite.pixelsPerUnit);
            
            myRigidbody.MovePosition(oldPosition + newMove);
        }

        private void OnEnable()
        {
            if (InputManager.Instance is null)
                return;
            
            InputManager.Instance.InputMaster.Player.Movement.Enable();
        }

        private void OnDisable()
        {
            if (InputManager.Instance is null)
                return;
            
            InputManager.Instance.InputMaster.Player.Movement.Disable();
            EventBus.Instance.OnPlayerPushRequest -= OnPlayerPushRequest;
        }
    }
}