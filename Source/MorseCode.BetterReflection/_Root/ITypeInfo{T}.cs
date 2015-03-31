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

    /// <summary>
    /// An interface representing type info for type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The type represented by this type info.
    /// </typeparam>
    public interface ITypeInfo<T> : ITypeInfo
    {
        #region Public Methods and Operators

        /// <summary>
        /// Gets a property info instance for each public instance property accessible through type <typeparamref name="T"/>.
        /// </summary>
        /// <returns>
        /// An enumerable of property info instances for each public instance property accessible through type <typeparamref name="T"/>.
        /// </returns>
        new IEnumerable<IPropertyInfo<T>> GetProperties();

        /// <summary>
        /// Gets a property info instance for the public instance property with name <paramref name="name"/> accessible through type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="name">
        /// The name of the property for which to get property info.
        /// </param>
        /// <returns>
        /// A property info instance for the public instance property with name <paramref name="name"/> accessible through type <typeparamref name="T"/> if
        /// it exists, <c>null</c> otherwise.
        /// </returns>
        new IPropertyInfo<T> GetProperty(string name);

        /// <summary>
        /// Gets a readable property info instance for the property represented by <paramref name="propertyExpression"/>.
        /// </summary>
        /// <typeparam name="TProperty">
        /// The type of the property.
        /// </typeparam>
        /// <param name="propertyExpression">
        /// The property expression of the form <c>o => o.Property</c> which returns a readable property of type <typeparamref name="TProperty"/> given an instance of type <typeparamref name="T"/>.
        /// </param>
        /// <returns>
        /// A readable property info instance for the property represented by <paramref name="propertyExpression"/>.
        /// </returns>
        IReadablePropertyInfo<T, TProperty> GetReadableProperty<TProperty>(Expression<Func<T, TProperty>> propertyExpression);

        /// <summary>
        /// Gets a writable property info instance for the property represented by <paramref name="propertyExpression"/>.
        /// </summary>
        /// <typeparam name="TProperty">
        /// The type of the property.
        /// </typeparam>
        /// <param name="propertyExpression">
        /// The property expression of the form <c>o => o.Property</c> which returns a writable property of type <typeparamref name="TProperty"/> given an instance of type <typeparamref name="T"/>.
        /// </param>
        /// <returns>
        /// A writable property info instance for the property represented by <paramref name="propertyExpression"/>.
        /// </returns>
        IWritablePropertyInfo<T, TProperty> GetWritableProperty<TProperty>(Expression<Func<T, TProperty>> propertyExpression);

        /// <summary>
        /// Gets a readable and writable property info instance for the property represented by <paramref name="propertyExpression"/>.
        /// </summary>
        /// <typeparam name="TProperty">
        /// The type of the property.
        /// </typeparam>
        /// <param name="propertyExpression">
        /// The property expression of the form <c>o => o.Property</c> which returns a readable and writable property of type <typeparamref name="TProperty"/> given an instance of type <typeparamref name="T"/>.
        /// </param>
        /// <returns>
        /// A readable and writable property info instance for the property represented by <paramref name="propertyExpression"/>.
        /// </returns>
        IReadWritePropertyInfo<T, TProperty> GetReadWriteProperty<TProperty>(Expression<Func<T, TProperty>> propertyExpression);

        /// <summary>
        /// Gets a method info instance for each public instance method accessible through type <typeparamref name="T"/>.
        /// </summary>
        /// <returns>
        /// An enumerable of method info instances for each public instance method accessible through type <typeparamref name="T"/>.
        /// </returns>
        new IEnumerable<IMethodInfo<T>> GetMethods();

        /// <summary>
        /// Gets a method info instance for the public instance method with name <paramref name="name"/> accessible through type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="name">
        /// The name of the method for which to get method info.
        /// </param>
        /// <returns>
        /// A method info instance for the public instance method with name <paramref name="name"/> accessible through type <typeparamref name="T"/> if
        /// it exists, <c>null</c> otherwise.
        /// </returns>
        new IMethodInfo<T> GetMethod(string name);

        /// <summary>
        /// Gets a method info instance for the method represented by <paramref name="methodExpression"/>.
        /// </summary>
        /// <typeparam name="TReturn">
        /// The return type of the method.
        /// </typeparam>
        /// <param name="methodExpression">
        /// The property expression of the form <c>o => (Func&lt;TReturn&gt;)o.Method</c> which returns a method convertible to delegate type <see cref="Func{TReturn}"/> given an instance of type <typeparamref name="T"/>.
        /// </param>
        /// <returns>
        /// A method info instance for the method represented by <paramref name="methodExpression"/>.
        /// </returns>
        IMethodInfo<T, TReturn> GetMethod<TReturn>(Expression<Func<T, Func<TReturn>>> methodExpression);

        /// <summary>
        /// Gets a method info instance for the method represented by <paramref name="methodExpression"/>.
        /// </summary>
        /// <typeparam name="TParameter1">
        /// The type of the first parameter.
        /// </typeparam>
        /// <typeparam name="TReturn">
        /// The return type of the method.
        /// </typeparam>
        /// <param name="methodExpression">
        /// The property expression of the form <c>o => (Func&lt;TParameter1,TReturn&gt;)o.Method</c> which returns a method convertible to delegate type <see cref="Func{TParameter1,TReturn}"/> given an instance of type <typeparamref name="T"/>.
        /// </param>
        /// <returns>
        /// A method info instance for the method represented by <paramref name="methodExpression"/>.
        /// </returns>
        IMethodInfo<T, TParameter1, TReturn> GetMethod<TParameter1, TReturn>(Expression<Func<T, Func<TParameter1, TReturn>>> methodExpression);

        /// <summary>
        /// Gets a method info instance for the method represented by <paramref name="methodExpression"/>.
        /// </summary>
        /// <typeparam name="TParameter1">
        /// The type of the first parameter.
        /// </typeparam>
        /// <typeparam name="TParameter2">
        /// The type of the second parameter.
        /// </typeparam>
        /// <typeparam name="TReturn">
        /// The return type of the method.
        /// </typeparam>
        /// <param name="methodExpression">
        /// The property expression of the form <c>o => (Func&lt;TParameter1,TParameter2,TReturn&gt;)o.Method</c> which returns a method convertible to delegate type <see cref="Func{TParameter1,TParameter2,TReturn}"/> given an instance of type <typeparamref name="T"/>.
        /// </param>
        /// <returns>
        /// A method info instance for the method represented by <paramref name="methodExpression"/>.
        /// </returns>
        IMethodInfo<T, TParameter1, TParameter2, TReturn> GetMethod<TParameter1, TParameter2, TReturn>(Expression<Func<T, Func<TParameter1, TParameter2, TReturn>>> methodExpression);

        /// <summary>
        /// Gets a method info instance for the method represented by <paramref name="methodExpression"/>.
        /// </summary>
        /// <typeparam name="TParameter1">
        /// The type of the first parameter.
        /// </typeparam>
        /// <typeparam name="TParameter2">
        /// The type of the second parameter.
        /// </typeparam>
        /// <typeparam name="TParameter3">
        /// The type of the third parameter.
        /// </typeparam>
        /// <typeparam name="TReturn">
        /// The return type of the method.
        /// </typeparam>
        /// <param name="methodExpression">
        /// The property expression of the form <c>o => (Func&lt;TParameter1,TParameter2,TParameter3,TReturn&gt;)o.Method</c> which returns a method convertible to delegate type <see cref="Func{TParameter1,TParameter2,TParameter3,TReturn}"/> given an instance of type <typeparamref name="T"/>.
        /// </param>
        /// <returns>
        /// A method info instance for the method represented by <paramref name="methodExpression"/>.
        /// </returns>
        IMethodInfo<T, TParameter1, TParameter2, TParameter3, TReturn> GetMethod<TParameter1, TParameter2, TParameter3, TReturn>(Expression<Func<T, Func<TParameter1, TParameter2, TParameter3, TReturn>>> methodExpression);

        /// <summary>
        /// Gets a method info instance for the method represented by <paramref name="methodExpression"/>.
        /// </summary>
        /// <typeparam name="TParameter1">
        /// The type of the first parameter.
        /// </typeparam>
        /// <typeparam name="TParameter2">
        /// The type of the second parameter.
        /// </typeparam>
        /// <typeparam name="TParameter3">
        /// The type of the third parameter.
        /// </typeparam>
        /// <typeparam name="TParameter4">
        /// The type of the fourth parameter.
        /// </typeparam>
        /// <typeparam name="TReturn">
        /// The return type of the method.
        /// </typeparam>
        /// <param name="methodExpression">
        /// The property expression of the form <c>o => (Func&lt;TParameter1,TParameter2,TParameter3,TParameter4,TReturn&gt;)o.Method</c> which returns a method convertible to delegate type <see cref="Func{TParameter1,TParameter2,TParameter3,TParameter4,TReturn}"/> given an instance of type <typeparamref name="T"/>.
        /// </param>
        /// <returns>
        /// A method info instance for the method represented by <paramref name="methodExpression"/>.
        /// </returns>
        IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TReturn> GetMethod<TParameter1, TParameter2, TParameter3, TParameter4, TReturn>(Expression<Func<T, Func<TParameter1, TParameter2, TParameter3, TParameter4, TReturn>>> methodExpression);

        /// <summary>
        /// Gets a method info instance for the method represented by <paramref name="methodExpression"/>.
        /// </summary>
        /// <typeparam name="TParameter1">
        /// The type of the first parameter.
        /// </typeparam>
        /// <typeparam name="TParameter2">
        /// The type of the second parameter.
        /// </typeparam>
        /// <typeparam name="TParameter3">
        /// The type of the third parameter.
        /// </typeparam>
        /// <typeparam name="TParameter4">
        /// The type of the fourth parameter.
        /// </typeparam>
        /// <typeparam name="TParameter5">
        /// The type of the fifth parameter.
        /// </typeparam>
        /// <typeparam name="TReturn">
        /// The return type of the method.
        /// </typeparam>
        /// <param name="methodExpression">
        /// The property expression of the form <c>o => (Func&lt;TParameter1,TParameter2,TParameter3,TParameter4,TParameter5,TReturn&gt;)o.Method</c> which returns a method convertible to delegate type <see cref="Func{TParameter1,TParameter2,TParameter3,TParameter4,TParameter5,TReturn}"/> given an instance of type <typeparamref name="T"/>.
        /// </param>
        /// <returns>
        /// A method info instance for the method represented by <paramref name="methodExpression"/>.
        /// </returns>
        IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TReturn> GetMethod<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TReturn>(Expression<Func<T, Func<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TReturn>>> methodExpression);

        /// <summary>
        /// Gets a method info instance for the method represented by <paramref name="methodExpression"/>.
        /// </summary>
        /// <typeparam name="TParameter1">
        /// The type of the first parameter.
        /// </typeparam>
        /// <typeparam name="TParameter2">
        /// The type of the second parameter.
        /// </typeparam>
        /// <typeparam name="TParameter3">
        /// The type of the third parameter.
        /// </typeparam>
        /// <typeparam name="TParameter4">
        /// The type of the fourth parameter.
        /// </typeparam>
        /// <typeparam name="TParameter5">
        /// The type of the fifth parameter.
        /// </typeparam>
        /// <typeparam name="TParameter6">
        /// The type of the sixth parameter.
        /// </typeparam>
        /// <typeparam name="TReturn">
        /// The return type of the method.
        /// </typeparam>
        /// <param name="methodExpression">
        /// The property expression of the form <c>o => (Func&lt;TParameter1,TParameter2,TParameter3,TParameter4,TParameter5,TParameter6,TReturn&gt;)o.Method</c> which returns a method convertible to delegate type <see cref="Func{TParameter1,TParameter2,TParameter3,TParameter4,TParameter5,TParameter6,TReturn}"/> given an instance of type <typeparamref name="T"/>.
        /// </param>
        /// <returns>
        /// A method info instance for the method represented by <paramref name="methodExpression"/>.
        /// </returns>
        IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TReturn> GetMethod<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TReturn>(Expression<Func<T, Func<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TReturn>>> methodExpression);

        /// <summary>
        /// Gets a method info instance for the method represented by <paramref name="methodExpression"/>.
        /// </summary>
        /// <typeparam name="TParameter1">
        /// The type of the first parameter.
        /// </typeparam>
        /// <typeparam name="TParameter2">
        /// The type of the second parameter.
        /// </typeparam>
        /// <typeparam name="TParameter3">
        /// The type of the third parameter.
        /// </typeparam>
        /// <typeparam name="TParameter4">
        /// The type of the fourth parameter.
        /// </typeparam>
        /// <typeparam name="TParameter5">
        /// The type of the fifth parameter.
        /// </typeparam>
        /// <typeparam name="TParameter6">
        /// The type of the sixth parameter.
        /// </typeparam>
        /// <typeparam name="TParameter7">
        /// The type of the seventh parameter.
        /// </typeparam>
        /// <typeparam name="TReturn">
        /// The return type of the method.
        /// </typeparam>
        /// <param name="methodExpression">
        /// The property expression of the form <c>o => (Func&lt;TParameter1,TParameter2,TParameter3,TParameter4,TParameter5,TParameter6,TParameter7,TReturn&gt;)o.Method</c> which returns a method convertible to delegate type <see cref="Func{TParameter1,TParameter2,TParameter3,TParameter4,TParameter5,TParameter6,TParameter7,TReturn}"/> given an instance of type <typeparamref name="T"/>.
        /// </param>
        /// <returns>
        /// A method info instance for the method represented by <paramref name="methodExpression"/>.
        /// </returns>
        IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TReturn> GetMethod<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TReturn>(Expression<Func<T, Func<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TReturn>>> methodExpression);

        /// <summary>
        /// Gets a method info instance for the method represented by <paramref name="methodExpression"/>.
        /// </summary>
        /// <typeparam name="TParameter1">
        /// The type of the first parameter.
        /// </typeparam>
        /// <typeparam name="TParameter2">
        /// The type of the second parameter.
        /// </typeparam>
        /// <typeparam name="TParameter3">
        /// The type of the third parameter.
        /// </typeparam>
        /// <typeparam name="TParameter4">
        /// The type of the fourth parameter.
        /// </typeparam>
        /// <typeparam name="TParameter5">
        /// The type of the fifth parameter.
        /// </typeparam>
        /// <typeparam name="TParameter6">
        /// The type of the sixth parameter.
        /// </typeparam>
        /// <typeparam name="TParameter7">
        /// The type of the seventh parameter.
        /// </typeparam>
        /// <typeparam name="TParameter8">
        /// The type of the eighth parameter.
        /// </typeparam>
        /// <typeparam name="TReturn">
        /// The return type of the method.
        /// </typeparam>
        /// <param name="methodExpression">
        /// The property expression of the form <c>o => (Func&lt;TParameter1,TParameter2,TParameter3,TParameter4,TParameter5,TParameter6,TParameter7,TParameter8,TReturn&gt;)o.Method</c> which returns a method convertible to delegate type <see cref="Func{TParameter1,TParameter2,TParameter3,TParameter4,TParameter5,TParameter6,TParameter7,TParameter8,TReturn}"/> given an instance of type <typeparamref name="T"/>.
        /// </param>
        /// <returns>
        /// A method info instance for the method represented by <paramref name="methodExpression"/>.
        /// </returns>
        IMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8, TReturn> GetMethod<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8, TReturn>(Expression<Func<T, Func<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8, TReturn>>> methodExpression);

        /// <summary>
        /// Gets a method info instance for the method represented by <paramref name="methodExpression"/>.
        /// </summary>
        /// <param name="methodExpression">
        /// The property expression of the form <c>o => o.Method</c> which returns a method convertible to delegate type <see cref="Action"/> given an instance of type <typeparamref name="T"/>.
        /// </param>
        /// <returns>
        /// A method info instance for the method represented by <paramref name="methodExpression"/>.
        /// </returns>
        IVoidMethodInfo<T> GetVoidMethod(Expression<Func<T, Action>> methodExpression);

        /// <summary>
        /// Gets a method info instance for the method represented by <paramref name="methodExpression"/>.
        /// </summary>
        /// <typeparam name="TParameter1">
        /// The type of the first parameter.
        /// </typeparam>
        /// <param name="methodExpression">
        /// The property expression of the form <c>o => (Action&lt;TParameter1&gt;)o.Method</c> which returns a method convertible to delegate type <see cref="Action{TParameter1}"/> given an instance of type <typeparamref name="T"/>.
        /// </param>
        /// <returns>
        /// A method info instance for the method represented by <paramref name="methodExpression"/>.
        /// </returns>
        IVoidMethodInfo<T, TParameter1> GetVoidMethod<TParameter1>(Expression<Func<T, Action<TParameter1>>> methodExpression);

        /// <summary>
        /// Gets a method info instance for the method represented by <paramref name="methodExpression"/>.
        /// </summary>
        /// <typeparam name="TParameter1">
        /// The type of the first parameter.
        /// </typeparam>
        /// <typeparam name="TParameter2">
        /// The type of the second parameter.
        /// </typeparam>
        /// <param name="methodExpression">
        /// The property expression of the form <c>o => (Action&lt;TParameter1,TParameter2&gt;)o.Method</c> which returns a method convertible to delegate type <see cref="Action{TParameter1,TParameter2}"/> given an instance of type <typeparamref name="T"/>.
        /// </param>
        /// <returns>
        /// A method info instance for the method represented by <paramref name="methodExpression"/>.
        /// </returns>
        IVoidMethodInfo<T, TParameter1, TParameter2> GetVoidMethod<TParameter1, TParameter2>(Expression<Func<T, Action<TParameter1, TParameter2>>> methodExpression);

        /// <summary>
        /// Gets a method info instance for the method represented by <paramref name="methodExpression"/>.
        /// </summary>
        /// <typeparam name="TParameter1">
        /// The type of the first parameter.
        /// </typeparam>
        /// <typeparam name="TParameter2">
        /// The type of the second parameter.
        /// </typeparam>
        /// <typeparam name="TParameter3">
        /// The type of the third parameter.
        /// </typeparam>
        /// <param name="methodExpression">
        /// The property expression of the form <c>o => (Action&lt;TParameter1,TParameter2,TParameter3&gt;)o.Method</c> which returns a method convertible to delegate type <see cref="Action{TParameter1,TParameter2,TParameter3}"/> given an instance of type <typeparamref name="T"/>.
        /// </param>
        /// <returns>
        /// A method info instance for the method represented by <paramref name="methodExpression"/>.
        /// </returns>
        IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3> GetVoidMethod<TParameter1, TParameter2, TParameter3>(Expression<Func<T, Action<TParameter1, TParameter2, TParameter3>>> methodExpression);

        /// <summary>
        /// Gets a method info instance for the method represented by <paramref name="methodExpression"/>.
        /// </summary>
        /// <typeparam name="TParameter1">
        /// The type of the first parameter.
        /// </typeparam>
        /// <typeparam name="TParameter2">
        /// The type of the second parameter.
        /// </typeparam>
        /// <typeparam name="TParameter3">
        /// The type of the third parameter.
        /// </typeparam>
        /// <typeparam name="TParameter4">
        /// The type of the fourth parameter.
        /// </typeparam>
        /// <param name="methodExpression">
        /// The property expression of the form <c>o => (Action&lt;TParameter1,TParameter2,TParameter3,TParameter4&gt;)o.Method</c> which returns a method convertible to delegate type <see cref="Action{TParameter1,TParameter2,TParameter3,TParameter4}"/> given an instance of type <typeparamref name="T"/>.
        /// </param>
        /// <returns>
        /// A method info instance for the method represented by <paramref name="methodExpression"/>.
        /// </returns>
        IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4> GetVoidMethod<TParameter1, TParameter2, TParameter3, TParameter4>(Expression<Func<T, Action<TParameter1, TParameter2, TParameter3, TParameter4>>> methodExpression);

        /// <summary>
        /// Gets a method info instance for the method represented by <paramref name="methodExpression"/>.
        /// </summary>
        /// <typeparam name="TParameter1">
        /// The type of the first parameter.
        /// </typeparam>
        /// <typeparam name="TParameter2">
        /// The type of the second parameter.
        /// </typeparam>
        /// <typeparam name="TParameter3">
        /// The type of the third parameter.
        /// </typeparam>
        /// <typeparam name="TParameter4">
        /// The type of the fourth parameter.
        /// </typeparam>
        /// <typeparam name="TParameter5">
        /// The type of the fifth parameter.
        /// </typeparam>
        /// <param name="methodExpression">
        /// The property expression of the form <c>o => (Action&lt;TParameter1,TParameter2,TParameter3,TParameter4,TParameter5&gt;)o.Method</c> which returns a method convertible to delegate type <see cref="Action{TParameter1,TParameter2,TParameter3,TParameter4,TParameter5}"/> given an instance of type <typeparamref name="T"/>.
        /// </param>
        /// <returns>
        /// A method info instance for the method represented by <paramref name="methodExpression"/>.
        /// </returns>
        IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5> GetVoidMethod<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5>(Expression<Func<T, Action<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5>>> methodExpression);

        /// <summary>
        /// Gets a method info instance for the method represented by <paramref name="methodExpression"/>.
        /// </summary>
        /// <typeparam name="TParameter1">
        /// The type of the first parameter.
        /// </typeparam>
        /// <typeparam name="TParameter2">
        /// The type of the second parameter.
        /// </typeparam>
        /// <typeparam name="TParameter3">
        /// The type of the third parameter.
        /// </typeparam>
        /// <typeparam name="TParameter4">
        /// The type of the fourth parameter.
        /// </typeparam>
        /// <typeparam name="TParameter5">
        /// The type of the fifth parameter.
        /// </typeparam>
        /// <typeparam name="TParameter6">
        /// The type of the sixth parameter.
        /// </typeparam>
        /// <param name="methodExpression">
        /// The property expression of the form <c>o => (Action&lt;TParameter1,TParameter2,TParameter3,TParameter4,TParameter5,TParameter6&gt;)o.Method</c> which returns a method convertible to delegate type <see cref="Action{TParameter1,TParameter2,TParameter3,TParameter4,TParameter5,TParameter6}"/> given an instance of type <typeparamref name="T"/>.
        /// </param>
        /// <returns>
        /// A method info instance for the method represented by <paramref name="methodExpression"/>.
        /// </returns>
        IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6> GetVoidMethod<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6>(Expression<Func<T, Action<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6>>> methodExpression);

        /// <summary>
        /// Gets a method info instance for the method represented by <paramref name="methodExpression"/>.
        /// </summary>
        /// <typeparam name="TParameter1">
        /// The type of the first parameter.
        /// </typeparam>
        /// <typeparam name="TParameter2">
        /// The type of the second parameter.
        /// </typeparam>
        /// <typeparam name="TParameter3">
        /// The type of the third parameter.
        /// </typeparam>
        /// <typeparam name="TParameter4">
        /// The type of the fourth parameter.
        /// </typeparam>
        /// <typeparam name="TParameter5">
        /// The type of the fifth parameter.
        /// </typeparam>
        /// <typeparam name="TParameter6">
        /// The type of the sixth parameter.
        /// </typeparam>
        /// <typeparam name="TParameter7">
        /// The type of the seventh parameter.
        /// </typeparam>
        /// <param name="methodExpression">
        /// The property expression of the form <c>o => (Action&lt;TParameter1,TParameter2,TParameter3,TParameter4,TParameter5,TParameter6,TParameter7&gt;)o.Method</c> which returns a method convertible to delegate type <see cref="Action{TParameter1,TParameter2,TParameter3,TParameter4,TParameter5,TParameter6,TParameter7}"/> given an instance of type <typeparamref name="T"/>.
        /// </param>
        /// <returns>
        /// A method info instance for the method represented by <paramref name="methodExpression"/>.
        /// </returns>
        IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7> GetVoidMethod<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7>(Expression<Func<T, Action<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7>>> methodExpression);

        /// <summary>
        /// Gets a method info instance for the method represented by <paramref name="methodExpression"/>.
        /// </summary>
        /// <typeparam name="TParameter1">
        /// The type of the first parameter.
        /// </typeparam>
        /// <typeparam name="TParameter2">
        /// The type of the second parameter.
        /// </typeparam>
        /// <typeparam name="TParameter3">
        /// The type of the third parameter.
        /// </typeparam>
        /// <typeparam name="TParameter4">
        /// The type of the fourth parameter.
        /// </typeparam>
        /// <typeparam name="TParameter5">
        /// The type of the fifth parameter.
        /// </typeparam>
        /// <typeparam name="TParameter6">
        /// The type of the sixth parameter.
        /// </typeparam>
        /// <typeparam name="TParameter7">
        /// The type of the seventh parameter.
        /// </typeparam>
        /// <typeparam name="TParameter8">
        /// The type of the eighth parameter.
        /// </typeparam>
        /// <param name="methodExpression">
        /// The property expression of the form <c>o => (Action&lt;TParameter1,TParameter2,TParameter3,TParameter4,TParameter5,TParameter6,TParameter7,TParameter8&gt;)o.Method</c> which returns a method convertible to delegate type <see cref="Action{TParameter1,TParameter2,TParameter3,TParameter4,TParameter5,TParameter6,TParameter7,TParameter8}"/> given an instance of type <typeparamref name="T"/>.
        /// </param>
        /// <returns>
        /// A method info instance for the method represented by <paramref name="methodExpression"/>.
        /// </returns>
        IVoidMethodInfo<T, TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8> GetVoidMethod<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8>(Expression<Func<T, Action<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8>>> methodExpression);

        #endregion
    }
}