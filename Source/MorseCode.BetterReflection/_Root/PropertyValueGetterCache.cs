#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PropertyValueGetterCache.cs" company="MorseCode Software">
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
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;

    public class PropertyValueGetterCache : IPropertyValueGetterCache
    {
        #region Fields

        private readonly Dictionary<Type, Dictionary<string, Delegate>> delegatesByPropertyNameByType =
            new Dictionary<Type, Dictionary<string, Delegate>>();

        private readonly Lazy<MethodInfo> getDelegateGenericMethodDefinition;

        private readonly IStaticReflectionHelper<PropertyValueGetterCache> staticReflectionHelper;

        #endregion

        #region Constructors and Destructors

        public PropertyValueGetterCache(IStaticReflectionHelper<PropertyValueGetterCache> staticReflectionHelper)
        {
            this.staticReflectionHelper = staticReflectionHelper;
            this.getDelegateGenericMethodDefinition = new Lazy<MethodInfo>(
                () =>
                    {
                        MethodInfo getDelegateMethodInfo =
                            this.staticReflectionHelper.GetMethodInfo(o => o.GetDelegate<object, object>(null));
                        return getDelegateMethodInfo.GetGenericMethodDefinition();
                    });
        }

        #endregion

        #region Explicit Interface Methods

        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1118:ParameterMustNotSpanMultipleLines",
            Justification = "Reviewed. Suppression is OK here.")]
        TProperty IPropertyValueGetterCache.GetValue<T, TProperty>(PropertyInfo propertyInfo, T o)
        {
            string propertyName = propertyInfo.Name;

            Dictionary<string, Delegate> delegatesByPropertyName;
            if (!this.delegatesByPropertyNameByType.TryGetValue(typeof(T), out delegatesByPropertyName))
            {
                delegatesByPropertyName = new Dictionary<string, Delegate>();
                this.delegatesByPropertyNameByType.Add(typeof(T), delegatesByPropertyName);
            }
            //Dictionary<string, Delegate> delegatesByPropertyName =
            //    this.delegatesByPropertyNameByType.GetOrAdd(
            //        typeof(T), t => new Dictionary<string, Delegate>());
            Delegate d;
            if (!delegatesByPropertyName.TryGetValue(propertyName, out d))
            {
                Type propertyType = propertyInfo.PropertyType;
                Type declaringType = propertyInfo.DeclaringType;

                if (declaringType == null)
                {
                    throw new InvalidOperationException("Property must have a declaring type.");
                }

                MethodInfo m = propertyInfo.GetGetMethod();
                MethodInfo getDelegateMethod =
                    this.getDelegateGenericMethodDefinition.Value.MakeGenericMethod(declaringType, propertyType);
                d = (Delegate)getDelegateMethod.Invoke(this, new object[] { m });

                delegatesByPropertyName.Add(propertyName, d);
            }
            //Delegate d = delegatesByPropertyName.GetOrAdd(
            //    propertyName,
            //    n =>
            //    {
            //        Type propertyType = propertyInfo.PropertyType;
            //        Type declaringType = propertyInfo.DeclaringType;

            //        if (declaringType == null)
            //        {
            //            throw new InvalidOperationException("Property must have a declaring type.");
            //        }

            //        MethodInfo m = propertyInfo.GetGetMethod();
            //        MethodInfo getDelegateMethod =
            //            this.getDelegateGenericMethodDefinition.Value.MakeGenericMethod(declaringType, propertyType);
            //        return (Delegate)getDelegateMethod.Invoke(this, new object[] { m });
            //    });
            Func<T, TProperty> typedDelegate = d as Func<T, TProperty>;
            if (typedDelegate == null)
            {
                Type propertyType = propertyInfo.PropertyType;
                Type declaringType = propertyInfo.DeclaringType;

                throw new InvalidOperationException(
                    "Property type " + propertyType.FullName
                    + (declaringType == null ? string.Empty : (" on object type " + declaringType.FullName))
                    + " was found, but property type " + typeof(TProperty).FullName + " on object type "
                    + typeof(T).FullName + " was expected.");
            }

            return typedDelegate(o);
        }

        #endregion

        #region Methods

        private Delegate GetDelegate<T, TProperty>(MethodInfo m)
        {
            return Delegate.CreateDelegate(typeof(Func<T, TProperty>), m);
        }

        #endregion
    }
}