using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SOULS
{
    public class DestroyOnTimer : MonoBehaviour
    {
        public float lifetime;

        private void Awake()
        {
            Destroy(gameObject, lifetime);
        }

    }
}