using System.Collections.Generic;
namespace CommandProject
{
    public interface IAllCondition<T> : ICondition<T> { }
    public class AllConditionBase<T> : IAllCondition<T>
    {
        public bool RequiredTruthiness { get; }
        private List<ICondition<T>> conditions;
        public bool Evaluate(T target)
        {
            bool truthiness = true;
            for (int i = 0, max = conditions.Count; i < max; i++)
            {
                if (conditions[i].Evaluate(target) != RequiredTruthiness)
                {
                    truthiness = false;
                    break;
                }
            }
            return truthiness;
        }
    }
}