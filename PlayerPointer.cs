using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOULS
{
    public class PlayerPointer : MonoBehaviour
    {
        MeshRenderer mesh;
        PlayerManager playerManager;

        private void Awake()
        {
            mesh = GetComponent<MeshRenderer>();
            playerManager = GetComponentInParent<PlayerManager>();
        }

        private void FixedUpdate()
        {
            HandlePointer();
        }

        private void HandlePointer()
        {
            if (playerManager.isInteracting)
            {
                if (mesh.enabled == false)
                {
                    mesh.enabled = true;
                }
            }
            else
            {
                if (mesh.enabled == true)
                {
                    mesh.enabled = false;
                }
            }
        }
    }
}