using System;
using System.Numerics;
using Animancer;
using Managers;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private DirectionalAnimationSet idles, moving;
        [SerializeField] private AnimancerComponent animancer;
        [SerializeField] private PlayerMovement movementComponent;

        public Vector2 Facing { get; private set; } = Vector2.down;

        private void Awake()
        {
            if (EventBus.Instance is null)
                return;
            
            EventBus.Instance.OnFacingDirectionChange += OnFacingDirectionChange;
        }

        private void OnFacingDirectionChange(Vector2 direction)
        {
            Facing = direction;
        }
        
        private void SetAnimation()
        {
            var set = movementComponent.Movement == Vector2.zero ? idles : moving;
            var clip = set.GetClip(Facing);
            animancer.Play(clip);
        }
    }
}