using System;
using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Player
{
    public class PlayerWeapon : MonoBehaviour
    {
        [SerializeField] private GameObject[] weapons;
        [SerializeField] private Transform weaponHolder;
        [SerializeField] private float mutationTime = 20f; 

        private GameObject _currentWeapon;
        private readonly List<int> _chosenIndexes = new List<int>();
        private int _currentWeaponIndex;
        [SerializeField] private float _currentMutationTime;

        private void Awake()
        {
            _currentWeapon = Instantiate(weapons.First(), weaponHolder);
            _currentWeaponIndex = 0;
            _chosenIndexes.Add(_currentWeaponIndex);
            _currentWeapon.transform.position = weaponHolder.position;
            _currentWeapon.transform.rotation = weaponHolder.rotation;
            _currentMutationTime = mutationTime;
        }

        private void FixedUpdate()
        {
            _currentMutationTime -= Time.fixedDeltaTime;

            if (!(_currentMutationTime <= 0))
                return;
            
            SetNewRandomWeapon();
            _currentMutationTime = mutationTime;
        }

        private void SetNewRandomWeapon()
        {
            var newIndex = _currentWeaponIndex;
            
            // If we've rotated through all of them then just pick one, if not pick a new one
            if (_chosenIndexes.Count == weapons.Length)
            {
                // Make sure we get a new weapon not the existing one.
                while (newIndex == _currentWeaponIndex && weapons.Length > 1)
                    newIndex = Random.Range(0, weapons.Length);
            }
            else
            {
                while (_chosenIndexes.Contains(newIndex))
                    newIndex = Random.Range(0, weapons.Length);
                
                _chosenIndexes.Add(newIndex);
            }

            // We only need to do all this stuff if it's a new weapon
            if (newIndex != _currentWeaponIndex)
            {
                Destroy(_currentWeapon);
                _currentWeapon = Instantiate(weapons[newIndex], weaponHolder);
                _currentWeaponIndex = newIndex;
                _currentWeapon.transform.position = weaponHolder.position;
                _currentWeapon.transform.rotation = weaponHolder.rotation;
            }

            LevelManager.Instance.IncreaseScore(1);
        }

        public int GetCurrentWeaponIndex()
        {
            return _currentWeaponIndex;
        }


    }

}