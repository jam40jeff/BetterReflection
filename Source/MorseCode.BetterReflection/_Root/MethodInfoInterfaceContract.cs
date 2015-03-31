﻿#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MethodInfoInterfaceContract.cs" company="MorseCode Software">
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

    [ContractClassFor(typeof(IMethodInfo))]
    internal abstract class MethodInfoInterfaceContract : IMethodInfo
    {
        #region Explicit Interface Properties

        MethodInfo IMethodInfo.MethodInfo
        {
            get
            {
                Contract.Ensures(Contract.Result<MethodInfo>() != null);

                return null;
            }
        }

        Type IMethodInfo.ObjectType
        {
            get
            {
                Contract.Ensures(Contract.Result<Type>() != null);

                return null;
            }
        }

        IReadOnlyList<Type> IMethodInfo.ParameterTypes
        {
            get
            {
                Contract.Ensures(Contract.Result<IReadOnlyList<Type>>() != null);

                return null;
            }
        }

        Type IMethodInfo.ReturnType
        {
            get
            {
                Contract.Ensures(Contract.Result<Type>() != null);

                return null;
            }
        }

        #endregion

        #region Explicit Interface Methods

        object IMethodInfo.InvokeFullyUntyped(object o, IEnumerable<object> parameters)
        {
            Contract.Requires<ArgumentNullException>(o != null, "o");

            return null;
        }

        object IMethodInfo.InvokeFullyUntyped(object o, params object[] parameters)
        {
            Contract.Requires<ArgumentNullException>(o != null, "o");

            return null;
        }

        #endregion
    }
}