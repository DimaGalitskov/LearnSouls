using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SOULS
{
    public class WeaponDamageCollider : DamageCollider
    {
        private void OnTriggerEnter(Collider other)
        {
            CharacterStatsManager otherStats = other.GetComponent<CharacterStatsManager>();
            CharacterManager otherManager = other.GetComponent<CharacterManager>();
            if (otherStats == null)
                return;

            if (characterTeam == otherManager.characterTeam)
                return;

            otherStats.TakeDamage(currentWeaponDamage);
        }
    }
}