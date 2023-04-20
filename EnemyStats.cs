using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SOULS
{
    public class EnemyStats : CharacterStats
    {
        Animator animator;

        private void Awake()
        {
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
            currentHealth -= damage;

            animator.Play("Damaged");

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                animator.Play("Dying");
            }
        }
    }
}