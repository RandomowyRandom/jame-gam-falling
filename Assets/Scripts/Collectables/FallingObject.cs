using System;
using UnityEngine;

namespace Collectables
{
    public class FallingObject: MonoBehaviour
    {
        [SerializeField]
        private float _speed = 1f;

        private void FixedUpdate()
        {
            transform.position += Vector3.up * (_speed * Time.fixedDeltaTime);
        }
    }
}