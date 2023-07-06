using System;
using UnityEngine;

namespace Common.Components
{
    public class AutoDestroyer: MonoBehaviour
    {
        [SerializeField]
        private float _timeToDestroy = 1f;
        private void Start()
        {
            Destroy(gameObject, _timeToDestroy);
        }
    }
}