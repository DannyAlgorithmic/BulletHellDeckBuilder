using System.Collections.Generic;
namespace CommandProject
{
    public interface IAnyCondition<T> : ICondition<T> { }
    public class AnyConditionBase<T> : IAnyCondition<T>
    {
        public bool RequiredTruthiness { get; }
        private List<ICondition<T>> conditions;
        public bool Evaluate(T target)
        {
            bool truthiness = false;
            for (int i = 0, max = conditions.Count; i < max; i++)
            {
                if (conditions[i].Evaluate(target) == RequiredTruthiness)
                {
                    truthiness = true;
                    break;
                }
            }
            return truthiness;
        }
    }
}