#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITypeInfo.cs" company="MorseCode Software">
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
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// An interface representing type info for a type.
    /// </summary>
    [ContractClass(typeof(TypeInfoInterfaceContract))]
    public interface ITypeInfo
    {
        #region Public Properties

        /// <summary>
        /// Gets the type represented by this type info.
        /// </summary>
        Type Type { get; }

        /// <summary>
        /// Gets the name of the type represented by this type info.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the full name of the type represented by this type info.
        /// </summary>
        string FullName { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>the type represented by this type info
        /// Gets a property info instance for each public instance property accessible through the type represented by this type info.
        /// </summary>
        /// <returns>
        /// An enumerable of property info instances for each public instance property accessible through the type represented by this type info.
        /// </returns>
        IEnumerable<IPropertyInfo> GetProperties();

        /// <summary>
        /// Gets a property info instance for the public instance property with name <paramref name="name"/> accessible through the type represented by this type info.
        /// </summary>
        /// <param name="name">
        /// The name of the property for which to get property info.
        /// </param>
        /// <returns>
        /// A property info instance for the public instance property with name <paramref name="name"/> accessible through the type represented by this type info if
        /// it exists, <c>null</c> otherwise.
        /// </returns>
        IPropertyInfo GetProperty(string name);

        /// <summary>
        /// Gets a method info instance for each public instance method accessible through the type represented by this type info.
        /// </summary>
        /// <returns>
        /// An enumerable of method info instances for each public instance method accessible through the type represented by this type info.
        /// </returns>
        IEnumerable<IMethodInfo> GetMethods();

        /// <summary>
        /// Gets a method info instance for the public instance method with name <paramref name="name"/> accessible through the type represented by this type info.
        /// </summary>
        /// <param name="name">
        /// The name of the method for which to get method info.
        /// </param>
        /// <returns>
        /// A method info instance for the public instance method with name <paramref name="name"/> accessible through the type represented by this type info if
        /// it exists, <c>null</c> otherwise.
        /// </returns>
        IMethodInfo GetMethod(string name);

        /// <summary>
        /// Gets a method info instance for the public instance method with name <paramref name="name"/> and parameter types <paramref name="types"/> accessible through the type represented by this type info.
        /// </summary>
        /// <param name="name">
        /// The name of the method for which to get method info.
        /// </param>
        /// <param name="types">
        /// An array of <see cref="System.Type"/> objects representing the number, order, and type of the parameters for the method to get, or an empty array of <see cref="System.Type"/> objects
        /// (as provided by the <see cref="System.Type.EmptyTypes"/> field) to get a method that takes no parameters.
        /// </param>
        /// <returns>
        /// A method info instance for the public instance method with name <paramref name="name"/> and parameter types <paramref name="types"/> accessible through the type represented by this type info if
        /// it exists, <c>null</c> otherwise.
        /// </returns>
        IMethodInfo GetMethod(string name, Type[] types);

        #endregion
    }
}