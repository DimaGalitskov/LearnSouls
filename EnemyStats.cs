using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SOULS
{
    public class EnemyStats : CharacterStats
    {
        EnemyAnimator enemyAnimator;
        EnemyManager enemyManager;

        private void Awake()
        {
            enemyManager = GetComponent<EnemyManager>();
            enemyAnimator = GetComponentInChildren<EnemyAnimator>();
        }

        private void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
        }

        int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public void TakeDamage(int damage)
        {
            if (enemyManager.isDead)
                return;

            currentHealth -= damage;

            enemyAnimator.PlayTargetAnimation("Damaged", true);

            if (currentHealth <= 0)
            {
                enemyManager.isDead = true;
                currentHealth = 0;
                enemyAnimator.PlayTargetAnimation("Dying", true);
            }
        }
    }
}