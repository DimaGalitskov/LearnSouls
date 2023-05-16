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
    }
}