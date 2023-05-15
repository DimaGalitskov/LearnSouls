using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SOULS
{
    public class BonfireInteractable : Interactable
    {
        // location for bonfire (for teleporting)
        // bonfire unique id (for saving)

        [Header("Bonfire teleportation transform")]
        public Transform teleportTransform;

        [Header("Activation")]
        public bool isActivated;
        public string activateAnimation;

        [Header("Bonfire FX")]
        public GameObject activationFX;
        public GameObject fireFX;

        private void Awake()
        {
            if (isActivated)
            {
                fireFX.gameObject.SetActive(true);
                interactableText = "Rest at bonfire";
            }
            else
            {
                interactableText = "Light bonfire";
            }
        }

        public override void Interact(PlayerManager playerManager)
        {
            Debug.Log("Bonfire interacted");
            PlayerAnimator playerAnimator;
            playerAnimator = playerManager.GetComponentInChildren<PlayerAnimator>();


            if (isActivated)
            {
                //open bonife menu
            }
            else
            {
                //activate bonfire
                isActivated = true;
                playerAnimator.PlayTargetAnimation(activateAnimation, true);
                interactableText = "Rest at bonfire";
                activationFX.gameObject.SetActive(true);
                fireFX.gameObject.SetActive(true);
            }
        }
    }
}