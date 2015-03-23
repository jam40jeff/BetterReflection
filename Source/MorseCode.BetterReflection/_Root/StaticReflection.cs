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

    public static class StaticReflection
    {
        #region Public Methods and Operators

        public static MemberInfo GetInScopeMemberInfo<TMember>(Expression<Func<TMember>> inScopeMemberExpression)
        {
            Contract.Requires<ArgumentNullException>(inScopeMemberExpression != null, "inScopeMemberExpression");
            Contract.Ensures(Contract.Result<MemberInfo>() != null);

            return GetInScopeMemberInfoInternal(inScopeMemberExpression);
        }

        public static MethodInfo GetInScopeMethodInfo(Expression<Action> inScopeMethodCallExpression)
        {
            Contract.Requires<ArgumentNullException>(inScopeMethodCallExpression != null, "inScopeMethodCallExpression");
            Contract.Ensures(Contract.Result<MemberInfo>() != null);

            return GetInScopeMethodInfoInternal(inScopeMethodCallExpression);
        }

        public static MethodInfo GetInScopeMethodInfo<TReturn>(Expression<Func<TReturn>> inScopeMethodCallExpression)
        {
            Contract.Requires<ArgumentNullException>(inScopeMethodCallExpression != null, "inScopeMethodCallExpression");
            Contract.Ensures(Contract.Result<MemberInfo>() != null);

            return GetInScopeMethodInfoInternal(inScopeMethodCallExpression);
        }

        public static MemberInfo GetMemberInfoFromMemberAccess(LambdaExpression expression)
        {
            Contract.Requires<ArgumentNullException>(expression != null, "expression");
            Contract.Ensures(Contract.Result<MemberInfo>() != null);

            return GetMemberInfoFromMemberAccessInternal(expression);
        }

        public static MethodInfo GetMethodInfoFromMethodCall(LambdaExpression expression)
        {
            Contract.Requires<ArgumentNullException>(expression != null, "expression");
            Contract.Ensures(Contract.Result<MethodInfo>() != null);

            return GetMethodInfoFromMethodCallInternal(expression);
        }

        #endregion

        #region Methods

        internal static MemberInfo GetInScopeMemberInfoInternal<TMember>(Expression<Func<TMember>> inScopeMemberExpression)
        {
            Contract.Requires(inScopeMemberExpression != null, "inScopeMemberExpression");
            Contract.Ensures(Contract.Result<MemberInfo>() != null);

            return GetMemberInfoFromMemberAccess(inScopeMemberExpression);
        }

        internal static MethodInfo GetInScopeMethodInfoInternal(Expression<Action> inScopeMethodCallExpression)
        {
            Contract.Requires(inScopeMethodCallExpression != null, "inScopeMethodCallExpression");
            Contract.Ensures(Contract.Result<MemberInfo>() != null);

            return GetMethodInfoFromMethodCall(inScopeMethodCallExpression);
        }

        internal static MethodInfo GetInScopeMethodInfoInternal<TReturn>(Expression<Func<TReturn>> inScopeMethodCallExpression)
        {
            Contract.Requires(inScopeMethodCallExpression != null, "inScopeMethodCallExpression");
            Contract.Ensures(Contract.Result<MemberInfo>() != null);

            return GetMethodInfoFromMethodCall(inScopeMethodCallExpression);
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
                            return (MethodInfo)methodInfoExpression.Value;
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