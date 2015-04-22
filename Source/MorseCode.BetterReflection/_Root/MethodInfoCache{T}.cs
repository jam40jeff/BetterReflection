#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MethodInfoCache{T}.cs" company="MorseCode Software">
// Copyright (c) 2015 MorseCode Software
// </copyright>
// <summary>
// The MIT License (MIT)
// 
// Copyright (c) 2015 MorseCode Software
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
#endregion

namespace MorseCode.BetterReflection
{
    using System;
    using System.Collections.Concurrent;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Reflection;

    internal static class MethodInfoCache<T>
    {
        // ReSharper disable StaticFieldInGenericType - refers to a method within this type and should have different values for different generic type parameters
        #region Static Fields

        private static readonly MethodInfo[] CreateMethodInfoGenericMethodDefinitions;

        private static readonly MethodInfo[] CreateVoidMethodInfoGenericMethodDefinitions;

        // ReSharper restore StaticFieldInGenericType
        private static readonly ConcurrentDictionary<MethodInfo, IMethodInfo<T>> MethodInfoByMethodInfo = new ConcurrentDictionary<MethodInfo, IMethodInfo<T>>();

        #endregion

        #region Constructors and Destructors

        static MethodInfoCache()
        {
            CreateMethodInfoGenericMethodDefinitions = new MethodInfo[9];
            CreateVoidMethodInfoGenericMethodDefinitions = new MethodInfo[8];

            MethodInfo createMethodInfoMethodInfo = StaticReflection.GetInScopeMethodInfoFromMethodCallInternal(() => CreateMethodInfo<object>(null));
            CreateMethodInfoGenericMethodDefinitions[0] = createMethodInfoMethodInfo.GetGenericMethodDefinition();

            MethodInfo createMethodInfo1MethodInfo = StaticReflection.GetInScopeMethodInfoFromMethodCallInternal(() => CreateMethodInfo<object, object>(null));
            CreateMethodInfoGenericMethodDefinitions[1] = createMethodInfo1MethodInfo.GetGenericMethodDefinition();

            MethodInfo createMethodInfo2MethodInfo = StaticReflection.GetInScopeMethodInfoFromMethodCallInternal(() => CreateMethodInfo<object, object, object>(null));
            CreateMethodInfoGenericMethodDefinitions[2] = createMethodInfo2MethodInfo.GetGenericMethodDefinition();

            MethodInfo createMethodInfo3MethodInfo = StaticReflection.GetInScopeMethodInfoFromMethodCallInternal(() => CreateMethodInfo<object, object, object, object>(null));
            CreateMethodInfoGenericMethodDefinitions[3] = createMethodInfo3MethodInfo.GetGenericMethodDefinition();

            MethodInfo createMethodInfo4MethodInfo = StaticReflection.GetInScopeMethodInfoFromMethodCallInternal(() => CreateMethodInfo<object, object, object, object, object>(null));
            CreateMethodInfoGenericMethodDefinitions[4] = createMethodInfo4MethodInfo.GetGenericMethodDefinition();

            MethodInfo createMethodInfo5MethodInfo = StaticReflection.GetInScopeMethodInfoFromMethodCallInternal(() => CreateMethodInfo<object, object, object, object, object, object>(null));
            CreateMethodInfoGenericMethodDefinitions[5] = createMethodInfo5MethodInfo.GetGenericMethodDefinition();

            MethodInfo createMethodInfo6MethodInfo = StaticReflection.GetInScopeMethodInfoFromMethodCallInternal(() => CreateMethodInfo<object, object, object, object, object, object, object>(null));
            CreateMethodInfoGenericMethodDefinitions[6] = createMethodInfo6MethodInfo.GetGenericMethodDefinition();

            MethodInfo createMethodInfo7MethodInfo = StaticReflection.GetInScopeMethodInfoFromMethodCallInternal(() => CreateMethodInfo<object, object, object, object, object, object, object, object>(null));
            CreateMethodInfoGenericMethodDefinitions[7] = createMethodInfo7MethodInfo.GetGenericMethodDefinition();

            MethodInfo createMethodInfo8MethodInfo = StaticReflection.GetInScopeMethodInfoFromMethodCallInternal(() => CreateMethodInfo<object, object, object, object, object, object, object, object, object>(null));
            CreateMethodInfoGenericMethodDefinitions[8] = createMethodInfo8MethodInfo.GetGenericMethodDefinition();

            MethodInfo createVoidMethodInfo1MethodInfo = StaticReflection.GetInScopeMethodInfoFromMethodCallInternal(() => CreateVoidMethodInfo<object>(null));
            CreateVoidMethodInfoGenericMethodDefinitions[0] = createVoidMethodInfo1MethodInfo.GetGenericMethodDefinition();

            MethodInfo createVoidMethodInfo2MethodInfo = StaticReflection.GetInScopeMethodInfoFromMethodCallInternal(() => CreateVoidMethodInfo<object, object>(null));
            CreateVoidMethodInfoGenericMethodDefinitions[1] = createVoidMethodInfo2MethodInfo.GetGenericMethodDefinition();

            MethodInfo createVoidMethodInfo3MethodInfo = StaticReflection.GetInScopeMethodInfoFromMethodCallInternal(() => CreateVoidMethodInfo<object, object, object>(null));
            CreateVoidMethodInfoGenericMethodDefinitions[2] = createVoidMethodInfo3MethodInfo.GetGenericMethodDefinition();

            MethodInfo createVoidMethodInfo4MethodInfo = StaticReflection.GetInScopeMethodInfoFromMethodCallInternal(() => CreateVoidMethodInfo<object, object, object, object>(null));
            CreateVoidMethodInfoGenericMethodDefinitions[3] = createVoidMethodInfo4MethodInfo.GetGenericMethodDefinition();

            MethodInfo createVoidMethodInfo5MethodInfo = StaticReflection.GetInScopeMethodInfoFromMethodCallInternal(() => CreateVoidMethodInfo<object, object, object, object, object>(null));
            CreateVoidMethodInfoGenericMethodDefinitions[4] = createVoidMethodInfo5MethodInfo.GetGenericMethodDefinition();

            MethodInfo createVoidMethodInfo6MethodInfo = StaticReflection.GetInScopeMethodInfoFromMethodCallInternal(() => CreateVoidMethodInfo<object, object, object, object, object, object>(null));
            CreateVoidMethodInfoGenericMethodDefinitions[5] = createVoidMethodInfo6MethodInfo.GetGenericMethodDefinition();

            MethodInfo createVoidMethodInfo7MethodInfo = StaticReflection.GetInScopeMethodInfoFromMethodCallInternal(() => CreateVoidMethodInfo<object, object, object, object, object, object, object>(null));
            CreateVoidMethodInfoGenericMethodDefinitions[6] = createVoidMethodInfo7MethodInfo.GetGenericMethodDefinition();

            MethodInfo createVoidMethodInfo8MethodInfo = StaticReflection.GetInScopeMethodInfoFromMethodCallInternal(() => CreateVoidMethodInfo<object, object, object, object, object, object, object, object>(null));
            CreateVoidMethodInfoGenericMethodDefinitions[7] = createVoidMethodInfo8MethodInfo.GetGenericMethodDefinition();
        }

