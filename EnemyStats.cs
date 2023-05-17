using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SOULS
{
    public class EnemyStatsManager : CharacterStatsManager
    {
        EnemyAnimatorManager enemyAnimator;
        EnemyManager enemyManager;
        UIEnemyHealthBar enemyHealthBar;

        private void Awake()
        {
            enemyManager = GetComponent<EnemyManager>();
            enemyAnimator = GetComponent<EnemyAnimatorManager>();
            enemyHealthBar = GetComponentInChildren<UIEnemyHealthBar>();
        }

        private void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            enemyHealthBar.SetMaxHealth(maxHealth);
        }

        int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public override void TakeDamage(int damage)
        {
            if (enemyManager.isDead)
                return;

            currentHealth -= damage;
            enemyHealthBar.SetCurrentHealth(currentHealth);
            enemyAnimator.PlayTargetAnimation("Damaged", true);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                enemyManager.isDead = true;
                enemyAnimator.PlayTargetAnimation("Dying", true);
            }
        }

        public override void TakeDamageNoAnimation(int damage)
        {
            if (enemyManager.isDead)
                return;

            currentHealth -= damage;
            enemyHealthBar.SetCurrentHealth(currentHealth);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                enemyManager.isDead = true;
                enemyAnimator.PlayTargetAnimation("Dying", true);
            }
        }
    }
}