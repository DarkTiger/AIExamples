using AI.Movement;

namespace AI.DecisionTree
{
    public class SeekShoot : Action
    {
        public SeekBehaviour seekBe;
        public FleeBehaviour fleeBe;

        public ShootAction shootAction;

        public override void MakeDecision()
        {
            GetComponent<Agent>().maximumLinearVelocity = 0;

            seekBe.weight = 1;
            fleeBe.weight = 0;

            var allAgents = FindObjectsOfType<Agent>();
            foreach(var agent in allAgents)
            {
                if (agent != GetComponent<Agent>()
                    && agent.GetComponent<HealthState>().team != this.GetComponent<HealthState>().team
                    )
                {
                    seekBe.targetTransform = agent.transform;
                    break;
                }
            }

            shootAction.Shoot();
        }
    }
}
