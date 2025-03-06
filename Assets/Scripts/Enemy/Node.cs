using System.Collections.Generic;
using UnityEngine;
using System;

namespace PetesPlatformer
{
    public class Node
    {
        private List<Action> m_abortActions = new();

        public enum Status { Success, Failure, Running }
        public readonly string r_name;
        public readonly List<Node> r_children = new();
        public int m_currentChild;
        public Node m_parentNode;

        public Node(string name = "Node")
        {
            r_name = name;
        }

        public Node AddChild(Node child)
        {
            r_children.Add(child);
            child.m_parentNode = this;
            return this;
        }

        public Node NextLayer()
        {
            return r_children[^1];
        }

        public Node LayerComplete()
        {
            return m_parentNode;
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
