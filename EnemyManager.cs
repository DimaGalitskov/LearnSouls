using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SOULS
{
    public class EnemyManager : CharacterManager
    {
        EnemyLocomotionManager enemyLocomotionManager;
        EnemyAnimatorManager enemyAnimator;

        public NavMeshAgent navMeshAgent;
        public State currentState;
        public CharacterStatsManager characterStatsManager;
        public EnemyStatsManager enemyStats;
        public Rigidbody enemyRigidbody;

        public bool isPerformingAction;
        public float rotationSpeed = 25;
        public float maximumAggroRadius = 5f;
        public float minimumAggroRadius = 0.5f;

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
            enemyAnimator = GetComponent<EnemyAnimatorManager>();
            enemyStats = GetComponent<EnemyStatsManager>();
            enemyRigidbody = GetComponent<Rigidbody>();
            navMeshAgent = GetComponentInChildren<NavMeshAgent>();
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