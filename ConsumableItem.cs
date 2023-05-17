using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SOULS
{
    public class ConsumableItem : Item
    {
        [Header("Item Count")]
        public int maxItemAmount;
        public int currentItemAmount;

        [Header("Item Model")]
        public GameObject itemModel;

        [Header("Animations")]
        public string consumeAnimation;
        public bool isInteracting;

        public virtual void AttemptToConsumeItem(PlayerAnimatorManager playerAnimator, PlayerWeaponSlotManager weaponSlotManager, PlayerEffectsManager playerEffecter)
        {
            if (currentItemAmount > 0)
            {
                currentItemAmount--;
                playerAnimator.PlayTargetAnimation(consumeAnimation, isInteracting);
            }
            else
            {
                playerAnimator.PlayTargetAnimation("Failing", true);
            }
        }
    }
}