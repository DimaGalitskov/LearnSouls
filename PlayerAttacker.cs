using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOULS
{
    public class PlayerAttacker : MonoBehaviour
    {
        AnimatorHandler animatorHandler;
        InputHandler inputHandler;
        public string lastAttack;

        private void Awake()
        {
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            inputHandler = GetComponent<InputHandler>();
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
            animatorHandler.PlayTargetAnimation(weapon.OH_Light_Attack_1, true);
            animatorHandler.SetActionParticle(weapon.OH_Light_Particle_1);
            lastAttack = weapon.OH_Light_Attack_1;
        }

        public void HandleHeavyAttack(WeaponItem weapon)
        {
            animatorHandler.PlayTargetAnimation(weapon.OH_Heavy_Attack_1, true);
            lastAttack = weapon.OH_Heavy_Attack_1;
        }
    }
}
