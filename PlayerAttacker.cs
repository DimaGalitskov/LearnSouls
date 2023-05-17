using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOULS
{
    public class PlayerCombatManager : MonoBehaviour
    {
        PlayerAnimatorManager animatorHandler;
        PlayerManager playerManager;
        PlayerInventoryManager playerInventory;
        PlayerStatsManager playerStats;
        InputHandler inputHandler;
        PlayerWeaponSlotManager weaponSlotManager;

        public string lastAttack;

        private void Awake()
        {
            playerManager = GetComponent<PlayerManager>();
            playerInventory = GetComponent<PlayerInventoryManager>();
            playerStats = GetComponent<PlayerStatsManager>();
            inputHandler = GetComponent<InputHandler>();
            animatorHandler = GetComponent<PlayerAnimatorManager>();
            weaponSlotManager = GetComponent<PlayerWeaponSlotManager>();
        }

        public void HandleWeaponCombo(WeaponItem weapon)
        {
            if (playerStats.currentStamina <= 0)
                return;

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
                else if (lastAttack == weapon.OH_Light_Attack_3)
                {
                    animatorHandler.PlayTargetAnimation(weapon.OH_Light_Attack_4, true);
                    animatorHandler.SetActionParticle(weapon.OH_Light_Particle_4);
                    lastAttack = weapon.OH_Light_Attack_4;
                }
                else if (lastAttack == weapon.OH_Heavy_Attack_1)
                {
                    Debug.Log("heavy combo");
                    animatorHandler.PlayTargetAnimation(weapon.OH_Heavy_Attack_2, true);
                    animatorHandler.SetActionParticle(weapon.OH_Heavy_Particle_2);
                    lastAttack = weapon.OH_Heavy_Attack_2;
                }
                else if (lastAttack == weapon.spell_1.name)
                {
                    playerInventory.currentSpell = weapon.spell_2;
                    HandleLightCast(playerInventory.leftWeapon);
                }
                else if (lastAttack == weapon.spell_heavy_1.name)
                {
                    playerInventory.currentSpell = weapon.spell_heavy_2;
                    HandleLightCast(playerInventory.leftWeapon);
                }
            }
        }



        public void HandleLightAttack(WeaponItem weapon)
        {
            if (playerStats.currentStamina <= 0)
                return;

            weaponSlotManager.attackingWeapon = weapon;
            animatorHandler.anim.SetBool("isUsingRightHand", true);
            animatorHandler.PlayTargetAnimation(weapon.OH_Light_Attack_1, true);
            animatorHandler.SetActionParticle(weapon.OH_Light_Particle_1);
            lastAttack = weapon.OH_Light_Attack_1;
        }

        public void HandleHeavyAttack(WeaponItem weapon)
        {
            if (playerStats.currentStamina <= 0)
                return;

            weaponSlotManager.attackingWeapon = weapon;
            animatorHandler.anim.SetBool("isUsingRightHand", true);
            animatorHandler.PlayTargetAnimation(weapon.OH_Heavy_Attack_1, true);
            animatorHandler.SetActionParticle(weapon.OH_Heavy_Particle_1);
            lastAttack = weapon.OH_Heavy_Attack_1;
        }


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

        public void HandleRTAction()
        {
            if (playerInventory.rightWeapon.weaponType == WeaponType.MeleeWeapon)
            {
                PerformRTMeleeAction();
            }
        }

        public void HandleLBAction()
        {
            if (playerInventory.leftWeapon.weaponType == WeaponType.PyroCaster)
            {
                PerformLBMagicAction(playerInventory.leftWeapon);
            }
        }

        public void HandleLTAction()
        {
            if (playerInventory.leftWeapon.weaponType == WeaponType.PyroCaster)
            {
                PerformLTMagicAction(playerInventory.leftWeapon);
            }
        }

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

                HandleLightAttack(playerInventory.rightWeapon);
            }
        }

        public void PerformRTMeleeAction()
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

                HandleHeavyAttack(playerInventory.rightWeapon);
            }
        }

        public void PerformRBMagicAction(WeaponItem weapon)
        {
            if (playerManager.isInteracting)
                return;

            if (weapon.weaponType == WeaponType.FaithCaster)
            {
                if (playerInventory.currentSpell != null && playerInventory.currentSpell.spellType == SpellType.Faith)
                {
                    if (playerStats.currentStamina >= playerInventory.currentSpell.staminaCost)
                    {
                        playerInventory.currentSpell.AttemptToCastSpell(animatorHandler, playerStats, weaponSlotManager);
                    }
                    else
                    {
                        animatorHandler.PlayTargetAnimation("Failing", true);
                    }
                }
            }
        }

        public void PerformLBMagicAction(WeaponItem weapon)
        {
            if (playerManager.canDoCombo)
            {
                inputHandler.comboFlag = true;
                HandleWeaponCombo(playerInventory.leftWeapon);
                inputHandler.comboFlag = false;
            }
            else
            {
                if (playerManager.isInteracting)
                    return;

                if (playerManager.canDoCombo)
                    return;


                animatorHandler.anim.SetBool("isUsingRightHand", false);
                playerInventory.currentSpell = weapon.spell_1;
                HandleLightCast(playerInventory.leftWeapon);
            }
        }

        public void PerformLTMagicAction(WeaponItem weapon)
        {
            if (playerManager.canDoCombo)
            {
                inputHandler.comboFlag = true;
                HandleWeaponCombo(playerInventory.leftWeapon);
                inputHandler.comboFlag = false;
            }
            else
            {
                if (playerManager.isInteracting)
                    return;

                if (playerManager.canDoCombo)
                    return;


                animatorHandler.anim.SetBool("isUsingRightHand", false);
                playerInventory.currentSpell = weapon.spell_heavy_1;
                HandleLightCast(playerInventory.leftWeapon);
            }
        }

        public void HandleLightCast(WeaponItem weapon)
        {
            if (playerStats.currentStamina <= 0)
                return;

            if (playerInventory.currentSpell != null)
            {
                playerInventory.currentSpell.AttemptToCastSpell(animatorHandler, playerStats, weaponSlotManager);
                lastAttack = playerInventory.currentSpell.name;
            }
        }

        private void SuccessfullyCastSpell()
        {
            playerInventory.currentSpell.SuccessfullyCastSpell(animatorHandler, playerStats, weaponSlotManager);
            animatorHandler.anim.SetBool("isFiringSpell", true);
        }

    }
}
