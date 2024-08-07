using AI.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    public class GetHealthState : StateMachineBehaviour
    {
        public Agent agent;
        public SeekBehaviour seekBe;
        public FleeBehaviour fleeBe;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            agent.maximumLinearVelocity = 1;

            seekBe.weight = 1;
            fleeBe.weight = 0;

            var pickups = FindObjectsOfType<HealthPickup>();
            foreach(HealthPickup pickup in pickups)
            {
                if (pickup.isEnabled)
                {
                    seekBe.targetTransform = pickup.transform;
                    break;
                }
            }
            /*
            else
            {
                seekBe.weight = 0;
                fleeBe.weight = 1;
                fleeBe.targetTransform = FindObjectOfType<Base>().transform;
            }*/
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }

    }

}