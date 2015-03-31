#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReadOnlyPropertyInfo{T,TProperty}.cs" company="MorseCode Software">
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
    using System.Diagnostics.Contracts;
    using System.Reflection;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    using MorseCode.FrameworkExtensions;

    [Serializable]
    internal class ReadOnlyPropertyInfo<T, TProperty> : IReadablePropertyInfo<T, TProperty>, ISerializable
    {
        #region Fields

        private readonly Lazy<Func<T, TProperty>> getter;

        private readonly PropertyInfo propertyInfo;

        private readonly IReadablePropertyInfo<T, TProperty> readWritePropertyInfo;

        #endregion

        #region Constructors and Destructors

        public ReadOnlyPropertyInfo(PropertyInfo propertyInfo)
        {
            this.propertyInfo = propertyInfo;

            this.readWritePropertyInfo = this;

            this.getter = new Lazy<Func<T, TProperty>>(() => DelegateUtility.CreateDelegate<Func<T, TProperty>>(propertyInfo.GetGetMethod()));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadOnlyPropertyInfo{T,TProperty}"/> class from serialized data.
        /// </summary>
        /// <param name="info">
        /// The serialization info.
        /// </param>
        /// <param name="context">
        /// The serialization context.
        /// </param>
        [ContractVerification(false)]
        // ReSharper disable UnusedParameter.Local
        protected ReadOnlyPropertyInfo(SerializationInfo info, StreamingContext context)
            // ReSharper restore UnusedParameter.Local
            : this((PropertyInfo)info.GetValue("p", typeof(PropertyInfo)))
        {
        }

        #endregion

        #region Explicit Interface Properties

        bool IPropertyInfo.IsReadable
        {
            get
            {
                return true;
            }
        }

        bool IPropertyInfo.IsWritable
        {
            get
            {
                return this.IsWritable;
            }
        }

        string IPropertyInfo.Name
        {
            get
            {
                return this.propertyInfo.Name;
            }
        }

        Type IPropertyInfo.ObjectType
        {
            get
            {
                return typeof(T);
            }
        }

        PropertyInfo IPropertyInfo.PropertyInfo
        {
            get
            {
                return this.propertyInfo;
            }
        }

        Type IPropertyInfo.PropertyType
        {
            get
            {
                return typeof(TProperty);
            }
        }

        #endregion

        #region Properties

        protected virtual bool IsWritable
        {
            get
            {
                return false;
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
            info.AddValue("p", this.propertyInfo);
        }

        #endregion

        #region Explicit Interface Methods

        object IReadablePropertyInfo<T>.GetValue(T o)
        {
            return this.readWritePropertyInfo.GetValue(o);
        }

        TProperty IReadablePropertyInfo<T, TProperty>.GetValue(T o)
        {
            return this.getter.Value(o);
        }

        object IReadablePropertyInfo.GetValueUntyped(object o)
        {
            if (!(o is T))
            {
                throw new ArgumentException("Object was of type " + o.GetType().FullName + ", but must be convertible to type " + typeof(T).FullName + ".", StaticReflection.GetInScopeMemberInfoInternal(() => o).Name);
            }

            return this.readWritePropertyInfo.GetValue((T)o);
        }

        #endregion
    }
}