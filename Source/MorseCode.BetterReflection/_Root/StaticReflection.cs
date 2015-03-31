#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StaticReflection.cs" company="MorseCode Software">
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
    /// Provides methods for statically obtaining reflection info.
    /// </summary>
    public static class StaticReflection
    {
        #region Public Methods and Operators

        /// <summary>
        /// Gets member info for an in-scope member, which may be a local variable, parameter, static member, or any instance member
        /// currently in scope.
        /// </summary>
        /// <param name="inScopeMemberExpression">
        /// The in-scope member expression of the form <c>() => member</c> which returns a member of type <typeparamref name="TMember"/>.
        /// </param>
        /// <typeparam name="TMember">
        /// The type of the member.
        /// </typeparam>
        /// <returns>
        /// The member info for the member.
        /// </returns>
        public static MemberInfo GetInScopeMemberInfo<TMember>(Expression<Func<TMember>> inScopeMemberExpression)
        {
            Contract.Requires<ArgumentNullException>(inScopeMemberExpression != null, "inScopeMemberExpression");
            Contract.Ensures(Contract.Result<MemberInfo>() != null);

            return GetInScopeMemberInfoInternal(inScopeMemberExpression);
        }

        /// <summary>
        /// Gets the name of an in-scope member, which may be a local variable, parameter, static member, or any instance member
        /// currently in scope.
        /// </summary>
        /// <param name="inScopeMemberExpression">
        /// The in-scope member expression of the form <c>() => member</c> which returns a member of type <typeparamref name="TMember"/>.
        /// </param>
        /// <typeparam name="TMember">
        /// The type of the member.
        /// </typeparam>
        /// <returns>
        /// The name of the member.
        /// </returns>
        public static string GetInScopeMemberName<TMember>(Expression<Func<TMember>> inScopeMemberExpression)
        {
            Contract.Requires<ArgumentNullException>(inScopeMemberExpression != null, "inScopeMemberExpression");
            Contract.Ensures(Contract.Result<string>() != null);

            return GetInScopeMemberInfoInternal(inScopeMemberExpression).Name;
        }

        /// <summary>
        /// Gets method info for an in-scope method, which may be a static method, or any instance method currently in scope.
        /// </summary>
        /// <param name="inScopeMethodExpression">
        /// The in-scope method expression of the form <c>() => (TMethod)method</c> which returns a method convertible to delegate type <typeparamref name="TMethod"/>.
        /// </param>
        /// <typeparam name="TMethod">
        /// The <see cref="Func{TReturn}"/> or <see cref="Action"/> delegate type of the method.
        /// </typeparam>
        /// <returns>
        /// The method info for the method.
        /// </returns>
        public static MethodInfo GetInScopeMethodInfo<TMethod>(Expression<Func<TMethod>> inScopeMethodExpression)
        {
            Contract.Requires<ArgumentNullException>(inScopeMethodExpression != null, "inScopeMethodExpression");
            Contract.Ensures(Contract.Result<MethodInfo>() != null);

            return GetInScopeMethodInfoInternal(inScopeMethodExpression);
        }

        /// <summary>
        /// Gets method info for an in-scope method, which may be a static method, or any instance method currently in scope, by calling the method.
        /// </summary>
        /// <param name="inScopeMethodCallExpression">
        /// The in-scope method call expression of the form <c>() =&gt; Method(...)</c> where default values are passed for any parameters where necessary.
        /// </param>
        /// <returns>
        /// The member info for the member.
        /// </returns>
        public static MethodInfo GetInScopeMethodInfoFromMethodCall(Expression<Action> inScopeMethodCallExpression)
        {
            Contract.Requires<ArgumentNullException>(inScopeMethodCallExpression != null, "inScopeMethodCallExpression");
            Contract.Ensures(Contract.Result<MemberInfo>() != null);

            return GetInScopeMethodInfoFromMethodCallInternal(inScopeMethodCallExpression);
        }

        /// <summary>
        /// Gets method info for an in-scope method, which may be a static method, or any instance method currently in scope, by calling the method.
        /// </summary>
        /// <typeparam name="TReturn">
        /// The return type of the method.
        /// </typeparam>
        /// <param name="inScopeMethodCallExpression">
        /// The in-scope method call expression of the form <c>() =&gt; Method(...)</c> where default values are passed for any parameters where necessary.
        /// </param>
        /// <returns>
        /// The member info for the member.
        /// </returns>
        public static MethodInfo GetInScopeMethodInfoFromMethodCall<TReturn>(Expression<Func<TReturn>> inScopeMethodCallExpression)
        {
            Contract.Requires<ArgumentNullException>(inScopeMethodCallExpression != null, "inScopeMethodCallExpression");
            Contract.Ensures(Contract.Result<MemberInfo>() != null);

            return GetInScopeMethodInfoFromMethodCallInternal(inScopeMethodCallExpression);
        }

        /// <summary>
        /// Gets the name of an in-scope method, which may be a static method, or any instance method currently in scope, by calling the method.
        /// </summary>
        /// <param name="inScopeMethodCallExpression">
        /// The in-scope method call expression of the form <c>() =&gt; Method(...)</c> where default values are passed for any parameters where necessary.
        /// </param>
        /// <returns>
        /// The member info for the member.
        /// </returns>
        public static string GetInScopeMethodName(Expression<Action> inScopeMethodCallExpression)
        {
            Contract.Requires<ArgumentNullException>(inScopeMethodCallExpression != null, "inScopeMethodCallExpression");
            Contract.Ensures(Contract.Result<string>() != null);

            return GetInScopeMethodInfoFromMethodCallInternal(inScopeMethodCallExpression).Name;
        }

        /// <summary>
        /// Gets the name of an in-scope method, which may be a static method, or any instance method currently in scope, by calling the method.
        /// </summary>
        /// <typeparam name="TReturn">
        /// The return type of the method.
        /// </typeparam>
        /// <param name="inScopeMethodCallExpression">
        /// The in-scope method call expression of the form <c>() =&gt; Method(...)</c> where default values are passed for any parameters where necessary.
        /// </param>
        /// <returns>
        /// The member info for the member.
        /// </returns>
        public static string GetInScopeMethodName<TReturn>(Expression<Func<TReturn>> inScopeMethodCallExpression)
        {
            Contract.Requires<ArgumentNullException>(inScopeMethodCallExpression != null, "inScopeMethodCallExpression");
            Contract.Ensures(Contract.Result<string>() != null);

            return GetInScopeMethodInfoFromMethodCallInternal(inScopeMethodCallExpression).Name;
        }

        #endregion

        #region Methods

        internal static MemberInfo GetInScopeMemberInfoInternal<TMember>(Expression<Func<TMember>> inScopeMemberExpression)
        {
            Contract.Requires(inScopeMemberExpression != null, "inScopeMemberExpression");
            Contract.Ensures(Contract.Result<MemberInfo>() != null);

            return GetMemberInfoFromMemberAccessInternal(inScopeMemberExpression);
        }

        internal static MethodInfo GetInScopeMethodInfoInternal<TMember>(Expression<Func<TMember>> inScopeMethodExpression)
        {
            Contract.Requires(inScopeMethodExpression != null, "inScopeMethodExpression");
            Contract.Ensures(Contract.Result<MemberInfo>() != null);

            return GetMethodInfoFromMemberAccessInternal(inScopeMethodExpression);
        }

        internal static MethodInfo GetInScopeMethodInfoFromMethodCallInternal(Expression<Action> inScopeMethodCallExpression)
        {
            Contract.Requires(inScopeMethodCallExpression != null, "inScopeMethodCallExpression");
            Contract.Ensures(Contract.Result<MemberInfo>() != null);

            return GetMethodInfoFromMethodCallInternal(inScopeMethodCallExpression);
        }

        internal static MethodInfo GetInScopeMethodInfoFromMethodCallInternal<TReturn>(Expression<Func<TReturn>> inScopeMethodCallExpression)
        {
            Contract.Requires(inScopeMethodCallExpression != null, "inScopeMethodCallExpression");
            Contract.Ensures(Contract.Result<MemberInfo>() != null);

            return GetMethodInfoFromMethodCallInternal(inScopeMethodCallExpression);
        }

        internal static MemberInfo GetMemberInfoFromMemberAccessInternal(LambdaExpression expression)
        {
            Contract.Requires(expression != null, "expression");
            Contract.Ensures(Contract.Result<MemberInfo>() != null);

            Expression currentExpression = expression.Body;

            if (currentExpression.NodeType == ExpressionType.Convert)
            {
                UnaryExpression convertExpression = (UnaryExpression)currentExpression;
                currentExpression = convertExpression.Operand;
            }

            if (currentExpression.NodeType == ExpressionType.MemberAccess)
            {
                MemberExpression memberExpression = (MemberExpression)currentExpression;
                if (memberExpression.Expression is MemberExpression)
                {
                    throw new ArgumentException("LambdaExpression must be a single member access.", "expression");
                }

                return memberExpression.Member;
            }

            throw new ArgumentException("LambdaExpression must be a member access.", "expression");
        }

        internal static MethodInfo GetMethodInfoFromMemberAccessInternal(LambdaExpression expression)
        {
            Contract.Requires(expression != null, "expression");
            Contract.Ensures(Contract.Result<MemberInfo>() != null);

            Expression currentExpression = expression.Body;

            if (currentExpression.NodeType == ExpressionType.Convert)
            {
                UnaryExpression convertExpression = (UnaryExpression)currentExpression;
                currentExpression = convertExpression.Operand;
            }

            if (currentExpression.NodeType == ExpressionType.Call)
            {
                MethodCallExpression createDelegateExpression = (MethodCallExpression)currentExpression;
                if (createDelegateExpression.Method.DeclaringType == typeof(MethodInfo) && createDelegateExpression.Method.Name == "CreateDelegate")
                {
                    currentExpression = createDelegateExpression.Object;
                    if (currentExpression != null && currentExpression.NodeType == ExpressionType.Constant)
                    {
                        ConstantExpression methodInfoExpression = (ConstantExpression)currentExpression;
                        if (methodInfoExpression.Type == typeof(MethodInfo))
                        {
                            MethodInfo result = (MethodInfo)methodInfoExpression.Value;
                            if (result != null)
                            {
                                return result;
                            }
                        }
                    }
                }
            }

            throw new ArgumentException("LambdaExpression must be a method member access.", "expression");
        }

        internal static MethodInfo GetMethodInfoFromMethodCallInternal(LambdaExpression expression)
        {
            Contract.Requires(expression != null, "expression");
            Contract.Ensures(Contract.Result<MethodInfo>() != null);

            Expression currentExpression = expression.Body;

            if (currentExpression.NodeType == ExpressionType.Convert)
            {
                UnaryExpression convertExpression = (UnaryExpression)currentExpression;
                currentExpression = convertExpression.Operand;
            }

            if (currentExpression.NodeType == ExpressionType.Call)
            {
                MethodCallExpression methodCallExpression = (MethodCallExpression)currentExpression;
                return methodCallExpression.Method;
            }

            throw new ArgumentException("LambdaExpression must be a method call.", "expression");
        }

        #endregion
    }
}