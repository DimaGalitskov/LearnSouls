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

        public bool rollFlag;
        public bool sprintFlag;
        public float rollInputTimer;

        PlayerControls inputActions;
        PlayerAttacker playerAttacker;
        PlayerInventory playerInventory;
        CameraHandler cameraHandler;

        Vector2 movementInput;
        Vector2 cameraInput;

        private void Awake()
        {
            playerAttacker = GetComponent<PlayerAttacker>();
            playerInventory = GetComponent<PlayerInventory>();
        }

        public void OnEnable()
        {
            if (inputActions == null)
            {
                inputActions = new PlayerControls();
                inputActions.PlayerMovement.Movement.performed += inputActions =>
                    movementInput = inputActions.ReadValue<Vector2>();
                inputActions.PlayerMovement.Camera.performed += ctx =>
                    cameraInput = ctx.ReadValue<Vector2>();

                inputActions.PlayerActions.Roll.performed += ctx => b_Input = true;
                inputActions.PlayerActions.Roll.canceled += ctx => b_Input = false;

                inputActions.PlayerActions.RB.performed += ctx => rb_Input = true;
                inputActions.PlayerActions.RB.canceled += ctx => rb_Input = false;

                inputActions.PlayerActions.RT.performed += ctx => rt_Input = true;
                inputActions.PlayerActions.RT.canceled += ctx => rt_Input = false;
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
                playerAttacker.HandleLightAttack(playerInventory.rightWeapon);
            }
            if (rt_Input)
            {
                playerAttacker.HandleHeavyAttack(playerInventory.rightWeapon);
            }
        }
    }
}