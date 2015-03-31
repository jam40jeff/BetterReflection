#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPropertyInfo.cs" company="MorseCode Software">
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
    using System.Reflection;

    /// <summary>
    /// An interface representing property info for an instance property.
    /// </summary>
    [ContractClass(typeof(PropertyInfoInterfaceContract))]
    public interface IPropertyInfo
    {
        #region Public Properties

        /// <summary>
        /// Gets the underlying Reflection API property info.
        /// </summary>
        PropertyInfo PropertyInfo { get; }

        /// <summary>
        /// Gets the type on which this instance property may be used.
        /// </summary>
        Type ObjectType { get; }

        /// <summary>
        /// Gets the type of this property.
        /// </summary>
        Type PropertyType { get; }

        /// <summary>
        /// Gets a value indicating whether the property is readable.
        /// </summary>
        bool IsReadable { get; }

        /// <summary>
        /// Gets a value indicating whether the property is writable.
        /// </summary>
        bool IsWritable { get; }

        #endregion
    }
}