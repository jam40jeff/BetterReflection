#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PropertyInfo.cs" company="MorseCode Software">
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
    using System.Reflection;

    internal class PropertyInfo<T, TProperty> : IPropertyInfo<T, TProperty>
    {
        #region Fields

        private readonly PropertyInfo propertyInfo;

        private readonly IPropertyValueGetterCache propertyValueGetterCache;

        private readonly IPropertyValueSetterCache propertyValueSetterCache;

        #endregion

        #region Constructors and Destructors

        public PropertyInfo(
            PropertyInfo propertyInfo,
            IPropertyValueGetterCache propertyValueGetterCache,
            IPropertyValueSetterCache propertyValueSetterCache)
        {
            this.propertyInfo = propertyInfo;
            this.propertyValueGetterCache = propertyValueGetterCache;
            this.propertyValueSetterCache = propertyValueSetterCache;
        }

        #endregion

        #region Public Properties

        public Type PropertyType
        {
            get
            {
                return typeof(TProperty);
            }
        }

        #endregion

        #region Explicit Interface Properties

        PropertyInfo IPropertyInfo<T>.PropertyInfo
        {
            get
            {
                return this.propertyInfo;
            }
        }

        PropertyInfo IPropertyInfoWithGetValue<T>.PropertyInfo
        {
            get
            {
                return this.propertyInfo;
            }
        }

        PropertyInfo IPropertyInfoWithSetValue<T>.PropertyInfo
        {
            get
            {
                return this.propertyInfo;
            }
        }

        #endregion

        #region Explicit Interface Methods

        object IPropertyInfoWithGetValue<T>.GetValue(T o)
        {
            return this.propertyValueGetterCache.GetValue<T, TProperty>(this.propertyInfo, o);
        }

        TProperty IPropertyInfoWithGetValue<T, TProperty>.GetValue(T o)
        {
            return this.propertyValueGetterCache.GetValue<T, TProperty>(this.propertyInfo, o);
        }

        void IPropertyInfoWithSetValue<T>.SetValue(T o, object value)
        {
            this.propertyValueSetterCache.SetValue(this.propertyInfo, o, (TProperty)value);
        }

        void IPropertyInfoWithSetValue<T, TProperty>.SetValue(T o, TProperty value)
        {
            this.propertyValueSetterCache.SetValue(this.propertyInfo, o, value);
        }

        #endregion
    }
}