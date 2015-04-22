#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeInfoTests.cs" company="MorseCode Software">
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
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization.Formatters.Binary;

    using MorseCode.FrameworkExtensions;

    using NUnit.Framework;

    [TestFixture]
    public class TypeInfoTests
    {
        #region Public Methods and Operators

        [Test]
        public void FullName()
        {
            ITypeInfo<DateTime> typeInfo = TypeInfoFactory.GetTypeInfo<DateTime>();

            Assert.AreEqual(typeof(DateTime).FullName, typeInfo.FullName);
        }

        [Test]
        public void GetMethod()
        {
            ITypeInfo<DateTime> typeInfo = TypeInfoFactory.GetTypeInfo<DateTime>();
            IMethodInfo<DateTime, char, IFormatProvider, string[]> getDateTimeFormatsMethod = typeInfo.GetMethod(o => (Func<char, IFormatProvider, string[]>)o.GetDateTimeFormats);

            Assert.IsNotNull(getDateTimeFormatsMethod);
            Assert.AreSame(typeof(DateTime).GetMethod("GetDateTimeFormats", new[] { typeof(char), typeof(IFormatProvider) }), getDateTimeFormatsMethod.MethodInfo);
        }

        [Test]
        public void GetMethodByName()
        {
            ITypeInfo<DateTime> typeInfo = TypeInfoFactory.GetTypeInfo<DateTime>();
            IMethodInfo<DateTime> addMethod = typeInfo.GetMethod("Add");

            Assert.IsNotNull(addMethod);
            Assert.AreSame(typeof(DateTime).GetMethod("Add"), addMethod.MethodInfo);
        }

        [Test]
        public void GetMethodByNameAndTypes()
        {
            ITypeInfo<DateTime> typeInfo = TypeInfoFactory.GetTypeInfo<DateTime>();
            IMethodInfo<DateTime> getDateTimeFormatsMethod = typeInfo.GetMethod("GetDateTimeFormats", new[] { typeof(char), typeof(IFormatProvider) });

            Assert.IsNotNull(getDateTimeFormatsMethod);
            Assert.AreSame(typeof(DateTime).GetMethod("GetDateTimeFormats", new[] { typeof(char), typeof(IFormatProvider) }), getDateTimeFormatsMethod.MethodInfo);
        }

        [Test]
        public void GetMethodByNameAndTypesWithWrongName()
        {
            ITypeInfo<DateTime> typeInfo = TypeInfoFactory.GetTypeInfo<DateTime>();

            ArgumentException actual = null;
            try
            {
                typeInfo.GetMethod("GetDateTimeFormats2", new[] { typeof(char), typeof(IFormatProvider) });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("No public instance method was found with name GetDateTimeFormats2 and types { " + typeof(char).FullName + ", " + typeof(IFormatProvider).FullName + " } on type " + typeof(DateTime).FullName + ".", actual.Message);
        }

        [Test]
        public void GetMethodByNameAndTypesWithWrongTypes()
        {
            ITypeInfo<DateTime> typeInfo = TypeInfoFactory.GetTypeInfo<DateTime>();

            ArgumentException actual = null;
            try
            {
                typeInfo.GetMethod("GetDateTimeFormats", new[] { typeof(int), typeof(IFormatProvider) });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("No public instance method was found with name GetDateTimeFormats and types { " + typeof(int).FullName + ", " + typeof(IFormatProvider).FullName + " } on type " + typeof(DateTime).FullName + ".", actual.Message);
        }

        [Test]
        public void GetMethodByNameWithWrongName()
        {
            ITypeInfo<DateTime> typeInfo = TypeInfoFactory.GetTypeInfo<DateTime>();

            ArgumentException actual = null;
            try
            {
                typeInfo.GetMethod("Add2");
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("No public instance method was found with name Add2 on type " + typeof(DateTime).FullName + ".", actual.Message);
        }

        [Test]
        public void GetMethodWithEightParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, int, string>)o.TestMethod);

            Assert.IsNotNull(method);
            Assert.AreSame(typeof(Test).GetMethod("TestMethod", Enumerable.Range(0, 8).Select(_ => typeof(int)).ToArray()), method.MethodInfo);
        }

        [Test]
        public void GetMethodWithFiveParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, string>)o.TestMethod);

            Assert.IsNotNull(method);
            Assert.AreSame(typeof(Test).GetMethod("TestMethod", Enumerable.Range(0, 5).Select(_ => typeof(int)).ToArray()), method.MethodInfo);
        }

        [Test]
        public void GetMethodWithFourParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, string>)o.TestMethod);

            Assert.IsNotNull(method);
            Assert.AreSame(typeof(Test).GetMethod("TestMethod", Enumerable.Range(0, 4).Select(_ => typeof(int)).ToArray()), method.MethodInfo);
        }

        [Test]
        public void GetMethodWithNoParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, string> method = typeInfo.GetMethod(o => (Func<string>)o.TestMethod);

            Assert.IsNotNull(method);
            Assert.AreSame(typeof(Test).GetMethod("TestMethod", System.Type.EmptyTypes), method.MethodInfo);
        }

        [Test]
        public void GetMethodWithOneParameter()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, string> method = typeInfo.GetMethod(o => (Func<int, string>)o.TestMethod);

            Assert.IsNotNull(method);
            Assert.AreSame(typeof(Test).GetMethod("TestMethod", Enumerable.Range(0, 1).Select(_ => typeof(int)).ToArray()), method.MethodInfo);
        }

        [Test]
        public void GetMethodWithOutParameterByName()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> testMethodMethod = typeInfo.GetMethod("TestMethodWithOutParameter");

            Assert.IsNotNull(testMethodMethod);
            Assert.AreSame(typeof(Test).GetMethod("TestMethodWithOutParameter"), testMethodMethod.MethodInfo);
        }

        [Test]
        public void GetMethodWithRefParameterByName()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> testMethodMethod = typeInfo.GetMethod("TestMethodWithRefParameter");

            Assert.IsNotNull(testMethodMethod);
            Assert.AreSame(typeof(Test).GetMethod("TestMethodWithRefParameter"), testMethodMethod.MethodInfo);
        }

        [Test]
        public void GetMethodWithSevenParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, int, string>)o.TestMethod);

            Assert.IsNotNull(method);
            Assert.AreSame(typeof(Test).GetMethod("TestMethod", Enumerable.Range(0, 7).Select(_ => typeof(int)).ToArray()), method.MethodInfo);
        }

        [Test]
        public void GetMethodWithSixParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, int, int, int, string>)o.TestMethod);

            Assert.IsNotNull(method);
            Assert.AreSame(typeof(Test).GetMethod("TestMethod", Enumerable.Range(0, 6).Select(_ => typeof(int)).ToArray()), method.MethodInfo);
        }

        [Test]
        public void GetMethodWithThreeParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, int, string>)o.TestMethod);

            Assert.IsNotNull(method);
            Assert.AreSame(typeof(Test).GetMethod("TestMethod", Enumerable.Range(0, 3).Select(_ => typeof(int)).ToArray()), method.MethodInfo);
        }

        [Test]
        public void GetMethodWithTwoParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test, int, int, string> method = typeInfo.GetMethod(o => (Func<int, int, string>)o.TestMethod);

            Assert.IsNotNull(method);
            Assert.AreSame(typeof(Test).GetMethod("TestMethod", Enumerable.Range(0, 2).Select(_ => typeof(int)).ToArray()), method.MethodInfo);
        }

        [Test]
        public void GetMethods()
        {
            ITypeInfo<DateTime> typeInfo = TypeInfoFactory.GetTypeInfo<DateTime>();
            IReadOnlyList<IMethodInfo<DateTime>> methods = typeInfo.GetMethods().ToArray();

            IReadOnlyList<MethodInfo> expectedMethods = typeof(DateTime).GetMethods(BindingFlags.Instance | BindingFlags.Public);

            Assert.IsNotNull(methods);
            Assert.IsNotNull(expectedMethods);
            Assert.IsTrue(methods.Select(m => m.MethodInfo).SetEqual(expectedMethods, ReferenceEqualityComparer.Instance));
        }

        [Test]
        public void GetProperties()
        {
            ITypeInfo<DateTime> typeInfo = TypeInfoFactory.GetTypeInfo<DateTime>();
            IReadOnlyList<IPropertyInfo<DateTime>> properties = typeInfo.GetProperties().ToArray();

            IReadOnlyList<PropertyInfo> expectedProperties = typeof(DateTime).GetProperties(BindingFlags.Instance | BindingFlags.Public);

            Assert.IsNotNull(properties);
            Assert.IsNotNull(expectedProperties);
            Assert.IsTrue(properties.Select(m => m.PropertyInfo).SetEqual(expectedProperties, ReferenceEqualityComparer.Instance));
        }

        [Test]
        public void GetPropertyByName()
        {
            ITypeInfo<DateTime> typeInfo = TypeInfoFactory.GetTypeInfo<DateTime>();
            IPropertyInfo<DateTime> addProperty = typeInfo.GetProperty("Day");

            Assert.IsNotNull(addProperty);
            Assert.AreSame(typeof(DateTime).GetProperty("Day"), addProperty.PropertyInfo);
        }

        [Test]
        public void GetPropertyByNameWithWrongName()
        {
            ITypeInfo<DateTime> typeInfo = TypeInfoFactory.GetTypeInfo<DateTime>();

            ArgumentException actual = null;
            try
            {
                typeInfo.GetProperty("Day2");
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("No public instance property was found with name Day2 on type " + typeof(DateTime).FullName + ".", actual.Message);
        }

        [Test]
        public void UntypedGetProperties()
        {
            ITypeInfo typeInfo = TypeInfoFactory.GetTypeInfo(typeof(DateTime));
            IReadOnlyList<IPropertyInfo> properties = typeInfo.GetProperties().ToArray();

            IReadOnlyList<PropertyInfo> expectedProperties = typeof(DateTime).GetProperties(BindingFlags.Instance | BindingFlags.Public);

            Assert.IsNotNull(properties);
            Assert.IsNotNull(expectedProperties);
            Assert.IsTrue(properties.Select(m => m.PropertyInfo).SetEqual(expectedProperties, ReferenceEqualityComparer.Instance));
        }

        [Test]
        public void UntypedGetPropertyByName()
        {
            ITypeInfo typeInfo = TypeInfoFactory.GetTypeInfo(typeof(DateTime));
            IPropertyInfo addProperty = typeInfo.GetProperty("Day");

            Assert.IsNotNull(addProperty);
            Assert.AreSame(typeof(DateTime).GetProperty("Day"), addProperty.PropertyInfo);
        }

        [Test]
        public void UntypedGetPropertyByNameWithWrongName()
        {
            ITypeInfo typeInfo = TypeInfoFactory.GetTypeInfo(typeof(DateTime));

            ArgumentException actual = null;
            try
            {
                typeInfo.GetProperty("Day2");
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("No public instance property was found with name Day2 on type " + typeof(DateTime).FullName + ".", actual.Message);
        }

        [Test]
        public void Name()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();

            Assert.AreEqual(typeof(Test).Name, typeInfo.Name);
        }

        [Test]
        public void Type()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();

            Assert.AreEqual(typeof(Test), typeInfo.Type);
        }

        [Test]
        public void GetReadWriteProperty()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IWritablePropertyInfo<Test, int> readWritePropertyProperty = typeInfo.GetReadWriteProperty(o => o.ReadWriteProperty);

            Assert.IsNotNull(readWritePropertyProperty);
            Assert.AreSame(typeof(Test).GetProperty("ReadWriteProperty"), readWritePropertyProperty.PropertyInfo);
        }

        [Test]
        public void GetReadWritePropertyNotWritable()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();

            InvalidOperationException actual = null;
            try
            {
                typeInfo.GetReadWriteProperty(o => o.ReadableProperty);
            }
            catch (InvalidOperationException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.IsNotNull("Property with name ReadableProperty on type " + typeof(Test).FullName + " is not readable and writable.", actual.Message);
        }

        [Test]
        public void GetReadableProperty()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IReadablePropertyInfo<Test, int> readablePropertyProperty = typeInfo.GetReadableProperty(o => o.ReadableProperty);

            Assert.IsNotNull(readablePropertyProperty);
            Assert.AreSame(typeof(Test).GetProperty("ReadableProperty"), readablePropertyProperty.PropertyInfo);
        }

        [Test]
        public void GetReadablePropertyOnField()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();

            InvalidOperationException actual = null;
            try
            {
                typeInfo.GetReadableProperty(o => o.Field);
            }
            catch (InvalidOperationException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Expression is not a property.", actual.Message);
        }

        [Test]
        public void GetReadablePropertyOnReadWriteProperty()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IReadablePropertyInfo<Test, int> readablePropertyProperty = typeInfo.GetReadableProperty(o => o.ReadWriteProperty);

            Assert.IsNotNull(readablePropertyProperty);
            Assert.AreSame(typeof(Test).GetProperty("ReadWriteProperty"), readablePropertyProperty.PropertyInfo);
        }

        [Test]
        public void Serialization()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();
            formatter.Serialize(memoryStream, typeInfo);
            memoryStream.Position = 0;
            ITypeInfo<Test> result = (ITypeInfo<Test>)formatter.Deserialize(memoryStream);

            Assert.AreEqual(typeInfo.FullName, result.FullName);
            Assert.AreEqual(typeInfo.Name, result.Name);
            Assert.AreEqual(typeInfo.Type, result.Type);
        }

        [Test]
        public void GetVoidMethod()
        {
            ITypeInfo<List<string>> typeInfo = TypeInfoFactory.GetTypeInfo<List<string>>();
            IVoidMethodInfo<List<string>, int, int, IComparer<string>> sortMethod = typeInfo.GetVoidMethod(o => (Action<int, int, IComparer<string>>)o.Sort);

            Assert.IsNotNull(sortMethod);
            Assert.AreSame(typeof(List<string>).GetMethod("Sort", new[] { typeof(int), typeof(int), typeof(IComparer<string>) }), sortMethod.MethodInfo);
        }

        [Test]
        public void GetVoidMethodWithEightParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int, int>)o.TestVoidMethod);

            Assert.IsNotNull(method);
            Assert.AreSame(typeof(Test).GetMethod("TestVoidMethod", Enumerable.Range(0, 8).Select(_ => typeof(int)).ToArray()), method.MethodInfo);
        }

        [Test]
        public void GetVoidMethodWithFiveParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int>)o.TestVoidMethod);

            Assert.IsNotNull(method);
            Assert.AreSame(typeof(Test).GetMethod("TestVoidMethod", Enumerable.Range(0, 5).Select(_ => typeof(int)).ToArray()), method.MethodInfo);
        }

        [Test]
        public void GetVoidMethodWithFourParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int>)o.TestVoidMethod);

            Assert.IsNotNull(method);
            Assert.AreSame(typeof(Test).GetMethod("TestVoidMethod", Enumerable.Range(0, 4).Select(_ => typeof(int)).ToArray()), method.MethodInfo);
        }

        [Test]
        public void GetVoidMethodWithNoParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test> method = typeInfo.GetVoidMethod(o => (Action)o.TestVoidMethod);

            Assert.IsNotNull(method);
            Assert.AreSame(typeof(Test).GetMethod("TestVoidMethod", System.Type.EmptyTypes), method.MethodInfo);
        }

        [Test]
        public void GetVoidMethodWithOneParameter()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int> method = typeInfo.GetVoidMethod(o => (Action<int>)o.TestVoidMethod);

            Assert.IsNotNull(method);
            Assert.AreSame(typeof(Test).GetMethod("TestVoidMethod", Enumerable.Range(0, 1).Select(_ => typeof(int)).ToArray()), method.MethodInfo);
        }

        [Test]
        public void GetVoidMethodWithSevenParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int, int>)o.TestVoidMethod);

            Assert.IsNotNull(method);
            Assert.AreSame(typeof(Test).GetMethod("TestVoidMethod", Enumerable.Range(0, 7).Select(_ => typeof(int)).ToArray()), method.MethodInfo);
        }

        [Test]
        public void GetVoidMethodWithSixParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int, int, int, int>)o.TestVoidMethod);

            Assert.IsNotNull(method);
            Assert.AreSame(typeof(Test).GetMethod("TestVoidMethod", Enumerable.Range(0, 6).Select(_ => typeof(int)).ToArray()), method.MethodInfo);
        }

        [Test]
        public void GetVoidMethodWithThreeParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int, int>)o.TestVoidMethod);

            Assert.IsNotNull(method);
            Assert.AreSame(typeof(Test).GetMethod("TestVoidMethod", Enumerable.Range(0, 3).Select(_ => typeof(int)).ToArray()), method.MethodInfo);
        }

        [Test]
        public void GetVoidMethodWithTwoParameters()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IVoidMethodInfo<Test, int, int> method = typeInfo.GetVoidMethod(o => (Action<int, int>)o.TestVoidMethod);

            Assert.IsNotNull(method);
            Assert.AreSame(typeof(Test).GetMethod("TestVoidMethod", Enumerable.Range(0, 2).Select(_ => typeof(int)).ToArray()), method.MethodInfo);
        }

        [Test]
        public void GetWritableProperty()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IWritablePropertyInfo<Test, int> writablePropertyProperty = typeInfo.GetWritableProperty(o => o.ReadWriteProperty);

            Assert.IsNotNull(writablePropertyProperty);
            Assert.AreSame(typeof(Test).GetProperty("ReadWriteProperty"), writablePropertyProperty.PropertyInfo);
        }

        [Test]
        public void GetWritablePropertyNotWritable()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();

            InvalidOperationException actual = null;
            try
            {
                typeInfo.GetWritableProperty(o => o.ReadableProperty);
            }
            catch (InvalidOperationException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.IsNotNull("Property with name ReadableProperty on type " + typeof(Test).FullName + " is not writable.", actual.Message);
        }

        [Test]
        public void TypeInfoFactoryGetTypeInfo()
        {
            ITypeInfo<DateTime> typeInfo = TypeInfoFactory.GetTypeInfo<DateTime>();

            Assert.IsNotNull(typeInfo);
        }

        [Test]
        public void TypeInfoFactoryGetTypeInfoUntyped()
        {
            ITypeInfo typeInfoUntyped = TypeInfoFactory.GetTypeInfo(typeof(DateTime));
            ITypeInfo<DateTime> typeInfo = TypeInfoFactory.GetTypeInfo<DateTime>();

            Assert.IsNotNull(typeInfoUntyped);
            Assert.IsNotNull(typeInfo);
            Assert.AreSame(typeInfoUntyped, typeInfo);
        }

        [Test]
        public void TypeInfoFactoryGetTypeInfoUntypedForNull()
        {
            ArgumentNullException actual = null;
            try
            {
                TypeInfoFactory.GetTypeInfo(null);
            }
            catch (ArgumentNullException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("type", actual.ParamName);
        }

        [Test]
        public void UntypedGetMethodByName()
        {
            ITypeInfo typeInfo = TypeInfoFactory.GetTypeInfo(typeof(DateTime));
            IMethodInfo addMethod = typeInfo.GetMethod("Add");

            Assert.IsNotNull(addMethod);
            Assert.AreSame(typeof(DateTime).GetMethod("Add"), addMethod.MethodInfo);
        }

        [Test]
        public void UntypedGetMethodByNameAndTypes()
        {
            ITypeInfo typeInfo = TypeInfoFactory.GetTypeInfo(typeof(DateTime));
            IMethodInfo getDateTimeFormatsMethod = typeInfo.GetMethod("GetDateTimeFormats", new[] { typeof(char), typeof(IFormatProvider) });

            Assert.IsNotNull(getDateTimeFormatsMethod);
            Assert.AreSame(typeof(DateTime).GetMethod("GetDateTimeFormats", new[] { typeof(char), typeof(IFormatProvider) }), getDateTimeFormatsMethod.MethodInfo);
        }

        [Test]
        public void UntypedGetMethodByNameAndTypesWithWrongName()
        {
            ITypeInfo typeInfo = TypeInfoFactory.GetTypeInfo(typeof(DateTime));

            ArgumentException actual = null;
            try
            {
                typeInfo.GetMethod("GetDateTimeFormats2", new[] { typeof(char), typeof(IFormatProvider) });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("No public instance method was found with name GetDateTimeFormats2 and types { " + typeof(char).FullName + ", " + typeof(IFormatProvider).FullName + " } on type " + typeof(DateTime).FullName + ".", actual.Message);
        }

        [Test]
        public void UntypedGetMethodByNameAndTypesWithWrongTypes()
        {
            ITypeInfo typeInfo = TypeInfoFactory.GetTypeInfo(typeof(DateTime));

            ArgumentException actual = null;
            try
            {
                typeInfo.GetMethod("GetDateTimeFormats", new[] { typeof(int), typeof(IFormatProvider) });
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("No public instance method was found with name GetDateTimeFormats and types { " + typeof(int).FullName + ", " + typeof(IFormatProvider).FullName + " } on type " + typeof(DateTime).FullName + ".", actual.Message);
        }

        [Test]
        public void UntypedGetMethodByNameWithWrongName()
        {
            ITypeInfo typeInfo = TypeInfoFactory.GetTypeInfo(typeof(DateTime));

            ArgumentException actual = null;
            try
            {
                typeInfo.GetMethod("Add2");
            }
            catch (ArgumentException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("No public instance method was found with name Add2 on type " + typeof(DateTime).FullName + ".", actual.Message);
        }

        [Test]
        public void UntypedGetMethodWithOutParameterByName()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> testMethodMethod = typeInfo.GetMethod("TestMethodWithOutParameter");

            Assert.IsNotNull(testMethodMethod);
            Assert.AreSame(typeof(Test).GetMethod("TestMethodWithOutParameter"), testMethodMethod.MethodInfo);
        }

        [Test]
        public void UntypedGetMethodWithRefParameterByName()
        {
            ITypeInfo<Test> typeInfo = TypeInfoFactory.GetTypeInfo<Test>();
            IMethodInfo<Test> testMethodMethod = typeInfo.GetMethod("TestMethodWithRefParameter");

            Assert.IsNotNull(testMethodMethod);
            Assert.AreSame(typeof(Test).GetMethod("TestMethodWithRefParameter"), testMethodMethod.MethodInfo);
        }

        [Test]
        public void UntypedGetMethods()
        {
            ITypeInfo typeInfo = TypeInfoFactory.GetTypeInfo(typeof(DateTime));
            IReadOnlyList<IMethodInfo> methods = typeInfo.GetMethods().ToArray();

            IReadOnlyList<MethodInfo> expectedMethods = typeof(DateTime).GetMethods(BindingFlags.Instance | BindingFlags.Public);

            Assert.IsNotNull(methods);
            Assert.IsNotNull(expectedMethods);
            Assert.IsTrue(methods.Select(m => m.MethodInfo).SetEqual(expectedMethods, ReferenceEqualityComparer.Instance));
        }

        #endregion

        private class Test
        {
#pragma warning disable 649
            public int Field;
#pragma warning restore 649

            // ReSharper disable UnusedMember.Local

            // ReSharper disable UnusedAutoPropertyAccessor.Local
            #region Public Properties

            public int ReadWriteProperty { get; set; }

            // ReSharper restore UnusedAutoPropertyAccessor.Local

            // ReSharper disable UnusedAutoPropertyAccessor.Local
            public int ReadableProperty { get; private set; }

            // ReSharper restore UnusedAutoPropertyAccessor.Local

            // ReSharper restore UnusedAutoPropertyAccessor.Local
            // ReSharper disable UnusedMember.Local
            public int WritableProperty { private get; set; }

            #endregion

            #region Public Methods and Operators

            public string TestMethod()
            {
                return null;
            }

            // ReSharper disable UnusedParameter.Local
            public string TestMethod(int parameter1)
            {
                return null;
            }

            public string TestMethod(int parameter1, int parameter2)
            {
                return null;
            }

            public string TestMethod(int parameter1, int parameter2, int parameter3)
            {
                return null;
            }

            public string TestMethod(int parameter1, int parameter2, int parameter3, int parameter4)
            {
                return null;
            }

            public string TestMethod(int parameter1, int parameter2, int parameter3, int parameter4, int parameter5)
            {
                return null;
            }

            public string TestMethod(int parameter1, int parameter2, int parameter3, int parameter4, int parameter5, int parameter6)
            {
                return null;
            }

            public string TestMethod(int parameter1, int parameter2, int parameter3, int parameter4, int parameter5, int parameter6, int parameter7)
            {
                return null;
            }

            public string TestMethod(int parameter1, int parameter2, int parameter3, int parameter4, int parameter5, int parameter6, int parameter7, int parameter8)
            {
                return null;
            }

            public void TestMethodWithOutParameter(out string value)
            // ReSharper restore UnusedMember.Local
            {
                value = "test";
            }

            // ReSharper disable UnusedMember.Local
            // ReSharper disable UnusedParameter.Local
            public void TestMethodWithRefParameter(ref string value)
            // ReSharper restore UnusedParameter.Local
            // ReSharper restore UnusedMember.Local
            {
            }

            public void TestVoidMethod()
            {
            }

            public void TestVoidMethod(int parameter1)
            {
            }

            public void TestVoidMethod(int parameter1, int parameter2)
            {
            }

            public void TestVoidMethod(int parameter1, int parameter2, int parameter3)
            {
            }

            public void TestVoidMethod(int parameter1, int parameter2, int parameter3, int parameter4)
            {
            }

            public void TestVoidMethod(int parameter1, int parameter2, int parameter3, int parameter4, int parameter5)
            {
            }

            public void TestVoidMethod(int parameter1, int parameter2, int parameter3, int parameter4, int parameter5, int parameter6)
            {
            }

            public void TestVoidMethod(int parameter1, int parameter2, int parameter3, int parameter4, int parameter5, int parameter6, int parameter7)
            {
            }

            public void TestVoidMethod(int parameter1, int parameter2, int parameter3, int parameter4, int parameter5, int parameter6, int parameter7, int parameter8)
            {
            }

            #endregion

            // ReSharper restore UnusedParameter.Local

            // ReSharper restore UnusedMember.Local
        }
    }
}