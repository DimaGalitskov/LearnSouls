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

        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimator enemyAnimator)
        {
            //Select one of the available attacks based on the attack scores
            //If the selected attack if not able to be used because of bad angle, select a new attack
            //If the attack is viable, then spot the movement and attack the target
            //Set our recovery time to the attacks recovery
            //Return to the combat stance state

            Vector3 targetDirection = enemyManager.currentTarget.transform.position - transform.position;
            float viewableAngle = Vector3.Angle(targetDirection, transform.forward);
            float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);


            if (enemyManager.isPerformingAction)
                return combatStanceState;

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
                            enemyManager.currentRecoveryTime = currentAttack.recoveryTime;
                            currentAttack = null;
                            return combatStanceState;
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
    }
}