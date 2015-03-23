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
    using System.Diagnostics.CodeAnalysis;
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
            MethodInfo createPropertyInfoMethodInfo = StaticReflection.GetInScopeMethodInfoInternal(() => CreatePropertyInfo<object>(null));
            CreatePropertyInfoGenericMethodDefinition = createPropertyInfoMethodInfo.GetGenericMethodDefinition();
        }

        #endregion

        #region Public Methods and Operators

        [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1501:StatementMustNotBeOnSingleLine", Justification = "Reviewed. Suppression is OK here.")]
        public static IPropertyInfo<T> GetPropertyInfo(PropertyInfo propertyInfo)
        {
            return PropertyInfoByPropertyInfo.GetOrAdd(propertyInfo, p => (IPropertyInfo<T>)CreatePropertyInfoGenericMethodDefinition.MakeGenericMethod(p.PropertyType).Invoke(null, new object[] { p }));
        }

        public static IPropertyInfo<T, TProperty> GetPropertyInfo<TProperty>(PropertyInfo propertyInfo)
        {
            IPropertyInfo<T> info = GetPropertyInfo(propertyInfo);
            IPropertyInfo<T, TProperty> typedInfo = info as IPropertyInfo<T, TProperty>;
            if (typedInfo == null)
            {
                throw new InvalidOperationException("For property with name " + info.PropertyInfo.Name + ", property type " + info.PropertyType.FullName + " was found, but property type " + typeof(TProperty).FullName + " was expected.");
            }

            return typedInfo;
        }

        #endregion

        #region Methods

        private static IPropertyInfo<T, TProperty> CreatePropertyInfo<TProperty>(PropertyInfo propertyInfo)
        {
            return new PropertyInfo<T, TProperty>(propertyInfo);
        }

        #endregion
    }
}