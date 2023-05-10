using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SOULS
{
    public class EnemyStats : CharacterStats
    {
        EnemyAnimator enemyAnimator;
        EnemyManager enemyManager;
        UIEnemyHealthBar enemyHealthBar;

        private void Awake()
        {
            enemyManager = GetComponent<EnemyManager>();
            enemyAnimator = GetComponentInChildren<EnemyAnimator>();
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

        public void TakeDamage(int damage)
        {
            if (enemyManager.isDead)
                return;

            currentHealth -= damage;
            enemyHealthBar.SetCurrentHealth(currentHealth);
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