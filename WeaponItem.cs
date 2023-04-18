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

        [Header("One Handed Attack Animations")]
        public string OH_Light_Attack_1;
        public string OH_Light_Attack_2;
        public string OH_Light_Attack_3;
        public string OH_Heavy_Attack_1;

        [Header("One Handed Attack Effects")]
        public ParticleSystem OH_Light_Particle_1;
        public ParticleSystem OH_Light_Particle_2;
        public ParticleSystem OH_Light_Particle_3;
    }
}
