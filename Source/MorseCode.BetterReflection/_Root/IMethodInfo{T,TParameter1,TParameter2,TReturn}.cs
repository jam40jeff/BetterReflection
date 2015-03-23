namespace MorseCode.BetterReflection
{
    public interface IMethodInfo<in T, in TParameter1, in TParameter2, out TReturn> : IMethodInfo<T>
    {
        TReturn Invoke(T o, TParameter1 parameter1, TParameter2 parameter2);
    }
}