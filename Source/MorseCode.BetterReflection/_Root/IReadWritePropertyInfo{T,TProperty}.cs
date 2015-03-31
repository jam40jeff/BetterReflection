#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IReadWritePropertyInfo{T,TProperty}.cs" company="MorseCode Software">
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
    /// An interface representing property info for an instance property of type <typeparamref name="TProperty"/>
    /// on type <typeparamref name="T"/> whose value is readable and writable.
    /// </summary>
    /// <typeparam name="T">
    /// The type on which this instance method may be accessed.
    /// </typeparam>
    /// <typeparam name="TProperty">
    /// The type of the property.
    /// </typeparam>
    [ContractClass(typeof(ReadWritePropertyInfoInterfaceContract<,>))]
    public interface IReadWritePropertyInfo<in T, TProperty> : IReadWritePropertyInfo<T>, IReadablePropertyInfo<T, TProperty>, IWritablePropertyInfo<T, TProperty>
    {
    }
}