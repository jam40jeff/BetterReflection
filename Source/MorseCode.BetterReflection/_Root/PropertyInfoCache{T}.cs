#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PropertyInfoCache{T}.cs" company="MorseCode Software">
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
    using System.Collections.Concurrent;
    using System.Diagnostics.Contracts;
    using System.Reflection;

    internal static class PropertyInfoCache<T>
    {
        // ReSharper disable StaticFieldInGenericType - refers to a method within this type and should have different values for different generic type parameters
        #region Static Fields

        private static readonly MethodInfo CreatePropertyInfoGenericMethodDefinition;

        // ReSharper restore StaticFieldInGenericType
        private static readonly ConcurrentDictionary<PropertyInfo, IPropertyInfo<T>> PropertyInfoByPropertyInfo = new ConcurrentDictionary<PropertyInfo, IPropertyInfo<T>>();

        #endregion

        #region Constructors and Destructors

        static PropertyInfoCache()
        {
            MethodInfo createPropertyInfoMethodInfo = StaticReflection.GetInScopeMethodInfoFromMethodCallInternal(() => CreatePropertyInfo<object>(null));
            CreatePropertyInfoGenericMethodDefinition = createPropertyInfoMethodInfo.GetGenericMethodDefinition();
        }

        #endregion

        #region Methods

        internal static IPropertyInfo<T> GetPropertyInfo(PropertyInfo propertyInfo)
        {
            Contract.Requires(propertyInfo != null);
            Contract.Ensures(Contract.Result<IPropertyInfo<T>>() != null);

            IPropertyInfo<T> result = PropertyInfoByPropertyInfo.GetOrAdd(propertyInfo, p => (IPropertyInfo<T>)CreatePropertyInfoGenericMethodDefinition.MakeGenericMethod(p.PropertyType).Invoke(null, new object[] { p }));
            Contract.Assume(result != null);
            return result;
        }

        internal static IReadWritePropertyInfo<T, TProperty> GetReadWritePropertyInfo<TProperty>(PropertyInfo propertyInfo)
        {
            Contract.Requires(propertyInfo != null);
            Contract.Ensures(Contract.Result<IReadWritePropertyInfo<T, TProperty>>() != null);

            IPropertyInfo<T> info = GetPropertyInfo(propertyInfo);

            if (typeof(TProperty) != info.PropertyType)
            {
                throw new InvalidOperationException("For property with name " + info.PropertyInfo.Name + " on type " + typeof(T).FullName + ", property type " + info.PropertyType.FullName + " was found, but a type assignable to " + typeof(TProperty).FullName + " was expected.");
            }

            if (!info.IsReadable || !info.IsWritable)
            {
                throw new InvalidOperationException("Property with name " + info.PropertyInfo.Name + " on type " + typeof(T).FullName + " is not readable and writable.");
            }

            IReadWritePropertyInfo<T, TProperty> typedInfo = info as IReadWritePropertyInfo<T, TProperty>;
            if (typedInfo == null)
            {
                throw new InvalidOperationException("An unknown error occurred obtaining readable and writable property info for property with name " + info.PropertyInfo.Name + " on type " + typeof(T).FullName + ".");
            }

            return typedInfo;
        }

        internal static IReadablePropertyInfo<T, TProperty> GetReadablePropertyInfo<TProperty>(PropertyInfo propertyInfo)
        {
            Contract.Requires(propertyInfo != null);
            Contract.Ensures(Contract.Result<IReadablePropertyInfo<T, TProperty>>() != null);

            IPropertyInfo<T> info = GetPropertyInfo(propertyInfo);

            if (!typeof(TProperty).IsAssignableFrom(info.PropertyType))
            {
                throw new InvalidOperationException("For property with name " + info.PropertyInfo.Name + " on type " + typeof(T).FullName + ", property type " + info.PropertyType.FullName + " was found, but a type assignable to " + typeof(TProperty).FullName + " was expected.");
            }

            if (!info.IsReadable)
            {
                throw new InvalidOperationException("Property with name " + info.PropertyInfo.Name + " on type " + typeof(T).FullName + " is not readable.");
            }

            IReadablePropertyInfo<T, TProperty> typedInfo = info as IReadablePropertyInfo<T, TProperty>;
            if (typedInfo == null)
            {
                throw new InvalidOperationException("An unknown error occurred obtaining readable property info for property with name " + info.PropertyInfo.Name + " on type " + typeof(T).FullName + ".");
            }

            return typedInfo;
        }

        internal static IWritablePropertyInfo<T, TProperty> GetWritablePropertyInfo<TProperty>(PropertyInfo propertyInfo)
        {
            Contract.Requires(propertyInfo != null);
            Contract.Ensures(Contract.Result<IWritablePropertyInfo<T, TProperty>>() != null);

            IPropertyInfo<T> info = GetPropertyInfo(propertyInfo);

            if (!info.PropertyType.IsAssignableFrom(typeof(TProperty)))
            {
                throw new InvalidOperationException("For property with name " + info.PropertyInfo.Name + " on type " + typeof(T).FullName + ", property type " + info.PropertyType.FullName + " was found, but a type assignable to " + typeof(TProperty).FullName + " was expected.");
            }

            if (!info.IsWritable)
            {
                throw new InvalidOperationException("Property with name " + info.PropertyInfo.Name + " on type " + typeof(T).FullName + " is not writable.");
            }

            IWritablePropertyInfo<T, TProperty> typedInfo = info as IWritablePropertyInfo<T, TProperty>;
            if (typedInfo == null)
            {
                throw new InvalidOperationException("An unknown error occurred obtaining writable property info for property with name " + info.PropertyInfo.Name + " on type " + typeof(T).FullName + ".");
            }

            return typedInfo;
        }

        private static IPropertyInfo<T> CreatePropertyInfo<TProperty>(PropertyInfo propertyInfo)
        {
            Contract.Requires(propertyInfo != null);
            Contract.Ensures(Contract.Result<IPropertyInfo<T>>() != null);

            if (propertyInfo.CanRead && propertyInfo.CanWrite)
            {
                return new ReadWritePropertyInfo<T, TProperty>(propertyInfo);
            }

            if (propertyInfo.CanRead)
            {
                return new ReadOnlyPropertyInfo<T, TProperty>(propertyInfo);
            }

            if (propertyInfo.CanWrite)
            {
                return new WriteOnlyPropertyInfo<T, TProperty>(propertyInfo);
            }

            throw new InvalidOperationException("Cannot create property info for a property which is neither readable nor writable.");
        }

        #endregion
    }
}