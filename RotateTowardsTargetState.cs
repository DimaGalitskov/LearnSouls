using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;


namespace SOULS
{
    public class RotateTowardsTargetState : State
    {
        public CombatStanceState combatStanceState;
        public DeadState deadState;

        public override State Tick(EnemyManager enemyManager, EnemyStatsManager enemyStats, EnemyAnimatorManager enemyAnimator)
        {
            enemyAnimator.anim.SetFloat("Horizontal", 0);
            enemyAnimator.anim.SetFloat("Vertical", 0);

            Vector3 targetDirection = enemyManager.characterStatsManager.transform.position - enemyManager.transform.position;
            float viewableAngle = Vector3.SignedAngle(targetDirection, enemyManager.transform.forward, Vector3.up);

            if (enemyManager.isDead)
                return deadState;

            if (enemyManager.isInteracting)
                return this;

            if (viewableAngle >= 45
                && viewableAngle < 180)
            {
                enemyAnimator.PlayTargetAnimationWithRootRotation("Turning Left", true);
                return combatStanceState;
            }
            else if (viewableAngle > -180
                && viewableAngle <= -45)
            {
                enemyAnimator.PlayTargetAnimationWithRootRotation("Turning Right", true);
                return combatStanceState;
            }

            return combatStanceState;
        }
    }
}