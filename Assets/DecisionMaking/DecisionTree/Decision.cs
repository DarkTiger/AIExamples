namespace AI.DecisionTree
{
    public abstract class Decision : Node
    {
        public Node trueNode;
        public Node falseNode;

        public abstract bool CheckCondition();

        public override void MakeDecision()
        {
            if (CheckCondition())
            {
                trueNode.MakeDecision();
            }
            else
            {
                falseNode.MakeDecision();
            }
        }
    }
}
