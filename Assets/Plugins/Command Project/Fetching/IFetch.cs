namespace CommandProject
{
    public interface IFetch<T, TResult> { TResult Fetch(T _target); }
}