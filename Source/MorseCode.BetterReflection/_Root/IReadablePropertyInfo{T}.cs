#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IReadablePropertyInfo{T}.cs" company="MorseCode Software">
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
    /// An interface representing property info for an instance property on type <typeparamref name="T"/>
    /// whose value is readable.
    /// </summary>
    /// <typeparam name="T">
    /// The type on which this instance method may be accessed.
    /// </typeparam>
    [ContractClass(typeof(ReadablePropertyInfoInterfaceContract<>))]
    public interface IReadablePropertyInfo<in T> : IReadablePropertyInfo, IPropertyInfo<T>
    {
        #region Public Methods and Operators

        /// <summary>
        /// Gets the property value with a typed object instance on which to get the property but not a typed property value.
        /// </summary>
        /// <param name="o">
        /// The object on which to get the property.
        /// </param>
        /// <returns>
        /// The value of the property.
        /// </returns>
        object GetValue(T o);

        #endregion
    }
}