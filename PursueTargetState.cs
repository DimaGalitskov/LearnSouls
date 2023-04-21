using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SOULS
{
    public class PursueTargetState : State
    {
        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimator enemyAnimator)
        {
            //Chase the target
            //If within the attack range, switch to combat stance state
            //If the target is out of range, return this state and keep chasing
            return this;
        }
    }
}