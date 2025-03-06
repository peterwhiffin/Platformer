namespace PetesPlatformer
{
    public class BehaviorTree : Node
    {
        public BehaviorTree(string name) : base(name) { }


        public override Status Proccess()
        {
            while(m_currentChild < r_children.Count)
            {
                var status =  r_children[m_currentChild].Proccess();

                if(status != Status.Success)
                {
                    return status;
                }

                m_currentChild++;
            }

            return Status.Success;
        }
    }
}
