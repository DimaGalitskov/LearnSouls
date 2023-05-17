using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOULS
{

    public class CharacterStatsManager : MonoBehaviour
    {
        public int healthLevel = 10;
        public int maxHealth;
        public int currentHealth;

        public int staminaLevel = 10;
        public float maxStamina;
        public float currentStamina;

        public virtual void TakeDamage(int damage)
        {

        }

        public virtual void TakeDamageNoAnimation(int damage)
        {

        }
    }
}