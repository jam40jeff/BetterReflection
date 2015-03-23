namespace MorseCode.BetterReflection
{
    public interface IVoidMethodInfo<in T> : IMethodInfo<T>
    {
        void Invoke(T o);
    }
}