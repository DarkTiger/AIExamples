using AI.Movement;

namespace AI.DecisionTree
{
    public class EnemyCloseCondition : Decision
    {
        public float minDistance = 1;

        public override bool CheckCondition()
        {
            var allAgents = FindObjectsOfType<Agent>();
            foreach (var agent in allAgents)
            {
                if (agent != GetComponent<Agent>())
                {
                    if ( (agent.transform.position - this.transform.position).sqrMagnitude < minDistance * minDistance
                        && agent.GetComponent<HealthState>().team != this.GetComponent<HealthState>().team)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
