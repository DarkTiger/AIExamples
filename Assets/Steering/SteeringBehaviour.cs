using UnityEngine;

namespace AI.Movement
{
    public abstract class SteeringBehaviour : MonoBehaviour
    {
        [Range(0,1)]
        public float weight = 1.0f;

        public abstract SteeringOutput GetSteering();
    }

}