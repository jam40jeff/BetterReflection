#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IVoidMethodInfo{T}.cs" company="MorseCode Software">
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
    using System.Diagnostics.Contracts;

    /// <summary>
    /// An interface representing method info for an instance method on type <typeparamref name="T"/> with no parameters.
    /// </summary>
    /// <typeparam name="T">
    /// The type on which this instance method may be called.
    /// </typeparam>
    [ContractClass(typeof(VoidMethodInfoInterfaceContract<>))]
    public interface IVoidMethodInfo<in T> : IMethodInfo<T>
    {
        #region Public Methods and Operators

        /// <summary>
        /// Invokes the method.
        /// </summary>
        /// <param name="o">
        /// The object on which to call the method.
        /// </param>
        void Invoke(T o);

        #endregion
    }
}