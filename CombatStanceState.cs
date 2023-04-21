using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SOULS
{
    public class CombatStanceState : State
    {
        public AttackState attackState;
        public PursueTargetState pursueTargetState;

        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimator enemyAnimator)
        {
            enemyManager.distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
            //Check for attack range
            //Potentially circle player of walk around to flank them
            //If in attack range then return the attack state
            if (enemyManager.currentRecoveryTime <= 0
                && enemyManager.distanceFromTarget <= enemyManager.maximumAttackRange)
            {
                return attackState;
            }
            else if (enemyManager.distanceFromTarget > enemyManager.maximumAttackRange)
            {
                return pursueTargetState;
            }
            else
            {
                return this;
            }
            //If in the attack cooldown, return this state and continue circling
            //If player runs out of range then go pursue the target
        }
    }
}