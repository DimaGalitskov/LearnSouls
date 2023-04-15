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

        private void OnEnable()
        {
            root = GetComponent<UIDocument>().rootVisualElement;
            healthBar = root.Q<ProgressBar>("HealthBar");
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
    }
}
