namespace CommandProject
{
    public class ConditionalEffect<T> : IEffect<T>
    {
        private ICondition<T> condition;
        private IEffect<T> effect;
        public void Execute(T target) { if (condition.Evaluate(target)) effect.Execute(target); }
    }
}