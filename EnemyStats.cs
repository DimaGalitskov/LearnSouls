using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SOULS
{
    public class EnemyStats : CharacterStats
    {
        Animator animator;
        EnemyManager enemyManager;

        private void Awake()
        {
            enemyManager = GetComponent<EnemyManager>();
            animator = GetComponentInChildren<Animator>();
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

            animator.Play("Damaged");

            if (currentHealth <= 0)
            {
                enemyManager.isDead = true;
                currentHealth = 0;
                animator.Play("Dying");
            }
        }
    }
}