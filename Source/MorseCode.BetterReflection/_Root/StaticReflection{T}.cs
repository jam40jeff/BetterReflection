#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StaticReflection{T}.cs" company="MorseCode Software">
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
    using System.Diagnostics.Contracts;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// Provides methods for statically obtaining reflection info on type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The type for which to obtain static reflection info.
    /// </typeparam>
    public static class StaticReflection<T>
    {
        #region Public Methods and Operators

        /// <summary>
        /// Gets member info for a member accessible on type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="memberExpression">
        /// The member expression of the form <c>o => o.Member</c> which returns a member of type <typeparamref name="TMember"/> given an instance of type <typeparamref name="T"/>.
        /// </param>
        /// <typeparam name="TMember">
        /// The type of the member.
        /// </typeparam>
        /// <returns>
        /// The member info for the member.
        /// </returns>
        public static MemberInfo GetMemberInfo<TMember>(Expression<Func<T, TMember>> memberExpression)
        {
            Contract.Requires<ArgumentNullException>(memberExpression != null, "memberExpression");
            Contract.Ensures(Contract.Result<MemberInfo>() != null);

            return GetMemberInfoInternal(memberExpression);
        }

        /// <summary>
        /// Gets method info for a method accessible on type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="methodExpression">
        /// The method expression of the form <c>o => (TMethod)o.Method</c> which returns a method convertible to delegate type <typeparamref name="TMethod"/> given an instance of type <typeparamref name="T"/>.
        /// </param>
        /// <typeparam name="TMethod">
        /// The <see cref="Func{TReturn}"/> or <see cref="Action"/> delegate type of the method.
        /// </typeparam>
        /// <returns>
        /// The method info for the method.
        /// </returns>
        public static MethodInfo GetMethodInfo<TMethod>(Expression<Func<T, TMethod>> methodExpression)
        {
            Contract.Requires<ArgumentNullException>(methodExpression != null, "methodExpression");
            Contract.Ensures(Contract.Result<MethodInfo>() != null);

            return GetMethodInfoInternal(methodExpression);
        }

        /// <summary>
        /// Gets method info for a method accessible on type <typeparamref name="T"/> by calling the method.
        /// </summary>
        /// <param name="methodCallExpression">
        /// The in-scope method call expression of the form <c>o =&gt; o.Method(...)</c> where default values are passed for any parameters where necessary.
        /// </param>
        /// <returns>
        /// The member info for the member.
        /// </returns>
        public static MethodInfo GetMethodInfoFromMethodCall(Expression<Action<T>> methodCallExpression)
        {
            Contract.Requires<ArgumentNullException>(methodCallExpression != null, "methodCallExpression");
            Contract.Ensures(Contract.Result<MethodInfo>() != null);

            return GetMethodInfoFromMethodCallInternal(methodCallExpression);
        }

        /// <summary>
        /// Gets method info for a method accessible on type <typeparamref name="T"/> by calling the method.
        /// </summary>
        /// <typeparam name="TReturn">
        /// The return type of the method.
        /// </typeparam>
        /// <param name="methodCallExpression">
        /// The in-scope method call expression of the form <c>o =&gt; o.Method(...)</c> where default values are passed for any parameters where necessary.
        /// </param>
        /// <returns>
        /// The member info for the member.
        /// </returns>
        public static MethodInfo GetMethodInfoFromMethodCall<TReturn>(Expression<Func<T, TReturn>> methodCallExpression)
        {
            Contract.Requires<ArgumentNullException>(methodCallExpression != null, "methodCallExpression");
            Contract.Ensures(Contract.Result<MethodInfo>() != null);

            return GetMethodInfoFromMethodCallInternal(methodCallExpression);
        }

        #endregion

        #region Methods

        internal static MemberInfo GetMemberInfoInternal<TMember>(Expression<Func<T, TMember>> memberExpression)
        {
            Contract.Requires(memberExpression != null, "memberExpression");
            Contract.Ensures(Contract.Result<MemberInfo>() != null);

            return StaticReflection.GetMemberInfoFromMemberAccessInternal(memberExpression);
        }

        internal static MethodInfo GetMethodInfoFromMethodCallInternal(Expression<Action<T>> methodCallExpression)
        {
            Contract.Requires(methodCallExpression != null, "methodCallExpression");
            Contract.Ensures(Contract.Result<MemberInfo>() != null);

            return StaticReflection.GetMethodInfoFromMethodCallInternal(methodCallExpression);
        }

        internal static MethodInfo GetMethodInfoFromMethodCallInternal<TReturn>(Expression<Func<T, TReturn>> methodCallExpression)
        {
            Contract.Requires(methodCallExpression != null, "methodCallExpression");
            Contract.Ensures(Contract.Result<MemberInfo>() != null);

            return StaticReflection.GetMethodInfoFromMethodCallInternal(methodCallExpression);
        }

        internal static MethodInfo GetMethodInfoInternal<TMethod>(Expression<Func<T, TMethod>> methodExpression)
        {
            Contract.Requires(methodExpression != null, "methodExpression");
            Contract.Ensures(Contract.Result<MethodInfo>() != null);

            return StaticReflection.GetMethodInfoFromMemberAccessInternal(methodExpression);
        }

        #endregion
    }
}