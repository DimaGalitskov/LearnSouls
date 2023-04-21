using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOULS
{
    public class EnemyManager : MonoBehaviour
    {
        EnemyLocomotionManager enemyLocomotionManager;
        EnemyAnimator enemyAnimator;
        EnemyStats enemyStats;

        public State currentState;
        public CharacterStats currentTarget;

        public bool isPerformingAction;

        [Header("AI Settings")]
        public float detectionRadius = 20;
        public float maximumDetectionAngle = 50;
        public float minimumDetectionAngle = -50;

        public float currentRecoveryTime = 0;

        private void Awake()
        {
            enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
            enemyAnimator = GetComponentInChildren<EnemyAnimator>();
            enemyStats = GetComponent<EnemyStats>();
        }

        // Update is called once per frame
        void Update()
        {
            HandleRecoveryTime();
        }

        private void FixedUpdate()
        {
            HandleState();
        }

        void HandleState()
        {
            if (currentState != null)
            {
                State nextState = currentState.Tick(this, enemyStats, enemyAnimator);

                if (nextState != null)
                {
                    SwitchToNextState(nextState);
                }
            }
        }

        private void SwitchToNextState(State state)
        {
            currentState = state;
        }

        private void HandleRecoveryTime()
        {
            if (currentRecoveryTime > 0)
            {
                currentRecoveryTime -= Time.deltaTime;
            }

            if (isPerformingAction)
            {
                if (currentRecoveryTime <= 0)
                {
                    isPerformingAction = false;
                }
            }
        }

        //#region Attacks
        //
        //private void AttackTarget()
        //{
        //    if (isPerformingAction)
        //        return;
        //
        //    if (currentAttack == null)
        //    {
        //        GetNewAttack();
        //    }
        //    else
        //    {
        //        isPerformingAction = true;
        //        currentRecoveryTime = currentAttack.recoveryTime;
        //        enemyAnimator.PlayTargetAnimation(currentAttack.actionAnimation, true);
        //        currentAttack = null;
        //    }
        //}
        //
        //private void GetNewAttack()
        //{
        //    Vector3 targetDirection = enemyLocomotionManager.currentTarget.transform.position - transform.position;
        //    float viewableAngle = Vector3.Angle(targetDirection, transform.forward);
        //    enemyLocomotionManager.distanceFromTarget = Vector3.Distance(enemyLocomotionManager.currentTarget.transform.position, transform.position);
        //
        //    int maxScore = 0;
        //    for (int i = 0; i < enemyAttackActions.Length; i++)
        //    {
        //        EnemyAttackAction enemyAttackAction = enemyAttackActions[i];
        //
        //        if (enemyLocomotionManager.distanceFromTarget <= enemyAttackAction.maximumDistanceToAttack
        //            && enemyLocomotionManager.distanceFromTarget >= enemyAttackAction.minimumDistanceToAttack)
        //        {
        //            if (viewableAngle <= enemyAttackAction.maximumAttackAngle
        //                && viewableAngle >= enemyAttackAction.minimumAttackAngle)
        //            {
        //                maxScore += enemyAttackAction.attackScore;
        //            }
        //        }
        //    }
        //
        //    int randomValue = Random.Range(0, maxScore);
        //    int temporaryScore = 0;
        //
        //    for (int i = 0; i < enemyAttackActions.Length; i++)
        //    {
        //        EnemyAttackAction enemyAttackAction = enemyAttackActions[i];
        //
        //        if (enemyLocomotionManager.distanceFromTarget <= enemyAttackAction.maximumDistanceToAttack
        //            && enemyLocomotionManager.distanceFromTarget >= enemyAttackAction.minimumDistanceToAttack)
        //        {
        //            if (viewableAngle <= enemyAttackAction.maximumAttackAngle
        //                && viewableAngle >= enemyAttackAction.minimumAttackAngle)
        //            {
        //                if (currentAttack != null)
        //                    return;
        //
        //                temporaryScore += enemyAttackAction.attackScore;
        //
        //                if (temporaryScore > randomValue)
        //                {
        //                    currentAttack = enemyAttackAction;
        //                }
        //            }
        //        }
        //    }
        //}
        //#endregion
    }
}