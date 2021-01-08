using System;
using UnityEngine;

namespace UI
{
    public class SyncTransformWithTarget : MonoBehaviour
    {
        [SerializeField] private Transform target;

        private void Update()
        {
            transform.position = target.position;
        }
    }
}