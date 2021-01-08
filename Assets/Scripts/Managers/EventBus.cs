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
        public event Action<GameObject> OnPlayerProjectileReturn;

        /// <summary>
        /// Invoke <see cref="OnPlayerDeath"/>.
        /// </summary>
        public void PlayerDied()
        {
            OnPlayerDeath?.Invoke();
        }

        
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
    }
}