        #endregion

        #region Methods

        internal static IMethodInfo<T, TReturn> GetMethodInfo<TReturn>(MethodInfo methodInfo)
        {
            Contract.Requires(methodInfo != null);
            Contract.Ensures(Contract.Result<IMethodInfo<T, TReturn>>() != null);

            IMethodInfo<T> info = GetMethodInfo(methodInfo);
            IMethodInfo<T, TReturn> typedInfo = info as IMethodInfo<T, TReturn>;
            if (typedInfo == null)
            {
                throw new InvalidOperationException("For method with name " + info.MethodInfo.Name + ", the actual types did not match the expected types.");
            }

            return typedInfo;
        }

        internal static IMethodInfo<T, TParameter1, TReturn> GetMethodInfo<TParameter1, TReturn>(MethodInfo methodInfo)
        {
            Contract.Requires(methodInfo != null);
            Contract.Ensures(Contract.Result<IMethodInfo<T, TParameter1, TReturn>>() != null);

            IMethodInfo<T> info = GetMethodInfo(methodInfo);
            IMethodInfo<T, TParameter1, TReturn> typedInfo = info as IMethodInfo<T, TParameter1, TReturn>;
            if (typedInfo == null)
            {
                throw new InvalidOperationException("For method with name " + info.MethodInfo.Name + ", the actual types did not match the expected types.");
            }

            return typedInfo;
        }

