namespace CommandProject
{
    public interface IAndCondition<T> : ICondition<T> { }
    public class AndConditionBase<T> : IAndCondition<T>
    {
        public bool RequiredTruthiness { get; }
        private ICondition<T> conditionA, conditionB;
        public bool Evaluate(T target) => (conditionA.Evaluate(target) && conditionB.Evaluate(target)) == RequiredTruthiness;
    }
}