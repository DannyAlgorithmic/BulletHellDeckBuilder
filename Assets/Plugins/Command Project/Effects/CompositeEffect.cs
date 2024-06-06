using System.Collections.Generic;
namespace CommandProject
{
    public class CompositeEffect<T> : IEffect<T>
    {
        private List<IEffect<T>> effects;
        public void Execute(T target) => effects.ForEach(e => e.Execute(target));
    }
}