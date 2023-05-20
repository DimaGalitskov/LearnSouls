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
        public bool x_Input;
        public bool a_input;
        public bool rb_Input;
        public bool rt_Input;
        public bool lb_Input;
        public bool lt_Input;
        public bool dPad_Up;
        public bool dPad_Down;
        public bool dPad_Right;
        public bool dPad_Left;
        public bool menuInput;

        public bool rollFlag;
        public bool sprintFlag;
        public bool comboFlag;
        public float rollInputTimer;

        PlayerControls inputActions;
        PlayerCombatManager playerAttacker;
        PlayerInventoryManager playerInventory;
        PlayerManager playerManager;
        PlayerStatsManager playerStats;
        PlayerEffectsManager playerEffecter;
        PlayerAnimatorManager playerAnimator;
        CameraHandler cameraHandler;
        PlayerWeaponSlotManager weaponSlotManager;

        Vector2 movementInput;
        Vector2 cameraInput;
        Vector2 dPadInput;

        private void Awake()
        {
            playerAttacker = GetComponent<PlayerCombatManager>();
            playerInventory = GetComponent<PlayerInventoryManager>();
            playerManager = GetComponent<PlayerManager>();
            playerStats = GetComponent<PlayerStatsManager>();
            playerEffecter = GetComponent<PlayerEffectsManager>();
            playerAnimator = GetComponent<PlayerAnimatorManager>();
            weaponSlotManager = GetComponent<PlayerWeaponSlotManager>();
        }

        public void OnEnable()
        {
            if (inputActions == null)
            {
                inputActions = new PlayerControls();
                inputActions.PlayerMovement.Movement.performed += context => movementInput = context.ReadValue<Vector2>();
                //inputActions.PlayerMovement.Camera.performed += context => cameraInput = context.ReadValue<Vector2>();

                inputActions.PlayerActions.B.performed += ctx => b_Input = true;
                inputActions.PlayerActions.B.canceled += ctx => b_Input = false;

                inputActions.PlayerActions.X.performed += ctx => x_Input = true;

                inputActions.PlayerActions.A.performed += ctx => a_input = true;

                inputActions.PlayerActions.RB.performed += ctx => rb_Input = true;

                inputActions.PlayerActions.RT.performed += ctx => rt_Input = true;

                inputActions.PlayerActions.LB.performed += ctx => lb_Input = true;

                inputActions.PlayerActions.LT.performed += ctx => lt_Input = true;

                //inputActions.PlayerQuickSlots.DpadRight.performed += ctx => dPad_Right = true;

                //inputActions.PlayerQuickSlots.DpadLeft.performed += ctx => dPad_Left = true;

                inputActions.PlayerActions.Menu.performed += ctx => menuInput = true;
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
            HandleWakeUp();
            HandleQuickSlotInput();
            HandleCosumableInput();
        }

        private void MoveInput(float delta) {
            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
        }

        private void HandleWakeUp()
        {
            if (playerManager.isChilling)
            {
                if (rb_Input || rt_Input || lb_Input || lt_Input)
                {
                    playerManager.isChilling = false;
                    playerManager.WakeUp();
                }
            }
        }

        private void HandleRollInput(float delta) {
            if (b_Input)
            {
                rollInputTimer += delta;

                if (playerStats.currentStamina <= 0)
                {
                    b_Input = false;
                    sprintFlag = false;
                }

                if (moveAmount > 0.5f && playerStats.currentStamina > 0)
                {
                    sprintFlag = true;
                }
            }
            else
            {
                sprintFlag = false;

                if (rollInputTimer > 0 && rollInputTimer < 0.2f)
                {
                    rollFlag = true;
                }

                rollInputTimer = 0;
            }
        }

        private void HandleAttackInput(float delta)
        {
            if (rb_Input)
            {
                playerAttacker.HandleRBAction();
            }
            if (rt_Input)
            {
                playerAttacker.HandleRTAction();
            }
            if (lb_Input)
            {
                playerAttacker.HandleLBAction();
            }
            if (lt_Input)
            {
                playerAttacker.HandleLTAction();
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

        private void HandleCosumableInput()
        {
            if (x_Input)
            {
                x_Input = false;
                playerInventory.currentConsumable.AttemptToConsumeItem(playerAnimator, weaponSlotManager, playerEffecter);
            }
        }
    }
}