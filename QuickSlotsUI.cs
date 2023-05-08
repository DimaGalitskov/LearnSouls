using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace SOULS
{
    public class QuickSlotsUI : MonoBehaviour
    {
        public VisualElement leftWeaponIcon;
        public VisualElement rightWeaponIcon;
        public VisualElement consumableSlot;
        public Label consumableCount;

        private void OnEnable()
        {
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;
            rightWeaponIcon = root.Q<VisualElement>("RightHandSlot");
            leftWeaponIcon = root.Q<VisualElement>("LeftHandSlot");
            consumableSlot = root.Q<VisualElement>("ConsumableSlot");
            consumableCount = consumableSlot.Q<Label>("Counter");
        }

        public void UpdateWeaponQuickSlotsUI(bool isLeft, WeaponItem weapon)
        {
            if (isLeft)
            {
                if (weapon.itemIcon != null)
                {
                    leftWeaponIcon.style.backgroundImage = new StyleBackground(weapon.itemIcon);
                }
                else
                {
                    leftWeaponIcon.style.backgroundImage = null;
                }
            }
            else
            {
                if (weapon.itemIcon != null)
                {
                    rightWeaponIcon.style.backgroundImage = new StyleBackground(weapon.itemIcon);
                }
                else
                {
                    rightWeaponIcon.style.backgroundImage = null;
                }
            }
        }

        public void UpdateQuickSlotUI(ConsumableItem consumableItem)
        {
            consumableSlot.style.backgroundImage = new StyleBackground(consumableItem.itemIcon);
            consumableCount.text = consumableItem.currentItemAmount.ToString();
        }
    }
}
