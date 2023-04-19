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

        private void OnEnable()
        {
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;
            rightWeaponIcon = root.Q<VisualElement>("RightHandSlot");
            leftWeaponIcon = root.Q<VisualElement>("LeftHandSlot");
        }

        public void UpdateWeaponQuickSlotsUI(bool isLeft, WeaponItem weapon)
        {
            if (isLeft)
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
            else
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
        }
    }
}
