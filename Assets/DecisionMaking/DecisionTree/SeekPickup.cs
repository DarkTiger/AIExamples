using AI.Movement;

namespace AI.DecisionTree
{
    public class SeekPickup : Action
    {
        public SeekBehaviour seekBe;
        public FleeBehaviour fleeBe;

        public override void MakeDecision()
        {
            GetComponent<Agent>().maximumLinearVelocity = 1;

            seekBe.weight = 1;
            fleeBe.weight = 0;

            var pickup = FindObjectOfType<HealthPickup>();
            if (pickup)
            {
                seekBe.targetTransform = pickup.transform;
            } else
            {
                seekBe.weight = 0;
                fleeBe.weight = 1;
                fleeBe.targetTransform = FindObjectOfType<Base>().transform;
            }
        }
    }
}
