using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOULS
{
    public class PlayerAnimatorManager : AnimatorManager
    {
        InputHandler inputHandler;
        PlayerLocomotionManager playerLocomotionManager;

        int vertical;
        int horizontal;

        protected override void Awake()
        {
            base.Awake();
            anim = GetComponent<Animator>();
            inputHandler = GetComponent<InputHandler>();
            playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
            vertical = Animator.StringToHash("Vertical");
            horizontal = Animator.StringToHash("Horizontal");
        }

        public void UpdateAnumatorValues(float verticalMovement, float horizontalMovement, bool isSprinting) {
            float v = 0;

            if (verticalMovement > 0 && verticalMovement<0.55f) 
            {
                v = 0.5f;
            }
            else if (verticalMovement > 0.55f) {
                v = 1;
            }
            else if (verticalMovement < 0 && verticalMovement > -0.55f) {
                v = -0.5f;
            }
            else if (verticalMovement < -0.55f) {
                v = -1;
            }
            else {
                v = 0;
            }

            float h = 0;

            if (horizontalMovement > 0 && horizontalMovement < 0.55f)
            {
                h = 0.5f;
            }
            else if(horizontalMovement > 0.55f) {
                h = 1f;
            }
            else if (horizontalMovement < 0 && horizontalMovement > -0.55f) {
                h = -0.5f;
            }
            else if (horizontalMovement < -0.55f) {
                h = -1;
            }
            else {
                h = 0;
            }

            if (isSprinting)
            {
                v = 2;
                h = horizontalMovement;
            }

            anim.SetFloat(vertical, v, 0.1f, Time.deltaTime);
            anim.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
        }

        private void OnAnimatorMove()
        {
            if (characterManager.isInteracting == false)
                return;

            float delta = Time.deltaTime;
            playerLocomotionManager.rigidbody.drag = 0;
            Vector3 deltaposition = anim.deltaPosition;
            deltaposition.y = 0;
            Vector3 velocity = deltaposition / delta;
            playerLocomotionManager.rigidbody.velocity = velocity;
        }
    }
}
