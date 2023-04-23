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
        public float maximumAttackRange = 2f;

        [Header("AI Settings")]
        public float detectionRadius = 20;
        public float maximumDetectionAngle = 50;
        public float minimumDetectionAngle = -50;

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

            isInteracting = enemyAnimator.anim.GetBool("isInteracting");
        }

        private void FixedUpdate()
        {
            HandleState();
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