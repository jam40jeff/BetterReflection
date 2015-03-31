#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReadWritePropertyInfo{T,TProperty}.cs" company="MorseCode Software">
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
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Contracts;
    using System.Reflection;
    using System.Runtime.Serialization;

    using MorseCode.FrameworkExtensions;

    [Serializable]
    internal class ReadWritePropertyInfo<T, TProperty> : ReadOnlyPropertyInfo<T, TProperty>, IReadWritePropertyInfo<T, TProperty>
    {
        #region Fields

        private readonly IReadWritePropertyInfo<T, TProperty> readWritePropertyInfo;

        private readonly Lazy<Action<T, TProperty>> setter;

        #endregion

        #region Constructors and Destructors

        public ReadWritePropertyInfo(PropertyInfo propertyInfo)
            : base(propertyInfo)
        {
            this.readWritePropertyInfo = this;

            this.setter = new Lazy<Action<T, TProperty>>(() => DelegateUtility.CreateDelegate<Action<T, TProperty>>(propertyInfo.GetSetMethod()));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadWritePropertyInfo{T,TProperty}"/> class from serialized data.
        /// </summary>
        /// <param name="info">
        /// The serialization info.
        /// </param>
        /// <param name="context">
        /// The serialization context.
        /// </param>
        [SuppressMessage("Microsoft.Usage", "CA2236:CallBaseClassMethodsOnISerializableTypes", Justification = "The serialization constructions do nothing other than provide values to the real constructors.")]
        [ContractVerification(false)]
        // ReSharper disable UnusedParameter.Local
        protected ReadWritePropertyInfo(SerializationInfo info, StreamingContext context) // ReSharper restore UnusedParameter.Local
            : this((PropertyInfo)info.GetValue("p", typeof(PropertyInfo)))
        {
        }

        #endregion

        #region Properties

        protected override bool IsWritable
        {
            get
            {
                return true;
            }
        }

        #endregion

        #region Explicit Interface Methods

        void IWritablePropertyInfo<T, TProperty>.SetValue(T o, TProperty value)
        {
            this.setter.Value(o, value);
        }

        void IWritablePropertyInfo.SetValueFullyUntyped(object o, object value)
        {
            if (!(o is T))
            {
                throw new ArgumentException("Object was of type " + o.GetType().FullName + ", but must be convertible to type " + typeof(T).FullName + ".", StaticReflection.GetInScopeMemberInfoInternal(() => o).Name);
            }

            this.readWritePropertyInfo.SetValuePartiallyUntyped((T)o, value);
        }

        void IWritablePropertyInfo<T>.SetValuePartiallyUntyped(T o, object value)
        {
            if (!(value is TProperty))
            {
                throw new ArgumentException("Value was of type " + value.GetType().FullName + ", but must be convertible to type " + typeof(TProperty).FullName + ".", StaticReflection.GetInScopeMemberInfoInternal(() => value).Name);
            }

            this.readWritePropertyInfo.SetValue(o, (TProperty)value);
        }

        #endregion
    }
}