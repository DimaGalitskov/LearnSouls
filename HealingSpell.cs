using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SOULS
{
    [CreateAssetMenu(menuName = "Spells / Healing Spell")]

    public class HealingSpell : SpellItem
    {
        public int healAmount;

        public override void AttemptToCastSpell(PlayerAnimator animatorHandler, PlayerStats playerStats)
        {
            base.AttemptToCastSpell(animatorHandler, playerStats);
            GameObject instantiatedWarmupSpellFX = Instantiate(spellWarupFX, animatorHandler.transform);
            animatorHandler.PlayTargetAnimation(spellAnimation, true);
        }

        public override void SuccessfullyCastSpell(PlayerAnimator animatorHandler, PlayerStats playerStats)
        {
            base.SuccessfullyCastSpell(animatorHandler, playerStats);
            GameObject instantiatedCastSpellFX = Instantiate(spellCastFX, animatorHandler.transform);
            animatorHandler.PlayTargetAnimation(spellAnimation, true);
            playerStats.HealPlayer(healAmount);
        }
    }
}