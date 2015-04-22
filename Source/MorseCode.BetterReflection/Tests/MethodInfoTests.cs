#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MethodInfoTests.cs" company="MorseCode Software">
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

namespace MorseCode.BetterReflection.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Formatters.Binary;

    using NUnit.Framework;

    [TestFixture]
    public class MethodInfoTests
    {
        [Test]
        public void ReflectionWrapperMethodInfoPartiallyUntypedMethodInfo()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestReflectionWrapperMethod");

            Assert.AreEqual(typeof(Test).GetMethod("TestReflectionWrapperMethod"), method.MethodInfo);
        }

        [Test]
        public void ReflectionWrapperMethodInfoPartiallyUntypedName()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestReflectionWrapperMethod");

            Assert.AreEqual(typeof(Test).GetMethod("TestReflectionWrapperMethod").Name, method.Name);
        }

        [Test]
        public void ReflectionWrapperMethodInfoPartiallyUntypedObjectType()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestReflectionWrapperMethod");

            Assert.AreEqual(typeof(Test), method.ObjectType);
        }

        [Test]
        public void ReflectionWrapperMethodInfoPartiallyUntypedParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestReflectionWrapperMethod");

            Assert.IsTrue(new[] { typeof(int), typeof(int) }.SequenceEqual(method.ParameterTypes.Where(p => p.IsByRef).Select(p => p.GetElementType())));
        }

        [Test]
        public void ReflectionWrapperMethodInfoPartiallyUntypedReturnType()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestReflectionWrapperMethod");

            Assert.AreEqual(typeof(string), method.ReturnType);
        }

        [Test]
        public void ReflectionWrapperMethodInfoPartiallyUntypedInvokePartiallyUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestReflectionWrapperMethod");

            object[] arguments = new object[] { 1, 2 };
            Assert.AreEqual("5", method.InvokePartiallyUntyped(new Test(), arguments));
			Assert.AreEqual(2, arguments[0]);
			Assert.AreEqual(3, arguments[1]);
        }

        [Test]
        public void ReflectionWrapperMethodInfoPartiallyUntypedInvokePartiallyUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestReflectionWrapperMethod");

            ArgumentNullException actual = null;
            try
            {
                method.InvokePartiallyUntyped(null, new object[] { 1, 2 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void ReflectionWrapperMethodInfoPartiallyUntypedInvokePartiallyUntypedEnumerable()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestReflectionWrapperMethod");

            object[] arguments = new object[] { 1, 2 };
            Assert.AreEqual("5", method.InvokePartiallyUntyped(new Test(), (IEnumerable<object>)arguments));
			Assert.AreEqual(2, arguments[0]);
			Assert.AreEqual(3, arguments[1]);
        }

        [Test]
        public void ReflectionWrapperMethodInfoPartiallyUntypedInvokePartiallyUntypedEnumerableWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestReflectionWrapperMethod");

            ArgumentNullException actual = null;
            try
            {
                method.InvokePartiallyUntyped(null, (IEnumerable<object>)new object[] { 1, 2 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void ReflectionWrapperMethodInfoPartiallyUntypedInvokePartiallyUntypedNoParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestReflectionWrapperMethod");

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[0]);
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received no parameters, but expected parameters of type { System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void ReflectionWrapperMethodInfoPartiallyUntypedInvokePartiallyUntypedWrongNumberOfParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestReflectionWrapperMethod");

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2, 3 });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32 }, but expected parameters of type { System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void ReflectionWrapperMethodInfoPartiallyUntypedInvokePartiallyUntypedMismatchedParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestReflectionWrapperMethod");

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { 1, "2" });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.String }, but expected parameters of type { System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void ReflectionWrapperMethodInfoPartiallyUntypedInvokeFullyUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestReflectionWrapperMethod");

            object[] arguments = new object[] { 1, 2 };
            Assert.AreEqual("5", method.InvokeFullyUntyped(new Test(), arguments));
			Assert.AreEqual(2, arguments[0]);
			Assert.AreEqual(3, arguments[1]);
        }

        [Test]
        public void ReflectionWrapperMethodInfoPartiallyUntypedInvokeFullyUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestReflectionWrapperMethod");

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, new object[] { 1, 2 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void ReflectionWrapperMethodInfoPartiallyUntypedInvokeFullyUntypedEnumerable()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestReflectionWrapperMethod");

            object[] arguments = new object[] { 1, 2 };
            Assert.AreEqual("5", method.InvokeFullyUntyped(new Test(), (IEnumerable<object>)arguments));
			Assert.AreEqual(2, arguments[0]);
			Assert.AreEqual(3, arguments[1]);
        }

        [Test]
        public void ReflectionWrapperMethodInfoPartiallyUntypedInvokeFullyUntypedEnumerableWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestReflectionWrapperMethod");

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, (IEnumerable<object>)new object[] { 1, 2 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void ReflectionWrapperMethodInfoPartiallyUntypedInvokeFullyUntypedNoParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestReflectionWrapperMethod");

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[0]);
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received no parameters, but expected parameters of type { System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void ReflectionWrapperMethodInfoPartiallyUntypedInvokeFullyUntypedWrongNumberOfParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestReflectionWrapperMethod");

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, 3 });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32 }, but expected parameters of type { System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void ReflectionWrapperMethodInfoPartiallyUntypedInvokeFullyUntypedMismatchedParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestReflectionWrapperMethod");

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, "2" });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.String }, but expected parameters of type { System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void ReflectionWrapperMethodInfoFullyUntypedMethodInfo()
        {
            ITypeInfo typeInfo = TypeInfoFactory.GetTypeInfo(typeof(Test));
            IMethodInfo method = typeInfo.GetMethod("TestReflectionWrapperMethod");

            Assert.AreEqual(typeof(Test).GetMethod("TestReflectionWrapperMethod"), method.MethodInfo);
        }

        [Test]
        public void ReflectionWrapperMethodInfoFullyUntypedName()
        {
            ITypeInfo typeInfo = TypeInfoFactory.GetTypeInfo(typeof(Test));
            IMethodInfo method = typeInfo.GetMethod("TestReflectionWrapperMethod");

            Assert.AreEqual(typeof(Test).GetMethod("TestReflectionWrapperMethod").Name, method.Name);
        }

        [Test]
        public void ReflectionWrapperMethodInfoFullyUntypedObjectType()
        {
            ITypeInfo typeInfo = TypeInfoFactory.GetTypeInfo(typeof(Test));
            IMethodInfo method = typeInfo.GetMethod("TestReflectionWrapperMethod");

            Assert.AreEqual(typeof(Test), method.ObjectType);
        }

        [Test]
        public void ReflectionWrapperMethodInfoFullyUntypedParameterTypes()
        {
            ITypeInfo typeInfo = TypeInfoFactory.GetTypeInfo(typeof(Test));
            IMethodInfo method = typeInfo.GetMethod("TestReflectionWrapperMethod");

            Assert.IsTrue(new[] { typeof(int), typeof(int) }.SequenceEqual(method.ParameterTypes.Where(p => p.IsByRef).Select(p => p.GetElementType())));
        }

        [Test]
        public void ReflectionWrapperMethodInfoFullyUntypedReturnType()
        {
            ITypeInfo typeInfo = TypeInfoFactory.GetTypeInfo(typeof(Test));
            IMethodInfo method = typeInfo.GetMethod("TestReflectionWrapperMethod");

            Assert.AreEqual(typeof(string), method.ReturnType);
        }

        [Test]
        public void ReflectionWrapperMethodInfoFullyUntypedInvokeFullyUntyped()
        {
            ITypeInfo typeInfo = TypeInfoFactory.GetTypeInfo(typeof(Test));
            IMethodInfo method = typeInfo.GetMethod("TestReflectionWrapperMethod");

            object[] arguments = new object[] { 1, 2 };
            Assert.AreEqual("5", method.InvokeFullyUntyped(new Test(), arguments));
			Assert.AreEqual(2, arguments[0]);
			Assert.AreEqual(3, arguments[1]);
        }

        [Test]
        public void ReflectionWrapperMethodInfoFullyUntypedInvokeFullyUntypedWithNull()
        {
            ITypeInfo typeInfo = TypeInfoFactory.GetTypeInfo(typeof(Test));
            IMethodInfo method = typeInfo.GetMethod("TestReflectionWrapperMethod");

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, new object[] { 1, 2 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void ReflectionWrapperMethodInfoFullyUntypedInvokeFullyUntypedEnumerable()
        {
            ITypeInfo typeInfo = TypeInfoFactory.GetTypeInfo(typeof(Test));
            IMethodInfo method = typeInfo.GetMethod("TestReflectionWrapperMethod");

            object[] arguments = new object[] { 1, 2 };
            Assert.AreEqual("5", method.InvokeFullyUntyped(new Test(), (IEnumerable<object>)arguments));
			Assert.AreEqual(2, arguments[0]);
			Assert.AreEqual(3, arguments[1]);
        }

        [Test]
        public void ReflectionWrapperMethodInfoFullyUntypedInvokeFullyUntypedEnumerableWithNull()
        {
            ITypeInfo typeInfo = TypeInfoFactory.GetTypeInfo(typeof(Test));
            IMethodInfo method = typeInfo.GetMethod("TestReflectionWrapperMethod");

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, (IEnumerable<object>)new object[] { 1, 2 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void ReflectionWrapperMethodInfoFullyUntypedInvokeFullyUntypedNoParameters()
        {
            ITypeInfo typeInfo = TypeInfoFactory.GetTypeInfo(typeof(Test));
            IMethodInfo method = typeInfo.GetMethod("TestReflectionWrapperMethod");

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[0]);
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received no parameters, but expected parameters of type { System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void ReflectionWrapperMethodInfoFullyUntypedInvokeFullyUntypedWrongNumberOfParameters()
        {
            ITypeInfo typeInfo = TypeInfoFactory.GetTypeInfo(typeof(Test));
            IMethodInfo method = typeInfo.GetMethod("TestReflectionWrapperMethod");

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, 3 });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32 }, but expected parameters of type { System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void ReflectionWrapperMethodInfoFullyUntypedInvokeFullyUntypedMismatchedParameterTypes()
        {
            ITypeInfo typeInfo = TypeInfoFactory.GetTypeInfo(typeof(Test));
            IMethodInfo method = typeInfo.GetMethod("TestReflectionWrapperMethod");

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, "2" });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.String }, but expected parameters of type { System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoPartiallyUntypedMethodInfo()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestMethodUnique");

            Assert.AreEqual(typeof(Test).GetMethod("TestMethodUnique"), method.MethodInfo);
        }

        [Test]
        public void MethodInfoPartiallyUntypedName()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestMethodUnique");

            Assert.AreEqual(typeof(Test).GetMethod("TestMethodUnique").Name, method.Name);
        }

        [Test]
        public void MethodInfoPartiallyUntypedObjectType()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestMethodUnique");

            Assert.AreEqual(typeof(Test), method.ObjectType);
        }

        [Test]
        public void MethodInfoPartiallyUntypedParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestMethodUnique");

            Assert.IsTrue(new[] { typeof(int), typeof(int) }.SequenceEqual(method.ParameterTypes));
        }

        [Test]
        public void MethodInfoPartiallyUntypedReturnType()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestMethodUnique");

            Assert.AreEqual(typeof(string), method.ReturnType);
        }

        [Test]
        public void MethodInfoPartiallyUntypedInvokePartiallyUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestMethodUnique");

            Assert.AreEqual("3", method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2 }));
        }

        [Test]
        public void MethodInfoPartiallyUntypedInvokePartiallyUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestMethodUnique");

            ArgumentNullException actual = null;
            try
            {
                method.InvokePartiallyUntyped(null, new object[] { 1, 2 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoPartiallyUntypedInvokePartiallyUntypedEnumerable()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestMethodUnique");

            Assert.AreEqual("3", method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2 }));
        }

        [Test]
        public void MethodInfoPartiallyUntypedInvokePartiallyUntypedEnumerableWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestMethodUnique");

            ArgumentNullException actual = null;
            try
            {
                method.InvokePartiallyUntyped(null, (IEnumerable<object>)new object[] { 1, 2 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoPartiallyUntypedInvokePartiallyUntypedNoParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestMethodUnique");

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[0]);
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received no parameters, but expected parameters of type { System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoPartiallyUntypedInvokePartiallyUntypedWrongNumberOfParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestMethodUnique");

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2, 3 });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32 }, but expected parameters of type { System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoPartiallyUntypedInvokePartiallyUntypedMismatchedParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestMethodUnique");

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { 1, "2" });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.String }, but expected parameters of type { System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoPartiallyUntypedInvokeFullyUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestMethodUnique");

            Assert.AreEqual("3", method.InvokeFullyUntyped(new Test(), new object[] { 1, 2 }));
        }

        [Test]
        public void MethodInfoPartiallyUntypedInvokeFullyUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestMethodUnique");

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, new object[] { 1, 2 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoPartiallyUntypedInvokeFullyUntypedEnumerable()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestMethodUnique");

            Assert.AreEqual("3", method.InvokeFullyUntyped(new Test(), (IEnumerable<object>)new object[] { 1, 2 }));
        }

        [Test]
        public void MethodInfoPartiallyUntypedInvokeFullyUntypedEnumerableWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestMethodUnique");

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, (IEnumerable<object>)new object[] { 1, 2 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoPartiallyUntypedInvokeFullyUntypedNoParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestMethodUnique");

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[0]);
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received no parameters, but expected parameters of type { System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoPartiallyUntypedInvokeFullyUntypedWrongNumberOfParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestMethodUnique");

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, 3 });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32 }, but expected parameters of type { System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoPartiallyUntypedInvokeFullyUntypedMismatchedParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestMethodUnique");

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, "2" });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.String }, but expected parameters of type { System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoFullyUntypedMethodInfo()
        {
            ITypeInfo typeInfo = TypeInfoFactory.GetTypeInfo(typeof(Test));
            IMethodInfo method = typeInfo.GetMethod("TestMethodUnique");

            Assert.AreEqual(typeof(Test).GetMethod("TestMethodUnique"), method.MethodInfo);
        }

        [Test]
        public void MethodInfoFullyUntypedName()
        {
            ITypeInfo typeInfo = TypeInfoFactory.GetTypeInfo(typeof(Test));
            IMethodInfo method = typeInfo.GetMethod("TestMethodUnique");

            Assert.AreEqual(typeof(Test).GetMethod("TestMethodUnique").Name, method.Name);
        }

        [Test]
        public void MethodInfoFullyUntypedObjectType()
        {
            ITypeInfo typeInfo = TypeInfoFactory.GetTypeInfo(typeof(Test));
            IMethodInfo method = typeInfo.GetMethod("TestMethodUnique");

            Assert.AreEqual(typeof(Test), method.ObjectType);
        }

        [Test]
        public void MethodInfoFullyUntypedParameterTypes()
        {
            ITypeInfo typeInfo = TypeInfoFactory.GetTypeInfo(typeof(Test));
            IMethodInfo method = typeInfo.GetMethod("TestMethodUnique");

            Assert.IsTrue(new[] { typeof(int), typeof(int) }.SequenceEqual(method.ParameterTypes));
        }

        [Test]
        public void MethodInfoFullyUntypedReturnType()
        {
            ITypeInfo typeInfo = TypeInfoFactory.GetTypeInfo(typeof(Test));
            IMethodInfo method = typeInfo.GetMethod("TestMethodUnique");

            Assert.AreEqual(typeof(string), method.ReturnType);
        }

        [Test]
        public void MethodInfoFullyUntypedInvokeFullyUntyped()
        {
            ITypeInfo typeInfo = TypeInfoFactory.GetTypeInfo(typeof(Test));
            IMethodInfo method = typeInfo.GetMethod("TestMethodUnique");

            Assert.AreEqual("3", method.InvokeFullyUntyped(new Test(), new object[] { 1, 2 }));
        }

        [Test]
        public void MethodInfoFullyUntypedInvokeFullyUntypedWithNull()
        {
            ITypeInfo typeInfo = TypeInfoFactory.GetTypeInfo(typeof(Test));
            IMethodInfo method = typeInfo.GetMethod("TestMethodUnique");

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, new object[] { 1, 2 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoFullyUntypedInvokeFullyUntypedEnumerable()
        {
            ITypeInfo typeInfo = TypeInfoFactory.GetTypeInfo(typeof(Test));
            IMethodInfo method = typeInfo.GetMethod("TestMethodUnique");

            Assert.AreEqual("3", method.InvokeFullyUntyped(new Test(), (IEnumerable<object>)new object[] { 1, 2 }));
        }

        [Test]
        public void MethodInfoFullyUntypedInvokeFullyUntypedEnumerableWithNull()
        {
            ITypeInfo typeInfo = TypeInfoFactory.GetTypeInfo(typeof(Test));
            IMethodInfo method = typeInfo.GetMethod("TestMethodUnique");

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, (IEnumerable<object>)new object[] { 1, 2 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoFullyUntypedInvokeFullyUntypedNoParameters()
        {
            ITypeInfo typeInfo = TypeInfoFactory.GetTypeInfo(typeof(Test));
            IMethodInfo method = typeInfo.GetMethod("TestMethodUnique");

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[0]);
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received no parameters, but expected parameters of type { System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoFullyUntypedInvokeFullyUntypedWrongNumberOfParameters()
        {
            ITypeInfo typeInfo = TypeInfoFactory.GetTypeInfo(typeof(Test));
            IMethodInfo method = typeInfo.GetMethod("TestMethodUnique");

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, 3 });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32 }, but expected parameters of type { System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoFullyUntypedInvokeFullyUntypedMismatchedParameterTypes()
        {
            ITypeInfo typeInfo = TypeInfoFactory.GetTypeInfo(typeof(Test));
            IMethodInfo method = typeInfo.GetMethod("TestMethodUnique");

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, "2" });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.String }, but expected parameters of type { System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoInvokeExpectsNoParametersWrongNumberOfParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestMethodUniqueNoParameters");

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1 });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32 }, but expected no parameters." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoPartiallyUntypedMethodInfo()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestVoidMethodUnique");

            Assert.AreEqual(typeof(Test).GetMethod("TestVoidMethodUnique"), method.MethodInfo);
        }

        [Test]
        public void VoidMethodInfoPartiallyUntypedName()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestVoidMethodUnique");

            Assert.AreEqual(typeof(Test).GetMethod("TestVoidMethodUnique").Name, method.Name);
        }

        [Test]
        public void VoidMethodInfoPartiallyUntypedObjectType()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestVoidMethodUnique");

            Assert.AreEqual(typeof(Test), method.ObjectType);
        }

        [Test]
        public void VoidMethodInfoPartiallyUntypedParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestVoidMethodUnique");

            Assert.IsTrue(new[] { typeof(int), typeof(int), typeof(int) }.SequenceEqual(method.ParameterTypes));
        }

        [Test]
        public void VoidMethodInfoPartiallyUntypedReturnType()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestVoidMethodUnique");

            Assert.AreEqual(typeof(void), method.ReturnType);
        }

        [Test]
        public void VoidMethodInfoPartiallyUntypedInvokePartiallyUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestVoidMethodUnique");

            Exception actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2, 3 });
            }
            catch (Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2, 3", actual.Message);
        }

        [Test]
        public void VoidMethodInfoPartiallyUntypedInvokePartiallyUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestVoidMethodUnique");

            ArgumentNullException actual = null;
            try
            {
                method.InvokePartiallyUntyped(null, new object[] { 1, 2, 3 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoPartiallyUntypedInvokePartiallyUntypedEnumerable()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestVoidMethodUnique");

            Exception actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), (IEnumerable<object>)new object[] { 1, 2, 3 });
            }
            catch (Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2, 3", actual.Message);
        }

        [Test]
        public void VoidMethodInfoPartiallyUntypedInvokePartiallyUntypedEnumerableWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestVoidMethodUnique");

            ArgumentNullException actual = null;
            try
            {
                method.InvokePartiallyUntyped(null, (IEnumerable<object>)new object[] { 1, 2, 3 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoPartiallyUntypedInvokePartiallyUntypedNoParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestVoidMethodUnique");

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[0]);
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received no parameters, but expected parameters of type { System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoPartiallyUntypedInvokePartiallyUntypedWrongNumberOfParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestVoidMethodUnique");

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2, 3, 4 });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32, System.Int32 }, but expected parameters of type { System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoPartiallyUntypedInvokePartiallyUntypedMismatchedParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestVoidMethodUnique");

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2, "3" });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.String }, but expected parameters of type { System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoPartiallyUntypedInvokeFullyUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestVoidMethodUnique");

            Exception actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, 3 });
            }
            catch (Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2, 3", actual.Message);
        }

        [Test]
        public void VoidMethodInfoPartiallyUntypedInvokeFullyUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestVoidMethodUnique");

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, new object[] { 1, 2, 3 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoPartiallyUntypedInvokeFullyUntypedEnumerable()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestVoidMethodUnique");

            Exception actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), (IEnumerable<object>)new object[] { 1, 2, 3 });
            }
            catch (Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2, 3", actual.Message);
        }

        [Test]
        public void VoidMethodInfoPartiallyUntypedInvokeFullyUntypedEnumerableWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestVoidMethodUnique");

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, (IEnumerable<object>)new object[] { 1, 2, 3 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoPartiallyUntypedInvokeFullyUntypedNoParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestVoidMethodUnique");

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[0]);
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received no parameters, but expected parameters of type { System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoPartiallyUntypedInvokeFullyUntypedWrongNumberOfParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestVoidMethodUnique");

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, 3, 4 });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32, System.Int32 }, but expected parameters of type { System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoPartiallyUntypedInvokeFullyUntypedMismatchedParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestVoidMethodUnique");

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, "3" });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.String }, but expected parameters of type { System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoFullyUntypedMethodInfo()
        {
            ITypeInfo typeInfo = TypeInfoFactory.GetTypeInfo(typeof(Test));
            IMethodInfo method = typeInfo.GetMethod("TestVoidMethodUnique");

            Assert.AreEqual(typeof(Test).GetMethod("TestVoidMethodUnique"), method.MethodInfo);
        }

        [Test]
        public void VoidMethodInfoFullyUntypedName()
        {
            ITypeInfo typeInfo = TypeInfoFactory.GetTypeInfo(typeof(Test));
            IMethodInfo method = typeInfo.GetMethod("TestVoidMethodUnique");

            Assert.AreEqual(typeof(Test).GetMethod("TestVoidMethodUnique").Name, method.Name);
        }

        [Test]
        public void VoidMethodInfoFullyUntypedObjectType()
        {
            ITypeInfo typeInfo = TypeInfoFactory.GetTypeInfo(typeof(Test));
            IMethodInfo method = typeInfo.GetMethod("TestVoidMethodUnique");

            Assert.AreEqual(typeof(Test), method.ObjectType);
        }

        [Test]
        public void VoidMethodInfoFullyUntypedParameterTypes()
        {
            ITypeInfo typeInfo = TypeInfoFactory.GetTypeInfo(typeof(Test));
            IMethodInfo method = typeInfo.GetMethod("TestVoidMethodUnique");

            Assert.IsTrue(new[] { typeof(int), typeof(int), typeof(int) }.SequenceEqual(method.ParameterTypes));
        }

        [Test]
        public void VoidMethodInfoFullyUntypedReturnType()
        {
            ITypeInfo typeInfo = TypeInfoFactory.GetTypeInfo(typeof(Test));
            IMethodInfo method = typeInfo.GetMethod("TestVoidMethodUnique");

            Assert.AreEqual(typeof(void), method.ReturnType);
        }

        [Test]
        public void VoidMethodInfoFullyUntypedInvokeFullyUntyped()
        {
            ITypeInfo typeInfo = TypeInfoFactory.GetTypeInfo(typeof(Test));
            IMethodInfo method = typeInfo.GetMethod("TestVoidMethodUnique");

            Exception actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, 3 });
            }
            catch (Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2, 3", actual.Message);
        }

        [Test]
        public void VoidMethodInfoFullyUntypedInvokeFullyUntypedWithNull()
        {
            ITypeInfo typeInfo = TypeInfoFactory.GetTypeInfo(typeof(Test));
            IMethodInfo method = typeInfo.GetMethod("TestVoidMethodUnique");

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, new object[] { 1, 2, 3 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoFullyUntypedInvokeFullyUntypedEnumerable()
        {
            ITypeInfo typeInfo = TypeInfoFactory.GetTypeInfo(typeof(Test));
            IMethodInfo method = typeInfo.GetMethod("TestVoidMethodUnique");

            Exception actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), (IEnumerable<object>)new object[] { 1, 2, 3 });
            }
            catch (Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2, 3", actual.Message);
        }

        [Test]
        public void VoidMethodInfoFullyUntypedInvokeFullyUntypedEnumerableWithNull()
        {
            ITypeInfo typeInfo = TypeInfoFactory.GetTypeInfo(typeof(Test));
            IMethodInfo method = typeInfo.GetMethod("TestVoidMethodUnique");

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, (IEnumerable<object>)new object[] { 1, 2, 3 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoFullyUntypedInvokeFullyUntypedNoParameters()
        {
            ITypeInfo typeInfo = TypeInfoFactory.GetTypeInfo(typeof(Test));
            IMethodInfo method = typeInfo.GetMethod("TestVoidMethodUnique");

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[0]);
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received no parameters, but expected parameters of type { System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoFullyUntypedInvokeFullyUntypedWrongNumberOfParameters()
        {
            ITypeInfo typeInfo = TypeInfoFactory.GetTypeInfo(typeof(Test));
            IMethodInfo method = typeInfo.GetMethod("TestVoidMethodUnique");

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, 3, 4 });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32, System.Int32 }, but expected parameters of type { System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoFullyUntypedInvokeFullyUntypedMismatchedParameterTypes()
        {
            ITypeInfo typeInfo = TypeInfoFactory.GetTypeInfo(typeof(Test));
            IMethodInfo method = typeInfo.GetMethod("TestVoidMethodUnique");

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, "3" });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.String }, but expected parameters of type { System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoInvokeExpectsNoParametersWrongNumberOfParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestVoidMethodUniqueNoParameters");

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1 });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32 }, but expected no parameters." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithNoParametersMethodInfo()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, string> method = typeInfo.GetMethod(o => (Func<string>)o.TestMethod);

            Assert.AreEqual(typeof(Test).GetMethod("TestMethod", Type.EmptyTypes), method.MethodInfo);
        }

        [Test]
        public void MethodInfoWithNoParametersName()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, string> method = typeInfo.GetMethod(o => (Func<string>)o.TestMethod);

            Assert.AreEqual(typeof(Test).GetMethod("TestMethod", Type.EmptyTypes).Name, method.Name);
        }

        [Test]
        public void MethodInfoWithNoParametersObjectType()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, string> method = typeInfo.GetMethod(o => (Func<string>)o.TestMethod);

            Assert.AreEqual(typeof(Test), method.ObjectType);
        }

        [Test]
        public void MethodInfoWithNoParametersParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, string> method = typeInfo.GetMethod(o => (Func<string>)o.TestMethod);

            Assert.IsTrue(Type.EmptyTypes .SequenceEqual(method.ParameterTypes));
        }

        [Test]
        public void MethodInfoWithNoParametersReturnType()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, string> method = typeInfo.GetMethod(o => (Func<string>)o.TestMethod);

            Assert.AreEqual(typeof(string), method.ReturnType);
        }

        [Test]
        public void MethodInfoWithNoParametersInvoke()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, string> method = typeInfo.GetMethod(o => (Func<string>)o.TestMethod);

            Assert.AreEqual("0", method.Invoke(new Test()));
        }

        [Test]
        public void MethodInfoWithNoParametersInvokeWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, string> method = typeInfo.GetMethod(o => (Func<string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.Invoke(null);
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoWithNoParametersInvokePartiallyUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, string> method = typeInfo.GetMethod(o => (Func<string>)o.TestMethod);

            Assert.AreEqual("0", method.InvokePartiallyUntyped(new Test(), new object[0]));
        }

        [Test]
        public void MethodInfoWithNoParametersInvokePartiallyUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, string> method = typeInfo.GetMethod(o => (Func<string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokePartiallyUntyped(null, new object[0]);
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoWithNoParametersInvokePartiallyUntypedEnumerable()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, string> method = typeInfo.GetMethod(o => (Func<string>)o.TestMethod);

            Assert.AreEqual("0", method.InvokePartiallyUntyped(new Test(), (IEnumerable<object>)new object[0]));
        }

        [Test]
        public void MethodInfoWithNoParametersInvokePartiallyUntypedEnumerableWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, string> method = typeInfo.GetMethod(o => (Func<string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokePartiallyUntyped(null, (IEnumerable<object>)new object[0]);
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoWithNoParametersInvokePartiallyUntypedWrongNumberOfParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, string> method = typeInfo.GetMethod(o => (Func<string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { 1 });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32 }, but expected no parameters." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithNoParametersInvokeFullyUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, string> method = typeInfo.GetMethod(o => (Func<string>)o.TestMethod);

            Assert.AreEqual("0", method.InvokeFullyUntyped(new Test(), new object[0]));
        }

        [Test]
        public void MethodInfoWithNoParametersInvokeFullyUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, string> method = typeInfo.GetMethod(o => (Func<string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, new object[0]);
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoWithNoParametersInvokeFullyUntypedEnumerable()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, string> method = typeInfo.GetMethod(o => (Func<string>)o.TestMethod);

            Assert.AreEqual("0", method.InvokeFullyUntyped(new Test(), (IEnumerable<object>)new object[0]));
        }

        [Test]
        public void MethodInfoWithNoParametersInvokeFullyUntypedEnumerableWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, string> method = typeInfo.GetMethod(o => (Func<string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, (IEnumerable<object>)new object[0]);
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoWithNoParametersInvokeFullyUntypedWrongNumberOfParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, string> method = typeInfo.GetMethod(o => (Func<string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1 });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32 }, but expected no parameters." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithNoParametersSerialization()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, string> method = typeInfo.GetMethod(o => (Func<string>)o.TestMethod);
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();
            formatter.Serialize(memoryStream, method);
            memoryStream.Position = 0;
            IMethodInfo<Test, string> result = (IMethodInfo<Test, string>)formatter.Deserialize(memoryStream);

            Assert.AreEqual(method.MethodInfo, result.MethodInfo);
            Assert.AreEqual(method.Name, result.Name);
            Assert.AreEqual(method.ObjectType, result.ObjectType);
            Assert.IsTrue(method.ParameterTypes.SequenceEqual(result.ParameterTypes));
            Assert.AreEqual(method.ReturnType, result.ReturnType);
        }

        [Test]
        public void MethodInfoWithOneParameterMethodInfo()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, string> method = typeInfo.GetMethod(o => (Func<int, string>)o.TestMethod);

            Assert.AreEqual(typeof(Test).GetMethod("TestMethod", new[] { typeof(int) }), method.MethodInfo);
        }

        [Test]
        public void MethodInfoWithOneParameterName()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, string> method = typeInfo.GetMethod(o => (Func<int, string>)o.TestMethod);

            Assert.AreEqual(typeof(Test).GetMethod("TestMethod", new[] { typeof(int) }).Name, method.Name);
        }

        [Test]
        public void MethodInfoWithOneParameterObjectType()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, string> method = typeInfo.GetMethod(o => (Func<int, string>)o.TestMethod);

            Assert.AreEqual(typeof(Test), method.ObjectType);
        }

        [Test]
        public void MethodInfoWithOneParameterParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, string> method = typeInfo.GetMethod(o => (Func<int, string>)o.TestMethod);

            Assert.IsTrue(new[] { typeof(int) }.SequenceEqual(method.ParameterTypes));
        }

        [Test]
        public void MethodInfoWithOneParameterReturnType()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, string> method = typeInfo.GetMethod(o => (Func<int, string>)o.TestMethod);

            Assert.AreEqual(typeof(string), method.ReturnType);
        }

        [Test]
        public void MethodInfoWithOneParameterInvoke()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, string> method = typeInfo.GetMethod(o => (Func<int, string>)o.TestMethod);

            Assert.AreEqual("1", method.Invoke(new Test(), 1));
        }

        [Test]
        public void MethodInfoWithOneParameterInvokeWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, string> method = typeInfo.GetMethod(o => (Func<int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.Invoke(null, 1);
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoWithOneParameterInvokePartiallyUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, string> method = typeInfo.GetMethod(o => (Func<int, string>)o.TestMethod);

            Assert.AreEqual("1", method.InvokePartiallyUntyped(new Test(), new object[] { 1 }));
        }

        [Test]
        public void MethodInfoWithOneParameterInvokePartiallyUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, string> method = typeInfo.GetMethod(o => (Func<int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokePartiallyUntyped(null, new object[] { 1 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoWithOneParameterInvokePartiallyUntypedEnumerable()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, string> method = typeInfo.GetMethod(o => (Func<int, string>)o.TestMethod);

            Assert.AreEqual("1", method.InvokePartiallyUntyped(new Test(), (IEnumerable<object>)new object[] { 1 }));
        }

        [Test]
        public void MethodInfoWithOneParameterInvokePartiallyUntypedEnumerableWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, string> method = typeInfo.GetMethod(o => (Func<int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokePartiallyUntyped(null, (IEnumerable<object>)new object[] { 1 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoWithOneParameterInvokePartiallyUntypedNoParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, string> method = typeInfo.GetMethod(o => (Func<int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[0]);
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received no parameters, but expected parameters of type { System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithOneParameterInvokePartiallyUntypedWrongNumberOfParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, string> method = typeInfo.GetMethod(o => (Func<int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2 });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32 }, but expected parameters of type { System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithOneParameterInvokePartiallyUntypedMismatchedParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, string> method = typeInfo.GetMethod(o => (Func<int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { "1" });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.String }, but expected parameters of type { System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithOneParameterInvokeFullyUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, string> method = typeInfo.GetMethod(o => (Func<int, string>)o.TestMethod);

            Assert.AreEqual("1", method.InvokeFullyUntyped(new Test(), new object[] { 1 }));
        }

        [Test]
        public void MethodInfoWithOneParameterInvokeFullyUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, string> method = typeInfo.GetMethod(o => (Func<int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, new object[] { 1 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoWithOneParameterInvokeFullyUntypedEnumerable()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, string> method = typeInfo.GetMethod(o => (Func<int, string>)o.TestMethod);

            Assert.AreEqual("1", method.InvokeFullyUntyped(new Test(), (IEnumerable<object>)new object[] { 1 }));
        }

        [Test]
        public void MethodInfoWithOneParameterInvokeFullyUntypedEnumerableWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, string> method = typeInfo.GetMethod(o => (Func<int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, (IEnumerable<object>)new object[] { 1 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoWithOneParameterInvokeFullyUntypedNoParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, string> method = typeInfo.GetMethod(o => (Func<int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[0]);
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received no parameters, but expected parameters of type { System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithOneParameterInvokeFullyUntypedWrongNumberOfParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, string> method = typeInfo.GetMethod(o => (Func<int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, 2 });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32 }, but expected parameters of type { System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithOneParameterInvokeFullyUntypedMismatchedParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, string> method = typeInfo.GetMethod(o => (Func<int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { "1" });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.String }, but expected parameters of type { System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithOneParameterSerialization()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, string> method = typeInfo.GetMethod(o => (Func<int, string>)o.TestMethod);
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();
            formatter.Serialize(memoryStream, method);
            memoryStream.Position = 0;
            IMethodInfo<Test, int, string> result = (IMethodInfo<Test, int, string>)formatter.Deserialize(memoryStream);

            Assert.AreEqual(method.MethodInfo, result.MethodInfo);
            Assert.AreEqual(method.Name, result.Name);
            Assert.AreEqual(method.ObjectType, result.ObjectType);
            Assert.IsTrue(method.ParameterTypes.SequenceEqual(result.ParameterTypes));
            Assert.AreEqual(method.ReturnType, result.ReturnType);
        }

        [Test]
        public void MethodInfoWithTwoParametersMethodInfo()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, string>)o.TestMethod);

            Assert.AreEqual(typeof(Test).GetMethod("TestMethod", new[] { typeof(int), typeof(int) }), method.MethodInfo);
        }

        [Test]
        public void MethodInfoWithTwoParametersName()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, string>)o.TestMethod);

            Assert.AreEqual(typeof(Test).GetMethod("TestMethod", new[] { typeof(int), typeof(int) }).Name, method.Name);
        }

        [Test]
        public void MethodInfoWithTwoParametersObjectType()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, string>)o.TestMethod);

            Assert.AreEqual(typeof(Test), method.ObjectType);
        }

        [Test]
        public void MethodInfoWithTwoParametersParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, string>)o.TestMethod);

            Assert.IsTrue(new[] { typeof(int), typeof(int) }.SequenceEqual(method.ParameterTypes));
        }

        [Test]
        public void MethodInfoWithTwoParametersReturnType()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, string>)o.TestMethod);

            Assert.AreEqual(typeof(string), method.ReturnType);
        }

        [Test]
        public void MethodInfoWithTwoParametersInvoke()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, string>)o.TestMethod);

            Assert.AreEqual("3", method.Invoke(new Test(), 1, 2));
        }

        [Test]
        public void MethodInfoWithTwoParametersInvokeWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.Invoke(null, 1, 2);
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoWithTwoParametersInvokePartiallyUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, string>)o.TestMethod);

            Assert.AreEqual("3", method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2 }));
        }

        [Test]
        public void MethodInfoWithTwoParametersInvokePartiallyUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokePartiallyUntyped(null, new object[] { 1, 2 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoWithTwoParametersInvokePartiallyUntypedEnumerable()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, string>)o.TestMethod);

            Assert.AreEqual("3", method.InvokePartiallyUntyped(new Test(), (IEnumerable<object>)new object[] { 1, 2 }));
        }

        [Test]
        public void MethodInfoWithTwoParametersInvokePartiallyUntypedEnumerableWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokePartiallyUntyped(null, (IEnumerable<object>)new object[] { 1, 2 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoWithTwoParametersInvokePartiallyUntypedNoParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[0]);
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received no parameters, but expected parameters of type { System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithTwoParametersInvokePartiallyUntypedWrongNumberOfParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2, 3 });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32 }, but expected parameters of type { System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithTwoParametersInvokePartiallyUntypedMismatchedParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { 1, "2" });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.String }, but expected parameters of type { System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithTwoParametersInvokeFullyUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, string>)o.TestMethod);

            Assert.AreEqual("3", method.InvokeFullyUntyped(new Test(), new object[] { 1, 2 }));
        }

        [Test]
        public void MethodInfoWithTwoParametersInvokeFullyUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, new object[] { 1, 2 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoWithTwoParametersInvokeFullyUntypedEnumerable()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, string>)o.TestMethod);

            Assert.AreEqual("3", method.InvokeFullyUntyped(new Test(), (IEnumerable<object>)new object[] { 1, 2 }));
        }

        [Test]
        public void MethodInfoWithTwoParametersInvokeFullyUntypedEnumerableWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, (IEnumerable<object>)new object[] { 1, 2 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoWithTwoParametersInvokeFullyUntypedNoParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[0]);
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received no parameters, but expected parameters of type { System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithTwoParametersInvokeFullyUntypedWrongNumberOfParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, 3 });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32 }, but expected parameters of type { System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithTwoParametersInvokeFullyUntypedMismatchedParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, "2" });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.String }, but expected parameters of type { System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithTwoParametersSerialization()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, string>)o.TestMethod);
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();
            formatter.Serialize(memoryStream, method);
            memoryStream.Position = 0;
            IMethodInfo<Test, int, int, string> result = (IMethodInfo<Test, int, int, string>)formatter.Deserialize(memoryStream);

            Assert.AreEqual(method.MethodInfo, result.MethodInfo);
            Assert.AreEqual(method.Name, result.Name);
            Assert.AreEqual(method.ObjectType, result.ObjectType);
            Assert.IsTrue(method.ParameterTypes.SequenceEqual(result.ParameterTypes));
            Assert.AreEqual(method.ReturnType, result.ReturnType);
        }

        [Test]
        public void MethodInfoWithThreeParametersMethodInfo()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, string>)o.TestMethod);

            Assert.AreEqual(typeof(Test).GetMethod("TestMethod", new[] { typeof(int), typeof(int), typeof(int) }), method.MethodInfo);
        }

        [Test]
        public void MethodInfoWithThreeParametersName()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, string>)o.TestMethod);

            Assert.AreEqual(typeof(Test).GetMethod("TestMethod", new[] { typeof(int), typeof(int), typeof(int) }).Name, method.Name);
        }

        [Test]
        public void MethodInfoWithThreeParametersObjectType()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, string>)o.TestMethod);

            Assert.AreEqual(typeof(Test), method.ObjectType);
        }

        [Test]
        public void MethodInfoWithThreeParametersParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, string>)o.TestMethod);

            Assert.IsTrue(new[] { typeof(int), typeof(int), typeof(int) }.SequenceEqual(method.ParameterTypes));
        }

        [Test]
        public void MethodInfoWithThreeParametersReturnType()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, string>)o.TestMethod);

            Assert.AreEqual(typeof(string), method.ReturnType);
        }

        [Test]
        public void MethodInfoWithThreeParametersInvoke()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, string>)o.TestMethod);

            Assert.AreEqual("6", method.Invoke(new Test(), 1, 2, 3));
        }

        [Test]
        public void MethodInfoWithThreeParametersInvokeWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.Invoke(null, 1, 2, 3);
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoWithThreeParametersInvokePartiallyUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, string>)o.TestMethod);

            Assert.AreEqual("6", method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2, 3 }));
        }

        [Test]
        public void MethodInfoWithThreeParametersInvokePartiallyUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokePartiallyUntyped(null, new object[] { 1, 2, 3 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoWithThreeParametersInvokePartiallyUntypedEnumerable()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, string>)o.TestMethod);

            Assert.AreEqual("6", method.InvokePartiallyUntyped(new Test(), (IEnumerable<object>)new object[] { 1, 2, 3 }));
        }

        [Test]
        public void MethodInfoWithThreeParametersInvokePartiallyUntypedEnumerableWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokePartiallyUntyped(null, (IEnumerable<object>)new object[] { 1, 2, 3 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoWithThreeParametersInvokePartiallyUntypedNoParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[0]);
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received no parameters, but expected parameters of type { System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithThreeParametersInvokePartiallyUntypedWrongNumberOfParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2, 3, 4 });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32, System.Int32 }, but expected parameters of type { System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithThreeParametersInvokePartiallyUntypedMismatchedParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2, "3" });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.String }, but expected parameters of type { System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithThreeParametersInvokeFullyUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, string>)o.TestMethod);

            Assert.AreEqual("6", method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, 3 }));
        }

        [Test]
        public void MethodInfoWithThreeParametersInvokeFullyUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, new object[] { 1, 2, 3 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoWithThreeParametersInvokeFullyUntypedEnumerable()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, string>)o.TestMethod);

            Assert.AreEqual("6", method.InvokeFullyUntyped(new Test(), (IEnumerable<object>)new object[] { 1, 2, 3 }));
        }

        [Test]
        public void MethodInfoWithThreeParametersInvokeFullyUntypedEnumerableWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, (IEnumerable<object>)new object[] { 1, 2, 3 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoWithThreeParametersInvokeFullyUntypedNoParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[0]);
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received no parameters, but expected parameters of type { System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithThreeParametersInvokeFullyUntypedWrongNumberOfParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, 3, 4 });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32, System.Int32 }, but expected parameters of type { System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithThreeParametersInvokeFullyUntypedMismatchedParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, "3" });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.String }, but expected parameters of type { System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithThreeParametersSerialization()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, string>)o.TestMethod);
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();
            formatter.Serialize(memoryStream, method);
            memoryStream.Position = 0;
            IMethodInfo<Test, int, int, int, string> result = (IMethodInfo<Test, int, int, int, string>)formatter.Deserialize(memoryStream);

            Assert.AreEqual(method.MethodInfo, result.MethodInfo);
            Assert.AreEqual(method.Name, result.Name);
            Assert.AreEqual(method.ObjectType, result.ObjectType);
            Assert.IsTrue(method.ParameterTypes.SequenceEqual(result.ParameterTypes));
            Assert.AreEqual(method.ReturnType, result.ReturnType);
        }

        [Test]
        public void MethodInfoWithFourParametersMethodInfo()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual(typeof(Test).GetMethod("TestMethod", new[] { typeof(int), typeof(int), typeof(int), typeof(int) }), method.MethodInfo);
        }

        [Test]
        public void MethodInfoWithFourParametersName()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual(typeof(Test).GetMethod("TestMethod", new[] { typeof(int), typeof(int), typeof(int), typeof(int) }).Name, method.Name);
        }

        [Test]
        public void MethodInfoWithFourParametersObjectType()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual(typeof(Test), method.ObjectType);
        }

        [Test]
        public void MethodInfoWithFourParametersParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, string>)o.TestMethod);

            Assert.IsTrue(new[] { typeof(int), typeof(int), typeof(int), typeof(int) }.SequenceEqual(method.ParameterTypes));
        }

        [Test]
        public void MethodInfoWithFourParametersReturnType()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual(typeof(string), method.ReturnType);
        }

        [Test]
        public void MethodInfoWithFourParametersInvoke()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual("10", method.Invoke(new Test(), 1, 2, 3, 4));
        }

        [Test]
        public void MethodInfoWithFourParametersInvokeWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.Invoke(null, 1, 2, 3, 4);
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoWithFourParametersInvokePartiallyUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual("10", method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2, 3, 4 }));
        }

        [Test]
        public void MethodInfoWithFourParametersInvokePartiallyUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokePartiallyUntyped(null, new object[] { 1, 2, 3, 4 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoWithFourParametersInvokePartiallyUntypedEnumerable()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual("10", method.InvokePartiallyUntyped(new Test(), (IEnumerable<object>)new object[] { 1, 2, 3, 4 }));
        }

        [Test]
        public void MethodInfoWithFourParametersInvokePartiallyUntypedEnumerableWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokePartiallyUntyped(null, (IEnumerable<object>)new object[] { 1, 2, 3, 4 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoWithFourParametersInvokePartiallyUntypedNoParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[0]);
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received no parameters, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithFourParametersInvokePartiallyUntypedWrongNumberOfParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5 });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithFourParametersInvokePartiallyUntypedMismatchedParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2, 3, "4" });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32, System.String }, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithFourParametersInvokeFullyUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual("10", method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, 3, 4 }));
        }

        [Test]
        public void MethodInfoWithFourParametersInvokeFullyUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, new object[] { 1, 2, 3, 4 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoWithFourParametersInvokeFullyUntypedEnumerable()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual("10", method.InvokeFullyUntyped(new Test(), (IEnumerable<object>)new object[] { 1, 2, 3, 4 }));
        }

        [Test]
        public void MethodInfoWithFourParametersInvokeFullyUntypedEnumerableWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, (IEnumerable<object>)new object[] { 1, 2, 3, 4 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoWithFourParametersInvokeFullyUntypedNoParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[0]);
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received no parameters, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithFourParametersInvokeFullyUntypedWrongNumberOfParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5 });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithFourParametersInvokeFullyUntypedMismatchedParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, 3, "4" });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32, System.String }, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithFourParametersSerialization()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, string>)o.TestMethod);
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();
            formatter.Serialize(memoryStream, method);
            memoryStream.Position = 0;
            IMethodInfo<Test, int, int, int, int, string> result = (IMethodInfo<Test, int, int, int, int, string>)formatter.Deserialize(memoryStream);

            Assert.AreEqual(method.MethodInfo, result.MethodInfo);
            Assert.AreEqual(method.Name, result.Name);
            Assert.AreEqual(method.ObjectType, result.ObjectType);
            Assert.IsTrue(method.ParameterTypes.SequenceEqual(result.ParameterTypes));
            Assert.AreEqual(method.ReturnType, result.ReturnType);
        }

        [Test]
        public void MethodInfoWithFiveParametersMethodInfo()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual(typeof(Test).GetMethod("TestMethod", new[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) }), method.MethodInfo);
        }

        [Test]
        public void MethodInfoWithFiveParametersName()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual(typeof(Test).GetMethod("TestMethod", new[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) }).Name, method.Name);
        }

        [Test]
        public void MethodInfoWithFiveParametersObjectType()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual(typeof(Test), method.ObjectType);
        }

        [Test]
        public void MethodInfoWithFiveParametersParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, string>)o.TestMethod);

            Assert.IsTrue(new[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) }.SequenceEqual(method.ParameterTypes));
        }

        [Test]
        public void MethodInfoWithFiveParametersReturnType()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual(typeof(string), method.ReturnType);
        }

        [Test]
        public void MethodInfoWithFiveParametersInvoke()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual("15", method.Invoke(new Test(), 1, 2, 3, 4, 5));
        }

        [Test]
        public void MethodInfoWithFiveParametersInvokeWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.Invoke(null, 1, 2, 3, 4, 5);
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoWithFiveParametersInvokePartiallyUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual("15", method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5 }));
        }

        [Test]
        public void MethodInfoWithFiveParametersInvokePartiallyUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokePartiallyUntyped(null, new object[] { 1, 2, 3, 4, 5 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoWithFiveParametersInvokePartiallyUntypedEnumerable()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual("15", method.InvokePartiallyUntyped(new Test(), (IEnumerable<object>)new object[] { 1, 2, 3, 4, 5 }));
        }

        [Test]
        public void MethodInfoWithFiveParametersInvokePartiallyUntypedEnumerableWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokePartiallyUntyped(null, (IEnumerable<object>)new object[] { 1, 2, 3, 4, 5 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoWithFiveParametersInvokePartiallyUntypedNoParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[0]);
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received no parameters, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithFiveParametersInvokePartiallyUntypedWrongNumberOfParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5, 6 });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithFiveParametersInvokePartiallyUntypedMismatchedParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2, 3, 4, "5" });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.String }, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithFiveParametersInvokeFullyUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual("15", method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5 }));
        }

        [Test]
        public void MethodInfoWithFiveParametersInvokeFullyUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, new object[] { 1, 2, 3, 4, 5 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoWithFiveParametersInvokeFullyUntypedEnumerable()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual("15", method.InvokeFullyUntyped(new Test(), (IEnumerable<object>)new object[] { 1, 2, 3, 4, 5 }));
        }

        [Test]
        public void MethodInfoWithFiveParametersInvokeFullyUntypedEnumerableWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, (IEnumerable<object>)new object[] { 1, 2, 3, 4, 5 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoWithFiveParametersInvokeFullyUntypedNoParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[0]);
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received no parameters, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithFiveParametersInvokeFullyUntypedWrongNumberOfParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5, 6 });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithFiveParametersInvokeFullyUntypedMismatchedParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, 3, 4, "5" });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.String }, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithFiveParametersSerialization()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, string>)o.TestMethod);
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();
            formatter.Serialize(memoryStream, method);
            memoryStream.Position = 0;
            IMethodInfo<Test, int, int, int, int, int, string> result = (IMethodInfo<Test, int, int, int, int, int, string>)formatter.Deserialize(memoryStream);

            Assert.AreEqual(method.MethodInfo, result.MethodInfo);
            Assert.AreEqual(method.Name, result.Name);
            Assert.AreEqual(method.ObjectType, result.ObjectType);
            Assert.IsTrue(method.ParameterTypes.SequenceEqual(result.ParameterTypes));
            Assert.AreEqual(method.ReturnType, result.ReturnType);
        }

        [Test]
        public void MethodInfoWithSixParametersMethodInfo()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual(typeof(Test).GetMethod("TestMethod", new[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) }), method.MethodInfo);
        }

        [Test]
        public void MethodInfoWithSixParametersName()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual(typeof(Test).GetMethod("TestMethod", new[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) }).Name, method.Name);
        }

        [Test]
        public void MethodInfoWithSixParametersObjectType()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual(typeof(Test), method.ObjectType);
        }

        [Test]
        public void MethodInfoWithSixParametersParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, string>)o.TestMethod);

            Assert.IsTrue(new[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) }.SequenceEqual(method.ParameterTypes));
        }

        [Test]
        public void MethodInfoWithSixParametersReturnType()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual(typeof(string), method.ReturnType);
        }

        [Test]
        public void MethodInfoWithSixParametersInvoke()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual("21", method.Invoke(new Test(), 1, 2, 3, 4, 5, 6));
        }

        [Test]
        public void MethodInfoWithSixParametersInvokeWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.Invoke(null, 1, 2, 3, 4, 5, 6);
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoWithSixParametersInvokePartiallyUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual("21", method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5, 6 }));
        }

        [Test]
        public void MethodInfoWithSixParametersInvokePartiallyUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokePartiallyUntyped(null, new object[] { 1, 2, 3, 4, 5, 6 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoWithSixParametersInvokePartiallyUntypedEnumerable()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual("21", method.InvokePartiallyUntyped(new Test(), (IEnumerable<object>)new object[] { 1, 2, 3, 4, 5, 6 }));
        }

        [Test]
        public void MethodInfoWithSixParametersInvokePartiallyUntypedEnumerableWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokePartiallyUntyped(null, (IEnumerable<object>)new object[] { 1, 2, 3, 4, 5, 6 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoWithSixParametersInvokePartiallyUntypedNoParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[0]);
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received no parameters, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithSixParametersInvokePartiallyUntypedWrongNumberOfParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5, 6, 7 });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithSixParametersInvokePartiallyUntypedMismatchedParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5, "6" });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.String }, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithSixParametersInvokeFullyUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual("21", method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5, 6 }));
        }

        [Test]
        public void MethodInfoWithSixParametersInvokeFullyUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, new object[] { 1, 2, 3, 4, 5, 6 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoWithSixParametersInvokeFullyUntypedEnumerable()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual("21", method.InvokeFullyUntyped(new Test(), (IEnumerable<object>)new object[] { 1, 2, 3, 4, 5, 6 }));
        }

        [Test]
        public void MethodInfoWithSixParametersInvokeFullyUntypedEnumerableWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, (IEnumerable<object>)new object[] { 1, 2, 3, 4, 5, 6 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoWithSixParametersInvokeFullyUntypedNoParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[0]);
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received no parameters, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithSixParametersInvokeFullyUntypedWrongNumberOfParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5, 6, 7 });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithSixParametersInvokeFullyUntypedMismatchedParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5, "6" });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.String }, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithSixParametersSerialization()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, string>)o.TestMethod);
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();
            formatter.Serialize(memoryStream, method);
            memoryStream.Position = 0;
            IMethodInfo<Test, int, int, int, int, int, int, string> result = (IMethodInfo<Test, int, int, int, int, int, int, string>)formatter.Deserialize(memoryStream);

            Assert.AreEqual(method.MethodInfo, result.MethodInfo);
            Assert.AreEqual(method.Name, result.Name);
            Assert.AreEqual(method.ObjectType, result.ObjectType);
            Assert.IsTrue(method.ParameterTypes.SequenceEqual(result.ParameterTypes));
            Assert.AreEqual(method.ReturnType, result.ReturnType);
        }

        [Test]
        public void MethodInfoWithSevenParametersMethodInfo()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual(typeof(Test).GetMethod("TestMethod", new[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) }), method.MethodInfo);
        }

        [Test]
        public void MethodInfoWithSevenParametersName()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual(typeof(Test).GetMethod("TestMethod", new[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) }).Name, method.Name);
        }

        [Test]
        public void MethodInfoWithSevenParametersObjectType()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual(typeof(Test), method.ObjectType);
        }

        [Test]
        public void MethodInfoWithSevenParametersParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, string>)o.TestMethod);

            Assert.IsTrue(new[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) }.SequenceEqual(method.ParameterTypes));
        }

        [Test]
        public void MethodInfoWithSevenParametersReturnType()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual(typeof(string), method.ReturnType);
        }

        [Test]
        public void MethodInfoWithSevenParametersInvoke()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual("28", method.Invoke(new Test(), 1, 2, 3, 4, 5, 6, 7));
        }

        [Test]
        public void MethodInfoWithSevenParametersInvokeWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.Invoke(null, 1, 2, 3, 4, 5, 6, 7);
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoWithSevenParametersInvokePartiallyUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual("28", method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5, 6, 7 }));
        }

        [Test]
        public void MethodInfoWithSevenParametersInvokePartiallyUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokePartiallyUntyped(null, new object[] { 1, 2, 3, 4, 5, 6, 7 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoWithSevenParametersInvokePartiallyUntypedEnumerable()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual("28", method.InvokePartiallyUntyped(new Test(), (IEnumerable<object>)new object[] { 1, 2, 3, 4, 5, 6, 7 }));
        }

        [Test]
        public void MethodInfoWithSevenParametersInvokePartiallyUntypedEnumerableWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokePartiallyUntyped(null, (IEnumerable<object>)new object[] { 1, 2, 3, 4, 5, 6, 7 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoWithSevenParametersInvokePartiallyUntypedNoParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[0]);
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received no parameters, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithSevenParametersInvokePartiallyUntypedWrongNumberOfParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5, 6, 7, 8 });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithSevenParametersInvokePartiallyUntypedMismatchedParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5, 6, "7" });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.String }, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithSevenParametersInvokeFullyUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual("28", method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5, 6, 7 }));
        }

        [Test]
        public void MethodInfoWithSevenParametersInvokeFullyUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, new object[] { 1, 2, 3, 4, 5, 6, 7 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoWithSevenParametersInvokeFullyUntypedEnumerable()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual("28", method.InvokeFullyUntyped(new Test(), (IEnumerable<object>)new object[] { 1, 2, 3, 4, 5, 6, 7 }));
        }

        [Test]
        public void MethodInfoWithSevenParametersInvokeFullyUntypedEnumerableWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, (IEnumerable<object>)new object[] { 1, 2, 3, 4, 5, 6, 7 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoWithSevenParametersInvokeFullyUntypedNoParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[0]);
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received no parameters, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithSevenParametersInvokeFullyUntypedWrongNumberOfParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5, 6, 7, 8 });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithSevenParametersInvokeFullyUntypedMismatchedParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5, 6, "7" });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.String }, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithSevenParametersSerialization()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, string>)o.TestMethod);
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();
            formatter.Serialize(memoryStream, method);
            memoryStream.Position = 0;
            IMethodInfo<Test, int, int, int, int, int, int, int, string> result = (IMethodInfo<Test, int, int, int, int, int, int, int, string>)formatter.Deserialize(memoryStream);

            Assert.AreEqual(method.MethodInfo, result.MethodInfo);
            Assert.AreEqual(method.Name, result.Name);
            Assert.AreEqual(method.ObjectType, result.ObjectType);
            Assert.IsTrue(method.ParameterTypes.SequenceEqual(result.ParameterTypes));
            Assert.AreEqual(method.ReturnType, result.ReturnType);
        }

        [Test]
        public void MethodInfoWithEightParametersMethodInfo()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual(typeof(Test).GetMethod("TestMethod", new[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) }), method.MethodInfo);
        }

        [Test]
        public void MethodInfoWithEightParametersName()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual(typeof(Test).GetMethod("TestMethod", new[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) }).Name, method.Name);
        }

        [Test]
        public void MethodInfoWithEightParametersObjectType()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual(typeof(Test), method.ObjectType);
        }

        [Test]
        public void MethodInfoWithEightParametersParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, int, string>)o.TestMethod);

            Assert.IsTrue(new[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) }.SequenceEqual(method.ParameterTypes));
        }

        [Test]
        public void MethodInfoWithEightParametersReturnType()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual(typeof(string), method.ReturnType);
        }

        [Test]
        public void MethodInfoWithEightParametersInvoke()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual("36", method.Invoke(new Test(), 1, 2, 3, 4, 5, 6, 7, 8));
        }

        [Test]
        public void MethodInfoWithEightParametersInvokeWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.Invoke(null, 1, 2, 3, 4, 5, 6, 7, 8);
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoWithEightParametersInvokePartiallyUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual("36", method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5, 6, 7, 8 }));
        }

        [Test]
        public void MethodInfoWithEightParametersInvokePartiallyUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokePartiallyUntyped(null, new object[] { 1, 2, 3, 4, 5, 6, 7, 8 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoWithEightParametersInvokePartiallyUntypedEnumerable()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual("36", method.InvokePartiallyUntyped(new Test(), (IEnumerable<object>)new object[] { 1, 2, 3, 4, 5, 6, 7, 8 }));
        }

        [Test]
        public void MethodInfoWithEightParametersInvokePartiallyUntypedEnumerableWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokePartiallyUntyped(null, (IEnumerable<object>)new object[] { 1, 2, 3, 4, 5, 6, 7, 8 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoWithEightParametersInvokePartiallyUntypedNoParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[0]);
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received no parameters, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithEightParametersInvokePartiallyUntypedWrongNumberOfParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithEightParametersInvokePartiallyUntypedMismatchedParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5, 6, 7, "8" });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.String }, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithEightParametersInvokeFullyUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual("36", method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5, 6, 7, 8 }));
        }

        [Test]
        public void MethodInfoWithEightParametersInvokeFullyUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, new object[] { 1, 2, 3, 4, 5, 6, 7, 8 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoWithEightParametersInvokeFullyUntypedEnumerable()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual("36", method.InvokeFullyUntyped(new Test(), (IEnumerable<object>)new object[] { 1, 2, 3, 4, 5, 6, 7, 8 }));
        }

        [Test]
        public void MethodInfoWithEightParametersInvokeFullyUntypedEnumerableWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, (IEnumerable<object>)new object[] { 1, 2, 3, 4, 5, 6, 7, 8 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void MethodInfoWithEightParametersInvokeFullyUntypedNoParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[0]);
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received no parameters, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithEightParametersInvokeFullyUntypedWrongNumberOfParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithEightParametersInvokeFullyUntypedMismatchedParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, int, string>)o.TestMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5, 6, 7, "8" });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.String }, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void MethodInfoWithEightParametersSerialization()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, int, string>)o.TestMethod);
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();
            formatter.Serialize(memoryStream, method);
            memoryStream.Position = 0;
            IMethodInfo<Test, int, int, int, int, int, int, int, int, string> result = (IMethodInfo<Test, int, int, int, int, int, int, int, int, string>)formatter.Deserialize(memoryStream);

            Assert.AreEqual(method.MethodInfo, result.MethodInfo);
            Assert.AreEqual(method.Name, result.Name);
            Assert.AreEqual(method.ObjectType, result.ObjectType);
            Assert.IsTrue(method.ParameterTypes.SequenceEqual(result.ParameterTypes));
            Assert.AreEqual(method.ReturnType, result.ReturnType);
        }

        [Test]
        public void VoidMethodInfoWithNoParametersMethodInfo()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test> method = typeInfo.GetVoidMethod(o => (Action)o.TestVoidMethod);

            Assert.AreEqual(typeof(Test).GetMethod("TestVoidMethod", Type.EmptyTypes), method.MethodInfo);
        }

        [Test]
        public void VoidMethodInfoWithNoParametersName()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test> method = typeInfo.GetVoidMethod(o => (Action)o.TestVoidMethod);

            Assert.AreEqual(typeof(Test).GetMethod("TestVoidMethod", Type.EmptyTypes).Name, method.Name);
        }

        [Test]
        public void VoidMethodInfoWithNoParametersObjectType()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test> method = typeInfo.GetVoidMethod(o => (Action)o.TestVoidMethod);

            Assert.AreEqual(typeof(Test), method.ObjectType);
        }

        [Test]
        public void VoidMethodInfoWithNoParametersParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test> method = typeInfo.GetVoidMethod(o => (Action)o.TestVoidMethod);

            Assert.IsTrue(Type.EmptyTypes .SequenceEqual(method.ParameterTypes));
        }

        [Test]
        public void VoidMethodInfoWithNoParametersReturnType()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test> method = typeInfo.GetVoidMethod(o => (Action)o.TestVoidMethod);

            Assert.AreEqual(typeof(void), method.ReturnType);
        }

        [Test]
        public void VoidMethodInfoWithNoParametersInvoke()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test> method = typeInfo.GetVoidMethod(o => (Action)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.Invoke(new Test());
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("0", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithNoParametersInvokeWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test> method = typeInfo.GetVoidMethod(o => (Action)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.Invoke(null);
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoWithNoParametersInvokePartiallyUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test> method = typeInfo.GetVoidMethod(o => (Action)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[0]);
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("0", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithNoParametersInvokePartiallyUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test> method = typeInfo.GetVoidMethod(o => (Action)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokePartiallyUntyped(null, new object[0]);
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoWithNoParametersInvokePartiallyUntypedEnumerable()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test> method = typeInfo.GetVoidMethod(o => (Action)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), (IEnumerable<object>)new object[0]);
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("0", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithNoParametersInvokePartiallyUntypedEnumerableWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test> method = typeInfo.GetVoidMethod(o => (Action)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokePartiallyUntyped(null, (IEnumerable<object>)new object[0]);
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoWithNoParametersInvokePartiallyUntypedWrongNumberOfParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test> method = typeInfo.GetVoidMethod(o => (Action)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { 1 });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32 }, but expected no parameters." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithNoParametersInvokeFullyUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test> method = typeInfo.GetVoidMethod(o => (Action)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[0]);
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("0", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithNoParametersInvokeFullyUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test> method = typeInfo.GetVoidMethod(o => (Action)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, new object[0]);
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoWithNoParametersInvokeFullyUntypedEnumerable()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test> method = typeInfo.GetVoidMethod(o => (Action)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), (IEnumerable<object>)new object[0]);
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("0", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithNoParametersInvokeFullyUntypedEnumerableWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test> method = typeInfo.GetVoidMethod(o => (Action)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, (IEnumerable<object>)new object[0]);
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoWithNoParametersInvokeFullyUntypedWrongNumberOfParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test> method = typeInfo.GetVoidMethod(o => (Action)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1 });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32 }, but expected no parameters." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithNoParametersSerialization()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test> method = typeInfo.GetVoidMethod(o => (Action)o.TestVoidMethod);
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();
            formatter.Serialize(memoryStream, method);
            memoryStream.Position = 0;
            IVoidMethodInfo<Test> result = (IVoidMethodInfo<Test>)formatter.Deserialize(memoryStream);

            Assert.AreEqual(method.MethodInfo, result.MethodInfo);
            Assert.AreEqual(method.Name, result.Name);
            Assert.AreEqual(method.ObjectType, result.ObjectType);
            Assert.IsTrue(method.ParameterTypes.SequenceEqual(result.ParameterTypes));
            Assert.AreEqual(method.ReturnType, result.ReturnType);
        }

        [Test]
        public void VoidMethodInfoWithOneParameterMethodInfo()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int> method = typeInfo.GetVoidMethod(o => (Action<int>)o.TestVoidMethod);

            Assert.AreEqual(typeof(Test).GetMethod("TestVoidMethod", new[] { typeof(int) }), method.MethodInfo);
        }

        [Test]
        public void VoidMethodInfoWithOneParameterName()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int> method = typeInfo.GetVoidMethod(o => (Action<int>)o.TestVoidMethod);

            Assert.AreEqual(typeof(Test).GetMethod("TestVoidMethod", new[] { typeof(int) }).Name, method.Name);
        }

        [Test]
        public void VoidMethodInfoWithOneParameterObjectType()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int> method = typeInfo.GetVoidMethod(o => (Action<int>)o.TestVoidMethod);

            Assert.AreEqual(typeof(Test), method.ObjectType);
        }

        [Test]
        public void VoidMethodInfoWithOneParameterParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int> method = typeInfo.GetVoidMethod(o => (Action<int>)o.TestVoidMethod);

            Assert.IsTrue(new[] { typeof(int) }.SequenceEqual(method.ParameterTypes));
        }

        [Test]
        public void VoidMethodInfoWithOneParameterReturnType()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int> method = typeInfo.GetVoidMethod(o => (Action<int>)o.TestVoidMethod);

            Assert.AreEqual(typeof(void), method.ReturnType);
        }

        [Test]
        public void VoidMethodInfoWithOneParameterInvoke()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int> method = typeInfo.GetVoidMethod(o => (Action<int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.Invoke(new Test(), 1);
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithOneParameterInvokeWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int> method = typeInfo.GetVoidMethod(o => (Action<int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.Invoke(null, 1);
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoWithOneParameterInvokePartiallyUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int> method = typeInfo.GetVoidMethod(o => (Action<int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { 1 });
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithOneParameterInvokePartiallyUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int> method = typeInfo.GetVoidMethod(o => (Action<int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokePartiallyUntyped(null, new object[] { 1 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoWithOneParameterInvokePartiallyUntypedEnumerable()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int> method = typeInfo.GetVoidMethod(o => (Action<int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), (IEnumerable<object>)new object[] { 1 });
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithOneParameterInvokePartiallyUntypedEnumerableWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int> method = typeInfo.GetVoidMethod(o => (Action<int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokePartiallyUntyped(null, (IEnumerable<object>)new object[] { 1 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoWithOneParameterInvokePartiallyUntypedNoParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int> method = typeInfo.GetVoidMethod(o => (Action<int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[0]);
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received no parameters, but expected parameters of type { System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithOneParameterInvokePartiallyUntypedWrongNumberOfParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int> method = typeInfo.GetVoidMethod(o => (Action<int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2 });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32 }, but expected parameters of type { System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithOneParameterInvokePartiallyUntypedMismatchedParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int> method = typeInfo.GetVoidMethod(o => (Action<int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { "1" });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.String }, but expected parameters of type { System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithOneParameterInvokeFullyUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int> method = typeInfo.GetVoidMethod(o => (Action<int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1 });
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithOneParameterInvokeFullyUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int> method = typeInfo.GetVoidMethod(o => (Action<int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, new object[] { 1 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoWithOneParameterInvokeFullyUntypedEnumerable()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int> method = typeInfo.GetVoidMethod(o => (Action<int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), (IEnumerable<object>)new object[] { 1 });
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithOneParameterInvokeFullyUntypedEnumerableWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int> method = typeInfo.GetVoidMethod(o => (Action<int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, (IEnumerable<object>)new object[] { 1 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoWithOneParameterInvokeFullyUntypedNoParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int> method = typeInfo.GetVoidMethod(o => (Action<int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[0]);
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received no parameters, but expected parameters of type { System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithOneParameterInvokeFullyUntypedWrongNumberOfParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int> method = typeInfo.GetVoidMethod(o => (Action<int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, 2 });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32 }, but expected parameters of type { System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithOneParameterInvokeFullyUntypedMismatchedParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int> method = typeInfo.GetVoidMethod(o => (Action<int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { "1" });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.String }, but expected parameters of type { System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithOneParameterSerialization()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int> method = typeInfo.GetVoidMethod(o => (Action<int>)o.TestVoidMethod);
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();
            formatter.Serialize(memoryStream, method);
            memoryStream.Position = 0;
            IVoidMethodInfo<Test, int> result = (IVoidMethodInfo<Test, int>)formatter.Deserialize(memoryStream);

            Assert.AreEqual(method.MethodInfo, result.MethodInfo);
            Assert.AreEqual(method.Name, result.Name);
            Assert.AreEqual(method.ObjectType, result.ObjectType);
            Assert.IsTrue(method.ParameterTypes.SequenceEqual(result.ParameterTypes));
            Assert.AreEqual(method.ReturnType, result.ReturnType);
        }

        [Test]
        public void VoidMethodInfoWithTwoParametersMethodInfo()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int>)o.TestVoidMethod);

            Assert.AreEqual(typeof(Test).GetMethod("TestVoidMethod", new[] { typeof(int), typeof(int) }), method.MethodInfo);
        }

        [Test]
        public void VoidMethodInfoWithTwoParametersName()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int>)o.TestVoidMethod);

            Assert.AreEqual(typeof(Test).GetMethod("TestVoidMethod", new[] { typeof(int), typeof(int) }).Name, method.Name);
        }

        [Test]
        public void VoidMethodInfoWithTwoParametersObjectType()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int>)o.TestVoidMethod);

            Assert.AreEqual(typeof(Test), method.ObjectType);
        }

        [Test]
        public void VoidMethodInfoWithTwoParametersParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int>)o.TestVoidMethod);

            Assert.IsTrue(new[] { typeof(int), typeof(int) }.SequenceEqual(method.ParameterTypes));
        }

        [Test]
        public void VoidMethodInfoWithTwoParametersReturnType()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int>)o.TestVoidMethod);

            Assert.AreEqual(typeof(void), method.ReturnType);
        }

        [Test]
        public void VoidMethodInfoWithTwoParametersInvoke()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.Invoke(new Test(), 1, 2);
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithTwoParametersInvokeWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.Invoke(null, 1, 2);
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoWithTwoParametersInvokePartiallyUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2 });
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithTwoParametersInvokePartiallyUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokePartiallyUntyped(null, new object[] { 1, 2 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoWithTwoParametersInvokePartiallyUntypedEnumerable()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), (IEnumerable<object>)new object[] { 1, 2 });
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithTwoParametersInvokePartiallyUntypedEnumerableWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokePartiallyUntyped(null, (IEnumerable<object>)new object[] { 1, 2 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoWithTwoParametersInvokePartiallyUntypedNoParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[0]);
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received no parameters, but expected parameters of type { System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithTwoParametersInvokePartiallyUntypedWrongNumberOfParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2, 3 });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32 }, but expected parameters of type { System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithTwoParametersInvokePartiallyUntypedMismatchedParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { 1, "2" });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.String }, but expected parameters of type { System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithTwoParametersInvokeFullyUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, 2 });
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithTwoParametersInvokeFullyUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, new object[] { 1, 2 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoWithTwoParametersInvokeFullyUntypedEnumerable()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), (IEnumerable<object>)new object[] { 1, 2 });
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithTwoParametersInvokeFullyUntypedEnumerableWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, (IEnumerable<object>)new object[] { 1, 2 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoWithTwoParametersInvokeFullyUntypedNoParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[0]);
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received no parameters, but expected parameters of type { System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithTwoParametersInvokeFullyUntypedWrongNumberOfParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, 3 });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32 }, but expected parameters of type { System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithTwoParametersInvokeFullyUntypedMismatchedParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, "2" });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.String }, but expected parameters of type { System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithTwoParametersSerialization()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int>)o.TestVoidMethod);
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();
            formatter.Serialize(memoryStream, method);
            memoryStream.Position = 0;
            IVoidMethodInfo<Test, int, int> result = (IVoidMethodInfo<Test, int, int>)formatter.Deserialize(memoryStream);

            Assert.AreEqual(method.MethodInfo, result.MethodInfo);
            Assert.AreEqual(method.Name, result.Name);
            Assert.AreEqual(method.ObjectType, result.ObjectType);
            Assert.IsTrue(method.ParameterTypes.SequenceEqual(result.ParameterTypes));
            Assert.AreEqual(method.ReturnType, result.ReturnType);
        }

        [Test]
        public void VoidMethodInfoWithThreeParametersMethodInfo()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int>)o.TestVoidMethod);

            Assert.AreEqual(typeof(Test).GetMethod("TestVoidMethod", new[] { typeof(int), typeof(int), typeof(int) }), method.MethodInfo);
        }

        [Test]
        public void VoidMethodInfoWithThreeParametersName()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int>)o.TestVoidMethod);

            Assert.AreEqual(typeof(Test).GetMethod("TestVoidMethod", new[] { typeof(int), typeof(int), typeof(int) }).Name, method.Name);
        }

        [Test]
        public void VoidMethodInfoWithThreeParametersObjectType()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int>)o.TestVoidMethod);

            Assert.AreEqual(typeof(Test), method.ObjectType);
        }

        [Test]
        public void VoidMethodInfoWithThreeParametersParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int>)o.TestVoidMethod);

            Assert.IsTrue(new[] { typeof(int), typeof(int), typeof(int) }.SequenceEqual(method.ParameterTypes));
        }

        [Test]
        public void VoidMethodInfoWithThreeParametersReturnType()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int>)o.TestVoidMethod);

            Assert.AreEqual(typeof(void), method.ReturnType);
        }

        [Test]
        public void VoidMethodInfoWithThreeParametersInvoke()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.Invoke(new Test(), 1, 2, 3);
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2, 3", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithThreeParametersInvokeWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.Invoke(null, 1, 2, 3);
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoWithThreeParametersInvokePartiallyUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2, 3 });
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2, 3", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithThreeParametersInvokePartiallyUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokePartiallyUntyped(null, new object[] { 1, 2, 3 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoWithThreeParametersInvokePartiallyUntypedEnumerable()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), (IEnumerable<object>)new object[] { 1, 2, 3 });
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2, 3", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithThreeParametersInvokePartiallyUntypedEnumerableWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokePartiallyUntyped(null, (IEnumerable<object>)new object[] { 1, 2, 3 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoWithThreeParametersInvokePartiallyUntypedNoParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[0]);
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received no parameters, but expected parameters of type { System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithThreeParametersInvokePartiallyUntypedWrongNumberOfParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2, 3, 4 });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32, System.Int32 }, but expected parameters of type { System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithThreeParametersInvokePartiallyUntypedMismatchedParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2, "3" });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.String }, but expected parameters of type { System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithThreeParametersInvokeFullyUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, 3 });
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2, 3", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithThreeParametersInvokeFullyUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, new object[] { 1, 2, 3 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoWithThreeParametersInvokeFullyUntypedEnumerable()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), (IEnumerable<object>)new object[] { 1, 2, 3 });
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2, 3", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithThreeParametersInvokeFullyUntypedEnumerableWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, (IEnumerable<object>)new object[] { 1, 2, 3 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoWithThreeParametersInvokeFullyUntypedNoParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[0]);
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received no parameters, but expected parameters of type { System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithThreeParametersInvokeFullyUntypedWrongNumberOfParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, 3, 4 });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32, System.Int32 }, but expected parameters of type { System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithThreeParametersInvokeFullyUntypedMismatchedParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, "3" });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.String }, but expected parameters of type { System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithThreeParametersSerialization()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int>)o.TestVoidMethod);
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();
            formatter.Serialize(memoryStream, method);
            memoryStream.Position = 0;
            IVoidMethodInfo<Test, int, int, int> result = (IVoidMethodInfo<Test, int, int, int>)formatter.Deserialize(memoryStream);

            Assert.AreEqual(method.MethodInfo, result.MethodInfo);
            Assert.AreEqual(method.Name, result.Name);
            Assert.AreEqual(method.ObjectType, result.ObjectType);
            Assert.IsTrue(method.ParameterTypes.SequenceEqual(result.ParameterTypes));
            Assert.AreEqual(method.ReturnType, result.ReturnType);
        }

        [Test]
        public void VoidMethodInfoWithFourParametersMethodInfo()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int>)o.TestVoidMethod);

            Assert.AreEqual(typeof(Test).GetMethod("TestVoidMethod", new[] { typeof(int), typeof(int), typeof(int), typeof(int) }), method.MethodInfo);
        }

        [Test]
        public void VoidMethodInfoWithFourParametersName()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int>)o.TestVoidMethod);

            Assert.AreEqual(typeof(Test).GetMethod("TestVoidMethod", new[] { typeof(int), typeof(int), typeof(int), typeof(int) }).Name, method.Name);
        }

        [Test]
        public void VoidMethodInfoWithFourParametersObjectType()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int>)o.TestVoidMethod);

            Assert.AreEqual(typeof(Test), method.ObjectType);
        }

        [Test]
        public void VoidMethodInfoWithFourParametersParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int>)o.TestVoidMethod);

            Assert.IsTrue(new[] { typeof(int), typeof(int), typeof(int), typeof(int) }.SequenceEqual(method.ParameterTypes));
        }

        [Test]
        public void VoidMethodInfoWithFourParametersReturnType()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int>)o.TestVoidMethod);

            Assert.AreEqual(typeof(void), method.ReturnType);
        }

        [Test]
        public void VoidMethodInfoWithFourParametersInvoke()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.Invoke(new Test(), 1, 2, 3, 4);
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2, 3, 4", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithFourParametersInvokeWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.Invoke(null, 1, 2, 3, 4);
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoWithFourParametersInvokePartiallyUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2, 3, 4 });
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2, 3, 4", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithFourParametersInvokePartiallyUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokePartiallyUntyped(null, new object[] { 1, 2, 3, 4 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoWithFourParametersInvokePartiallyUntypedEnumerable()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), (IEnumerable<object>)new object[] { 1, 2, 3, 4 });
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2, 3, 4", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithFourParametersInvokePartiallyUntypedEnumerableWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokePartiallyUntyped(null, (IEnumerable<object>)new object[] { 1, 2, 3, 4 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoWithFourParametersInvokePartiallyUntypedNoParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[0]);
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received no parameters, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithFourParametersInvokePartiallyUntypedWrongNumberOfParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5 });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithFourParametersInvokePartiallyUntypedMismatchedParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2, 3, "4" });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32, System.String }, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithFourParametersInvokeFullyUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, 3, 4 });
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2, 3, 4", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithFourParametersInvokeFullyUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, new object[] { 1, 2, 3, 4 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoWithFourParametersInvokeFullyUntypedEnumerable()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), (IEnumerable<object>)new object[] { 1, 2, 3, 4 });
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2, 3, 4", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithFourParametersInvokeFullyUntypedEnumerableWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, (IEnumerable<object>)new object[] { 1, 2, 3, 4 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoWithFourParametersInvokeFullyUntypedNoParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[0]);
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received no parameters, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithFourParametersInvokeFullyUntypedWrongNumberOfParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5 });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithFourParametersInvokeFullyUntypedMismatchedParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, 3, "4" });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32, System.String }, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithFourParametersSerialization()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int>)o.TestVoidMethod);
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();
            formatter.Serialize(memoryStream, method);
            memoryStream.Position = 0;
            IVoidMethodInfo<Test, int, int, int, int> result = (IVoidMethodInfo<Test, int, int, int, int>)formatter.Deserialize(memoryStream);

            Assert.AreEqual(method.MethodInfo, result.MethodInfo);
            Assert.AreEqual(method.Name, result.Name);
            Assert.AreEqual(method.ObjectType, result.ObjectType);
            Assert.IsTrue(method.ParameterTypes.SequenceEqual(result.ParameterTypes));
            Assert.AreEqual(method.ReturnType, result.ReturnType);
        }

        [Test]
        public void VoidMethodInfoWithFiveParametersMethodInfo()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int>)o.TestVoidMethod);

            Assert.AreEqual(typeof(Test).GetMethod("TestVoidMethod", new[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) }), method.MethodInfo);
        }

        [Test]
        public void VoidMethodInfoWithFiveParametersName()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int>)o.TestVoidMethod);

            Assert.AreEqual(typeof(Test).GetMethod("TestVoidMethod", new[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) }).Name, method.Name);
        }

        [Test]
        public void VoidMethodInfoWithFiveParametersObjectType()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int>)o.TestVoidMethod);

            Assert.AreEqual(typeof(Test), method.ObjectType);
        }

        [Test]
        public void VoidMethodInfoWithFiveParametersParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int>)o.TestVoidMethod);

            Assert.IsTrue(new[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) }.SequenceEqual(method.ParameterTypes));
        }

        [Test]
        public void VoidMethodInfoWithFiveParametersReturnType()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int>)o.TestVoidMethod);

            Assert.AreEqual(typeof(void), method.ReturnType);
        }

        [Test]
        public void VoidMethodInfoWithFiveParametersInvoke()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.Invoke(new Test(), 1, 2, 3, 4, 5);
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2, 3, 4, 5", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithFiveParametersInvokeWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.Invoke(null, 1, 2, 3, 4, 5);
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoWithFiveParametersInvokePartiallyUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5 });
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2, 3, 4, 5", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithFiveParametersInvokePartiallyUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokePartiallyUntyped(null, new object[] { 1, 2, 3, 4, 5 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoWithFiveParametersInvokePartiallyUntypedEnumerable()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), (IEnumerable<object>)new object[] { 1, 2, 3, 4, 5 });
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2, 3, 4, 5", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithFiveParametersInvokePartiallyUntypedEnumerableWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokePartiallyUntyped(null, (IEnumerable<object>)new object[] { 1, 2, 3, 4, 5 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoWithFiveParametersInvokePartiallyUntypedNoParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[0]);
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received no parameters, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithFiveParametersInvokePartiallyUntypedWrongNumberOfParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5, 6 });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithFiveParametersInvokePartiallyUntypedMismatchedParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2, 3, 4, "5" });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.String }, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithFiveParametersInvokeFullyUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5 });
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2, 3, 4, 5", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithFiveParametersInvokeFullyUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, new object[] { 1, 2, 3, 4, 5 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoWithFiveParametersInvokeFullyUntypedEnumerable()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), (IEnumerable<object>)new object[] { 1, 2, 3, 4, 5 });
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2, 3, 4, 5", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithFiveParametersInvokeFullyUntypedEnumerableWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, (IEnumerable<object>)new object[] { 1, 2, 3, 4, 5 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoWithFiveParametersInvokeFullyUntypedNoParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[0]);
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received no parameters, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithFiveParametersInvokeFullyUntypedWrongNumberOfParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5, 6 });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithFiveParametersInvokeFullyUntypedMismatchedParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, 3, 4, "5" });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.String }, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithFiveParametersSerialization()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int>)o.TestVoidMethod);
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();
            formatter.Serialize(memoryStream, method);
            memoryStream.Position = 0;
            IVoidMethodInfo<Test, int, int, int, int, int> result = (IVoidMethodInfo<Test, int, int, int, int, int>)formatter.Deserialize(memoryStream);

            Assert.AreEqual(method.MethodInfo, result.MethodInfo);
            Assert.AreEqual(method.Name, result.Name);
            Assert.AreEqual(method.ObjectType, result.ObjectType);
            Assert.IsTrue(method.ParameterTypes.SequenceEqual(result.ParameterTypes));
            Assert.AreEqual(method.ReturnType, result.ReturnType);
        }

        [Test]
        public void VoidMethodInfoWithSixParametersMethodInfo()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int>)o.TestVoidMethod);

            Assert.AreEqual(typeof(Test).GetMethod("TestVoidMethod", new[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) }), method.MethodInfo);
        }

        [Test]
        public void VoidMethodInfoWithSixParametersName()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int>)o.TestVoidMethod);

            Assert.AreEqual(typeof(Test).GetMethod("TestVoidMethod", new[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) }).Name, method.Name);
        }

        [Test]
        public void VoidMethodInfoWithSixParametersObjectType()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int>)o.TestVoidMethod);

            Assert.AreEqual(typeof(Test), method.ObjectType);
        }

        [Test]
        public void VoidMethodInfoWithSixParametersParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int>)o.TestVoidMethod);

            Assert.IsTrue(new[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) }.SequenceEqual(method.ParameterTypes));
        }

        [Test]
        public void VoidMethodInfoWithSixParametersReturnType()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int>)o.TestVoidMethod);

            Assert.AreEqual(typeof(void), method.ReturnType);
        }

        [Test]
        public void VoidMethodInfoWithSixParametersInvoke()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.Invoke(new Test(), 1, 2, 3, 4, 5, 6);
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2, 3, 4, 5, 6", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithSixParametersInvokeWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.Invoke(null, 1, 2, 3, 4, 5, 6);
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoWithSixParametersInvokePartiallyUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5, 6 });
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2, 3, 4, 5, 6", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithSixParametersInvokePartiallyUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokePartiallyUntyped(null, new object[] { 1, 2, 3, 4, 5, 6 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoWithSixParametersInvokePartiallyUntypedEnumerable()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), (IEnumerable<object>)new object[] { 1, 2, 3, 4, 5, 6 });
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2, 3, 4, 5, 6", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithSixParametersInvokePartiallyUntypedEnumerableWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokePartiallyUntyped(null, (IEnumerable<object>)new object[] { 1, 2, 3, 4, 5, 6 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoWithSixParametersInvokePartiallyUntypedNoParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[0]);
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received no parameters, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithSixParametersInvokePartiallyUntypedWrongNumberOfParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5, 6, 7 });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithSixParametersInvokePartiallyUntypedMismatchedParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5, "6" });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.String }, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithSixParametersInvokeFullyUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5, 6 });
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2, 3, 4, 5, 6", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithSixParametersInvokeFullyUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, new object[] { 1, 2, 3, 4, 5, 6 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoWithSixParametersInvokeFullyUntypedEnumerable()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), (IEnumerable<object>)new object[] { 1, 2, 3, 4, 5, 6 });
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2, 3, 4, 5, 6", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithSixParametersInvokeFullyUntypedEnumerableWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, (IEnumerable<object>)new object[] { 1, 2, 3, 4, 5, 6 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoWithSixParametersInvokeFullyUntypedNoParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[0]);
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received no parameters, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithSixParametersInvokeFullyUntypedWrongNumberOfParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5, 6, 7 });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithSixParametersInvokeFullyUntypedMismatchedParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5, "6" });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.String }, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithSixParametersSerialization()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int>)o.TestVoidMethod);
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();
            formatter.Serialize(memoryStream, method);
            memoryStream.Position = 0;
            IVoidMethodInfo<Test, int, int, int, int, int, int> result = (IVoidMethodInfo<Test, int, int, int, int, int, int>)formatter.Deserialize(memoryStream);

            Assert.AreEqual(method.MethodInfo, result.MethodInfo);
            Assert.AreEqual(method.Name, result.Name);
            Assert.AreEqual(method.ObjectType, result.ObjectType);
            Assert.IsTrue(method.ParameterTypes.SequenceEqual(result.ParameterTypes));
            Assert.AreEqual(method.ReturnType, result.ReturnType);
        }

        [Test]
        public void VoidMethodInfoWithSevenParametersMethodInfo()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int>)o.TestVoidMethod);

            Assert.AreEqual(typeof(Test).GetMethod("TestVoidMethod", new[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) }), method.MethodInfo);
        }

        [Test]
        public void VoidMethodInfoWithSevenParametersName()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int>)o.TestVoidMethod);

            Assert.AreEqual(typeof(Test).GetMethod("TestVoidMethod", new[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) }).Name, method.Name);
        }

        [Test]
        public void VoidMethodInfoWithSevenParametersObjectType()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int>)o.TestVoidMethod);

            Assert.AreEqual(typeof(Test), method.ObjectType);
        }

        [Test]
        public void VoidMethodInfoWithSevenParametersParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int>)o.TestVoidMethod);

            Assert.IsTrue(new[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) }.SequenceEqual(method.ParameterTypes));
        }

        [Test]
        public void VoidMethodInfoWithSevenParametersReturnType()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int>)o.TestVoidMethod);

            Assert.AreEqual(typeof(void), method.ReturnType);
        }

        [Test]
        public void VoidMethodInfoWithSevenParametersInvoke()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.Invoke(new Test(), 1, 2, 3, 4, 5, 6, 7);
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2, 3, 4, 5, 6, 7", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithSevenParametersInvokeWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.Invoke(null, 1, 2, 3, 4, 5, 6, 7);
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoWithSevenParametersInvokePartiallyUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5, 6, 7 });
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2, 3, 4, 5, 6, 7", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithSevenParametersInvokePartiallyUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokePartiallyUntyped(null, new object[] { 1, 2, 3, 4, 5, 6, 7 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoWithSevenParametersInvokePartiallyUntypedEnumerable()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), (IEnumerable<object>)new object[] { 1, 2, 3, 4, 5, 6, 7 });
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2, 3, 4, 5, 6, 7", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithSevenParametersInvokePartiallyUntypedEnumerableWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokePartiallyUntyped(null, (IEnumerable<object>)new object[] { 1, 2, 3, 4, 5, 6, 7 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoWithSevenParametersInvokePartiallyUntypedNoParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[0]);
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received no parameters, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithSevenParametersInvokePartiallyUntypedWrongNumberOfParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5, 6, 7, 8 });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithSevenParametersInvokePartiallyUntypedMismatchedParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5, 6, "7" });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.String }, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithSevenParametersInvokeFullyUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5, 6, 7 });
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2, 3, 4, 5, 6, 7", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithSevenParametersInvokeFullyUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, new object[] { 1, 2, 3, 4, 5, 6, 7 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoWithSevenParametersInvokeFullyUntypedEnumerable()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), (IEnumerable<object>)new object[] { 1, 2, 3, 4, 5, 6, 7 });
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2, 3, 4, 5, 6, 7", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithSevenParametersInvokeFullyUntypedEnumerableWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, (IEnumerable<object>)new object[] { 1, 2, 3, 4, 5, 6, 7 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoWithSevenParametersInvokeFullyUntypedNoParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[0]);
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received no parameters, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithSevenParametersInvokeFullyUntypedWrongNumberOfParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5, 6, 7, 8 });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithSevenParametersInvokeFullyUntypedMismatchedParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5, 6, "7" });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.String }, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithSevenParametersSerialization()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int>)o.TestVoidMethod);
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();
            formatter.Serialize(memoryStream, method);
            memoryStream.Position = 0;
            IVoidMethodInfo<Test, int, int, int, int, int, int, int> result = (IVoidMethodInfo<Test, int, int, int, int, int, int, int>)formatter.Deserialize(memoryStream);

            Assert.AreEqual(method.MethodInfo, result.MethodInfo);
            Assert.AreEqual(method.Name, result.Name);
            Assert.AreEqual(method.ObjectType, result.ObjectType);
            Assert.IsTrue(method.ParameterTypes.SequenceEqual(result.ParameterTypes));
            Assert.AreEqual(method.ReturnType, result.ReturnType);
        }

        [Test]
        public void VoidMethodInfoWithEightParametersMethodInfo()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int, int>)o.TestVoidMethod);

            Assert.AreEqual(typeof(Test).GetMethod("TestVoidMethod", new[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) }), method.MethodInfo);
        }

        [Test]
        public void VoidMethodInfoWithEightParametersName()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int, int>)o.TestVoidMethod);

            Assert.AreEqual(typeof(Test).GetMethod("TestVoidMethod", new[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) }).Name, method.Name);
        }

        [Test]
        public void VoidMethodInfoWithEightParametersObjectType()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int, int>)o.TestVoidMethod);

            Assert.AreEqual(typeof(Test), method.ObjectType);
        }

        [Test]
        public void VoidMethodInfoWithEightParametersParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int, int>)o.TestVoidMethod);

            Assert.IsTrue(new[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) }.SequenceEqual(method.ParameterTypes));
        }

        [Test]
        public void VoidMethodInfoWithEightParametersReturnType()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int, int>)o.TestVoidMethod);

            Assert.AreEqual(typeof(void), method.ReturnType);
        }

        [Test]
        public void VoidMethodInfoWithEightParametersInvoke()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int, int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.Invoke(new Test(), 1, 2, 3, 4, 5, 6, 7, 8);
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2, 3, 4, 5, 6, 7, 8", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithEightParametersInvokeWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int, int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.Invoke(null, 1, 2, 3, 4, 5, 6, 7, 8);
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoWithEightParametersInvokePartiallyUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int, int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5, 6, 7, 8 });
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2, 3, 4, 5, 6, 7, 8", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithEightParametersInvokePartiallyUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int, int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokePartiallyUntyped(null, new object[] { 1, 2, 3, 4, 5, 6, 7, 8 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoWithEightParametersInvokePartiallyUntypedEnumerable()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int, int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), (IEnumerable<object>)new object[] { 1, 2, 3, 4, 5, 6, 7, 8 });
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2, 3, 4, 5, 6, 7, 8", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithEightParametersInvokePartiallyUntypedEnumerableWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int, int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokePartiallyUntyped(null, (IEnumerable<object>)new object[] { 1, 2, 3, 4, 5, 6, 7, 8 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoWithEightParametersInvokePartiallyUntypedNoParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int, int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[0]);
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received no parameters, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithEightParametersInvokePartiallyUntypedWrongNumberOfParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int, int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithEightParametersInvokePartiallyUntypedMismatchedParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int, int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokePartiallyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5, 6, 7, "8" });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.String }, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithEightParametersInvokeFullyUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int, int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5, 6, 7, 8 });
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2, 3, 4, 5, 6, 7, 8", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithEightParametersInvokeFullyUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int, int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, new object[] { 1, 2, 3, 4, 5, 6, 7, 8 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoWithEightParametersInvokeFullyUntypedEnumerable()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int, int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), (IEnumerable<object>)new object[] { 1, 2, 3, 4, 5, 6, 7, 8 });
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2, 3, 4, 5, 6, 7, 8", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithEightParametersInvokeFullyUntypedEnumerableWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int, int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeFullyUntyped(null, (IEnumerable<object>)new object[] { 1, 2, 3, 4, 5, 6, 7, 8 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
        }

        [Test]
        public void VoidMethodInfoWithEightParametersInvokeFullyUntypedNoParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int, int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[0]);
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received no parameters, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithEightParametersInvokeFullyUntypedWrongNumberOfParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int, int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithEightParametersInvokeFullyUntypedMismatchedParameterTypes()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int, int>)o.TestVoidMethod);

            ArgumentException actual = null;
            try
            {
                method.InvokeFullyUntyped(new Test(), new object[] { 1, 2, 3, 4, 5, 6, 7, "8" });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Received parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.String }, but expected parameters of type { System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32 }." + Environment.NewLine + "Parameter name: parameters", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithEightParametersSerialization()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int, int>)o.TestVoidMethod);
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();
            formatter.Serialize(memoryStream, method);
            memoryStream.Position = 0;
            IVoidMethodInfo<Test, int, int, int, int, int, int, int, int> result = (IVoidMethodInfo<Test, int, int, int, int, int, int, int, int>)formatter.Deserialize(memoryStream);

            Assert.AreEqual(method.MethodInfo, result.MethodInfo);
            Assert.AreEqual(method.Name, result.Name);
            Assert.AreEqual(method.ObjectType, result.ObjectType);
            Assert.IsTrue(method.ParameterTypes.SequenceEqual(result.ParameterTypes));
            Assert.AreEqual(method.ReturnType, result.ReturnType);
        }

        private class Test
        {
            // ReSharper disable UnusedMember.Local
            public string TestReflectionWrapperMethod(ref int parameter1, out int parameter2)
            // ReSharper restore UnusedMember.Local
            {
                parameter1++;
				parameter2 = parameter1 + 1;
                return (parameter1 + parameter2).ToString(CultureInfo.InvariantCulture);
            }

            // ReSharper disable UnusedMember.Local
            public string TestMethodUnique(int parameter1, int parameter2)
            // ReSharper restore UnusedMember.Local
            {
                return (parameter1 + parameter2).ToString(CultureInfo.InvariantCulture);
            }

            // ReSharper disable UnusedMember.Local
            public string TestMethodUniqueNoParameters()
            // ReSharper restore UnusedMember.Local
            {
                return null;
            }

            // ReSharper disable UnusedMember.Local
            public void TestVoidMethodUnique(int parameter1, int parameter2, int parameter3)
            // ReSharper restore UnusedMember.Local
            {
                throw new Exception(parameter1 + ", " + parameter2 + ", " + parameter3);
            }

            // ReSharper disable UnusedMember.Local
            public void TestVoidMethodUniqueNoParameters()
            // ReSharper restore UnusedMember.Local
            {
            }

            public string TestMethod()
            {
                return (0).ToString(CultureInfo.InvariantCulture);
            }

            public string TestMethod(int parameter1)
            {
                return (1).ToString(CultureInfo.InvariantCulture);
            }

            public string TestMethod(int parameter1, int parameter2)
            {
                return (1 + 2).ToString(CultureInfo.InvariantCulture);
            }

            public string TestMethod(int parameter1, int parameter2, int parameter3)
            {
                return (1 + 2 + 3).ToString(CultureInfo.InvariantCulture);
            }

            public string TestMethod(int parameter1, int parameter2, int parameter3, int parameter4)
            {
                return (1 + 2 + 3 + 4).ToString(CultureInfo.InvariantCulture);
            }

            public string TestMethod(int parameter1, int parameter2, int parameter3, int parameter4, int parameter5)
            {
                return (1 + 2 + 3 + 4 + 5).ToString(CultureInfo.InvariantCulture);
            }

            public string TestMethod(int parameter1, int parameter2, int parameter3, int parameter4, int parameter5, int parameter6)
            {
                return (1 + 2 + 3 + 4 + 5 + 6).ToString(CultureInfo.InvariantCulture);
            }

            public string TestMethod(int parameter1, int parameter2, int parameter3, int parameter4, int parameter5, int parameter6, int parameter7)
            {
                return (1 + 2 + 3 + 4 + 5 + 6 + 7).ToString(CultureInfo.InvariantCulture);
            }

            public string TestMethod(int parameter1, int parameter2, int parameter3, int parameter4, int parameter5, int parameter6, int parameter7, int parameter8)
            {
                return (1 + 2 + 3 + 4 + 5 + 6 + 7 + 8).ToString(CultureInfo.InvariantCulture);
            }

            public void TestVoidMethod()
            {
                throw new Exception("0");
            }

            public void TestVoidMethod(int parameter1)
            {
                throw new Exception("1");
            }

            public void TestVoidMethod(int parameter1, int parameter2)
            {
                throw new Exception("1, 2");
            }

            public void TestVoidMethod(int parameter1, int parameter2, int parameter3)
            {
                throw new Exception("1, 2, 3");
            }

            public void TestVoidMethod(int parameter1, int parameter2, int parameter3, int parameter4)
            {
                throw new Exception("1, 2, 3, 4");
            }

            public void TestVoidMethod(int parameter1, int parameter2, int parameter3, int parameter4, int parameter5)
            {
                throw new Exception("1, 2, 3, 4, 5");
            }

            public void TestVoidMethod(int parameter1, int parameter2, int parameter3, int parameter4, int parameter5, int parameter6)
            {
                throw new Exception("1, 2, 3, 4, 5, 6");
            }

            public void TestVoidMethod(int parameter1, int parameter2, int parameter3, int parameter4, int parameter5, int parameter6, int parameter7)
            {
                throw new Exception("1, 2, 3, 4, 5, 6, 7");
            }

            public void TestVoidMethod(int parameter1, int parameter2, int parameter3, int parameter4, int parameter5, int parameter6, int parameter7, int parameter8)
            {
                throw new Exception("1, 2, 3, 4, 5, 6, 7, 8");
            }
        }
    }
}