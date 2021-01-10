using System;
using System.Linq;
using Managers;
using UnityEngine;
using Player;

    public class EnemyProjectile : MonoBehaviour
    {

        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private int attackPower = 1;
        [SerializeField] private Rigidbody2D myRigidbody;

        Vector3 aim;
        Vector3 playerPosition;

        void Awake()
        {
            var player = LevelManager.Instance.Player.GetComponent<PlayerMovement>();
            playerPosition = player.transform.position;
        }

        void Start()
        {
            aim = (playerPosition - transform.position).normalized * moveSpeed;
            myRigidbody.velocity = new Vector2(aim.x, aim.y);
            Destroy(gameObject, 10f); //so it doesn't fly off forever
        }

 
        



    }


