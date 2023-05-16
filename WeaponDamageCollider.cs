using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SOULS
{
    public class WeaponDamageCollider : DamageCollider
    {
        private void OnTriggerEnter(Collider other)
        {
            CharacterStats otherStats = other.GetComponent<CharacterStats>();
            CharacterManager otherManager = other.GetComponent<CharacterManager>();
            if (otherStats == null)
                return;

            if (characterTeam == otherManager.characterTeam)
                return;

            otherStats.TakeDamage(currentWeaponDamage);
        }
    }
}