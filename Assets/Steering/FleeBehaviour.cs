using UnityEngine;

namespace AI.Movement
{
    public class FleeBehaviour : SteeringBehaviour
    {
        public Transform targetTransform;

        public override SteeringOutput GetSteering()
        {
            SteeringOutput steering = new SteeringOutput();
            if (targetTransform == null) return steering;
            steering.targetLinearVelocityPercent = -(targetTransform.position - transform.position).normalized;
            return steering;
        }
    }

}