using System;
using System.Linq;
using Com.LuisPedroFonseca.ProCamera2D;
using UnityEngine;

namespace Managers
{
    public class PlayerCameraManager : SingletonBehavior<PlayerCameraManager>
    {
        [SerializeField] private bool allowCameraShake = true;

        private ProCamera2D _currentCamera;
        
        private void Start()
        {
            SetAllowCameraShake(bool.Parse(PlayerPrefs.GetString("allowCameraShake", "true")));

            if (EventBus.Instance is null)
                return;
            
            EventBus.Instance.OnCameraShakeRequest += OnCameraShakeRequest;
            EventBus.Instance.OnCameraShakeDirectionRequest += OnCameraShakeInDirectionRequest;
        }

        private void OnCameraShakeRequest(string shakePreset)
        {
            // If we don't have a camera, or don't allow shake, or can't get the shake component, or we don't have a shake preset by that name
            // then bail
            if (_currentCamera == null || !allowCameraShake ||
                !_currentCamera.TryGetComponent(out ProCamera2DShake cameraShake) ||
                !cameraShake.ShakePresets.Any(x => x.name == shakePreset))
                return;
            
            cameraShake.Shake(shakePreset);
        }

        private void OnCameraShakeInDirectionRequest(string shakePreset, Vector2 direction)
        {
            if (_currentCamera == null || !allowCameraShake ||
                !_currentCamera.TryGetComponent(out ProCamera2DShake cameraShake) ||
                !cameraShake.ShakePresets.Any(x => x.name == shakePreset))
                return;

            var preset = cameraShake.ShakePresets.Single(x => x.name == shakePreset);

            var angle = Vector2.Angle(Vector2.left, direction);
            preset.InitialAngle = angle;
            
            cameraShake.Shake(preset);
        }

        public void SetAllowCameraShake(bool allow)
        {
            allowCameraShake = allow;
            PlayerPrefs.SetString("allowCameraShake", allow.ToString());
            PlayerPrefs.Save();
        }

        public void SetCamera(Camera newCamera)
        {
            if (!newCamera.TryGetComponent(out ProCamera2D proCamera))
                return;

            _currentCamera = proCamera;
        }
    }
}