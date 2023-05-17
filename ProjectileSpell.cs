using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOULS
{

    [CreateAssetMenu(menuName = "Spells/Projectile")]
    public class ProjectileSpell : SpellItem
    {
        [Header("Projectile Damage")]
        public float baseDamage;

        [Header("Projectile Physics")]
        public float projectileVelocity;
        public float projectileVerticalVelocity;
        public bool isAffectedByGravity;
        public float projectileMass;
        Rigidbody rigidbody;

        public override void AttemptToCastSpell(PlayerAnimatorManager animatorHandler, PlayerStatsManager playerStats, PlayerWeaponSlotManager weaponSlotManager)
        {
            base.AttemptToCastSpell(animatorHandler, playerStats, weaponSlotManager);
            GameObject instantiatedSpellFX = Instantiate(spellWarupFX, animatorHandler.transform);
            animatorHandler.PlayTargetAnimation(spellAnimation, true);
        }

        public override void SuccessfullyCastSpell(PlayerAnimatorManager animatorHandler, PlayerStatsManager playerStats, PlayerWeaponSlotManager weaponSlotManager)
        {
            base.SuccessfullyCastSpell(animatorHandler, playerStats, weaponSlotManager);
            GameObject instantiatedSpellFX = Instantiate(
                spellCastFX,
                weaponSlotManager.leftHandSlot.transform.position,
                animatorHandler.transform.rotation);
            rigidbody = instantiatedSpellFX.GetComponent<Rigidbody>();
            //get damage from the spell

            rigidbody.AddForce(instantiatedSpellFX.transform.forward * projectileVelocity);
            rigidbody.AddForce(instantiatedSpellFX.transform.up * projectileVerticalVelocity);
            rigidbody.useGravity = isAffectedByGravity;
            rigidbody.mass = projectileMass;
            instantiatedSpellFX.transform.parent = null;
        }
    }
}