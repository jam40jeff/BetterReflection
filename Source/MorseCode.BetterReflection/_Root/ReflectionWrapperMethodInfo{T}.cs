#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReflectionWrapperMethodInfo{T}.cs" company="MorseCode Software">
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
    internal class ReflectionWrapperMethodInfo<T> : IMethodInfo<T>, ISerializable
    {
        #region Fields

        private readonly MethodInfo methodInfo;

        private readonly IMethodInfo<T> methodInfoInstance;

        private readonly Lazy<IReadOnlyList<ParameterInfo>> methodParameters;
        private readonly Lazy<IReadOnlyList<Type>> parameterTypes;
        private readonly Lazy<IReadOnlyList<Type>> invokeParameterTypes;
        private readonly Lazy<Type> returnType;

        #endregion

        #region Constructors and Destructors

        public ReflectionWrapperMethodInfo(MethodInfo methodInfo)
        {
            Contract.Requires<ArgumentNullException>(methodInfo != null);
            Contract.Ensures(this.methodParameters != null);
            Contract.Ensures(this.parameterTypes != null);
            Contract.Ensures(this.invokeParameterTypes != null);
            Contract.Ensures(this.returnType != null);
            Contract.Ensures(this.methodInfoInstance != null);

            this.methodInfo = methodInfo;

            this.methodParameters = new Lazy<IReadOnlyList<ParameterInfo>>(methodInfo.GetParameters);
            this.parameterTypes = new Lazy<IReadOnlyList<Type>>(() => this.methodParameters.Value.Select(p => p.ParameterType).ToArray());
            this.invokeParameterTypes = new Lazy<IReadOnlyList<Type>>(() => this.parameterTypes.Value.Select(t => t.IsByRef ? t.GetElementType() : t).ToArray());
            this.returnType = new Lazy<Type>(() => methodInfo.ReturnType);

            this.methodInfoInstance = this;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReflectionWrapperMethodInfo{T}"/> class from serialized data.
        /// </summary>
        /// <param name="info">
        /// The serialization info.
        /// </param>
        /// <param name="context">
        /// The serialization context.
        /// </param>
        [ContractVerification(false)]
        // ReSharper disable UnusedParameter.Local
        protected ReflectionWrapperMethodInfo(SerializationInfo info, StreamingContext context)
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
                return this.parameterTypes.Value;
            }
        }

        Type IMethodInfo.ReturnType
        {
            get
            {
                return this.returnType.Value;
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
            IList<object> parameterList = parameters as IList<object>;
            object[] parameterArray = (parameters ?? new object[0]).ToArray();
            if (parameterArray.Length != this.methodParameters.Value.Count || !parameterArray.Select((p, i) => new { Parameter = p, Index = i }).All(parameterAndIndex => this.invokeParameterTypes.Value[parameterAndIndex.Index].IsInstanceOfType(parameterAndIndex.Parameter) || (parameterAndIndex.Parameter == null && (!this.invokeParameterTypes.Value[parameterAndIndex.Index].IsValueType || this.methodParameters.Value[parameterAndIndex.Index].IsOut))))
            {
                throw new ArgumentException("Received " + (parameterArray.Length < 1 ? "no parameters" : ("parameters of type { " + string.Join(", ", parameterArray.Select(p => p.GetType().FullName)) + " }")) + ", but expected parameters of type { " + string.Join(", ", (this.invokeParameterTypes.Value ?? new Type[0]).Select(t => t.FullName)) + " }.", StaticReflection.GetInScopeMemberInfoInternal(() => parameters).Name);
            }

            object result = this.methodInfo.Invoke(o, parameterArray);

            if (parameterList != null)
            {
                parameterArray.ForEach((p, i) => parameterList[i] = p);
            }

            return result;
        }

        object IMethodInfo<T>.InvokePartiallyUntyped(T o, params object[] parameters)
        {
            return this.methodInfoInstance.InvokePartiallyUntyped(o, (IEnumerable<object>)parameters);
        }

        #endregion

        [ContractInvariantMethod]
        private void CodeContractsInvariants()
        {
            Contract.Invariant(this.methodParameters != null);
            Contract.Invariant(this.parameterTypes != null);
            Contract.Invariant(this.invokeParameterTypes != null);
            Contract.Invariant(this.returnType != null);
            Contract.Invariant(this.methodInfoInstance != null);
        }
    }
}