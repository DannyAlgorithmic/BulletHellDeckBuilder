namespace CommandProject
{
    public interface IComparativeConditional<TA, TB> { bool Check(TA dataA, TB _dataB); }
    public interface AndComparativeCondition<TA, TB> : IComparativeConditional<TA, TB> { }
    public interface OrComparativeCondition<TA, TB> : IComparativeConditional<TA, TB> { }
    public interface XORComparativeCondition<TA, TB> : IComparativeConditional<TA, TB> { }
    public interface AllComparativeCondition<TA, TB> : IComparativeConditional<TA, TB> { }
    public interface AnyComparativeCondition<TA, TB> : IComparativeConditional<TA, TB> { }
}