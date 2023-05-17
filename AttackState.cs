using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SOULS
{
    public class AttackState : State
    {
        public RotateTowardsTargetState rotateTowardsTargetState;
        public CombatStanceState combatStanceState;
        public PursueTargetState pursueTargetState;
        public EnemyAttackAction currentAttack;

        bool isComboingNextAttack = false;
        public bool hasPerformedAttack = false;

        public override State Tick(EnemyManager enemyManager, EnemyStatsManager enemyStats, EnemyAnimatorManager enemyAnimator)
        {
            float distanceFromTarget = Vector3.Distance(enemyManager.characterStatsManager.transform.position, enemyManager.transform.position);
            RotateTowardsTargetWhileAttacking(enemyManager);


            if (distanceFromTarget > enemyManager.maximumAggroRadius)
            {
                return pursueTargetState;
            }

            if (isComboingNextAttack
                && enemyManager.canDoCombo)
            {
                AttackTargetWithCombo(enemyAnimator, enemyManager);
            }

            if (!hasPerformedAttack)
            {
                AttackTarget(enemyAnimator, enemyManager);
                RollForComboChance(enemyManager);
            }

            if (enemyManager.currentRecoveryTime < 0)
            {
                return rotateTowardsTargetState;
            }

            if (isComboingNextAttack
                && hasPerformedAttack)
            {
                return this;
            }

            return rotateTowardsTargetState;
        }


        private void AttackTarget(EnemyAnimatorManager enemyAnimator, EnemyManager enemyManager)
        {
            enemyAnimator.PlayTargetAnimation(currentAttack.actionAnimation, true);
            enemyAnimator.SetActionParticle(currentAttack.attackParticle);
            enemyManager.currentRecoveryTime = currentAttack.recoveryTime;
            hasPerformedAttack = true;
        }

        private void AttackTargetWithCombo(EnemyAnimatorManager enemyAnimator, EnemyManager enemyManager)
        {
            enemyAnimator.PlayTargetAnimation(currentAttack.actionAnimation, true);
            enemyAnimator.SetActionParticle(currentAttack.attackParticle);
            enemyManager.currentRecoveryTime = currentAttack.recoveryTime;
            isComboingNextAttack = false;
            currentAttack = null;
        }

        private void RotateTowardsTargetWhileAttacking(EnemyManager enemyManager)
        {
            //Rotate manually
            if (enemyManager.canRotate && enemyManager.isInteracting)
            {
                Vector3 direction = enemyManager.characterStatsManager.transform.position - enemyManager.transform.position;
                direction.y = 0;
                direction.Normalize();

                if (direction == Vector3.zero)
                {
                    direction = enemyManager.transform.forward;
                }

                Quaternion targetRotation = Quaternion.LookRotation(direction);
                enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, targetRotation, enemyManager.rotationSpeed / Time.deltaTime);
            }

        }

        private void RollForComboChance(EnemyManager enemyManager)
        {
            float comboRoll = Random.Range(0, 100);

            if (enemyManager.isAllowedCombo && comboRoll <= enemyManager.comboChance)
            {
                if (currentAttack.comboAction != null)
                {
                    isComboingNextAttack = true;
                    currentAttack = currentAttack.comboAction;
                    return;
                }
            }

            isComboingNextAttack = false;
            currentAttack = null;
        }
    }
}