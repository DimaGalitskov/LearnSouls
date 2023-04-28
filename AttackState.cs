using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SOULS
{
    public class AttackState : State
    {
        public CombatStanceState combatStanceState;

        public EnemyAttackAction[] enemyAttackActions;
        public EnemyAttackAction currentAttack;

        bool isComboing = false;

        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimator enemyAnimator)
        {
            if (enemyManager.isInteracting && enemyManager.canDoCombo == false)
            {
                return this;
            }
            else if (enemyManager.isInteracting && enemyManager.canDoCombo)
            {
                if (isComboing)
                {
                    enemyAnimator.PlayTargetAnimation(currentAttack.actionAnimation, true);
                    isComboing = false;
                }
            }


            Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
            float viewableAngle = Vector3.Angle(targetDirection, enemyManager.transform.forward);
            float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);

            HandleRotateTowardsTarget(enemyManager);

            if (enemyManager.isPerformingAction)
            {
                return combatStanceState;
            }

            if (currentAttack != null)
            {
                //If we are too close to perform the attack, get a new attack
                if (distanceFromTarget < currentAttack.minimumDistanceToAttack)
                {
                    return this;
                }
                //If we are close enough then we proceed
                else if (distanceFromTarget < currentAttack.maximumDistanceToAttack)
                {
                    //If our enemy is within our attack viewable angle, then we attack
                    if (viewableAngle <= currentAttack.maximumAttackAngle
                        && viewableAngle >= currentAttack.minimumAttackAngle)
                    {
                        if (enemyManager.currentRecoveryTime <= 0
                            && enemyManager.isPerformingAction == false)
                        {
                            enemyAnimator.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                            enemyAnimator.anim.SetFloat("Horizontal", 0, 0.1f, Time.deltaTime);
                            enemyAnimator.PlayTargetAnimation(currentAttack.actionAnimation, true);
                            enemyManager.isPerformingAction = true;

                            if (currentAttack.canCombo)
                            {
                                currentAttack = currentAttack.comboAction;
                                return this;
                            }
                            else
                            {
                                enemyManager.currentRecoveryTime = currentAttack.recoveryTime;
                                currentAttack = null;
                                return combatStanceState;
                            }
                        }
                    }
                }
            }
            else
            {
                GetNewAttack(enemyManager);
            }

            return combatStanceState;
        }


        private void GetNewAttack(EnemyManager enemyManager)
        {
            Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
            float viewableAngle = Vector3.Angle(targetDirection, enemyManager.transform.forward);
            float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);

            int maxScore = 0;
            for (int i = 0; i < enemyAttackActions.Length; i++)
            {
                EnemyAttackAction enemyAttackAction = enemyAttackActions[i];

                if (distanceFromTarget <= enemyAttackAction.maximumDistanceToAttack
                    && distanceFromTarget >= enemyAttackAction.minimumDistanceToAttack)
                {
                    if (viewableAngle <= enemyAttackAction.maximumAttackAngle
                        && viewableAngle >= enemyAttackAction.minimumAttackAngle)
                    {
                        maxScore += enemyAttackAction.attackScore;
                    }
                }
            }

            int randomValue = Random.Range(0, maxScore);
            int temporaryScore = 0;

            for (int i = 0; i < enemyAttackActions.Length; i++)
            {
                EnemyAttackAction enemyAttackAction = enemyAttackActions[i];

                if (distanceFromTarget <= enemyAttackAction.maximumDistanceToAttack
                    && distanceFromTarget >= enemyAttackAction.minimumDistanceToAttack)
                {
                    if (viewableAngle <= enemyAttackAction.maximumAttackAngle
                        && viewableAngle >= enemyAttackAction.minimumAttackAngle)
                    {
                        if (currentAttack != null)
                            return;

                        temporaryScore += enemyAttackAction.attackScore;

                        if (temporaryScore > randomValue)
                        {
                            currentAttack = enemyAttackAction;
                        }
                    }
                }
            }
        }

        private void HandleRotateTowardsTarget(EnemyManager enemyManager)
        {
            //Rotate manually
            if (enemyManager.isPerformingAction)
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
            //Rotate using Navmesh
            else
            {
                Vector3 relativeDirection = transform.InverseTransformDirection(enemyManager.navMeshAgent.desiredVelocity);
                Vector3 targetVelocity = enemyManager.enemyRigidbody.velocity;

                enemyManager.navMeshAgent.enabled = true;
                enemyManager.navMeshAgent.SetDestination(enemyManager.currentTarget.transform.position);
                enemyManager.enemyRigidbody.velocity = targetVelocity;
                enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, enemyManager.navMeshAgent.transform.rotation, enemyManager.rotationSpeed / Time.deltaTime);
            }
        }
    }
}