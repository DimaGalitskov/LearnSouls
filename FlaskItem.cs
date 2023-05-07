using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOULS
{
    [CreateAssetMenu(menuName = "Items / Flask")]

    public class FlaskItem : ConsumableItem
    {
        [Header("Flask Type")]
        public bool estusFlask;
        public bool ashenFlask;

        [Header("Recovered Amount")]
        public int healthRecoverAmount;
        public int FocusRecoverAmount;

        [Header("Consume FX")]
        public GameObject consumeFX;

        public override void AttemptToConsumeItem(PlayerAnimator playerAnimator, WeaponSlotManager weaponSlotManager, PlayerEffecter playerEffecter)
        {
            base.AttemptToConsumeItem(playerAnimator, weaponSlotManager, playerEffecter);
            playerEffecter.currentFX = consumeFX;
            playerEffecter.amountToHeal = healthRecoverAmount;
            GameObject instance = null;
            playerEffecter.instantiatedFX = instance;

            // instantiate item model
            // GameObject flask = Instantiate(itemModel, weaponSlotManager.righthandslot.transform);
            // weaponslotmanager.righthandslot.Unloadweapon();

            //play the FX if the successfully drink
        }

    }
}