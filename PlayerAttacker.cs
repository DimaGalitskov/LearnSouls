using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOULS
{
    public class PlayerAttacker : MonoBehaviour
    {
        PlayerAnimator animatorHandler;
        PlayerManager playerManager;
        PlayerInventory playerInventory;
        PlayerStats playerStats;
        InputHandler inputHandler;
        WeaponSlotManager weaponSlotManager;

        public string lastAttack;

        private void Awake()
        {
            playerManager = GetComponentInParent<PlayerManager>();
            playerInventory = GetComponentInParent<PlayerInventory>();
            playerStats = GetComponentInParent<PlayerStats>();
            inputHandler = GetComponentInParent<InputHandler>();
            animatorHandler = GetComponent<PlayerAnimator>();
            weaponSlotManager = GetComponent<WeaponSlotManager>();
        }

        public void HandleWeaponCombo(WeaponItem weapon)
        {
            if (inputHandler.comboFlag)
            {
                animatorHandler.anim.SetBool("canDoCombo", false);
                if (lastAttack == weapon.OH_Light_Attack_1)
                {
                    animatorHandler.PlayTargetAnimation(weapon.OH_Light_Attack_2, true);
                    animatorHandler.SetActionParticle(weapon.OH_Light_Particle_2);
                    lastAttack = weapon.OH_Light_Attack_2;
                }
                else if (lastAttack == weapon.OH_Light_Attack_2)
                {
                    animatorHandler.PlayTargetAnimation(weapon.OH_Light_Attack_3, true);
                    animatorHandler.SetActionParticle(weapon.OH_Light_Particle_3);
                    lastAttack = weapon.OH_Light_Attack_3;
                }
            }

        }

        public void HandleLightAttack(WeaponItem weapon)
        {
            weaponSlotManager.attackingWeapon = weapon;
            animatorHandler.PlayTargetAnimation(weapon.OH_Light_Attack_1, true);
            animatorHandler.SetActionParticle(weapon.OH_Light_Particle_1);
            lastAttack = weapon.OH_Light_Attack_1;
        }

        public void HandleHeavyAttack(WeaponItem weapon)
        {
            weaponSlotManager.attackingWeapon = weapon;
            animatorHandler.PlayTargetAnimation(weapon.OH_Heavy_Attack_1, true);
            lastAttack = weapon.OH_Heavy_Attack_1;
        }


        #region Input Actions
        public void HandleRBAction()
        {
            if (playerInventory.rightWeapon.weaponType == WeaponType.MeleeWeapon)
            {
                PerformRBMeleeAction();
            }
            else if (playerInventory.rightWeapon.weaponType == WeaponType.SpellCaster)
            {
                PerformRBMagicAction(playerInventory.rightWeapon);
            }
            else if (playerInventory.rightWeapon.weaponType == WeaponType.FaithCaster)
            {
                PerformRBMagicAction(playerInventory.rightWeapon);
            }
            else if (playerInventory.rightWeapon.weaponType == WeaponType.PyroCaster)
            {
                PerformRBMagicAction(playerInventory.rightWeapon);
            }
        }
        #endregion


        #region Attack Actions
        public void PerformRBMeleeAction()
        {
            if (playerManager.canDoCombo)
            {
                inputHandler.comboFlag = true;
                HandleWeaponCombo(playerInventory.rightWeapon);
                inputHandler.comboFlag = false;
            }
            else
            {
                if (playerManager.isInteracting)
                    return;

                if (playerManager.canDoCombo)
                    return;


                animatorHandler.anim.SetBool("isUsingRightHand", true);
                HandleLightAttack(playerInventory.rightWeapon);
            }
        }

        public void PerformRBMagicAction(WeaponItem weapon)
        {
            if (weapon.weaponType == WeaponType.FaithCaster)
            {
                if (playerInventory.currentSpell != null && playerInventory.currentSpell.spellType == SpellType.Faith)
                {
                    //check for mana
                    playerInventory.currentSpell.AttemptToCastSpell(animatorHandler, playerStats);
                }
            }
        }

        private void SuccessfullyCastSpell()
        {
            playerInventory.currentSpell.SuccessfullyCastSpell(animatorHandler, playerStats);
        }

        #endregion
    }
}
