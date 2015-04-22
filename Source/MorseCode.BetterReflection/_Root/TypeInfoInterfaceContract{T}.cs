#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeInfoInterfaceContract{T}.cs" company="MorseCode Software">
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
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq.Expressions;

    [ContractClassFor(typeof(ITypeInfo<>))]
    internal abstract class TypeInfoInterfaceContract<T> : ITypeInfo<T>
    {
        #region Explicit Interface Properties

        string ITypeInfo.FullName
        {
            get
            {
                return null;
            }
        }

        string ITypeInfo.Name
        {
            get
            {
                return null;
            }
        }

        Type ITypeInfo.Type
        {
            get
            {
                return null;
            }
        }

        #endregion

        #region Explicit Interface Methods

        IMethodInfo<T> ITypeInfo<T>.GetMethod(string name)
        {
            Contract.Requires<ArgumentNullException>(name != null, "name");
            Contract.Ensures(Contract.Result<IMethodInfo<T>>() != null);

            return null;
        }

        IMethodInfo<T> ITypeInfo<T>.GetMethod(string name, Type[] types)
        {
            Contract.Requires<ArgumentNullException>(name != null, "name");
            Contract.Requires<ArgumentNullException>(types != null, "types");
            Contract.Ensures(Contract.Result<IMethodInfo<T>>() != null);

            return null;
        }

        IMethodInfo<T, TReturn> ITypeInfo<T>.GetMethod<TReturn>(Expression<Func<T, Func<TReturn>>> methodExpression)
        {
            Contract.Requires<ArgumentNullException>(methodExpression != null, "methodExpression");
            Contract.Ensures(Contract.Result<IMethodInfo<T, TReturn>>() != null);

            return null;
        }

        IMethodInfo<T, TParameter1, TReturn> ITypeInfo<T>.GetMethod<TParameter1, TReturn>(Expression<Func<T, Func<TParameter1, TReturn>>> methodExpression)
        {
            Contract.Requires<ArgumentNullException>(methodExpression != null, "methodExpression");
            Contract.Ensures(Contract.Result<IMethodInfo<T, TParameter1, TReturn>>() != null);

            return null;
        }

        IMethodInfo<T, TParameter1, TParameter2, TReturn> ITypeInfo<T>.GetMethod<TParameter1, TParameter2, TReturn>(Expression<Func<T, Func<TParameter1, TParameter2, TReturn>>> methodExpression)
        {
            Contract.Requires<ArgumentNullException>(methodExpression != null, "methodExpression");
            Contract.Ensures(Contract.Result<IMethodInfo<T, TParameter1, TParameter2, TReturn>>() != null);

            return null;
        }

        IMethodInfo<T, TParameter1, TParameter2, TParameter3, TReturn> ITypeInfo<T>.GetMethod<TParameter1, TParameter2, TParameter3, TReturn>(Expression<Func<T, Func<TParameter1, TParameter2, TParameter3, TReturn>>> methodExpression)
        {
            Contract.Requires<ArgumentNullException>(methodExpression != null, "methodExpression");
            Contract.Ensures(Contract.Result<IMethodInfo<T, TParameter1, TParameter2, TParameter3, TReturn>>() != null);

            return null;
        }

        IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TReturn> ITypeInfo<T>.GetMethod<TParameter1, TParameter2, TParameter3, TParameter4, TReturn>(Expression<Func<T, Func<TParameter1, TParameter2, TParameter3, TParameter4, TReturn>>> methodExpression)
        {
            Contract.Requires<ArgumentNullException>(methodExpression != null, "methodExpression");
            Contract.Ensures(Contract.Result<IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TReturn>>() != null);

            return null;
        }

        IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TReturn> ITypeInfo<T>.GetMethod<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TReturn>(Expression<Func<T, Func<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TReturn>>> methodExpression)
        {
            Contract.Requires<ArgumentNullException>(methodExpression != null, "methodExpression");
            Contract.Ensures(Contract.Result<IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TReturn>>() != null);

            return null;
        }

        IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TReturn> ITypeInfo<T>.GetMethod<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TReturn>(Expression<Func<T, Func<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TReturn>>> methodExpression)
        {
            Contract.Requires<ArgumentNullException>(methodExpression != null, "methodExpression");
            Contract.Ensures(Contract.Result<IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TReturn>>() != null);

            return null;
        }

        IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TReturn> ITypeInfo<T>.GetMethod<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TReturn>(Expression<Func<T, Func<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TReturn>>> methodExpression)
        {
            Contract.Requires<ArgumentNullException>(methodExpression != null, "methodExpression");
            Contract.Ensures(Contract.Result<IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TReturn>>() != null);

            return null;
        }

        IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8, TReturn> ITypeInfo<T>.GetMethod<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8, TReturn>(Expression<Func<T, Func<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8, TReturn>>> methodExpression)
        {
            Contract.Requires<ArgumentNullException>(methodExpression != null, "methodExpression");
            Contract.Ensures(Contract.Result<IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8, TReturn>>() != null);

            return null;
        }

        IMethodInfo ITypeInfo.GetMethod(string name)
        {
            return null;
        }

        IMethodInfo ITypeInfo.GetMethod(string name, Type[] types)
        {
            return null;
        }

        IEnumerable<IMethodInfo<T>> ITypeInfo<T>.GetMethods()
        {
            Contract.Ensures(Contract.Result<IEnumerable<IMethodInfo<T>>>() != null);

            return null;
        }

        IEnumerable<IMethodInfo> ITypeInfo.GetMethods()
        {
            return null;
        }

        IEnumerable<IPropertyInfo> ITypeInfo.GetProperties()
        {
            return null;
        }

        IEnumerable<IPropertyInfo<T>> ITypeInfo<T>.GetProperties()
        {
            Contract.Ensures(Contract.Result<IEnumerable<IPropertyInfo<T>>>() != null);

            return null;
        }

        IPropertyInfo<T> ITypeInfo<T>.GetProperty(string name)
        {
            Contract.Requires<ArgumentNullException>(name != null, "name");
            Contract.Ensures(Contract.Result<IPropertyInfo<T>>() != null);

            return null;
        }

        IPropertyInfo ITypeInfo.GetProperty(string name)
        {
            return null;
        }

        IReadWritePropertyInfo<T, TProperty> ITypeInfo<T>.GetReadWriteProperty<TProperty>(Expression<Func<T, TProperty>> propertyExpression)
        {
            Contract.Requires<ArgumentNullException>(propertyExpression != null, "propertyExpression");
            Contract.Ensures(Contract.Result<IReadWritePropertyInfo<T, TProperty>>() != null);

            return null;
        }

        IReadablePropertyInfo<T, TProperty> ITypeInfo<T>.GetReadableProperty<TProperty>(Expression<Func<T, TProperty>> propertyExpression)
        {
            Contract.Requires<ArgumentNullException>(propertyExpression != null, "propertyExpression");
            Contract.Ensures(Contract.Result<IReadablePropertyInfo<T, TProperty>>() != null);

            return null;
        }

        IVoidMethodInfo<T> ITypeInfo<T>.GetVoidMethod(Expression<Func<T, Action>> methodExpression)
        {
            Contract.Requires<ArgumentNullException>(methodExpression != null, "methodExpression");
            Contract.Ensures(Contract.Result<IVoidMethodInfo<T>>() != null);

            return null;
        }

        IVoidMethodInfo<T, TParameter1> ITypeInfo<T>.GetVoidMethod<TParameter1>(Expression<Func<T, Action<TParameter1>>> methodExpression)
        {
            Contract.Requires<ArgumentNullException>(methodExpression != null, "methodExpression");
            Contract.Ensures(Contract.Result<IVoidMethodInfo<T, TParameter1>>() != null);

            return null;
        }

        IVoidMethodInfo<T, TParameter1, TParameter2> ITypeInfo<T>.GetVoidMethod<TParameter1, TParameter2>(Expression<Func<T, Action<TParameter1, TParameter2>>> methodExpression)
        {
            Contract.Requires<ArgumentNullException>(methodExpression != null, "methodExpression");
            Contract.Ensures(Contract.Result<IVoidMethodInfo<T, TParameter1, TParameter2>>() != null);

            return null;
        }

        IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3> ITypeInfo<T>.GetVoidMethod<TParameter1, TParameter2, TParameter3>(Expression<Func<T, Action<TParameter1, TParameter2, TParameter3>>> methodExpression)
        {
            Contract.Requires<ArgumentNullException>(methodExpression != null, "methodExpression");
            Contract.Ensures(Contract.Result<IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3>>() != null);

            return null;
        }

        IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4> ITypeInfo<T>.GetVoidMethod<TParameter1, TParameter2, TParameter3, TParameter4>(Expression<Func<T, Action<TParameter1, TParameter2, TParameter3, TParameter4>>> methodExpression)
        {
            Contract.Requires<ArgumentNullException>(methodExpression != null, "methodExpression");
            Contract.Ensures(Contract.Result<IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4>>() != null);

            return null;
        }

        IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5> ITypeInfo<T>.GetVoidMethod<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5>(Expression<Func<T, Action<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5>>> methodExpression)
        {
            Contract.Requires<ArgumentNullException>(methodExpression != null, "methodExpression");
            Contract.Ensures(Contract.Result<IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5>>() != null);

            return null;
        }

        IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6> ITypeInfo<T>.GetVoidMethod<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6>(Expression<Func<T, Action<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6>>> methodExpression)
        {
            Contract.Requires<ArgumentNullException>(methodExpression != null, "methodExpression");
            Contract.Ensures(Contract.Result<IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6>>() != null);

            return null;
        }

        IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7> ITypeInfo<T>.GetVoidMethod<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7>(Expression<Func<T, Action<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7>>> methodExpression)
        {
            Contract.Requires<ArgumentNullException>(methodExpression != null, "methodExpression");
            Contract.Ensures(Contract.Result<IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7>>() != null);

            return null;
        }

        IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8> ITypeInfo<T>.GetVoidMethod<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8>(Expression<Func<T, Action<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8>>> methodExpression)
        {
            Contract.Requires<ArgumentNullException>(methodExpression != null, "methodExpression");
            Contract.Ensures(Contract.Result<IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8>>() != null);

            return null;
        }

        IWritablePropertyInfo<T, TProperty> ITypeInfo<T>.GetWritableProperty<TProperty>(Expression<Func<T, TProperty>> propertyExpression)
        {
            Contract.Requires<ArgumentNullException>(propertyExpression != null, "propertyExpression");
            Contract.Ensures(Contract.Result<IWritablePropertyInfo<T, TProperty>>() != null);

            return null;
        }

        #endregion
    }
}