using AI.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    public class FSMDecisionMaker : DecisionMaker
    {
        public Animator aiAnimator; 

        void Start()
        {
            Agent myAgent = GetComponent<Agent>();

            var seekBe = myAgent.GetComponent<SeekBehaviour>();
            var fleeBe = myAgent.GetComponent<FleeBehaviour>();
            var shootAction = myAgent.GetComponent<ShootAction>();


            var goToBaseState = aiAnimator.GetBehaviour<GoToBaseState>();
            goToBaseState.agent = myAgent;
            goToBaseState.seekBe = seekBe;
            goToBaseState.fleeBe = fleeBe;

            var AttackEnemyState = aiAnimator.GetBehaviour<AttackEnemyState>();
            AttackEnemyState.agent = myAgent;
            AttackEnemyState.seekBe = seekBe;
            AttackEnemyState.fleeBe = fleeBe;
            AttackEnemyState.shootAction = shootAction;

            var GetHealthState = aiAnimator.GetBehaviour<GetHealthState>();
            GetHealthState.agent = myAgent;
            GetHealthState.seekBe = seekBe;
            GetHealthState.fleeBe = fleeBe;

            var RunAwayState = aiAnimator.GetBehaviour<RunAwayState>();
            RunAwayState.agent = myAgent;
            RunAwayState.seekBe = seekBe;
            RunAwayState.fleeBe = fleeBe;


            // Initialise inputs
            MakeDecision();
        }


        public override void MakeDecision()
        {
            // Check state switching
            // ... we just need to update inputs to the AC

            float closestEnemyDistanceSqr = float.MaxValue;
            var allAgents = FindObjectsOfType<Agent>();
            foreach (var agent in allAgents)
            {
                if (agent != GetComponent<Agent>())
                {
                    if ((agent.transform.position - this.transform.position).sqrMagnitude < closestEnemyDistanceSqr
                        && agent.GetComponent<HealthState>().team != this.GetComponent<HealthState>().team)
                    {
                        closestEnemyDistanceSqr = (agent.transform.position - this.transform.position).sqrMagnitude;
                    }
                }
            }
            aiAnimator.SetFloat("ClosestEnemyDistance", closestEnemyDistanceSqr);

            aiAnimator.SetInteger("MyHealth", (int)GetComponent<HealthState>().health);

            aiAnimator.SetInteger("HealthPickups", FindObjectsOfType<HealthPickup>().Length);

            // Execute current state
            // ... the AC will do that for us
        }

    }

}