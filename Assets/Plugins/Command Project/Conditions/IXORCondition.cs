namespace CommandProject
{
    public interface IXORCondition<T> : ICondition<T> { }
    public class XORConditionBase<T> : IXORCondition<T>
    {
        public bool RequiredTruthiness { get; }
        private ICondition<T> conditionA, conditionB;
        public bool Evaluate(T target) => (conditionA.Evaluate(target) ^ conditionB.Evaluate(target)) == RequiredTruthiness;
    }
}