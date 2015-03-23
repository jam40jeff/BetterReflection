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
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    internal class TypeInfo<T> : ITypeInfo<T>
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

        #endregion

        #region Explicit Interface Properties

        Type ITypeInfo.Type
        {
            get
            {
                return this.type;
            }
        }

        #endregion

        #region Explicit Interface Methods

        IEnumerable<IPropertyInfo<T>> ITypeInfo<T>.GetProperties()
        {
            return typeof(T).GetProperties().Select(PropertyInfoCache<T>.GetPropertyInfo);
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
            PropertyInfo propertyInfo = typeof(T).GetProperty(name);
            return propertyInfo == null ? null : PropertyInfoCache<T>.GetPropertyInfo(propertyInfo);
        }

        IPropertyInfo<T, TProperty> ITypeInfo<T>.GetProperty<TProperty>(Expression<Func<T, TProperty>> propertyExpression)
        {
            MemberInfo memberInfo = StaticReflection<T>.GetMemberInfoInternal(propertyExpression);
            PropertyInfo propertyInfo = memberInfo as PropertyInfo;
            if (propertyInfo == null)
            {
                throw new InvalidOperationException("Expression is not a property.");
            }

            return PropertyInfoCache<T>.GetPropertyInfo<TProperty>(propertyInfo);
        }

        IEnumerable<IMethodInfo> ITypeInfo.GetMethods()
        {
            return this.typeInfo.GetMethods();
        }

        IEnumerable<IMethodInfo<T>> ITypeInfo<T>.GetMethods()
        {
            return typeof(T).GetMethods().Select(MethodInfoCache<T>.GetMethodInfo);
        }

        IMethodInfo ITypeInfo.GetMethod(string name)
        {
            return this.typeInfo.GetMethod(name);
        }

        IMethodInfo<T> ITypeInfo<T>.GetMethod(string name)
        {
            MethodInfo methodInfo = typeof(T).GetMethod(name);
            return methodInfo == null ? null : MethodInfoCache<T>.GetMethodInfo(methodInfo);
        }

        private MethodInfo GetMethodInfo<TMethod>(Expression<Func<T, TMethod>> methodExpression)
        {
            return StaticReflection<T>.GetMethodInfoInternal(methodExpression);
        }

        IVoidMethodInfo<T> ITypeInfo<T>.GetVoidMethod(Expression<Func<T, Action>> methodExpression)
        {
            MethodInfo methodInfo = this.GetMethodInfo(methodExpression);
            return MethodInfoCache<T>.GetVoidMethodInfo(methodInfo);
        }

        IVoidMethodInfo<T, TParameter1> ITypeInfo<T>.GetVoidMethod<TParameter1>(Expression<Func<T, Action<TParameter1>>> methodExpression)
        {
            MethodInfo methodInfo = this.GetMethodInfo(methodExpression);
            return MethodInfoCache<T>.GetVoidMethodInfo<TParameter1>(methodInfo);
        }

        IVoidMethodInfo<T, TParameter1, TParameter2> ITypeInfo<T>.GetVoidMethod<TParameter1, TParameter2>(Expression<Func<T, Action<TParameter1, TParameter2>>> methodExpression)
        {
            MethodInfo methodInfo = this.GetMethodInfo(methodExpression);
            return MethodInfoCache<T>.GetVoidMethodInfo<TParameter1, TParameter2>(methodInfo);
        }

        IMethodInfo<T, TReturn> ITypeInfo<T>.GetMethod<TReturn>(Expression<Func<T, Func<TReturn>>> methodExpression)
        {
            MethodInfo methodInfo = this.GetMethodInfo(methodExpression);
            return MethodInfoCache<T>.GetMethodInfo<TReturn>(methodInfo);
        }

        IMethodInfo<T, TParameter1, TReturn> ITypeInfo<T>.GetMethod<TParameter1, TReturn>(Expression<Func<T, Func<TParameter1, TReturn>>> methodExpression)
        {
            MethodInfo methodInfo = this.GetMethodInfo(methodExpression);
            return MethodInfoCache<T>.GetMethodInfo<TParameter1, TReturn>(methodInfo);
        }

        IMethodInfo<T, TParameter1, TParameter2, TReturn> ITypeInfo<T>.GetMethod<TParameter1, TParameter2, TReturn>(Expression<Func<T, Func<TParameter1, TParameter2, TReturn>>> methodExpression)
        {
            MethodInfo methodInfo = this.GetMethodInfo(methodExpression);
            return MethodInfoCache<T>.GetMethodInfo<TParameter1, TParameter2, TReturn>(methodInfo);
        }

        #endregion
    }
}