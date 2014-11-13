#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStaticReflectionHelper.cs" company="MorseCode Software">
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
    using System.Linq.Expressions;
    using System.Reflection;

    public interface IStaticReflectionHelper
    {
        #region Public Methods and Operators

        /// <summary>
        /// Gets the <see cref="MemberInfo"/> for a local variable, parameter, field, or property which is in scope.
        /// </summary>
        /// <param name="inScopeMemberExpression">
        /// The expression for the in-scope member, which should be of the format <c>() => [inScopeMember]</c> or <c>() => this.[fieldOrProperty]</c>.  <c>[inScopeMember]</c> may be a local variable, parameter, field, or property.
        /// </param>
        /// <typeparam name="TMember">
        /// The type of the in-scope member.
        /// </typeparam>
        /// <returns>
        /// The <see cref="MemberInfo"/> for the in-scope member.
        /// </returns>
        MemberInfo GetInScopeMemberInfo<TMember>(Expression<Func<TMember>> inScopeMemberExpression);

        /// <summary>
        /// Gets the <see cref="MethodInfo"/> for a method which is in scope.
        /// </summary>
        /// <param name="inScopeMethodCallExpression">
        /// The expression for the in-scope method call, which should be of the format <c>() => [method]()</c> or <c>() => this.[method]()</c>.  If <c>[method]</c> has parameters, the default value for each may be passed as they are only used to determine which overload to choose.
        /// </param>
        /// <returns>
        /// The <see cref="MethodInfo"/> for the in-scope method.
        /// </returns>
        MethodInfo GetInScopeMethodInfo(Expression<Action> inScopeMethodCallExpression);

        /// <summary>
        /// Gets the <see cref="MethodInfo"/> for a method which is in scope.
        /// </summary>
        /// <param name="inScopeMethodCallExpression">
        /// The expression for the in-scope method call, which should be of the format <c>() => [method]()</c> or <c>() => this.[method]()</c>.  If <c>[method]</c> has parameters, the default value for each may be passed as they are only used to determine which overload to choose.
        /// </param>
        /// <typeparam name="TReturn">
        /// The type of the return value of the function.
        /// </typeparam>
        /// <returns>
        /// The <see cref="MethodInfo"/> for the in-scope method.
        /// </returns>
        MethodInfo GetInScopeMethodInfo<TReturn>(Expression<Func<TReturn>> inScopeMethodCallExpression);

        MemberInfo GetMemberInfoFromMemberAccess(LambdaExpression expression);

        MethodInfo GetMethodInfoFromMethodCall(LambdaExpression expression);

        #endregion
    }
}