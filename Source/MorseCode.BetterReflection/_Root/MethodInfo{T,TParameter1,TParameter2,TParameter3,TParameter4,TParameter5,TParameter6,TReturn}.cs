﻿#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MethodInfo{T,TParameter1,TParameter2,TParameter3,TParameter4,TParameter5,TParameter6,TReturn}.cs" company="MorseCode Software">
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
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    using MorseCode.FrameworkExtensions;

    [Serializable]
    internal class MethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TReturn> : IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TReturn>, ISerializable
    {
        #region Fields

        private readonly Lazy<Func<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TReturn>> invoker;

        private readonly MethodInfo methodInfo;

        private readonly IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TReturn> methodInfoInstance;

        #endregion

        #region Constructors and Destructors

        public MethodInfo(MethodInfo methodInfo)
        {
            this.methodInfo = methodInfo;

            this.methodInfoInstance = this;

            this.invoker = new Lazy<Func<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TReturn>>(() => DelegateUtility.CreateDelegate<Func<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TReturn>>(this.methodInfo));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodInfo{T,TParameter1,TParameter2,TParameter3,TParameter4,TParameter5,TParameter6,TReturn}"/> class from serialized data.
        /// </summary>
        /// <param name="info">
        /// The serialization info.
        /// </param>
        /// <param name="context">
        /// The serialization context.
        /// </param>
        [ContractVerification(false)]
        // ReSharper disable UnusedParameter.Local
        protected MethodInfo(SerializationInfo info, StreamingContext context)
            // ReSharper restore UnusedParameter.Local
            : this((MethodInfo)info.GetValue("m", typeof(MethodInfo)))
        {
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

        string IMethodInfo.Name
        {
            get
            {
                return this.methodInfo.Name;
            }
        }

        Type IMethodInfo.ObjectType
        {
            get
            {
                return typeof(T);
            }
        }

        IReadOnlyList<Type> IMethodInfo.ParameterTypes
        {
            get
            {
                return new[] { typeof(TParameter1), typeof(TParameter2), typeof(TParameter3), typeof(TParameter4), typeof(TParameter5), typeof(TParameter6) };
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

        #region Public Methods and Operators

        /// <summary>
        /// Gets the object data to serialize.
        /// </summary>
        /// <param name="info">
        /// The serialization info.
        /// </param>
        /// <param name="context">
        /// The serialization context.
        /// </param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("m", this.methodInfo);
        }

        #endregion

        #region Explicit Interface Methods

        TReturn IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TReturn>.Invoke(T o, TParameter1 parameter1, TParameter2 parameter2, TParameter3 parameter3, TParameter4 parameter4, TParameter5 parameter5, TParameter6 parameter6)
        {
            return this.invoker.Value(o, parameter1, parameter2, parameter3, parameter4, parameter5, parameter6);
        }

        object IMethodInfo<T>.InvokeUntyped(T o, params object[] parameters)
        {
            return this.methodInfo.Invoke(o, parameters);
        }

        #endregion
    }
}