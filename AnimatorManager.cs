using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOULS
{
    public class AnimatorManager : MonoBehaviour
    {
        public Animator anim;
        public bool canRotate;
        protected CharacterManager characterManager;
        protected CharacterStatsManager characterStatsManager;

        ParticleSystem particleSystem;

        protected virtual void Awake()
        {
            characterManager = GetComponent<CharacterManager>();
        }

        public void PlayTargetAnimation(string targetAnim, bool isInteracting)
        {
            anim.applyRootMotion = isInteracting;
            anim.SetBool("canRotate", false);
            anim.SetBool("isInteracting", isInteracting);
            anim.CrossFade(targetAnim, 0.1f);
        }

        public void PlayTargetAnimationWithRootRotation(string targetAnim, bool isInteracting)
        {
            anim.applyRootMotion = isInteracting;
            anim.SetBool("isRootRotating", true);
            anim.SetBool("isInteracting", isInteracting);
            anim.CrossFade(targetAnim, 0.1f);
        }

        public void SetActionParticle(ParticleSystem current)
        {
            particleSystem = current;
        }

        public void PlayActionParticle()
        {
            Instantiate(particleSystem, transform);
        }

        public void CanRotate()
        {
            anim.SetBool("canRotate", true);
        }

        public void StopRotation()
        {
            anim.SetBool("canRotate", false);
        }

        public void EnableCombo()
        {
            anim.SetBool("canDoCombo", true);
        }

        public void DisableCombo()
        {
            anim.SetBool("canDoCombo", false);
        }

        public void EnableIsInvulnerable()
        {
            anim.SetBool("isInvulnerable", true);
        }

        public void DisableIsInvulnerable()
        {
            anim.SetBool("isInvulnerable", false);
        }

        public void EnableParrying()
        {
            characterManager.isParrying = true;
        }

        public void DisableParrying()
        {
            characterManager.isParrying = false;
        }

        public void EnableRiposting()
        {
            characterManager.canBeReposted = true;
        }

        public void DisableRiposting()
        {
            characterManager.canBeReposted = false;
        }

    }
}