        internal static IMethodInfo<T, TParameter1, TParameter2, TReturn> GetMethodInfo<TParameter1, TParameter2, TReturn>(MethodInfo methodInfo)
        {
            Contract.Requires(methodInfo != null);
            Contract.Ensures(Contract.Result<IMethodInfo<T, TParameter1, TParameter2, TReturn>>() != null);

            IMethodInfo<T> info = GetMethodInfo(methodInfo);
            IMethodInfo<T, TParameter1, TParameter2, TReturn> typedInfo = info as IMethodInfo<T, TParameter1, TParameter2, TReturn>;
            if (typedInfo == null)
            {
                throw new InvalidOperationException("For method with name " + info.MethodInfo.Name + ", the actual types did not match the expected types.");
            }

            return typedInfo;
        }

        internal static IMethodInfo<T, TParameter1, TParameter2, TParameter3, TReturn> GetMethodInfo<TParameter1, TParameter2, TParameter3, TReturn>(MethodInfo methodInfo)
        {
            Contract.Requires(methodInfo != null);
            Contract.Ensures(Contract.Result<IMethodInfo<T, TParameter1, TParameter2, TParameter3, TReturn>>() != null);

            IMethodInfo<T> info = GetMethodInfo(methodInfo);
            IMethodInfo<T, TParameter1, TParameter2, TParameter3, TReturn> typedInfo = info as IMethodInfo<T, TParameter1, TParameter2, TParameter3, TReturn>;
            if (typedInfo == null)
            {
                throw new InvalidOperationException("For method with name " + info.MethodInfo.Name + ", the actual types did not match the expected types.");
            }

            return typedInfo;
        }

        internal static IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TReturn> GetMethodInfo<TParameter1, TParameter2, TParameter3, TParameter4, TReturn>(MethodInfo methodInfo)
        {
            Contract.Requires(methodInfo != null);
            Contract.Ensures(Contract.Result<IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TReturn>>() != null);

            IMethodInfo<T> info = GetMethodInfo(methodInfo);
            IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TReturn> typedInfo = info as IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TReturn>;
            if (typedInfo == null)
            {
                throw new InvalidOperationException("For method with name " + info.MethodInfo.Name + ", the actual types did not match the expected types.");
            }

            return typedInfo;
        }

        internal static IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TReturn> GetMethodInfo<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TReturn>(MethodInfo methodInfo)
        {
            Contract.Requires(methodInfo != null);
            Contract.Ensures(Contract.Result<IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TReturn>>() != null);

            IMethodInfo<T> info = GetMethodInfo(methodInfo);
            IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TReturn> typedInfo = info as IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TReturn>;
            if (typedInfo == null)
            {
                throw new InvalidOperationException("For method with name " + info.MethodInfo.Name + ", the actual types did not match the expected types.");
            }

            return typedInfo;
        }

        internal static IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TReturn> GetMethodInfo<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TReturn>(MethodInfo methodInfo)
        {
            Contract.Requires(methodInfo != null);
            Contract.Ensures(Contract.Result<IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TReturn>>() != null);

            IMethodInfo<T> info = GetMethodInfo(methodInfo);
            IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TReturn> typedInfo = info as IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TReturn>;
            if (typedInfo == null)
            {
                throw new InvalidOperationException("For method with name " + info.MethodInfo.Name + ", the actual types did not match the expected types.");
            }

            return typedInfo;
        }

        internal static IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TReturn> GetMethodInfo<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TReturn>(MethodInfo methodInfo)
        {
            Contract.Requires(methodInfo != null);
            Contract.Ensures(Contract.Result<IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TReturn>>() != null);

            IMethodInfo<T> info = GetMethodInfo(methodInfo);
            IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TReturn> typedInfo = info as IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TReturn>;
            if (typedInfo == null)
            {
                throw new InvalidOperationException("For method with name " + info.MethodInfo.Name + ", the actual types did not match the expected types.");
            }

            return typedInfo;
        }

        internal static IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8, TReturn> GetMethodInfo<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8, TReturn>(MethodInfo methodInfo)
        {
            Contract.Requires(methodInfo != null);
            Contract.Ensures(Contract.Result<IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8, TReturn>>() != null);

            IMethodInfo<T> info = GetMethodInfo(methodInfo);
            IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8, TReturn> typedInfo = info as IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8, TReturn>;
            if (typedInfo == null)
            {
                throw new InvalidOperationException("For method with name " + info.MethodInfo.Name + ", the actual types did not match the expected types.");
            }

            return typedInfo;
        }

