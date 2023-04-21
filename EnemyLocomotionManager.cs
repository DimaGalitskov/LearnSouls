using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOULS
{
    public class EnemyLocomotionManager : MonoBehaviour
    {
        EnemyManager enemyManager;
        EnemyAnimator enemyAnimator;

        public LayerMask detectionLayer;

        private void Awake()
        {
            enemyManager = GetComponent<EnemyManager>();
            enemyAnimator = GetComponentInChildren<EnemyAnimator>();
        }
    }
}