using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SOULS
{
    public class PlayerEffecter : MonoBehaviour
    {
        PlayerStats playerStats;
        WeaponSlotManager weaponSlotManager;
        public GameObject currentFX;  //the particle that will play whatever you will do
        public GameObject instantiatedFX;
        public int amountToHeal;

        private void Awake()
        {
            playerStats = GetComponentInParent<PlayerStats>();
            weaponSlotManager = GetComponent<WeaponSlotManager>();
        }

        public void HealPlayerFromEffect()
        {
            playerStats.HealPlayer(amountToHeal);
            GameObject healParticles = Instantiate(currentFX, playerStats.transform);
            Destroy(instantiatedFX);
            weaponSlotManager.LoadWeaponsOnBothSlots();
        }
    }
}