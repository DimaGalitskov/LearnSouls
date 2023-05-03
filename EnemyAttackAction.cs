using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOULS
{
    [CreateAssetMenu(menuName = "AI / Anemy Actions / Attack Actions")]
    public class EnemyAttackAction : EnemyAction
    {
        public ParticleSystem attackParticle;
        public bool canCombo;
        public EnemyAttackAction comboAction;

        public int attackScore = 3;
        public float recoveryTime = 2;

        public float maximumAttackAngle = 35;
        public float minimumAttackAngle = -35;

        public float minimumDistanceToAttack = 0;
        public float maximumDistanceToAttack = 3;
    }
}