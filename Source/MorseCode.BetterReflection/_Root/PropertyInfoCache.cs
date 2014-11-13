#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PropertyInfoCache.cs" company="MorseCode Software">
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
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;

    internal class PropertyInfoCache : IPropertyInfoCache
    {
        #region Fields

        private readonly MethodInfo createPropertyInfoGenericMethodDefinition;

        private readonly IPropertyInfoCache propertyInfoCache;

        private readonly IPropertyInfoFactory propertyInfoFactory;

        private readonly IStaticReflectionHelper<PropertyInfoCache> staticReflectionHelper;

        #endregion

        #region Constructors and Destructors

        public PropertyInfoCache(
            IPropertyInfoFactory propertyInfoFactory, IStaticReflectionHelperProvider staticReflectionHelperFactory)
        {
            this.propertyInfoFactory = propertyInfoFactory;
            this.staticReflectionHelper = staticReflectionHelperFactory.GetStaticReflectionHelper<PropertyInfoCache>();
            this.propertyInfoCache = this;

            MethodInfo createPropertyInfoMethodInfo =
                this.staticReflectionHelper.GetMethodInfo(o => o.CreatePropertyInfo<object, object>(null));
            this.createPropertyInfoGenericMethodDefinition = createPropertyInfoMethodInfo.GetGenericMethodDefinition();
        }

        #endregion

        #region Explicit Interface Methods

        [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1501:StatementMustNotBeOnSingleLine",
            Justification = "Reviewed. Suppression is OK here.")]
        IPropertyInfo<T> IPropertyInfoCache.GetPropertyInfo<T>(PropertyInfo propertyInfo)
        {
            return PropertyInfoDictionaryHelper<T>.PropertyInfosByPropertyName.GetOrAdd(
                propertyInfo.Name,
                n =>
                (IPropertyInfo<T>)
                this.createPropertyInfoGenericMethodDefinition.MakeGenericMethod(typeof(T), propertyInfo.PropertyType)
                    .Invoke(this, new object[] { propertyInfo }));
        }

        IPropertyInfo<T, TProperty> IPropertyInfoCache.GetPropertyInfo<T, TProperty>(PropertyInfo propertyInfo)
        {
            IPropertyInfo<T> info = this.propertyInfoCache.GetPropertyInfo<T>(propertyInfo);
            IPropertyInfo<T, TProperty> typedInfo = info as IPropertyInfo<T, TProperty>;
            if (typedInfo == null)
            {
                throw new InvalidOperationException(
                    "Property type " + info.PropertyType.FullName + " was found, but property type "
                    + typeof(TProperty).FullName + " was expected.");
            }

            return typedInfo;
        }

        #endregion

        #region Methods

        private IPropertyInfo<T, TProperty> CreatePropertyInfo<T, TProperty>(PropertyInfo propertyInfo)
        {
            return this.propertyInfoFactory.CreatePropertyInfo<T, TProperty>(propertyInfo);
        }

        #endregion

        private static class PropertyInfoDictionaryHelper<T>
        {
            #region Static Fields

            public static readonly IAddOnlyConcurrentDictionary<string, IPropertyInfo<T>> PropertyInfosByPropertyName =
                new AddOnlyConcurrentDictionary<string, IPropertyInfo<T>>();

            #endregion
        }
    }
}