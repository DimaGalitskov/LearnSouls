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

        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimator enemyAnimator)
        {
            float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
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

            if (isComboingNextAttack
                && hasPerformedAttack)
            {
                //go back to perform the combo attack
                return this;
            }

            return rotateTowardsTargetState;
        }


        private void AttackTarget(EnemyAnimator enemyAnimator, EnemyManager enemyManager)
        {
            enemyAnimator.PlayTargetAnimation(currentAttack.actionAnimation, true);
            enemyManager.currentRecoveryTime = currentAttack.recoveryTime;
            hasPerformedAttack = true;
        }

        private void AttackTargetWithCombo(EnemyAnimator enemyAnimator, EnemyManager enemyManager)
        {
            enemyAnimator.PlayTargetAnimation(currentAttack.actionAnimation, true);
            enemyManager.currentRecoveryTime = currentAttack.recoveryTime;
            isComboingNextAttack = false;
            currentAttack = null;
        }

        private void RotateTowardsTargetWhileAttacking(EnemyManager enemyManager)
        {
            //Rotate manually
            if (enemyManager.canRotate && enemyManager.isInteracting)
            {
                Vector3 direction = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
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
                }
                else
                {
                    isComboingNextAttack = false;
                    currentAttack = null;
                }
            }
        }
    }
}