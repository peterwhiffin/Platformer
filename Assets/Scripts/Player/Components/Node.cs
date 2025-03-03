using System.Collections.Generic;
namespace PetesPlatformer
{
    public class Node
    {
        public enum Status { Success, Failure, Running }
        public readonly string r_name;
        public readonly List<Node> r_children = new();
        public int m_currentChild;

        public Node(string name = "Node")
        {
            r_name = name;
        }

        public void AddChild(Node child)
        {
            r_children.Add(child);
        }

        public virtual Status Proccess()
        {
            return r_children[m_currentChild].Proccess();
        }

        public virtual void Reset()
        {
            m_currentChild = 0;

            foreach(Node child in r_children)
            {
                child.Reset();
            }
        }
    }
}
