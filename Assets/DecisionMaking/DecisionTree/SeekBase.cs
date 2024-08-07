using AI.Movement;
using UnityEngine;

namespace AI.DecisionTree
{

    public class SeekBase : Action
    {
        public SeekBehaviour seekBe;
        public FleeBehaviour fleeBe;

        public override void MakeDecision()
        {
            GetComponent<Agent>().maximumLinearVelocity = 1;

            Transform baseTransform = FindObjectOfType<Base>().transform;

            seekBe.weight = 1;
            fleeBe.weight = 0;

            seekBe.targetTransform = baseTransform;
        }
    }
}
