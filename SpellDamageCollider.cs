using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SOULS
{
    public class SpellDamageCollider : DamageCollider
    {
        [Header("Effects")]
        public GameObject projectileFX;
        public GameObject impactFX;

        bool hasCollided = false;
        Vector3 impactNoraml;
        Rigidbody rigidbody;
        CharacterStats spellTarget;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            projectileFX = Instantiate(projectileFX, transform.position, transform.rotation);
            projectileFX.transform.parent = transform;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!hasCollided)
            {
                spellTarget = other.transform.GetComponent<CharacterStats>();
                if (spellTarget != null)
                {
                    spellTarget.TakeDamage(currentWeaponDamage);
                }
                hasCollided = true;
                impactFX = Instantiate(impactFX,
                    other.GetContact(0).point,
                    //Quaternion.FromToRotation(Vector3.up, impactNoraml) *
                    gameObject.transform.rotation * impactFX.transform.rotation);

                Destroy(projectileFX);
                Destroy(impactFX, 1f);
                Destroy(gameObject, 1f);
            }
        }
    }
}