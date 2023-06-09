using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SOULS
{
    public class AmbushState : State
    {
        public bool isSleeping;
        public float detectionRaduis = 2;
        public float sleepRecoveryTime;
        public string sleepAnimation;
        public string wakeAnimation;

        public LayerMask detectionLayer;

        public PursueTargetState pursueTargetState;

        public override State Tick(EnemyManager enemyManager, EnemyStatsManager enemyStats, EnemyAnimatorManager enemyAnimator)
        {
            if (isSleeping
                && enemyManager.isInteracting == false)
            {
                enemyAnimator.PlayTargetAnimation(sleepAnimation, true);
            }

            #region Handle Target Detection

            Collider[] colliders = Physics.OverlapSphere(enemyManager.transform.position, detectionRaduis, detectionLayer);

            for (int i = 0; i < colliders.Length; i++)
            {
                CharacterStatsManager characterStats = colliders[i].transform.GetComponent<CharacterStatsManager>();

                if (characterStats != null)
                {
                    Vector3 targetDirection = characterStats.transform.position - enemyManager.transform.position;
                    float viewableAngle = Vector3.Angle(targetDirection, enemyManager.transform.forward);

                    if (viewableAngle > enemyManager.minimumDetectionAngle
                        && viewableAngle < enemyManager.maximumDetectionAngle)
                    {
                        enemyManager.characterStatsManager = characterStats;
                        isSleeping = false;
                        enemyManager.isPerformingAction = true;
                        enemyManager.currentRecoveryTime = sleepRecoveryTime;
                        enemyAnimator.PlayTargetAnimation(wakeAnimation, true);
                    }
                }
            }

            #endregion

            #region Handle State Change

            if (enemyManager.characterStatsManager != null)
            {
                return pursueTargetState;
            }
            else
            {
                return this;
            }

            #endregion
        }
    }
}