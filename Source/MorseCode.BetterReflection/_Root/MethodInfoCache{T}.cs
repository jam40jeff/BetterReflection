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
    using System.Diagnostics.CodeAnalysis;
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
            CreateVoidMethodInfoGenericMethodDefinitions = new MethodInfo[2];
            CreateMethodInfoGenericMethodDefinitions = new MethodInfo[3];

            MethodInfo createVoidMethodInfo1MethodInfo = StaticReflection.GetInScopeMethodInfoInternal(() => CreateVoidMethodInfo<object>(null));
            CreateVoidMethodInfoGenericMethodDefinitions[0] = createVoidMethodInfo1MethodInfo.GetGenericMethodDefinition();

            MethodInfo createVoidMethodInfo2MethodInfo = StaticReflection.GetInScopeMethodInfoInternal(() => CreateVoidMethodInfo<object, object>(null));
            CreateVoidMethodInfoGenericMethodDefinitions[1] = createVoidMethodInfo2MethodInfo.GetGenericMethodDefinition();

            MethodInfo createMethodInfoMethodInfo = StaticReflection.GetInScopeMethodInfoInternal(() => CreateMethodInfo<object>(null));
            CreateMethodInfoGenericMethodDefinitions[0] = createMethodInfoMethodInfo.GetGenericMethodDefinition();

            MethodInfo createMethodInfo1MethodInfo = StaticReflection.GetInScopeMethodInfoInternal(() => CreateMethodInfo<object, object>(null));
            CreateMethodInfoGenericMethodDefinitions[1] = createMethodInfo1MethodInfo.GetGenericMethodDefinition();

            MethodInfo createMethodInfo2MethodInfo = StaticReflection.GetInScopeMethodInfoInternal(() => CreateMethodInfo<object, object, object>(null));
            CreateMethodInfoGenericMethodDefinitions[2] = createMethodInfo2MethodInfo.GetGenericMethodDefinition();
        }

        #endregion

        #region Public Methods and Operators

        [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1501:StatementMustNotBeOnSingleLine", Justification = "Reviewed. Suppression is OK here.")]
        public static IMethodInfo<T> GetMethodInfo(MethodInfo methodInfo)
        {
            return MethodInfoByMethodInfo.GetOrAdd(
                methodInfo,
                m =>
                {
                    ParameterInfo[] parameters = m.GetParameters();
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
        }

        public static IMethodInfo<T, TReturn> GetMethodInfo<TReturn>(MethodInfo methodInfo)
        {
            IMethodInfo<T> info = GetMethodInfo(methodInfo);
            IMethodInfo<T, TReturn> typedInfo = info as IMethodInfo<T, TReturn>;
            if (typedInfo == null)
            {
                throw new InvalidOperationException("For method with name " + info.MethodInfo.Name + ", the actual types did not match the expected types.");
            }

            return typedInfo;
        }

        public static IMethodInfo<T, TReturn, TParameter1> GetMethodInfo<TReturn, TParameter1>(MethodInfo methodInfo)
        {
            IMethodInfo<T> info = GetMethodInfo(methodInfo);
            IMethodInfo<T, TReturn, TParameter1> typedInfo = info as IMethodInfo<T, TReturn, TParameter1>;
            if (typedInfo == null)
            {
                throw new InvalidOperationException("For method with name " + info.MethodInfo.Name + ", the actual types did not match the expected types.");
            }

            return typedInfo;
        }

        public static IMethodInfo<T, TReturn, TParameter1, TParameter2> GetMethodInfo<TReturn, TParameter1, TParameter2>(MethodInfo methodInfo)
        {
            IMethodInfo<T> info = GetMethodInfo(methodInfo);
            IMethodInfo<T, TReturn, TParameter1, TParameter2> typedInfo = info as IMethodInfo<T, TReturn, TParameter1, TParameter2>;
            if (typedInfo == null)
            {
                throw new InvalidOperationException("For method with name " + info.MethodInfo.Name + ", the actual types did not match the expected types.");
            }

            return typedInfo;
        }

        public static IVoidMethodInfo<T> GetVoidMethodInfo(MethodInfo methodInfo)
        {
            IMethodInfo<T> info = GetMethodInfo(methodInfo);
            IVoidMethodInfo<T> typedInfo = info as IVoidMethodInfo<T>;
            if (typedInfo == null)
            {
                throw new InvalidOperationException("For method with name " + info.MethodInfo.Name + ", the actual types did not match the expected types.");
            }

            return typedInfo;
        }

        public static IVoidMethodInfo<T, TParameter1> GetVoidMethodInfo<TParameter1>(MethodInfo methodInfo)
        {
            IMethodInfo<T> info = GetMethodInfo(methodInfo);
            IVoidMethodInfo<T, TParameter1> typedInfo = info as IVoidMethodInfo<T, TParameter1>;
            if (typedInfo == null)
            {
                throw new InvalidOperationException("For method with name " + info.MethodInfo.Name + ", the actual types did not match the expected types.");
            }

            return typedInfo;
        }

        public static IVoidMethodInfo<T, TParameter1, TParameter2> GetVoidMethodInfo<TParameter1, TParameter2>(MethodInfo methodInfo)
        {
            IMethodInfo<T> info = GetMethodInfo(methodInfo);
            IVoidMethodInfo<T, TParameter1, TParameter2> typedInfo = info as IVoidMethodInfo<T, TParameter1, TParameter2>;
            if (typedInfo == null)
            {
                throw new InvalidOperationException("For method with name " + info.MethodInfo.Name + ", the actual types did not match the expected types.");
            }

            return typedInfo;
        }

        #endregion

        #region Methods

        private static IMethodInfo<T, TReturn> CreateMethodInfo<TReturn>(MethodInfo methodInfo)
        {
            return new MethodInfo<T, TReturn>(methodInfo);
        }

        private static IMethodInfo<T, TParameter1, TReturn> CreateMethodInfo<TParameter1, TReturn>(MethodInfo methodInfo)
        {
            return new MethodInfo<T, TParameter1, TReturn>(methodInfo);
        }

        private static IMethodInfo<T, TParameter1, TParameter2, TReturn> CreateMethodInfo<TParameter1, TParameter2, TReturn>(MethodInfo methodInfo)
        {
            return new MethodInfo<T, TParameter1, TParameter2, TReturn>(methodInfo);
        }

        private static IVoidMethodInfo<T> CreateVoidMethodInfo(MethodInfo methodInfo)
        {
            return new VoidMethodInfo<T>(methodInfo);
        }

        private static IVoidMethodInfo<T, TParameter1> CreateVoidMethodInfo<TParameter1>(MethodInfo methodInfo)
        {
            return new VoidMethodInfo<T, TParameter1>(methodInfo);
        }

        private static IVoidMethodInfo<T, TParameter1, TParameter2> CreateVoidMethodInfo<TParameter1, TParameter2>(MethodInfo methodInfo)
        {
            return new VoidMethodInfo<T, TParameter1, TParameter2>(methodInfo);
        }

        #endregion
    }
}