namespace MorseCode.BetterReflection
{
    public interface IMethodInfo<in T, in TParameter1, out TReturn> : IMethodInfo<T>
    {
        TReturn Invoke(T o, TParameter1 parameter1);
    }
}