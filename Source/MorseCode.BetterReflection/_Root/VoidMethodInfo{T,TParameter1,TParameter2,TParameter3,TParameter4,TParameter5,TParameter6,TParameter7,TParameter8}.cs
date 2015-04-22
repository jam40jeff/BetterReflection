#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VoidMethodInfo{T,TParameter1,TParameter2,TParameter3,TParameter4,TParameter5,TParameter6,TParameter7,TParameter8}.cs" company="MorseCode Software">
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
    internal class VoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8> : IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8>, ISerializable
    {
        #region Fields

        private readonly Lazy<Action<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8>> invoker;

        private readonly MethodInfo methodInfo;

        private readonly IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8> methodInfoInstance;

        #endregion

        #region Constructors and Destructors

        public VoidMethodInfo(MethodInfo methodInfo)
        {
            this.methodInfo = methodInfo;

            this.methodInfoInstance = this;

            this.invoker = new Lazy<Action<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8>>(() => DelegateUtility.CreateDelegate<Action<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8>>(this.methodInfo));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VoidMethodInfo{T,TParameter1,TParameter2,TParameter3,TParameter4,TParameter5,TParameter6,TParameter7,TParameter8}"/> class from serialized data.
        /// </summary>
        /// <param name="info">
        /// The serialization info.
        /// </param>
        /// <param name="context">
        /// The serialization context.
        /// </param>
        [ContractVerification(false)]
        // ReSharper disable UnusedParameter.Local
        protected VoidMethodInfo(SerializationInfo info, StreamingContext context)
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
                return new[] { typeof(TParameter1), typeof(TParameter2), typeof(TParameter3), typeof(TParameter4), typeof(TParameter5), typeof(TParameter6), typeof(TParameter7), typeof(TParameter8) };
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

        void IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8>.Invoke(T o, TParameter1 parameter1, TParameter2 parameter2, TParameter3 parameter3, TParameter4 parameter4, TParameter5 parameter5, TParameter6 parameter6, TParameter7 parameter7, TParameter8 parameter8)
        {
            this.invoker.Value(o, parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8);
        }

        object IMethodInfo.InvokeFullyUntyped(object o, IEnumerable<object> parameters)
        {
            if (!(o is T))
            {
                throw new ArgumentException("Object was of type " + o.GetType().FullName + ", but must be convertible to type " + typeof(T).FullName + ".", StaticReflection.GetInScopeMemberInfoInternal(() => o).Name);
            }

            return this.methodInfoInstance.InvokePartiallyUntyped((T)o, parameters);
        }

        object IMethodInfo.InvokeFullyUntyped(object o, params object[] parameters)
        {
            return this.methodInfoInstance.InvokeFullyUntyped((T)o, (IEnumerable<object>)parameters);
        }

        object IMethodInfo<T>.InvokePartiallyUntyped(T o, IEnumerable<object> parameters)
        {
            IReadOnlyList<object> parameterList = (parameters ?? new object[0]).ToArray();
            if (parameterList.Count != 8 || !(parameterList[0] is TParameter1) || !(parameterList[1] is TParameter2) || !(parameterList[2] is TParameter3) || !(parameterList[3] is TParameter4) || !(parameterList[4] is TParameter5) || !(parameterList[5] is TParameter6) || !(parameterList[6] is TParameter7) || !(parameterList[7] is TParameter8))
            {
                throw new ArgumentException("Received " + (parameterList.Count < 1 ? "no parameters" : ("parameters of type { " + string.Join(", ", parameterList.Select(p => p.GetType().FullName)) + " }")) + ", but expected parameters of type { " + typeof(TParameter1) + ", " + typeof(TParameter2) + ", " + typeof(TParameter3) + ", " + typeof(TParameter4) + ", " + typeof(TParameter5) + ", " + typeof(TParameter6) + ", " + typeof(TParameter7) + ", " + typeof(TParameter8) + " }.", StaticReflection.GetInScopeMemberInfoInternal(() => parameters).Name);
            }

            this.methodInfoInstance.Invoke(o, (TParameter1)parameterList[0], (TParameter2)parameterList[1], (TParameter3)parameterList[2], (TParameter4)parameterList[3], (TParameter5)parameterList[4], (TParameter6)parameterList[5], (TParameter7)parameterList[6], (TParameter8)parameterList[7]);

            return null;
        }

        object IMethodInfo<T>.InvokePartiallyUntyped(T o, params object[] parameters)
        {
            return this.methodInfoInstance.InvokePartiallyUntyped(o, (IEnumerable<object>)parameters);
        }

        #endregion
    }
}