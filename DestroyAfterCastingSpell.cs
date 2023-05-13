using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SOULS
{
    public class DestroyAfterCastingSpell : MonoBehaviour
    {
        CharacterManager castingCharacter;

        private void Awake()
        {
            castingCharacter = GetComponentInParent<CharacterManager>();
        }

        private void Update()
        {
            if (castingCharacter.isFiringSpell)
            {
                Destroy(gameObject);
            }
        }
    }
}