        internal static IMethodInfo<T> GetMethodInfo(MethodInfo methodInfo)
        {
            Contract.Requires(methodInfo != null);
            Contract.Ensures(Contract.Result<IMethodInfo<T>>() != null);

            IMethodInfo<T> result = MethodInfoByMethodInfo.GetOrAdd(
                methodInfo,
                m =>
                {
                    ParameterInfo[] parameters = m.GetParameters();

                    if (parameters.Any(p => p.ParameterType.IsByRef))
                    {
                        return new ReflectionWrapperMethodInfo<T>(methodInfo);
                    }

                    if (m.ReturnType == typeof(void))
                    {
                        if (parameters.Length == 0)
                        {
                            return CreateVoidMethodInfo(methodInfo);
                        }

                        if (parameters.Length > CreateVoidMethodInfoGenericMethodDefinitions.Length)
                        {
                            throw new InvalidOperationException("Could not create an IVoidMethodInfo with " + parameters.Length + " parameters.");
                        }

                        return (IMethodInfo<T>)CreateVoidMethodInfoGenericMethodDefinitions[parameters.Length - 1].MakeGenericMethod(parameters.Select(p => p.ParameterType).ToArray()).Invoke(null, new object[] { m });
                    }

                    if (parameters.Length >= CreateMethodInfoGenericMethodDefinitions.Length)
                    {
                        throw new InvalidOperationException("Could not create an IMethodInfo with " + parameters.Length + " parameters.");
                    }

                    return (IMethodInfo<T>)CreateMethodInfoGenericMethodDefinitions[parameters.Length].MakeGenericMethod(parameters.Select(p => p.ParameterType).Concat(new[] { m.ReturnType }).ToArray()).Invoke(null, new object[] { m });
                });
            Contract.Assume(result != null);
            return result;
        }

        internal static IVoidMethodInfo<T> GetVoidMethodInfo(MethodInfo methodInfo)
        {
            Contract.Requires(methodInfo != null);
            Contract.Ensures(Contract.Result<IVoidMethodInfo<T>>() != null);

            IMethodInfo<T> info = GetMethodInfo(methodInfo);
            IVoidMethodInfo<T> typedInfo = info as IVoidMethodInfo<T>;
            if (typedInfo == null)
            {
                throw new InvalidOperationException("For method with name " + info.MethodInfo.Name + ", the actual types did not match the expected types.");
            }

            return typedInfo;
        }

        internal static IVoidMethodInfo<T, TParameter1> GetVoidMethodInfo<TParameter1>(MethodInfo methodInfo)
        {
            Contract.Requires(methodInfo != null);
            Contract.Ensures(Contract.Result<IVoidMethodInfo<T, TParameter1>>() != null);

            IMethodInfo<T> info = GetMethodInfo(methodInfo);
            IVoidMethodInfo<T, TParameter1> typedInfo = info as IVoidMethodInfo<T, TParameter1>;
            if (typedInfo == null)
            {
                throw new InvalidOperationException("For method with name " + info.MethodInfo.Name + ", the actual types did not match the expected types.");
            }

            return typedInfo;
        }

        internal static IVoidMethodInfo<T, TParameter1, TParameter2> GetVoidMethodInfo<TParameter1, TParameter2>(MethodInfo methodInfo)
        {
            Contract.Requires(methodInfo != null);
            Contract.Ensures(Contract.Result<IVoidMethodInfo<T, TParameter1, TParameter2>>() != null);

            IMethodInfo<T> info = GetMethodInfo(methodInfo);
            IVoidMethodInfo<T, TParameter1, TParameter2> typedInfo = info as IVoidMethodInfo<T, TParameter1, TParameter2>;
            if (typedInfo == null)
            {
                throw new InvalidOperationException("For method with name " + info.MethodInfo.Name + ", the actual types did not match the expected types.");
            }

            return typedInfo;
        }

        internal static IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3> GetVoidMethodInfo<TParameter1, TParameter2, TParameter3>(MethodInfo methodInfo)
        {
            Contract.Requires(methodInfo != null);
            Contract.Ensures(Contract.Result<IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3>>() != null);

            IMethodInfo<T> info = GetMethodInfo(methodInfo);
            IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3> typedInfo = info as IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3>;
            if (typedInfo == null)
            {
                throw new InvalidOperationException("For method with name " + info.MethodInfo.Name + ", the actual types did not match the expected types.");
            }

            return typedInfo;
        }

