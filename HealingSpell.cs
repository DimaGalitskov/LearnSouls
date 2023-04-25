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
            GameObject instantiatedWarmupSpellFX = Instantiate(spellWarupFX, animatorHandler.transform);
            animatorHandler.PlayTargetAnimation(spellAnimation, true);
            Debug.Log("Attempting to cast");
        }

        public override void SuccessfullyCastSpell(PlayerAnimator animatorHandler, PlayerStats playerStats)
        {
            GameObject instantiatedCastSpellFX = Instantiate(spellCastFX, animatorHandler.transform);
            animatorHandler.PlayTargetAnimation(spellAnimation, true);
            playerStats.currentHealth += healAmount;
            Debug.Log("Casting");
        }
    }
}