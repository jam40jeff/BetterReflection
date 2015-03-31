#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IVoidMethodInfo{T,TParameter1,TParameter2,TParameter3,TParameter4,TParameter5,TParameter6,TParameter7}.cs" company="MorseCode Software">
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
    /// An interface representing method info for an instance method on type <typeparamref name="T"/> accepting parameters of types
    /// <typeparamref name="TParameter1" />, <typeparamref name="TParameter2" />, <typeparamref name="TParameter3" />, <typeparamref name="TParameter4" />, <typeparamref name="TParameter5" />, <typeparamref name="TParameter6" />, and <typeparamref name="TParameter7" />.
    /// </summary>
    /// <typeparam name="T">
    /// The type on which this instance method may be called.
    /// </typeparam>
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
    [ContractClass(typeof(VoidMethodInfoInterfaceContract<,,,,,,,>))]
    public interface IVoidMethodInfo<in T, in TParameter1, in TParameter2, in TParameter3, in TParameter4, in TParameter5, in TParameter6, in TParameter7> : IMethodInfo<T>
    {
        #region Public Methods and Operators

        /// <summary>
        /// Invokes the method.
        /// </summary>
        /// <param name="o">
        /// The object on which to call the method.
        /// </param>
        /// <param name="parameter1">
        /// The first parameter.
        /// </param>
        /// <param name="parameter2">
        /// The second parameter.
        /// </param>
        /// <param name="parameter3">
        /// The third parameter.
        /// </param>
        /// <param name="parameter4">
        /// The fourth parameter.
        /// </param>
        /// <param name="parameter5">
        /// The fifth parameter.
        /// </param>
        /// <param name="parameter6">
        /// The sixth parameter.
        /// </param>
        /// <param name="parameter7">
        /// The seventh parameter.
        /// </param>
        void Invoke(T o, TParameter1 parameter1, TParameter2 parameter2, TParameter3 parameter3, TParameter4 parameter4, TParameter5 parameter5, TParameter6 parameter6, TParameter7 parameter7);

        #endregion
    }
}