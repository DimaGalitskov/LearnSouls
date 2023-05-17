using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SOULS
{
    public enum CharacterTeam
    {
        player,
        enemy,
        neutral
    }

    public class CharacterManager : MonoBehaviour
    {
        [Header("Spells")]
        public bool isFiringSpell;

        [Header("Team")]
        public CharacterTeam characterTeam;

        [Header("Interaction")]
        public bool isInteracting;

        [Header("Combat Flags")]
        public bool canDoCombo;
        public bool isUsingRightHand;
        public bool isInvulnerable;
        public bool isParrying;
        public bool canBeReposted;

        [Header("Movement Flags")]
        public bool isSprinting;
        public bool isInAir;
        public bool isGrounded;
        public bool isDead;
        public bool isChilling;
    }
}