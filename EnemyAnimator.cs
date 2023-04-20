using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOULS
{
    public class EnemyAnimator : AnimatorManager
    {
        EnemyLocomotionManager enemyLocomotionManager;

        private void Awake()
        {
            anim = GetComponent<Animator>();
            enemyLocomotionManager = GetComponentInParent<EnemyLocomotionManager>();
        }

        private void OnAnimatorMove()
        {
            float delta = Time.deltaTime;
            enemyLocomotionManager.enemyRigidbody.drag = 0;
            Vector3 deltaPostition = anim.deltaPosition;
            deltaPostition.y = 0;
            Vector3 velocity = deltaPostition / delta;
            enemyLocomotionManager.enemyRigidbody.velocity = velocity;
        }
    }
}