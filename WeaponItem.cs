using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOULS
{
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
        public string OH_Heavy_Attack_1;

        [Header("Attack Effects")]
        public ParticleSystem OH_Light_Particle_1;
        public ParticleSystem OH_Light_Particle_2;
        public ParticleSystem OH_Light_Particle_3;

        [Header("Stamina Costs")]
        public int baseStamina;
        public float lightAttackMultiplier;
        public float heavyAttackMultiplier;
    }
}
