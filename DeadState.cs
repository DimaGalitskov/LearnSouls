using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOULS
{
    public class DeadState : State
    {
        public override State Tick(EnemyManager enemyManager, EnemyStatsManager enemyStats, EnemyAnimatorManager enemyAnimator)
        {
            return this;
        }
    }
}