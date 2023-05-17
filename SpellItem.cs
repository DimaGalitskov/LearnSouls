using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SOULS
{
    public enum SpellType
    {
        Faith,
        Magic,
        Pyro
    }

    public class SpellItem : Item
    {
        public GameObject spellWarupFX;
        public GameObject spellCastFX;
        public string spellAnimation;

        [Header("Spell Cost")]
        public int staminaCost;

        [Header("Spell Type")]
        public SpellType spellType;

        [Header("Spell Description")]
        [TextArea]
        public string spellDescription;

        public virtual void AttemptToCastSpell(PlayerAnimatorManager animatorHandler, PlayerStatsManager playerStats, PlayerWeaponSlotManager weaponSlotManager)
        {
            Debug.Log("You attempt to cast");
        }

        public virtual void SuccessfullyCastSpell(PlayerAnimatorManager animatorHandler, PlayerStatsManager playerStats, PlayerWeaponSlotManager weaponSlotManager)
        {
            Debug.Log("You successfully cast");
            playerStats.DrainStamina(staminaCost);
        }
    }
}