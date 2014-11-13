#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeInfo.cs" company="MorseCode Software">
// Copyright (c) 2014 MorseCode Software
// </copyright>
// <summary>
// The MIT License (MIT)
// 
// Copyright (c) 2014 MorseCode Software
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

        private readonly IPropertyInfoCache propertyInfoCache;

        private readonly IStaticReflectionHelper<T> staticReflectionHelper;

        private readonly Type type;

        #endregion

        #region Constructors and Destructors

        public TypeInfo(IPropertyInfoCache propertyInfoCache, IStaticReflectionHelper<T> staticReflectionHelper)
        {
            this.propertyInfoCache = propertyInfoCache;
            this.staticReflectionHelper = staticReflectionHelper;
            this.type = typeof(T);
        }

        #endregion

        #region Explicit Interface Properties

        Type ITypeInfo<T>.Type
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
            return
                typeof(T).GetProperties().Select(p => this.propertyInfoCache.GetPropertyInfo<T>(p));
        }

        IEnumerable<IPropertyInfo<T>> ITypeInfo<T>.GetProperties(BindingFlags bindingFlags)
        {
            return
                typeof(T).GetProperties(bindingFlags)
                         .Select(p => this.propertyInfoCache.GetPropertyInfo<T>(p));
        }

        IPropertyInfo<T> ITypeInfo<T>.GetProperty(string name)
        {
            PropertyInfo propertyInfo = typeof(T).GetProperty(name);
            return propertyInfo == null
                       ? null
                       : this.propertyInfoCache.GetPropertyInfo<T>(propertyInfo);
        }

        IPropertyInfo<T> ITypeInfo<T>.GetProperty(string name, BindingFlags bindingFlags)
        {
            PropertyInfo propertyInfo = typeof(T).GetProperty(name, bindingFlags);
            return propertyInfo == null
                       ? null
                       : this.propertyInfoCache.GetPropertyInfo<T>(propertyInfo);
        }

        IPropertyInfo<T, TProperty> ITypeInfo<T>.GetProperty<TProperty>(
            Expression<Func<T, TProperty>> propertyExpression)
        {
            MemberInfo memberInfo = this.staticReflectionHelper.GetMemberInfo(propertyExpression);
            PropertyInfo propertyInfo = memberInfo as PropertyInfo;
            if (propertyInfo == null)
            {
                throw new InvalidOperationException("Expression is not a property.");
            }

            return this.propertyInfoCache.GetPropertyInfo<T, TProperty>(propertyInfo);
        }

        #endregion
    }
}