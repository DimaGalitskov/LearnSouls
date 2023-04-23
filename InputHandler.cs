using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOULS
{
    public class InputHandler : MonoBehaviour
    {
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public float mouseX;
        public float mouseY;

        public bool b_Input;
        public bool rb_Input;
        public bool rt_Input;
        public bool dPad_Up;
        public bool dPad_Down;
        public bool dPad_Right;
        public bool dPad_Left;

        public bool rollFlag;
        public bool sprintFlag;
        public bool comboFlag;
        public float rollInputTimer;

        PlayerControls inputActions;
        PlayerAttacker playerAttacker;
        PlayerInventory playerInventory;
        PlayerManager playerManager;
        AnimatorManager animatorManager;
        CameraHandler cameraHandler;

        Vector2 movementInput;
        Vector2 cameraInput;
        Vector2 dPadInput;

        private void Awake()
        {
            playerAttacker = GetComponent<PlayerAttacker>();
            playerInventory = GetComponent<PlayerInventory>();
            playerManager = GetComponent<PlayerManager>();
            animatorManager = GetComponentInChildren<AnimatorManager>();
        }

        public void OnEnable()
        {
            if (inputActions == null)
            {
                inputActions = new PlayerControls();
                inputActions.PlayerMovement.Movement.performed += context =>
                    movementInput = context.ReadValue<Vector2>();
                inputActions.PlayerMovement.Camera.performed += context =>
                    cameraInput = context.ReadValue<Vector2>();

                inputActions.PlayerActions.Roll.performed += ctx => b_Input = true;
                inputActions.PlayerActions.Roll.canceled += ctx => b_Input = false;

                inputActions.PlayerActions.RB.performed += ctx => rb_Input = true;

                inputActions.PlayerActions.RT.performed += ctx => rt_Input = true;

                inputActions.PlayerQuickSlots.DpadRight.performed += ctx => dPad_Right = true;

                inputActions.PlayerQuickSlots.DpadLeft.performed += ctx => dPad_Left = true;
            }

            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }

        public void TickInput(float delta) {
            MoveInput(delta);
            HandleRollInput(delta);
            HandleAttackInput(delta);
            HandleQuickSlotInput();
        }

        private void MoveInput(float delta) {
            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
        }

        private void HandleRollInput(float delta) {
            if (b_Input)
            {
                rollInputTimer += delta;
                sprintFlag = true;
            }
            else
            {
                if (rollInputTimer > 0 && rollInputTimer < 0.5f)
                {
                    sprintFlag = false;
                    rollFlag = true;
                }

                rollInputTimer = 0;
            }
        }

        private void HandleAttackInput(float delta)
        {
            if (rb_Input)
            {
                if (playerManager.canDoCombo)
                {
                    comboFlag = true;
                    playerAttacker.HandleWeaponCombo(playerInventory.rightWeapon);
                    comboFlag = false;
                }
                else
                {
                    if (playerManager.isInteracting)
                        return;

                    if (playerManager.canDoCombo)
                        return;


                    animatorManager.anim.SetBool("isUsingRightHand", true);
                    playerAttacker.HandleLightAttack(playerInventory.rightWeapon);
                }
            }
            if (rt_Input)
            {
                playerAttacker.HandleHeavyAttack(playerInventory.rightWeapon);
            }
        }

        private void HandleQuickSlotInput()
        {
            if (dPad_Right)
            {
                playerInventory.ChangeRightWeapon();
            }
            else if (dPad_Left)
            {
                playerInventory.ChangeLeftWeapon();
            }
        }
    }
}