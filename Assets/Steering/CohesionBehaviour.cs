using UnityEngine;

namespace AI.Movement
{
    public class CohesionBehaviour : SteeringBehaviour
    {
        public float radius = 1;

        public override SteeringOutput GetSteering()
        {
            SteeringOutput steering = new SteeringOutput();

            // Get all agents inside the circle
            Vector2 baricenter = Vector2.zero;
            int totalAgents = 0;
            foreach(var agent in FindObjectsOfType<Agent>())
            {
                if (agent.gameObject == this.gameObject) continue;

                if ((agent.transform.position - transform.position).sqrMagnitude <= radius * radius)
                {
                    baricenter += (Vector2)agent.transform.position;
                    totalAgents += 1;
                } 
            }

            // Nobody inside the circle :(
            if (totalAgents == 0)
            {
                return steering;
            }

            baricenter /= totalAgents;

            steering.targetLinearVelocityPercent = (baricenter - (Vector2)transform.position).normalized;
            return steering;
        }
    }

}