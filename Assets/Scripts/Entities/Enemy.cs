using System;
using Pathfinding;
using States;
using States;
using UnityEngine;
using UnityEngine.Serialization;

namespace Entities
{
    public class Enemy : BaseEntity 
    {
        public GameObject Target { get; set; }

        [SerializeField] protected float moveSpeed = 400f, nextWaypointDistance = 3f, timeTillSearchWhenStuck = 1f;
        [SerializeField] protected Rigidbody2D myRigidbody;
        [SerializeField] protected Transform spriteContainer;
        [SerializeField] protected bool shouldAttackPlayer = true;

        public Seeker seeker;

        public int currentIndex;
        public Path CurrentPath;
    }
}