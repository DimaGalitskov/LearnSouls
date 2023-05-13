using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace SOULS
{
    public class PlayerManager : CharacterManager
    {
        InputHandler inputHandler;
        Animator anim;
        CameraHandler cameraHandler;
        PlayerLocomotion playerLocomotion;
        PlayerStats playerStats;
        PlayerAnimator playerAnimator;

        [Header("Player Flags")]
        public bool isInteracting;
        public bool isSprinting;
        public bool isInAir;
        public bool isGrounded;
        public bool isDead;
        public bool canDoCombo;
        public bool isUsingRightHand;
        public bool isInvulnerable;
        public bool isChilling;

        [Header("Chilling Animations")]
        public string chillAnimation;
        public string wakeAnimation;


        private void Awake()
        {
            cameraHandler = FindObjectOfType<CameraHandler>();
            inputHandler = GetComponent<InputHandler>();
            anim = GetComponentInChildren<Animator>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
            playerStats = GetComponent<PlayerStats>();
            playerAnimator = GetComponentInChildren<PlayerAnimator>();
        }

        void Update()
        {
            float delta = Time.deltaTime;

            isInteracting = anim.GetBool("isInteracting");
            canDoCombo = anim.GetBool("canDoCombo");
            isUsingRightHand = anim.GetBool("isUsingRightHand");
            isInvulnerable = anim.GetBool("isInvulnerable");
            isFiringSpell = anim.GetBool("isFiringSpell");
            anim.SetBool("isInAir", isInAir);
            

            inputHandler.TickInput(delta);
            playerAnimator.canRotate = anim.GetBool("canRotate");
            playerLocomotion.HandleRollingAndSprinting(delta);
            playerLocomotion.HandleJumping();
            playerStats.RegenerateStamina();
        }

        private void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;

            if (isChilling)
            {
                playerAnimator.PlayTargetAnimation(chillAnimation, true);
            }
            playerLocomotion.HandleFalling(delta, playerLocomotion.moveDirection);
            playerLocomotion.HandleMovement(delta);
            playerLocomotion.HandleRotation(delta);


            if (cameraHandler != null)
            {
                cameraHandler.FollowTarget(delta);
                cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
            }
        }

        private void LateUpdate()
        {
            inputHandler.rollFlag = false;
            inputHandler.jump_input = false;
            inputHandler.rb_Input = false;
            inputHandler.rt_Input = false;
            inputHandler.lb_Input = false;
            inputHandler.lt_Input = false;
            inputHandler.dPad_Up = false;
            inputHandler.dPad_Down = false;
            inputHandler.dPad_Right = false;
            inputHandler.dPad_Left = false;

            if (isInAir)
            {
                playerLocomotion.inAirTimer += Time.deltaTime;
            }
        }

        public void WakeUp()
        {
            playerAnimator.PlayTargetAnimation(wakeAnimation, true);
        }
    }
}
