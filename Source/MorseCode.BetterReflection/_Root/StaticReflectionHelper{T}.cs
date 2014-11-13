#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StaticReflectionHelper{T}.cs" company="MorseCode Software">
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
    using System.Linq.Expressions;
    using System.Reflection;

    public class StaticReflectionHelper<T> : IStaticReflectionHelper<T>
    {
        #region Fields

        private readonly IStaticReflectionHelper staticReflectionHelper;

        #endregion

        #region Constructors and Destructors

        public StaticReflectionHelper(IStaticReflectionHelper staticReflectionHelper)
        {
            this.staticReflectionHelper = staticReflectionHelper;
        }

        #endregion

        #region Explicit Interface Methods

        MemberInfo IStaticReflectionHelper<T>.GetMemberInfo<TMember>(Expression<Func<T, TMember>> memberExpression)
        {
            Contract.Requires<ArgumentNullException>(memberExpression != null, "memberExpression");
            Contract.Ensures(Contract.Result<MemberInfo>() != null);

            return this.staticReflectionHelper.GetMemberInfoFromMemberAccess(memberExpression);
        }

        MethodInfo IStaticReflectionHelper<T>.GetMethodInfo(Expression<Action<T>> methodCallExpression)
        {
            Contract.Requires<ArgumentNullException>(methodCallExpression != null, "methodCallExpression");
            Contract.Ensures(Contract.Result<MemberInfo>() != null);

            return this.staticReflectionHelper.GetMethodInfoFromMethodCall(methodCallExpression);
        }

        MethodInfo IStaticReflectionHelper<T>.GetMethodInfo<TReturn>(Expression<Func<T, TReturn>> methodCallExpression)
        {
            Contract.Requires<ArgumentNullException>(methodCallExpression != null, "methodCallExpression");
            Contract.Ensures(Contract.Result<MemberInfo>() != null);

            return this.staticReflectionHelper.GetMethodInfoFromMethodCall(methodCallExpression);
        }

        #endregion
    }
}