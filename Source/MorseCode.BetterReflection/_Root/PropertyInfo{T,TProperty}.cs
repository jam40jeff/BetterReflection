#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PropertyInfo{T,TProperty}.cs" company="MorseCode Software">
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
    using System.Reflection;

    using MorseCode.FrameworkExtensions;

    internal class PropertyInfo<T, TProperty> : IPropertyInfo<T, TProperty>
    {
        #region Fields

        private readonly Lazy<Func<T, TProperty>> getter;

        private readonly PropertyInfo propertyInfo;

        private readonly IPropertyInfo<T, TProperty> propertyInfoFullyTyped;

        private readonly IPropertyInfo<T> propertyInfoPartiallyTyped;

        private readonly Lazy<Action<T, TProperty>> setter;

        #endregion

        #region Constructors and Destructors

        public PropertyInfo(PropertyInfo propertyInfo)
        {
            this.propertyInfo = propertyInfo;

            this.propertyInfoPartiallyTyped = this;
            this.propertyInfoFullyTyped = this;

            this.getter = new Lazy<Func<T, TProperty>>(() => DelegateUtility.CreateDelegate<Func<T, TProperty>>(this.propertyInfo.GetGetMethod()));
            this.setter = new Lazy<Action<T, TProperty>>(() => DelegateUtility.CreateDelegate<Action<T, TProperty>>(this.propertyInfo.GetSetMethod()));
        }

        #endregion

        #region Explicit Interface Properties

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

        PropertyInfo IPropertyInfoWithGetValue.PropertyInfo
        {
            get
            {
                return this.propertyInfo;
            }
        }

        PropertyInfo IPropertyInfoWithSetValue.PropertyInfo
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

        #region Explicit Interface Methods

        object IPropertyInfoWithGetValue<T>.GetValue(T o)
        {
            return this.propertyInfoFullyTyped.GetValue(o);
        }

        TProperty IPropertyInfoWithGetValue<T, TProperty>.GetValue(T o)
        {
            return this.getter.Value(o);
        }

        object IPropertyInfoWithGetValue.GetValueUntyped(object o)
        {
            if (!(o is T))
            {
                throw new ArgumentException("Object was of type " + o.GetType().FullName + ", but must be convertible to type " + typeof(T).FullName + ".", StaticReflection.GetInScopeMemberInfoInternal(() => o).Name);
            }

            return this.propertyInfoFullyTyped.GetValue((T)o);
        }

        void IPropertyInfoWithSetValue<T, TProperty>.SetValue(T o, TProperty value)
        {
            this.setter.Value(o, value);
        }

        void IPropertyInfoWithSetValue.SetValueFullyUntyped(object o, object value)
        {
            if (!(o is T))
            {
                throw new ArgumentException("Object was of type " + o.GetType().FullName + ", but must be convertible to type " + typeof(T).FullName + ".", StaticReflection.GetInScopeMemberInfoInternal(() => o).Name);
            }

            this.propertyInfoPartiallyTyped.SetValuePartiallyUntyped((T)o, value);
        }

        void IPropertyInfoWithSetValue<T>.SetValuePartiallyUntyped(T o, object value)
        {
            if (!(value is TProperty))
            {
                throw new ArgumentException("Value was of type " + value.GetType().FullName + ", but must be convertible to type " + typeof(TProperty).FullName + ".", StaticReflection.GetInScopeMemberInfoInternal(() => value).Name);
            }

            this.propertyInfoFullyTyped.SetValue(o, (TProperty)value);
        }

        #endregion
    }
}