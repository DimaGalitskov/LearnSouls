using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SOULS
{
    [CreateAssetMenu(menuName = "Spells / Healing Spell")]

    public class HealingSpell : SpellItem
    {
        public int healAmount;

        public override void AttemptToCastSpell(PlayerAnimatorManager animatorHandler, PlayerStatsManager playerStats, PlayerWeaponSlotManager weaponSlotManager)
        {
            base.AttemptToCastSpell(animatorHandler, playerStats, weaponSlotManager);
            GameObject instantiatedSpellFX = Instantiate(spellWarupFX, animatorHandler.transform);
            animatorHandler.PlayTargetAnimation(spellAnimation, true);
        }

        public override void SuccessfullyCastSpell(PlayerAnimatorManager animatorHandler, PlayerStatsManager playerStats, PlayerWeaponSlotManager weaponSlotManager)
        {
            base.SuccessfullyCastSpell(animatorHandler, playerStats, weaponSlotManager);
            GameObject instantiatedSpellFX = Instantiate(spellCastFX, animatorHandler.transform);
            instantiatedSpellFX.transform.parent = null;
            animatorHandler.PlayTargetAnimation(spellAnimation, true);
            playerStats.HealPlayer(healAmount);
        }
    }
}