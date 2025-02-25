using System;

namespace PetesPlatformer
{
    public class FuncPredicate : IPredicate
    {
        readonly Func<bool> m_func;

        public FuncPredicate(Func<bool> func)
        {
            m_func = func;
        }

        public bool Evaluate()
        {
            return m_func.Invoke();
        }
    }
}
