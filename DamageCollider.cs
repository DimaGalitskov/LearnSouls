using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOULS
{
    public class DamageCollider : MonoBehaviour
    {
        Collider damageCollider;
        public CharacterTeam characterTeam;
        public int currentWeaponDamage = 25;
        public bool enabledOnSpawn;
        public bool destroyColliderOnTimer;
        public float destroyColliderTimer;


        private void Awake()
        {
            damageCollider = GetComponent<Collider>();
            damageCollider.gameObject.SetActive(true);
            damageCollider.isTrigger = true;
            damageCollider.enabled = enabledOnSpawn;

            if (destroyColliderOnTimer)
            {
                Destroy(damageCollider, destroyColliderTimer);
            }
        }

        public void EnableDamageCollider()
        {
            damageCollider.enabled = true;

        }

        public void DisableDamageCollider()
        {
            damageCollider.enabled = false;
        }
    }
}