        internal static IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4> GetVoidMethodInfo<TParameter1, TParameter2, TParameter3, TParameter4>(MethodInfo methodInfo)
        {
            Contract.Requires(methodInfo != null);
            Contract.Ensures(Contract.Result<IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4>>() != null);

            IMethodInfo<T> info = GetMethodInfo(methodInfo);
            IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4> typedInfo = info as IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4>;
            if (typedInfo == null)
            {
                throw new InvalidOperationException("For method with name " + info.MethodInfo.Name + ", the actual types did not match the expected types.");
            }

            return typedInfo;
        }

        internal static IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5> GetVoidMethodInfo<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5>(MethodInfo methodInfo)
        {
            Contract.Requires(methodInfo != null);
            Contract.Ensures(Contract.Result<IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5>>() != null);

            IMethodInfo<T> info = GetMethodInfo(methodInfo);
            IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5> typedInfo = info as IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5>;
            if (typedInfo == null)
            {
                throw new InvalidOperationException("For method with name " + info.MethodInfo.Name + ", the actual types did not match the expected types.");
            }

            return typedInfo;
        }

        internal static IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6> GetVoidMethodInfo<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6>(MethodInfo methodInfo)
        {
            Contract.Requires(methodInfo != null);
            Contract.Ensures(Contract.Result<IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6>>() != null);

            IMethodInfo<T> info = GetMethodInfo(methodInfo);
            IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6> typedInfo = info as IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6>;
            if (typedInfo == null)
            {
                throw new InvalidOperationException("For method with name " + info.MethodInfo.Name + ", the actual types did not match the expected types.");
            }

            return typedInfo;
        }

        internal static IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7> GetVoidMethodInfo<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7>(MethodInfo methodInfo)
        {
            Contract.Requires(methodInfo != null);
            Contract.Ensures(Contract.Result<IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7>>() != null);

            IMethodInfo<T> info = GetMethodInfo(methodInfo);
            IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7> typedInfo = info as IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7>;
            if (typedInfo == null)
            {
                throw new InvalidOperationException("For method with name " + info.MethodInfo.Name + ", the actual types did not match the expected types.");
            }

            return typedInfo;
        }

        internal static IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8> GetVoidMethodInfo<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8>(MethodInfo methodInfo)
        {
            Contract.Requires(methodInfo != null);
            Contract.Ensures(Contract.Result<IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8>>() != null);

            IMethodInfo<T> info = GetMethodInfo(methodInfo);
            IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8> typedInfo = info as IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8>;
            if (typedInfo == null)
            {
                throw new InvalidOperationException("For method with name " + info.MethodInfo.Name + ", the actual types did not match the expected types.");
            }

            return typedInfo;
        }

        private static IMethodInfo<T, TReturn> CreateMethodInfo<TReturn>(MethodInfo methodInfo)
        {
            Contract.Requires(methodInfo != null);
            Contract.Ensures(Contract.Result<IMethodInfo<T, TReturn>>() != null);

            return new MethodInfo<T, TReturn>(methodInfo);
        }

        private static IMethodInfo<T, TParameter1, TReturn> CreateMethodInfo<TParameter1, TReturn>(MethodInfo methodInfo)
        {
            Contract.Requires(methodInfo != null);
            Contract.Ensures(Contract.Result<IMethodInfo<T, TParameter1, TReturn>>() != null);

            return new MethodInfo<T, TParameter1, TReturn>(methodInfo);
        }

        private static IMethodInfo<T, TParameter1, TParameter2, TReturn> CreateMethodInfo<TParameter1, TParameter2, TReturn>(MethodInfo methodInfo)
        {
            Contract.Requires(methodInfo != null);
            Contract.Ensures(Contract.Result<IMethodInfo<T, TParameter1, TParameter2, TReturn>>() != null);

            return new MethodInfo<T, TParameter1, TParameter2, TReturn>(methodInfo);
        }

        private static IMethodInfo<T, TParameter1, TParameter2, TParameter3, TReturn> CreateMethodInfo<TParameter1, TParameter2, TParameter3, TReturn>(MethodInfo methodInfo)
        {
            Contract.Requires(methodInfo != null);
            Contract.Ensures(Contract.Result<IMethodInfo<T, TParameter1, TParameter2, TParameter3, TReturn>>() != null);

            return new MethodInfo<T, TParameter1, TParameter2, TParameter3, TReturn>(methodInfo);
        }

