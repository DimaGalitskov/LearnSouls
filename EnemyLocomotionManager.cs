using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOULS
{
    public class EnemyLocomotionManager : MonoBehaviour
    {
        EnemyManager enemyManager;
        EnemyAnimator enemyAnimator;

        public CapsuleCollider charatcerCollider;
        public CapsuleCollider characterColliderBlocker;

        public LayerMask detectionLayer;

        private void Awake()
        {
            enemyManager = GetComponent<EnemyManager>();
            enemyAnimator = GetComponentInChildren<EnemyAnimator>();
        }

        private void Start()
        {
            Physics.IgnoreCollision(charatcerCollider, characterColliderBlocker, true);
        }
    }
}