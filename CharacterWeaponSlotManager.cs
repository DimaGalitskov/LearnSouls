using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SOULS
{
    public class CharacterWeaponSlotManager : MonoBehaviour
    {
        [Header("Unarmed Weapon")]
        public WeaponItem unarmedWeapon;

        [Header("Weapon Slots")]
        public WeaponHolderSlot rightHandSlot;
        public WeaponHolderSlot leftHandSlot;

        [Header("Damage Colliders")]
        public DamageCollider rightHandDamageCollider;
        public DamageCollider leftHandDamageCollider;
    }
}