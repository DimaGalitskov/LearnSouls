using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOULS
{
    public class Interactable : MonoBehaviour
    {
        public float radius = 1f;
        public string interactableText;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, radius);
        }

        public virtual void Interact(PlayerManager playerManager)
        {
            Debug.Log("You interacted with an object");
        }
    }
}