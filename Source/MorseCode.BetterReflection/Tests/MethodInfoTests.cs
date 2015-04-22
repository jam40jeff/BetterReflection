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
        public void ReflectionWrapperMethodInfoPartiallyUntypedInvokeUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestReflectionWrapperMethod");

            object[] arguments = new object[] { 1, 2 };
            Assert.AreEqual("5", method.InvokeUntyped(new Test(), arguments));
			Assert.AreEqual(2, arguments[0]);
			Assert.AreEqual(3, arguments[1]);
        }

        [Test]
        public void ReflectionWrapperMethodInfoPartiallyUntypedInvokeUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestReflectionWrapperMethod");

            ArgumentNullException actual = null;
            try
            {
                method.InvokeUntyped(null, new object[] { 1, 2 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
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
        public void MethodInfoPartiallyUntypedInvokeUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestMethodUnique");

            Assert.AreEqual("3", method.InvokeUntyped(new Test(), new object[] { 1, 2 }));
        }

        [Test]
        public void MethodInfoPartiallyUntypedInvokeUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestMethodUnique");

            ArgumentNullException actual = null;
            try
            {
                method.InvokeUntyped(null, new object[] { 1, 2 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
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
        public void VoidMethodInfoPartiallyUntypedInvokeUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestVoidMethodUnique");

            Exception actual = null;
            try
            {
                method.InvokeUntyped(new Test(), new object[] { 1, 2, 3 });
            }
            catch (Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2, 3", actual.Message);
        }

        [Test]
        public void VoidMethodInfoPartiallyUntypedInvokeUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> method = typeInfo.GetMethod("TestVoidMethodUnique");

            ArgumentNullException actual = null;
            try
            {
                method.InvokeUntyped(null, new object[] { 1, 2, 3 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
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
        public void MethodInfoWithNoParametersInvokeUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, string> method = typeInfo.GetMethod(o => (Func<string>)o.TestMethod);

            Assert.AreEqual("0", method.InvokeUntyped(new Test(), new object[0]));
        }

        [Test]
        public void MethodInfoWithNoParametersInvokeUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, string> method = typeInfo.GetMethod(o => (Func<string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeUntyped(null, new object[0]);
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
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
        public void MethodInfoWithOneParameterInvokeUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, string> method = typeInfo.GetMethod(o => (Func<int, string>)o.TestMethod);

            Assert.AreEqual("1", method.InvokeUntyped(new Test(), new object[] { 1 }));
        }

        [Test]
        public void MethodInfoWithOneParameterInvokeUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, string> method = typeInfo.GetMethod(o => (Func<int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeUntyped(null, new object[] { 1 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
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
        public void MethodInfoWithTwoParametersInvokeUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, string>)o.TestMethod);

            Assert.AreEqual("3", method.InvokeUntyped(new Test(), new object[] { 1, 2 }));
        }

        [Test]
        public void MethodInfoWithTwoParametersInvokeUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeUntyped(null, new object[] { 1, 2 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
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
        public void MethodInfoWithThreeParametersInvokeUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, string>)o.TestMethod);

            Assert.AreEqual("6", method.InvokeUntyped(new Test(), new object[] { 1, 2, 3 }));
        }

        [Test]
        public void MethodInfoWithThreeParametersInvokeUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeUntyped(null, new object[] { 1, 2, 3 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
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
        public void MethodInfoWithFourParametersInvokeUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual("10", method.InvokeUntyped(new Test(), new object[] { 1, 2, 3, 4 }));
        }

        [Test]
        public void MethodInfoWithFourParametersInvokeUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeUntyped(null, new object[] { 1, 2, 3, 4 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
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
        public void MethodInfoWithFiveParametersInvokeUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual("15", method.InvokeUntyped(new Test(), new object[] { 1, 2, 3, 4, 5 }));
        }

        [Test]
        public void MethodInfoWithFiveParametersInvokeUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeUntyped(null, new object[] { 1, 2, 3, 4, 5 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
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
        public void MethodInfoWithSixParametersInvokeUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual("21", method.InvokeUntyped(new Test(), new object[] { 1, 2, 3, 4, 5, 6 }));
        }

        [Test]
        public void MethodInfoWithSixParametersInvokeUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeUntyped(null, new object[] { 1, 2, 3, 4, 5, 6 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
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
        public void MethodInfoWithSevenParametersInvokeUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual("28", method.InvokeUntyped(new Test(), new object[] { 1, 2, 3, 4, 5, 6, 7 }));
        }

        [Test]
        public void MethodInfoWithSevenParametersInvokeUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeUntyped(null, new object[] { 1, 2, 3, 4, 5, 6, 7 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
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
        public void MethodInfoWithEightParametersInvokeUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, int, string>)o.TestMethod);

            Assert.AreEqual("36", method.InvokeUntyped(new Test(), new object[] { 1, 2, 3, 4, 5, 6, 7, 8 }));
        }

        [Test]
        public void MethodInfoWithEightParametersInvokeUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, int, string>)o.TestMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeUntyped(null, new object[] { 1, 2, 3, 4, 5, 6, 7, 8 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
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
        public void VoidMethodInfoWithNoParametersInvokeUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test> method = typeInfo.GetVoidMethod(o => (Action)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.InvokeUntyped(new Test(), new object[0]);
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("0", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithNoParametersInvokeUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test> method = typeInfo.GetVoidMethod(o => (Action)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeUntyped(null, new object[0]);
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
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
        public void VoidMethodInfoWithOneParameterInvokeUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int> method = typeInfo.GetVoidMethod(o => (Action<int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.InvokeUntyped(new Test(), new object[] { 1 });
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithOneParameterInvokeUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int> method = typeInfo.GetVoidMethod(o => (Action<int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeUntyped(null, new object[] { 1 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
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
        public void VoidMethodInfoWithTwoParametersInvokeUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.InvokeUntyped(new Test(), new object[] { 1, 2 });
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithTwoParametersInvokeUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeUntyped(null, new object[] { 1, 2 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
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
        public void VoidMethodInfoWithThreeParametersInvokeUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.InvokeUntyped(new Test(), new object[] { 1, 2, 3 });
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2, 3", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithThreeParametersInvokeUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeUntyped(null, new object[] { 1, 2, 3 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
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
        public void VoidMethodInfoWithFourParametersInvokeUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.InvokeUntyped(new Test(), new object[] { 1, 2, 3, 4 });
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2, 3, 4", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithFourParametersInvokeUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeUntyped(null, new object[] { 1, 2, 3, 4 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
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
        public void VoidMethodInfoWithFiveParametersInvokeUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.InvokeUntyped(new Test(), new object[] { 1, 2, 3, 4, 5 });
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2, 3, 4, 5", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithFiveParametersInvokeUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeUntyped(null, new object[] { 1, 2, 3, 4, 5 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
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
        public void VoidMethodInfoWithSixParametersInvokeUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.InvokeUntyped(new Test(), new object[] { 1, 2, 3, 4, 5, 6 });
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2, 3, 4, 5, 6", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithSixParametersInvokeUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeUntyped(null, new object[] { 1, 2, 3, 4, 5, 6 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
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
        public void VoidMethodInfoWithSevenParametersInvokeUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.InvokeUntyped(new Test(), new object[] { 1, 2, 3, 4, 5, 6, 7 });
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2, 3, 4, 5, 6, 7", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithSevenParametersInvokeUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeUntyped(null, new object[] { 1, 2, 3, 4, 5, 6, 7 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
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
        public void VoidMethodInfoWithEightParametersInvokeUntyped()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int, int>)o.TestVoidMethod);

            Exception actual = null;
            try
            {
                method.InvokeUntyped(new Test(), new object[] { 1, 2, 3, 4, 5, 6, 7, 8 });
            }
            catch(Exception e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("1, 2, 3, 4, 5, 6, 7, 8", actual.Message);
        }

        [Test]
        public void VoidMethodInfoWithEightParametersInvokeUntypedWithNull()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int, int>)o.TestVoidMethod);

            ArgumentNullException actual = null;
            try
            {
                method.InvokeUntyped(null, new object[] { 1, 2, 3, 4, 5, 6, 7, 8 });
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("o", actual.ParamName);
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
            public void TestVoidMethodUnique(int parameter1, int parameter2, int parameter3)
            // ReSharper restore UnusedMember.Local
            {
                throw new Exception(parameter1 + ", " + parameter2 + ", " + parameter3);
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