using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


namespace SOULS
{
    public class UIEnemyHealthBar : MonoBehaviour
    {
        public GameObject pivot;
        public Vector2 displacement = Vector2.zero;

        VisualElement root;
        ProgressBar healthBar;
        Camera mainCamera;
        float timeUntilBarIsHidden = 3;

        private void OnEnable()
        {
            root = GetComponentInChildren<UIDocument>().rootVisualElement;
            healthBar = root.Q<ProgressBar>("HealthBar");
            mainCamera = Camera.main;
        }

        private void LateUpdate()
        {
            HandleHiding();
            HandlePosition();
        }

        public void SetMaxHealth(int maxHealth)
        {
            healthBar.highValue = maxHealth;
            healthBar.value = maxHealth;
        }

        public void SetCurrentHealth(int currentHealth)
        {
            healthBar.value = currentHealth;
            timeUntilBarIsHidden = 3;
        }

        void HandlePosition()
        {
            Vector2 newPosition = RuntimePanelUtils.CameraTransformWorldToPanel(root.panel, pivot.transform.position, mainCamera);
            newPosition += displacement;
            root.transform.position = newPosition;
        }

        void HandleHiding()
        {
            timeUntilBarIsHidden -= Time.deltaTime;

            if (timeUntilBarIsHidden <= 0)
            {
                timeUntilBarIsHidden = 0;
                root.style.display = DisplayStyle.None;
            }

            else
            {
                if (root.style.display == DisplayStyle.None)
                {
                    root.style.display = DisplayStyle.Flex;
                }
            }

            if (healthBar.value <= 0)
            {
                //destroy this health bar
            }
        }
    }
}