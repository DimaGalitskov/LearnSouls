using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SOULS
{
    public class PlayerStats : CharacterStats
    {
        SoulsHUD soulsHUD;
        PlayerAnimator animatorHandler;
        PlayerManager playerManager;

        private void Awake()
        {
            animatorHandler = GetComponentInChildren<PlayerAnimator>();
            playerManager = GetComponent<PlayerManager>();
            soulsHUD = FindObjectOfType<SoulsHUD>();
        }

        private void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            soulsHUD.SetMaxHealth(maxHealth);

            maxStamina = SetMaxStaminaFromStaminaLevel();
            currentStamina = maxStamina;
            soulsHUD.SetMaxStamina(maxStamina);
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
            if (playerManager.isDead)
                return;

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

        public void DrainStamina(int drain)
        {
            currentStamina -= drain;
            soulsHUD.SetCurrentStamina(currentStamina);
        }

        public void HealPlayer(int healAmount)
        {
            currentHealth += healAmount;

            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }

            soulsHUD.SetCurrentHealth(currentHealth);
        }
    }
}