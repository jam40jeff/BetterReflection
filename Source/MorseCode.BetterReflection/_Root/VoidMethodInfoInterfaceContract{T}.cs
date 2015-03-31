﻿#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VoidMethodInfoInterfaceContract{T}.cs" company="MorseCode Software">
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
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Reflection;

    [ContractClassFor(typeof(IVoidMethodInfo<>))]
    internal abstract class VoidMethodInfoInterfaceContract<T> : IVoidMethodInfo<T>
    {
        #region Explicit Interface Properties

        MethodInfo IMethodInfo.MethodInfo
        {
            get
            {
                return null;
            }
        }

        Type IMethodInfo.ObjectType
        {
            get
            {
                return null;
            }
        }

        IReadOnlyList<Type> IMethodInfo.ParameterTypes
        {
            get
            {
                return null;
            }
        }

        Type IMethodInfo.ReturnType
        {
            get
            {
                return null;
            }
        }

        #endregion

        #region Explicit Interface Methods

        void IVoidMethodInfo<T>.Invoke(T o)
        {
            Contract.Requires<ArgumentNullException>(!ReferenceEquals(o, null), "o");
        }

        object IMethodInfo.InvokeFullyUntyped(object o, IEnumerable<object> parameters)
        {
            return null;
        }

        object IMethodInfo.InvokeFullyUntyped(object o, params object[] parameters)
        {
            return null;
        }

        object IMethodInfo<T>.InvokePartiallyUntyped(T o, IEnumerable<object> parameters)
        {
            return null;
        }

        object IMethodInfo<T>.InvokePartiallyUntyped(T o, params object[] parameters)
        {
            return null;
        }

        #endregion
    }
}