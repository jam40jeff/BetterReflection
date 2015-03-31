#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeInfoFactory.cs" company="MorseCode Software">
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

    /// <summary>
    /// Provides factory methods for creating type info instances.
    /// </summary>
    public static class TypeInfoFactory
    {
        #region Static Fields

        private static readonly MethodInfo GetTypeInfoGenericMethodDefinition;

        private static readonly ConcurrentDictionary<Type, ITypeInfo> TypeInfoByType = new ConcurrentDictionary<Type, ITypeInfo>();

        #endregion

        #region Constructors and Destructors

        static TypeInfoFactory()
        {
            GetTypeInfoGenericMethodDefinition = StaticReflection.GetInScopeMethodInfoFromMethodCallInternal(() => GetTypeInfo<object>());
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Gets the type info for type <paramref name="type"/>.
        /// </summary>
        /// <param name="type">
        /// The type for which to get the type info.
        /// </param>
        /// <returns>
        /// The type info for type <paramref name="type"/>.
        /// </returns>
        public static ITypeInfo GetTypeInfo(Type type)
        {
            Contract.Requires<ArgumentNullException>(type != null, "type");
            Contract.Ensures(Contract.Result<ITypeInfo>() != null);

            ITypeInfo typeInfo = TypeInfoByType.GetOrAdd(type, t => (ITypeInfo)GetTypeInfoGenericMethodDefinition.MakeGenericMethod(t).Invoke(null, new object[0]));
            Contract.Assume(typeInfo != null);
            return typeInfo;
        }

        /// <summary>
        /// Gets the type info for type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The type for which to get the type info.
        /// </typeparam>
        /// <returns>
        /// The type info for type <typeparamref name="T"/>.
        /// </returns>
        public static ITypeInfo<T> GetTypeInfo<T>()
        {
            Contract.Ensures(Contract.Result<ITypeInfo<T>>() != null);

            return Helper<T>.TypeInfo;
        }

        #endregion

        private static class Helper<T>
        {
            #region Static Fields

            public static readonly ITypeInfo<T> TypeInfo = new TypeInfo<T>();

            #endregion
        }
    }
}