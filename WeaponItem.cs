using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOULS
{
    public enum WeaponType
    {
        SpellCaster,
        FaithCaster,
        PyroCaster,
        MeleeWeapon
    }

    [CreateAssetMenu(menuName = "Items/Weapon Item")]
    public class WeaponItem : Item
    {
        public GameObject modelPrefab;
        public bool isUnarmed;

        [Header("Idle Animations")]
        public string right_Hand_Idle;
        public string left_Hand_Idle;

        [Header("Attack Animations")]
        public string OH_Light_Attack_1;
        public string OH_Light_Attack_2;
        public string OH_Light_Attack_3;
        public string OH_Light_Attack_4;
        public string OH_Heavy_Attack_1;
        public string OH_Heavy_Attack_2;

        [Header("Attack Effects")]
        public ParticleSystem OH_Light_Particle_1;
        public ParticleSystem OH_Light_Particle_2;
        public ParticleSystem OH_Light_Particle_3;
        public ParticleSystem OH_Light_Particle_4;
        public ParticleSystem OH_Heavy_Particle_1;
        public ParticleSystem OH_Heavy_Particle_2;

        [Header("Spells")]
        public SpellItem spell_1;
        public SpellItem spell_2;
        public SpellItem spell_heavy_1;
        public SpellItem spell_heavy_2;

        [Header("Stamina Costs")]
        public int baseStamina;
        public float lightAttackMultiplier;
        public float heavyAttackMultiplier;

        [Header("Weapon Type")]
        public WeaponType weaponType;
    }
}
