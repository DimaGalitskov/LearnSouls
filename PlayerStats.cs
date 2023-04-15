using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SOULS
{
    public class PlayerStats : MonoBehaviour
    {
        public int healthLevel = 10;
        public int maxHealth;
        public int currentHealth;
        public SoulsHUD soulsHUD;

        AnimatorHandler animatorHandler;
        PlayerManager playerManager;

        private void Start()
        {
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            playerManager = GetComponent<PlayerManager>();
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            soulsHUD.SetMaxHealth(maxHealth);
        }

        int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public void TakeDamage(int damage)
        {
            if (!playerManager.isDead)
            {
                currentHealth -= damage;
                soulsHUD.SetCurrentHealth(currentHealth);
                animatorHandler.PlayTargetAnimation("Damaged", true);

                if (currentHealth <= 0)
                {
                    playerManager.isDead = true;
                    currentHealth = 0;
                    animatorHandler.PlayTargetAnimation("Dying", true);
                }
            }
        }
    }
}