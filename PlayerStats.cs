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

        public int staminaLevel = 10;
        public int maxStamina;
        public int currentStamina;

        SoulsHUD soulsHUD;
        AnimatorHandler animatorHandler;
        PlayerManager playerManager;

        private void Awake()
        {
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            playerManager = GetComponent<PlayerManager>();
        }

        private void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            soulsHUD.SetMaxHealth(maxHealth);

            maxStamina = SetMaxStaminaFromStaminaLevel();
            currentStamina = maxStamina;
        }

        int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        int SetMaxStaminaFromStaminaLevel()
        {
            maxStamina = staminaLevel * 10;
            return maxStamina;
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

        public void DrainStamina(int drain)
        {
            currentStamina -= drain;
            soulsHUD.SetCurrentStamina(currentStamina);
        }
    }
}