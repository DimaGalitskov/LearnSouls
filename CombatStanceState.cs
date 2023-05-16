using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SOULS
{
    public class CombatStanceState : State
    {
        public AttackState attackState;
        public PursueTargetState pursueTargetState;
        public DeadState deadState;
        public EnemyAttackAction[] enemyAttackActions;

        bool isRandomPointSet = false;
        float verticalMovementValue = 0;
        float horizontalMovementValue = 0;

        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimator enemyAnimator)
        {
            float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
            enemyAnimator.anim.SetFloat("Horizontal", horizontalMovementValue, 0.2f, Time.deltaTime);
            enemyAnimator.anim.SetFloat("Vertical", verticalMovementValue, 0.2f, Time.deltaTime);
            attackState.hasPerformedAttack = false;

            if (enemyManager.isDead)
            {
                return deadState;
            }

            if (enemyManager.isInteracting)
            {
                enemyAnimator.anim.SetFloat("Horizontal", 0);
                enemyAnimator.anim.SetFloat("Vertical", 0);
                return this;
            }

            if (distanceFromTarget > enemyManager.maximumAggroRadius)
            {
                return pursueTargetState;
            }

            if (!isRandomPointSet)
            {
                isRandomPointSet = true;
                DecideCirclingAction(enemyAnimator);
            }

            HandleRotateTowardsTarget(enemyManager);

            if (enemyManager.currentRecoveryTime > 0)
                return this;

            if (attackState.currentAttack != null)
            {
                isRandomPointSet = false;
                return attackState;
            }
            else
            {
                GetNewAttack(enemyManager);
            }

            return this;
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

        private void DecideCirclingAction(EnemyAnimator enemyAnimator)
        {
            //circle with forward movement
            //circle with running
            //circle with walking
            WalkAroundTarget(enemyAnimator);
        }

        private void WalkAroundTarget(EnemyAnimator enemyAnimator)
        {
            verticalMovementValue = 0.5f;

            horizontalMovementValue = Random.Range(-1, 1);

            if (horizontalMovementValue <= 1
                && horizontalMovementValue >= 0 )
            {
                horizontalMovementValue = 0.5f;
            }
            else if (horizontalMovementValue >= -1
                && horizontalMovementValue < 0)
            {
                horizontalMovementValue = -0.5f;
            }
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
                        if (attackState.currentAttack != null)
                            return;

                        temporaryScore += enemyAttackAction.attackScore;

                        if (temporaryScore >= randomValue)
                        {
                            attackState.currentAttack = enemyAttackAction;
                        }
                    }
                }
            }
        }
    }
}