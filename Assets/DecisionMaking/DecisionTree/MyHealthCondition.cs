namespace AI.DecisionTree
{
    public class MyHealthCondition : Decision
    {
        public float minHealth = 5;

        public override bool CheckCondition()
        {
            var myHealthState = GetComponent<HealthState>();
            return myHealthState.health >= minHealth;
        }
    }
}
