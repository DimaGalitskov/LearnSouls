using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SOULS
{
    public class PlayerEffectsManager : MonoBehaviour
    {
        PlayerStatsManager playerStats;
        PlayerWeaponSlotManager weaponSlotManager;
        public GameObject currentFX;  //the particle that will play whatever you will do
        public GameObject instantiatedFX;
        public int amountToHeal;

        private void Awake()
        {
            playerStats = GetComponent<PlayerStatsManager>();
            weaponSlotManager = GetComponent<PlayerWeaponSlotManager>();
        }

        public void HealPlayerFromEffect()
        {
            playerStats.HealPlayer(amountToHeal);
            GameObject healParticles = Instantiate(currentFX, playerStats.transform);
            Destroy(instantiatedFX);
            weaponSlotManager.LoadWeaponsOnBothSlots();
            weaponSlotManager.UpdateConsumableUI();
        }
    }
}