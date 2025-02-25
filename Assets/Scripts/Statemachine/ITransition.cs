namespace PetesPlatformer
{
    public interface ITransition
    {
        IState TargetState { get; }
        IPredicate Predicate { get; }
    }
}
