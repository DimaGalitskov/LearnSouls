using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOULS
{
    public class ParticleUse : MonoBehaviour
    {
        ParticleSystem particleSystem;

        private void Awake()
        {
            particleSystem = GetComponent<ParticleSystem>();
        }

        public void ParticleActivate()
        {
            particleSystem.Play();
        }
    }
}
