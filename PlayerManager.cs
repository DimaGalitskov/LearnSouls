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
        PlayerLocomotionManager playerLocomotion;
        PlayerStatsManager playerStats;
        PlayerAnimatorManager playerAnimator;
        SoulsHUD soulsHUD;
        Interactable interactableObject;

        [Header("Intaractable Layers")]
        public LayerMask interactableLayers;

        [Header("Chilling Animations")]
        public string chillAnimation;
        public string wakeAnimation;


        private void Awake()
        {
            cameraHandler = FindObjectOfType<CameraHandler>();
            inputHandler = GetComponent<InputHandler>();
            anim = GetComponent<Animator>();
            playerLocomotion = GetComponent<PlayerLocomotionManager>();
            playerStats = GetComponent<PlayerStatsManager>();
            playerAnimator = GetComponent<PlayerAnimatorManager>();
            soulsHUD = FindObjectOfType<SoulsHUD>();
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
            playerStats.RegenerateStamina();

            CheckForInteractable();
            CheckForSceneReset();
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
            inputHandler.a_input = false;
            inputHandler.rb_Input = false;
            inputHandler.rt_Input = false;
            inputHandler.lb_Input = false;
            inputHandler.lt_Input = false;
            inputHandler.dPad_Up = false;
            inputHandler.dPad_Down = false;
            inputHandler.dPad_Right = false;
            inputHandler.dPad_Left = false;
            inputHandler.menuInput = false;
            inputHandler.menuInput = false;

            if (isInAir)
            {
                playerLocomotion.inAirTimer += Time.deltaTime;
            }
        }

        public void WakeUp()
        {
            playerAnimator.PlayTargetAnimation(wakeAnimation, true);
        }

        public void CheckForInteractable()
        {
            RaycastHit hit;

            if (Physics.SphereCast(transform.position, 0.3f, transform.forward, out hit, 1f, interactableLayers))
            {
                if (hit.collider.CompareTag("Interactable"))
                {
                    soulsHUD.ShowTooltip();
                    interactableObject = hit.collider.GetComponent<Interactable>();
                    if (interactableObject != null)
                    {
                        if (inputHandler.a_input)
                        {
                            interactableObject.Interact(this);
                        }
                    }
                }
            }
            else
            {
                interactableObject = null;
                soulsHUD.HideTooltip();
            }
        }

        public void CheckForSceneReset()
        {
            if (inputHandler.menuInput == false)
                return;

            UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
        }
    }
}
