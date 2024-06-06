namespace CommandProject
{
    public interface ICondition<T>
    {
        bool RequiredTruthiness { get; }
        bool Evaluate(T _data);
    }
}