#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITypeInfo{T}.cs" company="MorseCode Software">
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
    using System.Linq.Expressions;

    public interface ITypeInfo<T> : ITypeInfo
    {
        #region Public Methods and Operators

        IEnumerable<IPropertyInfo<T>> GetProperties();

        IPropertyInfo<T> GetProperty(string name);

        IPropertyInfo<T, TProperty> GetProperty<TProperty>(Expression<Func<T, TProperty>> propertyExpression);

        IEnumerable<IMethodInfo<T>> GetMethods();

        IMethodInfo<T> GetMethod(string name);

        IVoidMethodInfo<T> GetVoidMethod(Expression<Func<T, Action>> methodExpression);

        IVoidMethodInfo<T, TParameter1> GetVoidMethod<TParameter1>(Expression<Func<T, Action<TParameter1>>> methodExpression);

        IVoidMethodInfo<T, TParameter1, TParameter2> GetVoidMethod<TParameter1, TParameter2>(Expression<Func<T, Action<TParameter1, TParameter2>>> methodExpression);

        IMethodInfo<T, TReturn> GetMethod<TReturn>(Expression<Func<T, Func<TReturn>>> methodExpression);

        IMethodInfo<T, TParameter1, TReturn> GetMethod<TParameter1, TReturn>(Expression<Func<T, Func<TParameter1, TReturn>>> methodExpression);

        IMethodInfo<T, TParameter1, TParameter2, TReturn> GetMethod<TParameter1, TParameter2, TReturn>(Expression<Func<T, Func<TParameter1, TParameter2, TReturn>>> methodExpression);

        #endregion
    }
}