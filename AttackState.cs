using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SOULS
{
    public class AttackState : State
    {
        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimator enemyAnimator)
        {
            //Select one of the available attacks based on the attack scores
            //If the selected attack if not able to be used because of bad angle, select a new attack
            //If the attack is viable, then spot the movement and attack the target
            //Set our recovery time to the attacks recovery
            //Return to the combat stance state
            return this;

        }
    }
}