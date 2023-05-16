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
        VisualElement tooltip;

        private void OnEnable()
        {
            root = GetComponent<UIDocument>().rootVisualElement;
            healthBar = root.Q<ProgressBar>("HealthBar");
            staminaBar = root.Q<ProgressBar>("StaminaBar");
            tooltip = root.Q<VisualElement>("Interaction");
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

        public void SetMaxStamina(float maxStamina)
        {
            staminaBar.highValue = maxStamina;
            staminaBar.value = maxStamina;
        }

        public void SetCurrentStamina(float currentStamina)
        {
            staminaBar.value = currentStamina;
        }

        public void ShowTooltip()
        {
            tooltip.style.display = DisplayStyle.Flex;
        }

        public void HideTooltip()
        {
            tooltip.style.display = DisplayStyle.None;
        }
    }
}
