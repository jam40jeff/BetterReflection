﻿#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MethodInfo{T,TParameter1,TReturn}.cs" company="MorseCode Software">
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
    using System.Linq;
    using System.Reflection;

    using MorseCode.FrameworkExtensions;

    internal class MethodInfo<T, TParameter1, TReturn> : IMethodInfo<T, TParameter1, TReturn>
    {
        #region Fields

        private readonly Lazy<Func<T, TParameter1, TReturn>> invoker;

        private readonly MethodInfo methodInfo;

        private readonly IMethodInfo<T, TParameter1, TReturn> methodInfoInstance;

        #endregion

        #region Constructors and Destructors

        public MethodInfo(MethodInfo methodInfo)
        {
            this.methodInfo = methodInfo;

            this.methodInfoInstance = this;

            this.invoker = new Lazy<Func<T, TParameter1, TReturn>>(() => DelegateUtility.CreateDelegate<Func<T, TParameter1, TReturn>>(this.methodInfo));
        }

        #endregion

        #region Explicit Interface Properties

        MethodInfo IMethodInfo.MethodInfo
        {
            get
            {
                return this.methodInfo;
            }
        }

        Type IMethodInfo.ObjectType
        {
            get
            {
                return typeof(T);
            }
        }

        Type[] IMethodInfo.ParameterTypes
        {
            get
            {
                return new[] { typeof(TParameter1) };
            }
        }

        Type IMethodInfo.ReturnType
        {
            get
            {
                return typeof(TReturn);
            }
        }

        #endregion

        #region Explicit Interface Methods

        TReturn IMethodInfo<T, TParameter1, TReturn>.Invoke(T o, TParameter1 parameter1)
        {
            return this.invoker.Value(o, parameter1);
        }

        object IMethodInfo.InvokeFullyUntyped(object o, params object[] parameters)
        {
            if (!(o is T))
            {
                throw new ArgumentException("Object was of type " + o.GetType().FullName + ", but must be convertible to type " + typeof(T).FullName + ".", StaticReflection.GetInScopeMemberInfoInternal(() => o).Name);
            }

            return this.methodInfoInstance.InvokePartiallyUntyped((T)o, parameters);
        }

        object IMethodInfo<T>.InvokePartiallyUntyped(T o, params object[] parameters)
        {
            if (parameters == null || parameters.Length != 1 || !(parameters[0] is TParameter1))
            {
                throw new ArgumentException("Received parameters of type {" + string.Join(",", (parameters ?? new Type[0]).Select(p => p.GetType().FullName)) + "}, was of type " + o.GetType().FullName + ", but expected parameters of type {" + typeof(TParameter1) + "}.", StaticReflection.GetInScopeMemberInfoInternal(() => o).Name);
            }

            return this.methodInfoInstance.Invoke(o, (TParameter1)parameters[0]);
        }

        #endregion
    }
}