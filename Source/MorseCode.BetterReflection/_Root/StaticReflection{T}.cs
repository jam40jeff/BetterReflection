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

    public static class StaticReflection<T>
    {
        #region Public Methods and Operators

        public static MemberInfo GetMemberInfo<TMember>(Expression<Func<T, TMember>> memberExpression)
        {
            Contract.Requires<ArgumentNullException>(memberExpression != null, "memberExpression");
            Contract.Ensures(Contract.Result<MemberInfo>() != null);

            return GetMemberInfoInternal(memberExpression);
        }

        public static MethodInfo GetMethodInfo<TMethod>(Expression<Func<T, TMethod>> methodExpression)
        {
            Contract.Requires<ArgumentNullException>(methodExpression != null, "methodExpression");
            Contract.Ensures(Contract.Result<MethodInfo>() != null);

            return GetMethodInfoInternal(methodExpression);
        }

        public static MethodInfo GetMethodInfoFromMethodCall(Expression<Action<T>> methodCallExpression)
        {
            Contract.Requires<ArgumentNullException>(methodCallExpression != null, "methodCallExpression");
            Contract.Ensures(Contract.Result<MethodInfo>() != null);

            return GetMethodInfoFromMethodCallInternal(methodCallExpression);
        }

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

        internal static MethodInfo GetMethodInfoInternal<TMethod>(Expression<Func<T, TMethod>> methodExpression)
        {
            Contract.Requires(methodExpression != null, "methodExpression");
            Contract.Ensures(Contract.Result<MethodInfo>() != null);

            return StaticReflection.GetMethodInfoFromMemberAccessInternal(methodExpression);
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

        #endregion
    }
}