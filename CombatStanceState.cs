using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SOULS
{
    public class CombatStanceState : State
    {
        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimator enemyAnimator)
        {
            //Check for attack range
            //Potentially circle player of walk around to flank them
            //If in attack range then return the attack state
            //If in the attack cooldown, return this state and continue circling
            //If player runs out of range then go pursue the target
            return this;
        }
    }
}