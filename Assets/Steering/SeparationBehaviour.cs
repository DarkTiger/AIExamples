using UnityEngine;

namespace AI.Movement
{
    public class SeparationBehaviour : SteeringBehaviour
    {
        public float radius = 1;

        public override SteeringOutput GetSteering()
        {
            SteeringOutput steering = new SteeringOutput();

            // Get all agents inside the circle
            Vector2 totalSeparation = Vector2.zero;
            int totalAgents = 0;
            foreach(var agent in FindObjectsOfType<Agent>())
            {
                if (agent.gameObject == this.gameObject) continue;

                if ((agent.transform.position - transform.position).sqrMagnitude <= radius * radius)
                {
                    totalSeparation += -((Vector2)agent.transform.position - (Vector2)transform.position).normalized;
                    totalAgents += 1;
                } 
            }

            // Nobody inside the circle :(
            if (totalAgents == 0)
            {
                return steering;
            }

            totalSeparation /= totalAgents;

            steering.targetLinearVelocityPercent = totalSeparation;
            return steering;
        }
    }

}