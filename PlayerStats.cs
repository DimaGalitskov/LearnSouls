using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SOULS
{
    public class PlayerStats : CharacterStats
    {
        PlayerAnimator animatorHandler;
        PlayerManager playerManager;

        SoulsHUD soulsHUD;

        public float staminaRegenerationSpeed;
        float staminaTimer = 0;




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

        float SetMaxStaminaFromStaminaLevel()
        {
            maxStamina = staminaLevel * 10;
            return maxStamina;
        }

        public void TakeDamage(int damage)
        {
            if (playerManager.isInvulnerable)
                return;

            if (playerManager.isDead)
                return;

            currentHealth -= damage;
            animatorHandler.PlayTargetAnimation("Damaged", true);

            if (currentHealth <= 0)
            {
                playerManager.isDead = true;
                currentHealth = 0;
                animatorHandler.PlayTargetAnimation("Dying", true);
            }

            soulsHUD.SetCurrentHealth(currentHealth);
        }

        public void DrainStamina(int drain)
        {
            currentStamina -= drain;

            if (currentStamina <= 0)
            {
                currentStamina = 0;
            }

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

        public void RegenerateStamina()
        {
            if (playerManager.isInteracting)
            {
                staminaTimer = 0;
            }
            else
            {
                staminaTimer += Time.deltaTime;

                if (currentStamina < maxStamina && staminaTimer > 1f)
                {
                    currentStamina += staminaRegenerationSpeed * Time.deltaTime;
                    soulsHUD.SetCurrentStamina(Mathf.RoundToInt(currentStamina));
                }
            }
        }
    }
}