#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeInfo{T}.cs" company="MorseCode Software">
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
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    [Serializable]
    internal class TypeInfo<T> : ITypeInfo<T>, ISerializable
    {
        #region Fields

        private readonly Type type;

        private readonly ITypeInfo<T> typeInfo;

        #endregion

        #region Constructors and Destructors

        public TypeInfo()
        {
            this.type = typeof(T);
            this.typeInfo = this;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeInfo{T}"/> class from serialized data.
        /// </summary>
        /// <param name="info">
        /// The serialization info.
        /// </param>
        /// <param name="context">
        /// The serialization context.
        /// </param>
        [ContractVerification(false)]
        // ReSharper disable UnusedParameter.Local
        protected TypeInfo(SerializationInfo info, StreamingContext context)
            // ReSharper restore UnusedParameter.Local
            : this()
        {
        }

        #endregion

        #region Explicit Interface Properties

        Type ITypeInfo.Type
        {
            get
            {
                return this.type;
            }
        }

        string ITypeInfo.Name
        {
            get
            {
                return this.type.Name;
            }
        }

        string ITypeInfo.FullName
        {
            get
            {
                return this.type.FullName;
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
        }

        #endregion

        #region Explicit Interface Methods

        IEnumerable<IPropertyInfo<T>> ITypeInfo<T>.GetProperties()
        {
            return typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(PropertyInfoCache<T>.GetPropertyInfo);
        }

        IEnumerable<IPropertyInfo> ITypeInfo.GetProperties()
        {
            return this.typeInfo.GetProperties();
        }

        IPropertyInfo ITypeInfo.GetProperty(string name)
        {
            return this.typeInfo.GetProperty(name);
        }

        IPropertyInfo<T> ITypeInfo<T>.GetProperty(string name)
        {
            PropertyInfo propertyInfo = typeof(T).GetProperty(name, BindingFlags.Instance | BindingFlags.Public);
            if (propertyInfo == null)
            {
                throw new ArgumentException("No public instance property was found with name " + name + " on type " + typeof(T).FullName + ".");
            }

            return PropertyInfoCache<T>.GetPropertyInfo(propertyInfo);
        }

        IReadablePropertyInfo<T, TProperty> ITypeInfo<T>.GetReadableProperty<TProperty>(Expression<Func<T, TProperty>> propertyExpression)
        {
            PropertyInfo propertyInfo = GetPropertyInfo(propertyExpression);

            if (propertyInfo.GetMethod == null || propertyInfo.GetMethod.IsStatic || !propertyInfo.GetMethod.IsPublic)
            {
                throw new InvalidOperationException("Property with name " + propertyInfo.Name + " on type " + typeof(T).FullName + " is not readable.");
            }

            return PropertyInfoCache<T>.GetReadablePropertyInfo<TProperty>(propertyInfo);
        }

        IWritablePropertyInfo<T, TProperty> ITypeInfo<T>.GetWritableProperty<TProperty>(Expression<Func<T, TProperty>> propertyExpression)
        {
            PropertyInfo propertyInfo = GetPropertyInfo(propertyExpression);

            if (propertyInfo.SetMethod == null || propertyInfo.SetMethod.IsStatic || !propertyInfo.SetMethod.IsPublic)
            {
                throw new InvalidOperationException("Property with name " + propertyInfo.Name + " on type " + typeof(T).FullName + " is not writable.");
            }

            return PropertyInfoCache<T>.GetWritablePropertyInfo<TProperty>(propertyInfo);
        }

        IReadWritePropertyInfo<T, TProperty> ITypeInfo<T>.GetReadWriteProperty<TProperty>(Expression<Func<T, TProperty>> propertyExpression)
        {
            PropertyInfo propertyInfo = GetPropertyInfo(propertyExpression);

            if (propertyInfo.GetMethod == null || propertyInfo.GetMethod.IsStatic || !propertyInfo.GetMethod.IsPublic || propertyInfo.SetMethod == null || propertyInfo.SetMethod.IsStatic || !propertyInfo.SetMethod.IsPublic)
            {
                throw new InvalidOperationException("Property with name " + propertyInfo.Name + " on type " + typeof(T).FullName + " is not readable and writable.");
            }

            return PropertyInfoCache<T>.GetReadWritePropertyInfo<TProperty>(propertyInfo);
        }

        IEnumerable<IMethodInfo> ITypeInfo.GetMethods()
        {
            return this.typeInfo.GetMethods();
        }

        IEnumerable<IMethodInfo<T>> ITypeInfo<T>.GetMethods()
        {
            return typeof(T).GetMethods(BindingFlags.Instance | BindingFlags.Public).Select(MethodInfoCache<T>.GetMethodInfo);
        }

        IMethodInfo ITypeInfo.GetMethod(string name)
        {
            return this.typeInfo.GetMethod(name);
        }

        IMethodInfo<T> ITypeInfo<T>.GetMethod(string name)
        {
            MethodInfo methodInfo = typeof(T).GetMethod(name, BindingFlags.Instance | BindingFlags.Public);
            if (methodInfo == null)
            {
                throw new ArgumentException("No public instance method was found with name " + name + " on type " + typeof(T).FullName + ".");
            }

            return MethodInfoCache<T>.GetMethodInfo(methodInfo);
        }

        IMethodInfo ITypeInfo.GetMethod(string name, Type[] types)
        {
            return this.typeInfo.GetMethod(name, types);
        }

        IMethodInfo<T> ITypeInfo<T>.GetMethod(string name, Type[] types)
        {
            MethodInfo methodInfo = typeof(T).GetMethod(name, types);
            if (methodInfo == null || methodInfo.IsStatic || !methodInfo.IsPublic)
            {
                throw new ArgumentException("No public instance method was found with name " + name + " and types { " + string.Join(", ", types.Select(t => t.FullName)) + " } on type " + typeof(T).FullName + ".");
            }

            return MethodInfoCache<T>.GetMethodInfo(methodInfo);
        }

        IMethodInfo<T, TReturn> ITypeInfo<T>.GetMethod<TReturn>(Expression<Func<T, Func<TReturn>>> methodExpression)
        {
            MethodInfo methodInfo = GetMethodInfo(methodExpression);
            return MethodInfoCache<T>.GetMethodInfo<TReturn>(methodInfo);
        }

        IMethodInfo<T, TParameter1, TReturn> ITypeInfo<T>.GetMethod<TParameter1, TReturn>(Expression<Func<T, Func<TParameter1, TReturn>>> methodExpression)
        {
            MethodInfo methodInfo = GetMethodInfo(methodExpression);
            return MethodInfoCache<T>.GetMethodInfo<TParameter1, TReturn>(methodInfo);
        }

        IMethodInfo<T, TParameter1, TParameter2, TReturn> ITypeInfo<T>.GetMethod<TParameter1, TParameter2, TReturn>(Expression<Func<T, Func<TParameter1, TParameter2, TReturn>>> methodExpression)
        {
            MethodInfo methodInfo = GetMethodInfo(methodExpression);
            return MethodInfoCache<T>.GetMethodInfo<TParameter1, TParameter2, TReturn>(methodInfo);
        }

        IMethodInfo<T, TParameter1, TParameter2, TParameter3, TReturn> ITypeInfo<T>.GetMethod<TParameter1, TParameter2, TParameter3, TReturn>(Expression<Func<T, Func<TParameter1, TParameter2, TParameter3, TReturn>>> methodExpression)
        {
            MethodInfo methodInfo = GetMethodInfo(methodExpression);
            return MethodInfoCache<T>.GetMethodInfo<TParameter1, TParameter2, TParameter3, TReturn>(methodInfo);
        }

        IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TReturn> ITypeInfo<T>.GetMethod<TParameter1, TParameter2, TParameter3, TParameter4, TReturn>(Expression<Func<T, Func<TParameter1, TParameter2, TParameter3, TParameter4, TReturn>>> methodExpression)
        {
            MethodInfo methodInfo = GetMethodInfo(methodExpression);
            return MethodInfoCache<T>.GetMethodInfo<TParameter1, TParameter2, TParameter3, TParameter4, TReturn>(methodInfo);
        }

        IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TReturn> ITypeInfo<T>.GetMethod<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TReturn>(Expression<Func<T, Func<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TReturn>>> methodExpression)
        {
            MethodInfo methodInfo = GetMethodInfo(methodExpression);
            return MethodInfoCache<T>.GetMethodInfo<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TReturn>(methodInfo);
        }

        IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TReturn> ITypeInfo<T>.GetMethod<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TReturn>(Expression<Func<T, Func<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TReturn>>> methodExpression)
        {
            MethodInfo methodInfo = GetMethodInfo(methodExpression);
            return MethodInfoCache<T>.GetMethodInfo<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TReturn>(methodInfo);
        }

        IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TReturn> ITypeInfo<T>.GetMethod<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TReturn>(Expression<Func<T, Func<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TReturn>>> methodExpression)
        {
            MethodInfo methodInfo = GetMethodInfo(methodExpression);
            return MethodInfoCache<T>.GetMethodInfo<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TReturn>(methodInfo);
        }

        IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8, TReturn> ITypeInfo<T>.GetMethod<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8, TReturn>(Expression<Func<T, Func<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8, TReturn>>> methodExpression)
        {
            MethodInfo methodInfo = GetMethodInfo(methodExpression);
            return MethodInfoCache<T>.GetMethodInfo<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8, TReturn>(methodInfo);
        }

        IVoidMethodInfo<T> ITypeInfo<T>.GetVoidMethod(Expression<Func<T, Action>> methodExpression)
        {
            MethodInfo methodInfo = GetMethodInfo(methodExpression);
            return MethodInfoCache<T>.GetVoidMethodInfo(methodInfo);
        }

        IVoidMethodInfo<T, TParameter1> ITypeInfo<T>.GetVoidMethod<TParameter1>(Expression<Func<T, Action<TParameter1>>> methodExpression)
        {
            MethodInfo methodInfo = GetMethodInfo(methodExpression);
            return MethodInfoCache<T>.GetVoidMethodInfo<TParameter1>(methodInfo);
        }

        IVoidMethodInfo<T, TParameter1, TParameter2> ITypeInfo<T>.GetVoidMethod<TParameter1, TParameter2>(Expression<Func<T, Action<TParameter1, TParameter2>>> methodExpression)
        {
            MethodInfo methodInfo = GetMethodInfo(methodExpression);
            return MethodInfoCache<T>.GetVoidMethodInfo<TParameter1, TParameter2>(methodInfo);
        }

        IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3> ITypeInfo<T>.GetVoidMethod<TParameter1, TParameter2, TParameter3>(Expression<Func<T, Action<TParameter1, TParameter2, TParameter3>>> methodExpression)
        {
            MethodInfo methodInfo = GetMethodInfo(methodExpression);
            return MethodInfoCache<T>.GetVoidMethodInfo<TParameter1, TParameter2, TParameter3>(methodInfo);
        }

        IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4> ITypeInfo<T>.GetVoidMethod<TParameter1, TParameter2, TParameter3, TParameter4>(Expression<Func<T, Action<TParameter1, TParameter2, TParameter3, TParameter4>>> methodExpression)
        {
            MethodInfo methodInfo = GetMethodInfo(methodExpression);
            return MethodInfoCache<T>.GetVoidMethodInfo<TParameter1, TParameter2, TParameter3, TParameter4>(methodInfo);
        }

        IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5> ITypeInfo<T>.GetVoidMethod<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5>(Expression<Func<T, Action<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5>>> methodExpression)
        {
            MethodInfo methodInfo = GetMethodInfo(methodExpression);
            return MethodInfoCache<T>.GetVoidMethodInfo<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5>(methodInfo);
        }

        IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6> ITypeInfo<T>.GetVoidMethod<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6>(Expression<Func<T, Action<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6>>> methodExpression)
        {
            MethodInfo methodInfo = GetMethodInfo(methodExpression);
            return MethodInfoCache<T>.GetVoidMethodInfo<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6>(methodInfo);
        }

        IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7> ITypeInfo<T>.GetVoidMethod<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7>(Expression<Func<T, Action<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7>>> methodExpression)
        {
            MethodInfo methodInfo = GetMethodInfo(methodExpression);
            return MethodInfoCache<T>.GetVoidMethodInfo<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7>(methodInfo);
        }

        IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8> ITypeInfo<T>.GetVoidMethod<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8>(Expression<Func<T, Action<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8>>> methodExpression)
        {
            MethodInfo methodInfo = GetMethodInfo(methodExpression);
            return MethodInfoCache<T>.GetVoidMethodInfo<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8>(methodInfo);
        }

        private static MethodInfo GetMethodInfo<TMethod>(Expression<Func<T, TMethod>> methodExpression)
        {
            Contract.Requires(methodExpression != null);
            Contract.Ensures(Contract.Result<MethodInfo>() != null);

            return StaticReflection<T>.GetMethodInfoInternal(methodExpression);
        }

        private static PropertyInfo GetPropertyInfo<TProperty>(Expression<Func<T, TProperty>> propertyExpression)
        {
            Contract.Requires(propertyExpression != null);
            Contract.Ensures(Contract.Result<PropertyInfo>() != null);

            MemberInfo memberInfo = StaticReflection<T>.GetMemberInfoInternal(propertyExpression);
            PropertyInfo propertyInfo = memberInfo as PropertyInfo;
            if (propertyInfo == null)
            {
                throw new InvalidOperationException("Expression is not a property.");
            }

            return propertyInfo;
        }

        #endregion
    }
}