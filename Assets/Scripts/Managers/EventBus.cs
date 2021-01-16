using System;
using Constants;
using UnityEngine;
using Weapons;

namespace Managers
{
    public class EventBus : SingletonBehavior<EventBus>
    {
        /// <summary>
        /// Called when the player dies.
        /// </summary>
        public event Action OnPlayerDeath;
        
        /// <summary>
        /// Called when the player's health has changed.
        /// </summary>
        /// <para>Will be called with <c>(int newValue)</c> where newValue is what the player's health has been changed to.</para>
        public event Action<int> OnPlayerHealthChange;
        
        /// <summary>
        /// Called when an object wishes to change the health of the player. NOTE - this does not guarantee that the player's health will actually change just request it.
        /// </summary>
        /// <para>Will be called with <c>(int changeValue) where changeValue is what the player's health should be changed to.</c></para>
        public event Action<int> OnPlayerHealthChangeRequest;
        
        /// <summary>
        /// Called when the player's weapon has changed.
        /// </summary>
        /// <para>Will be called with <code>(BaseWeapon changedFrom, BaseWeapon changedTo)</code></para>
        public event Action<BaseWeapon, BaseWeapon> OnWeaponChange;
        
        /// <summary>
        /// Called when the player changes the direction they are facing.
        /// </summary>
        /// <para>Will be called with <c>(Vector2 newDirection).</c></para>
        public event Action<Vector2> OnFacingDirectionChange;
        
        /// <summary>
        /// Called when something wishes to change the level.
        /// </summary>
        /// <para>Will be called with <c>(Level newLevel).</c></para>
        public event Action<Level> OnLevelChangeRequest;

        /// <summary>
        /// Called when a projectile the player threw is returned.
        /// </summary>
        /// /// <para>Will be called with <c>(GameObject projectileGameObject).</c></para>
        public event Action<GameObject> OnPlayerProjectileReturn;

        /// <summary>
        /// Called when something wants to shake the camera.
        /// </summary>
        public event Action<string> OnCameraShakeRequest;

        /// <summary>
        /// Called when something wants to shake the camera in a specific direction
        /// </summary>
        public event Action<string, Vector2> OnCameraShakeDirectionRequest;
        
        /// <summary>
        /// Called when something wants to push the player.
        /// </summary>
        public event Action<Vector2, float> OnPlayerPushRequest;

        public event Action<BaseWeapon> OnWeaponStatusChange;

        /// <summary>
        /// Invoke <see cref="OnPlayerDeath"/>.
        /// </summary>
        public void PlayerDied()
        {
            OnPlayerDeath?.Invoke();
        }

        /// <summary>
        /// Invoke <see cref="OnPlayerHealthChangeRequest"/>
        /// </summary>
        /// <param name="changeAmount">The amount you wish to change the player's health by</param>
        public void ChangePlayerHealthRequest(int changeAmount)
        {
            OnPlayerHealthChange?.Invoke(changeAmount);
        }

        /// <summary>
        /// Invoke <see cref="OnWeaponChange"/>
        /// </summary>
        /// <param name="oldWeapon">The weapon that was active before the new one.</param>
        /// <param name="newWeapon">The new weapon being switched to.</param>
        public void WeaponChanged(BaseWeapon oldWeapon, BaseWeapon newWeapon)
        {
            OnWeaponChange?.Invoke(oldWeapon, newWeapon);
        }

        /// <summary>
        /// Invoke <see cref="OnLevelChangeRequest"/>
        /// </summary>
        /// <param name="newLevel">The level to change to.</param>
        public void ChangeLevel(Level newLevel)
        {
            OnLevelChangeRequest?.Invoke(newLevel);
        }
        
        /// <summary>
        /// Invoke <see cref="OnLevelChangeRequest"/>
        /// </summary>
        /// <param name="newLevel">The level to change to.</param>
        public void ChangeLevel(int newLevel)
        {
            OnLevelChangeRequest?.Invoke((Level)newLevel);
        }

        /// <summary>
        /// Invoke <see cref="OnPlayerProjectileReturn"/>
        /// </summary>
        /// <param name="projectile">The GameObject of the projectile returned.</param>
        public void WeaponProjectileReturned(GameObject projectile)
        {
            OnPlayerProjectileReturn?.Invoke(projectile);
        }

        /// <summary>
        /// Invoke <see cref="OnCameraShakeRequest"/>
        /// </summary>
        /// <param name="presetName">The name of the preset you wish to use.</param>
        public void ShakeCameraRequest(string presetName)
        {
            OnCameraShakeRequest?.Invoke(presetName);
        }

        /// <summary>
        /// Invoke <see cref="OnCameraShakeDirectionRequest"/>
        /// </summary>
        /// <param name="presetName">The name of the preset you wish to use.</param>
        /// <param name="direction">The direction the shake should be in.</param>
        public void ShakeCameraRequest(string presetName, Vector2 direction)
        {
            OnCameraShakeDirectionRequest?.Invoke(presetName, direction);
        }

        /// <summary>
        /// Invoke <see cref="OnPlayerPushRequest"/>
        /// </summary>
        /// <param name="direction">The direction to push the player.</param>
        /// <param name="amount">The amount to push the player.</param>
        public void PlayerPushRequest(Vector2 direction, float amount)
        {
            OnPlayerPushRequest?.Invoke(direction, amount);
        }

        public void PlayerFacingDirectionChange(Vector2 newDirection)
        {
            OnFacingDirectionChange?.Invoke(newDirection);
        }

        public void WeaponStatusChanged(BaseWeapon weapon)
        {
            OnWeaponStatusChange?.Invoke(weapon);
        }
    }
}