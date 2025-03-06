namespace PetesPlatformer
{
    public class LeafNode : Node
    {
        private readonly IStrategy r_strategy;

        public LeafNode(string name, IStrategy r_strategy) : base(name)
        {
            this.r_strategy = r_strategy;
        }

        public override Status Proccess()
        {
            return r_strategy.Process();
        }

        public override void Reset()
        {
            r_strategy.Reset();
        }
    }
}
