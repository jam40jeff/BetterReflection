namespace MorseCode.BetterReflection
{
    public interface IMethodInfo<in T, out TReturn> : IMethodInfo<T>
    {
        TReturn Invoke(T o);
    }
}