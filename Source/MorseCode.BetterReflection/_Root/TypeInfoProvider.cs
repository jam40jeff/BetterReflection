#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeInfoProvider.cs" company="MorseCode Software">
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
    internal class TypeInfoProvider : ITypeInfoProvider
    {
        #region Fields

        private readonly ISingletonsByType singletonsByType;

        private readonly IPropertyInfoCache propertyInfoCache;

        private readonly IStaticReflectionHelperProvider staticReflectionHelperFactory;

        #endregion

        #region Constructors and Destructors

        public TypeInfoProvider(
            ISingletonsByType concurrentDictionaryKeyedByType,
            IPropertyInfoCache propertyInfoCache,
            IStaticReflectionHelperProvider staticReflectionHelperFactory)
        {
            this.singletonsByType = concurrentDictionaryKeyedByType;
            this.propertyInfoCache = propertyInfoCache;
            this.staticReflectionHelperFactory = staticReflectionHelperFactory;
        }

        #endregion

        #region Explicit Interface Methods

        ITypeInfo<T> ITypeInfoProvider.GetTypeInfo<T>()
        {
            return
                this.singletonsByType.GetOrAdd(
                    () =>
                    new TypeInfo<T>(
                        this.propertyInfoCache, this.staticReflectionHelperFactory.GetStaticReflectionHelper<T>()));
        }

        #endregion
    }
}