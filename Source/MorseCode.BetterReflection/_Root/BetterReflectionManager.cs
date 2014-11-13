#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BetterReflectionManager.cs" company="MorseCode Software">
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
    using System.Diagnostics.Contracts;

    public static class BetterReflectionManager
    {
        #region Static Fields

        private static readonly Lazy<BetterReflectionManagerInternal> BetterReflectionManagerInternalInstance =
            new Lazy<BetterReflectionManagerInternal>(() => new BetterReflectionManagerInternal());

        #endregion

        #region Public Properties

        public static Func<ITypeInfoProvider> CreateTypeInfoFactoryDelegate
        {
            set
            {
                Contract.Assume(BetterReflectionManagerInternalInstance.Value != null);

                BetterReflectionManagerInternalInstance.Value.CreateTypeInfoFactoryDelegate = value;
            }
        }

        public static ITypeInfoProvider TypeInfoFactory
        {
            get
            {
                Contract.Assume(BetterReflectionManagerInternalInstance.Value != null);

                return BetterReflectionManagerInternalInstance.Value.TypeInfoFactory;
            }
        }

        #endregion

        private class BetterReflectionManagerInternal
        {
            #region Fields

            private readonly Func<ITypeInfoProvider> defaultCreateTypeInfoFactoryDelegate;

            private Func<ITypeInfoProvider> createTypeInfoFactoryDelegate;

            private Lazy<ITypeInfoProvider> typeInfoFactory;

            #endregion

            #region Constructors and Destructors

            public BetterReflectionManagerInternal()
            {
                Contract.Ensures(this.defaultCreateTypeInfoFactoryDelegate != null);
                Contract.Ensures(this.createTypeInfoFactoryDelegate != null);
                Contract.Ensures(this.typeInfoFactory != null);

                this.defaultCreateTypeInfoFactoryDelegate = () =>
                    {
                        StaticReflectionHelper staticReflectionHelper = new StaticReflectionHelper();
                        StaticReflectionHelperProvider staticReflectionHelperProvider =
                            new StaticReflectionHelperProvider(
                                new SingletonsByType(), staticReflectionHelper);
                        return new TypeInfoProvider(
                            new SingletonsByType(),
                            new PropertyInfoCache(
                                new PropertyInfoFactory(
                                    new PropertyValueGetterCache(
                                        new StaticReflectionHelper<PropertyValueGetterCache>(staticReflectionHelper)),
                                    new PropertyValueSetterCache()),
                                staticReflectionHelperProvider),
                            staticReflectionHelperProvider);
                    };
                this.createTypeInfoFactoryDelegate = this.defaultCreateTypeInfoFactoryDelegate;
                this.typeInfoFactory = new Lazy<ITypeInfoProvider>(this.createTypeInfoFactoryDelegate);
            }

            #endregion

            // ReSharper disable MemberHidesStaticFromOuterClass
            #region Properties

            internal Func<ITypeInfoProvider> CreateTypeInfoFactoryDelegate
                // ReSharper restore MemberHidesStaticFromOuterClass
            {
                set
                {
                    Contract.Ensures(this.createTypeInfoFactoryDelegate != null);
                    Contract.Ensures(this.typeInfoFactory != null);

                    if (this.createTypeInfoFactoryDelegate == value)
                    {
                        return;
                    }

                    this.createTypeInfoFactoryDelegate = value ?? this.defaultCreateTypeInfoFactoryDelegate;
                    this.typeInfoFactory = new Lazy<ITypeInfoProvider>(this.createTypeInfoFactoryDelegate);
                }
            }

            // ReSharper disable MemberHidesStaticFromOuterClass
            internal ITypeInfoProvider TypeInfoFactory // ReSharper restore MemberHidesStaticFromOuterClass
            {
                get
                {
                    return this.typeInfoFactory.Value;
                }
            }

            #endregion

            #region Methods

            [ContractInvariantMethod]
            private void CodeContractsInvariants()
            {
                Contract.Invariant(this.defaultCreateTypeInfoFactoryDelegate != null);
                Contract.Invariant(this.createTypeInfoFactoryDelegate != null);
                Contract.Invariant(this.typeInfoFactory != null);
            }

            #endregion
        }
    }
}