using AI.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    public class AttackEnemyState : StateMachineBehaviour
    {
        public Agent agent;
        public SeekBehaviour seekBe;
        public FleeBehaviour fleeBe;
        public ShootAction shootAction;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            shootAction.StartShooting();
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            agent.maximumLinearVelocity = 0;

            seekBe.weight = 1;
            fleeBe.weight = 0;

            var allAgents = FindObjectsOfType<Agent>();

            float closestDistanceSqr = float.MaxValue;

            foreach (var otherAgent in allAgents)
            {
                if (otherAgent != agent
                    && otherAgent.GetComponent<HealthState>().team != agent.GetComponent<HealthState>().team
                    && (otherAgent.transform.position - agent.transform.position).sqrMagnitude < closestDistanceSqr
                    )
                {
                    closestDistanceSqr = (otherAgent.transform.position - agent.transform.position).sqrMagnitude;
                    seekBe.targetTransform = otherAgent.transform;
                }
            }
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            shootAction.StopShooting();
        }

    }

}