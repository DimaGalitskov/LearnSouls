using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SOULS
{
    public class EnemyManager : MonoBehaviour
    {
        EnemyLocomotionManager enemyLocomotionManager;
        EnemyAnimator enemyAnimator;

        public NavMeshAgent navMeshAgent;
        public State currentState;
        public CharacterStats currentTarget;
        public EnemyStats enemyStats;
        public Rigidbody enemyRigidbody;

        public bool isPerformingAction;
        public bool isInteracting;
        public bool isDead;
        public float rotationSpeed = 25;
        public float maximumAggroRadius = 2f;

        [Header("Combat Flags")]
        public bool canDoCombo;

        [Header("Movement Flags")]
        public bool canRotate;
        public bool isRootRotating;

        [Header("AI Settings")]
        public float detectionRadius = 20;
        public float maximumDetectionAngle = 50;
        public float minimumDetectionAngle = -50;

        [Header("AI Combat Settings")]
        public bool isAllowedCombo;
        public float comboChance;

        public float currentRecoveryTime = 0;

        private void Awake()
        {
            enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
            enemyAnimator = GetComponentInChildren<EnemyAnimator>();
            enemyStats = GetComponent<EnemyStats>();
            navMeshAgent = GetComponentInChildren<NavMeshAgent>();
            enemyRigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            enemyRigidbody.isKinematic = false;
        }

        void Update()
        {
            HandleRecoveryTime();
            HandleState();

            isRootRotating = enemyAnimator.anim.GetBool("isRootRotating");
            isInteracting = enemyAnimator.anim.GetBool("isInteracting");
            canDoCombo = enemyAnimator.anim.GetBool("canDoCombo");
            canRotate = enemyAnimator.anim.GetBool("canRotate");
        }

        private void LateUpdate()
        {
            navMeshAgent.transform.localPosition = Vector3.zero;
            navMeshAgent.transform.localRotation = Quaternion.identity;
        }

        void HandleState()
        {
            if (currentState != null)
            {
                State nextState = currentState.Tick(this, enemyStats, enemyAnimator);

                if (nextState != null)
                {
                    SwitchToNextState(nextState);
                }
            }
        }

        private void SwitchToNextState(State state)
        {
            currentState = state;
        }

        private void HandleRecoveryTime()
        {
            if (currentRecoveryTime > 0)
            {
                currentRecoveryTime -= Time.deltaTime;
            }

            if (isPerformingAction)
            {
                if (currentRecoveryTime <= 0)
                {
                    isPerformingAction = false;
                }
            }
        }
    }
}