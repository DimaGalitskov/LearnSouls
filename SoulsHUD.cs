using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace SOULS
{
    public class SoulsHUD : MonoBehaviour
    {
        VisualElement root;
        ProgressBar healthBar;
        ProgressBar staminaBar;

        private void OnEnable()
        {
            root = GetComponent<UIDocument>().rootVisualElement;
            healthBar = root.Q<ProgressBar>("HealthBar");
            staminaBar = root.Q<ProgressBar>("StaminaBar");
        }

        public void SetMaxHealth(int maxHealth)
        {
            healthBar.highValue = maxHealth;
            healthBar.value = maxHealth;
        }

        public void SetCurrentHealth(int currentHealth)
        {
            healthBar.value = currentHealth;
        }

        public void SetMaxStamina(int maxStamina)
        {
            staminaBar.highValue = maxStamina;
            staminaBar.value = maxStamina;
        }

        public void SetCurrentStamina(int currentStamina)
        {
            staminaBar.value = currentStamina;
        }
    }
}
