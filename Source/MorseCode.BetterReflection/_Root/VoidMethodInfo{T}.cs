﻿#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VoidMethodInfo{T}.cs" company="MorseCode Software">
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

    internal class VoidMethodInfo<T> : IVoidMethodInfo<T>
    {
        #region Fields

        private readonly Lazy<Action<T>> invoker;

        private readonly MethodInfo methodInfo;

        private readonly IVoidMethodInfo<T> methodInfoInstance;

        #endregion

        #region Constructors and Destructors

        public VoidMethodInfo(MethodInfo methodInfo)
        {
            this.methodInfo = methodInfo;

            this.methodInfoInstance = this;

            this.invoker = new Lazy<Action<T>>(() => DelegateUtility.CreateDelegate<Action<T>>(this.methodInfo));
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
                return new Type[0];
            }
        }

        Type IMethodInfo.ReturnType
        {
            get
            {
                return typeof(void);
            }
        }

        #endregion

        #region Explicit Interface Methods

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
            if (parameters != null && parameters.Length != 0)
            {
                throw new ArgumentException("Received parameters of type {" + string.Join(",", parameters.Select(p => p.GetType().FullName)) + "}, was of type " + o.GetType().FullName + ", but expected no parameters.", StaticReflection.GetInScopeMemberInfoInternal(() => o).Name);
            }

            this.methodInfoInstance.Invoke(o);

            return null;
        }

        #endregion

        #region Methods

        void IVoidMethodInfo<T>.Invoke(T o)
        {
            this.invoker.Value(o);
        }

        #endregion
    }
}