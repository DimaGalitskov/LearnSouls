using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOULS
{
    public class EnemyAnimatorManager : AnimatorManager
    {
        EnemyManager enemyManager;

        protected override void Awake()
        {
            base.Awake();
            anim = GetComponent<Animator>();
            enemyManager = GetComponent<EnemyManager>();
        }

        private void OnAnimatorMove()
        {
            float delta = Time.deltaTime;
            enemyManager.enemyRigidbody.drag = 0;
            Vector3 deltaPostition = anim.deltaPosition;
            deltaPostition.y = 0;
            Vector3 velocity = deltaPostition / delta;
            enemyManager.enemyRigidbody.velocity = velocity;

            if (enemyManager.isRootRotating)
            {
                enemyManager.transform.rotation *= anim.deltaRotation;
            }
        }
    }
}