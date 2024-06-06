namespace CommandProject
{
    public interface IOrCondition<T> : ICondition<T> { }
    public class OrConditionBase<T> : IOrCondition<T>
    {
        public bool RequiredTruthiness { get; }
        private ICondition<T> conditionA, conditionB;
        public bool Evaluate(T target) => (conditionA.Evaluate(target) || conditionB.Evaluate(target)) == RequiredTruthiness;
    }
}