        private static IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TReturn> CreateMethodInfo<TParameter1, TParameter2, TParameter3, TParameter4, TReturn>(MethodInfo methodInfo)
        {
            Contract.Requires(methodInfo != null);
            Contract.Ensures(Contract.Result<IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TReturn>>() != null);

            return new MethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TReturn>(methodInfo);
        }

        private static IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TReturn> CreateMethodInfo<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TReturn>(MethodInfo methodInfo)
        {
            Contract.Requires(methodInfo != null);
            Contract.Ensures(Contract.Result<IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TReturn>>() != null);

            return new MethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TReturn>(methodInfo);
        }

        private static IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TReturn> CreateMethodInfo<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TReturn>(MethodInfo methodInfo)
        {
            Contract.Requires(methodInfo != null);
            Contract.Ensures(Contract.Result<IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TReturn>>() != null);

            return new MethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TReturn>(methodInfo);
        }

        private static IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TReturn> CreateMethodInfo<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TReturn>(MethodInfo methodInfo)
        {
            Contract.Requires(methodInfo != null);
            Contract.Ensures(Contract.Result<IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TReturn>>() != null);

            return new MethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TReturn>(methodInfo);
        }

        private static IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8, TReturn> CreateMethodInfo<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8, TReturn>(MethodInfo methodInfo)
        {
            Contract.Requires(methodInfo != null);
            Contract.Ensures(Contract.Result<IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8, TReturn>>() != null);

            return new MethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8, TReturn>(methodInfo);
        }

        private static IVoidMethodInfo<T> CreateVoidMethodInfo(MethodInfo methodInfo)
        {
            Contract.Requires(methodInfo != null);
            Contract.Ensures(Contract.Result<IVoidMethodInfo<T>>() != null);

            return new VoidMethodInfo<T>(methodInfo);
        }

        private static IVoidMethodInfo<T, TParameter1> CreateVoidMethodInfo<TParameter1>(MethodInfo methodInfo)
        {
            Contract.Requires(methodInfo != null);
            Contract.Ensures(Contract.Result<IVoidMethodInfo<T, TParameter1>>() != null);

            return new VoidMethodInfo<T, TParameter1>(methodInfo);
        }

        private static IVoidMethodInfo<T, TParameter1, TParameter2> CreateVoidMethodInfo<TParameter1, TParameter2>(MethodInfo methodInfo)
        {
            Contract.Requires(methodInfo != null);
            Contract.Ensures(Contract.Result<IVoidMethodInfo<T, TParameter1, TParameter2>>() != null);

            return new VoidMethodInfo<T, TParameter1, TParameter2>(methodInfo);
        }

        private static IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3> CreateVoidMethodInfo<TParameter1, TParameter2, TParameter3>(MethodInfo methodInfo)
        {
            Contract.Requires(methodInfo != null);
            Contract.Ensures(Contract.Result<IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3>>() != null);

            return new VoidMethodInfo<T, TParameter1, TParameter2, TParameter3>(methodInfo);
        }

        private static IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4> CreateVoidMethodInfo<TParameter1, TParameter2, TParameter3, TParameter4>(MethodInfo methodInfo)
        {
            Contract.Requires(methodInfo != null);
            Contract.Ensures(Contract.Result<IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4>>() != null);

            return new VoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4>(methodInfo);
        }

        private static IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5> CreateVoidMethodInfo<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5>(MethodInfo methodInfo)
        {
            Contract.Requires(methodInfo != null);
            Contract.Ensures(Contract.Result<IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5>>() != null);

            return new VoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5>(methodInfo);
        }

        private static IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6> CreateVoidMethodInfo<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6>(MethodInfo methodInfo)
        {
            Contract.Requires(methodInfo != null);
            Contract.Ensures(Contract.Result<IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6>>() != null);

            return new VoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6>(methodInfo);
        }

        private static IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7> CreateVoidMethodInfo<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7>(MethodInfo methodInfo)
        {
            Contract.Requires(methodInfo != null);
            Contract.Ensures(Contract.Result<IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7>>() != null);

            return new VoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7>(methodInfo);
        }

        private static IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8> CreateVoidMethodInfo<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8>(MethodInfo methodInfo)
        {
            Contract.Requires(methodInfo != null);
            Contract.Ensures(Contract.Result<IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8>>() != null);

            return new VoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8>(methodInfo);
        }

        #endregion
    }
}