using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOULS
{
    public class EnemyAnimator : AnimatorManager
    {
        EnemyManager enemyManager;

        private void Awake()
        {
            anim = GetComponent<Animator>();
            enemyManager = GetComponentInParent<EnemyManager>();
        }

        private void OnAnimatorMove()
        {
            float delta = Time.deltaTime;
            enemyManager.enemyRigidbody.drag = 0;
            Vector3 deltaPostition = anim.deltaPosition;
            deltaPostition.y = 0;
            Vector3 velocity = deltaPostition / delta;
            enemyManager.enemyRigidbody.velocity = velocity;
        }

        public void EnableCombo()
        {
            anim.SetBool("canDoCombo", true);
        }

        public void DisableCombo()
        {
            anim.SetBool("canDoCombo", false);
        }
    }
}