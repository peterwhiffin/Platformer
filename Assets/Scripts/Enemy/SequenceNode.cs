using Unity.VisualScripting;

namespace PetesPlatformer
{
    public class SelectorNode : Node
    {
        private bool m_processEveryChild = false;
        public SelectorNode(string name) : base(name) 
        {
            
        }

        public override Status Proccess()
        {
            foreach(Node child in r_children)
            {
                switch (child.Proccess())
                {
                    case Status.Running:
                        return Status.Running;
                    case Status.Success:
                        Reset();
                        return Status.Success;
                    case Status.Failure:
                        break;
                }
            }

            Reset();
            return Status.Failure;
        }
    }


    public class SequenceNode : Node
    {
        public SequenceNode(string name) : base(name) { }

        public override Status Proccess()
        {
            while (m_currentChild < r_children.Count)
            {
                var status = r_children[m_currentChild].Proccess();

                if (status != Status.Success)
                {
                    return status;
                }

                m_currentChild++;
            }

            return Status.Success;
        }
    }
}
