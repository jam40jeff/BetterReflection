#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Sandbox.cs" company="MorseCode Software">
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
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    using Fasterflect;

    using Microsoft.CSharp.RuntimeBinder;

    using NUnit.Framework;

    using Binder = Microsoft.CSharp.RuntimeBinder.Binder;

    [TestFixture]
    public class Sandbox
    {
        #region Fields

        private readonly Dictionary<string, CallSite<Func<CallSite, object, object>>> callSiteByPropertyName = new Dictionary<string, CallSite<Func<CallSite, object, object>>>();

        private readonly Dictionary<string, Func<object, object>> delegatesByPropertyName = new Dictionary<string, Func<object, object>>();

        private readonly ConcurrentDictionary<string, Func<object, object>> delegatesByPropertyName2 = new ConcurrentDictionary<string, Func<object, object>>();

        private readonly Dictionary<string, MemberGetter> fasterflectDelegatesByPropertyName = new Dictionary<string, MemberGetter>();

        private readonly ConcurrentDictionary<string, MemberGetter> fasterflectDelegatesByPropertyName2 = new ConcurrentDictionary<string, MemberGetter>();

        #endregion

        #region Public Methods and Operators

        [Test]
        public void WritablePropertyInfo()
        {
            Hey hey = new Hey();
            IWritablePropertyInfo<Hey, int> p = TypeInfoFactory.GetTypeInfo<Hey>().GetWritableProperty(o => o.TestInt);
            p.SetValue(hey, 5);

            Assert.AreEqual(5, hey.TestInt);
        }

        [Test]
        public void ReadablePropertyInfo()
        {
            Hey hey = new Hey();
            hey.TestInt = 5;
            IReadablePropertyInfo<Hey, int> p = TypeInfoFactory.GetTypeInfo<Hey>().GetReadableProperty(o => o.TestInt);

            Assert.AreEqual(5, p.GetValue(hey));
        }

        [Test]
        public void ReadOnlyPropertyInfo()
        {
            Hey hey = new Hey();
            IReadablePropertyInfo<Hey, int> p = TypeInfoFactory.GetTypeInfo<Hey>().GetReadableProperty(o => o.ReadOnlyProperty);

            Assert.AreEqual(7, p.GetValue(hey));
        }

        [Test]
        public void ReadOnlyPropertyInfoError()
        {
            InvalidOperationException actual = null;
            try
            {
                TypeInfoFactory.GetTypeInfo<Hey>().GetWritableProperty(o => o.ReadOnlyProperty);
            }
            catch (InvalidOperationException e)
            {
                actual = e;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual("Property with name " + StaticReflection<Hey>.GetMemberInfo(o => o.ReadOnlyProperty).Name + " on type " +
                typeof(Hey).FullName + " is not writable.", actual.Message);
        }

        [Test]
        public void GenericTypeInfoEquality()
        {
            Type t1 = typeof(TestGeneric<>);
            Type t2 = typeof(TestGeneric<>);
            Type t3 = typeof(TestGeneric<int>);
            Type t4 = typeof(TestGeneric<int>);
            Type t5 = typeof(TestGeneric<double>);
            Assert.AreEqual(t1, t2);
            Assert.AreNotEqual(t2, t3);
            Assert.AreEqual(t3, t4);
            Assert.AreNotEqual(t4, t5);
        }

        [Test]
        public void PropertyInfoEquality()
        {
            PropertyInfo p1 = typeof(Hey).GetProperty("TestInt");
            PropertyInfo p2 = typeof(Hey).GetProperty("TestInt");
            PropertyInfo p3 = TypeInfoFactory.GetTypeInfo<Hey>().GetReadableProperty(o => o.TestInt).PropertyInfo;
            Assert.AreEqual(p1, p2);
            Assert.AreEqual(p1, p3);
        }

        [Test]
        public void TestMethods()
        {
            IVoidMethodInfo<Hey> m1 = TypeInfoFactory.GetTypeInfo<Hey>().GetVoidMethod(o => o.Clear);
            IVoidMethodInfo<Hey, int> m2 = TypeInfoFactory.GetTypeInfo<Hey>().GetVoidMethod(o => (Action<int>)o.Set);
            IVoidMethodInfo<Hey, int, int> m3 = TypeInfoFactory.GetTypeInfo<Hey>().GetVoidMethod(o => (Action<int, int>)o.Set);
            IVoidMethodInfo<Hey, int, DateTime> m4 = TypeInfoFactory.GetTypeInfo<Hey>().GetVoidMethod(o => (Action<int, DateTime>)o.Set);
            IVoidMethodInfo<Hey, int, DateTime> m5 = TypeInfoFactory.GetTypeInfo<Hey>().GetVoidMethod(o => (Action<int, DateTime>)o.SetWithSubtract);
            IMethodInfo<Hey, int> m6 = TypeInfoFactory.GetTypeInfo<Hey>().GetMethod(o => (Func<int>)o.GetAsInteger);
            IMethodInfo<Hey, string> m7 = TypeInfoFactory.GetTypeInfo<Hey>().GetMethod(o => (Func<string>)o.Get);
            IMethodInfo<Hey, string, string> m8 = TypeInfoFactory.GetTypeInfo<Hey>().GetMethod(o => (Func<string, string>)o.Get);
            IMethodInfo<Hey, string, string, string> m9 = TypeInfoFactory.GetTypeInfo<Hey>().GetMethod(o => (Func<string, string, string>)o.Get);
            IMethodInfo<Hey, string, DateTime, string> m10 = TypeInfoFactory.GetTypeInfo<Hey>().GetMethod(o => (Func<string, DateTime, string>)o.Get);
            IMethodInfo<Hey, string, DateTime, string> m11 = TypeInfoFactory.GetTypeInfo<Hey>().GetMethod(o => (Func<string, DateTime, string>)o.GetWithLongFormat);

            Hey hey = new Hey();
            m2.Invoke(hey, 5);
            Assert.AreEqual(5, hey.TestInt);
            m1.Invoke(hey);
            Assert.AreEqual(0, hey.TestInt);
            m3.Invoke(hey, 3, 4);
            Assert.AreEqual(7, hey.TestInt);
            m4.Invoke(hey, 3, new DateTime(2015, 1, 1));
            Assert.AreEqual(2018, hey.TestInt);
            m5.Invoke(hey, 3, new DateTime(2015, 1, 1));
            Assert.AreEqual(-2012, hey.TestInt);
            Assert.AreEqual(-2012, m6.Invoke(hey));
            Assert.AreEqual("-2012", m7.Invoke(hey));
            Assert.AreEqual("a-2012", m8.Invoke(hey, "a"));
            Assert.AreEqual("a-2012b", m9.Invoke(hey, "a", "b"));
            Assert.AreEqual("a-201215", m10.Invoke(hey, "a", new DateTime(2015, 1, 1)));
            Assert.AreEqual("a-20122015", m11.Invoke(hey, "a", new DateTime(2015, 1, 1)));
        }

        [Test]
        public void TestProperties()
        {
            Hey hey = new Hey();
            hey.TestInt = 5;
            hey.TestInt2 = 35;

            int v1A = this.Dynamic(hey);
            int v1B = this.Dynamic2(hey);
            int v2A = this.CustomDynamic(hey, "TestInt");
            int v2B = this.CustomDynamic(hey, "TestInt2");
            int v3A = this.CustomDynamic2(hey, "TestInt");
            int v3B = this.CustomDynamic2(hey, "TestInt2");
            int v4A = this.CustomDynamic3(hey, "TestInt");
            int v4B = this.CustomDynamic3(hey, "TestInt2");
            int v5A = this.CustomDynamic4(hey, "TestInt");
            int v5B = this.CustomDynamic4(hey, "TestInt2");
            int v6A = this.Reflection(hey, "TestInt");
            int v6B = this.Reflection(hey, "TestInt2");
            int v7A = hey.TestInt;
            int v7B = hey.TestInt2;
            int v8A = this.Fasterflect(hey, "TestInt");
            int v8B = this.Fasterflect(hey, "TestInt2");
            int v9A = this.FasterflectWithCaching(hey, "TestInt");
            int v9B = this.FasterflectWithCaching(hey, "TestInt2");
            int v10A = this.FasterflectWithCaching2(hey, "TestInt");
            int v10B = this.FasterflectWithCaching2(hey, "TestInt2");
            int v12A = this.BetterReflection(hey, o => o.TestInt);
            int v12B = this.BetterReflection(hey, o => o.TestInt2);
            Stopwatch sw = new Stopwatch();

            sw.Start();
            for (int i = 0; i < 5000; i++)
            {
                this.CustomDynamic3(hey, "TestInt");
                this.CustomDynamic3(hey, "TestInt2");
            }
            sw.Stop();

            long ms6 = sw.ElapsedMilliseconds;
            sw.Reset();

            sw.Start();
            for (int i = 0; i < 5000; i++)
            {
                this.CustomDynamic4(hey, "TestInt");
                this.CustomDynamic4(hey, "TestInt2");
            }
            sw.Stop();

            long ms4 = sw.ElapsedMilliseconds;
            sw.Reset();

            sw.Start();
            for (int i = 0; i < 5000; i++)
            {
                this.Dynamic(hey);
                this.Dynamic2(hey);
            }
            sw.Stop();

            long ms1 = sw.ElapsedMilliseconds;
            sw.Reset();

            sw.Start();
            for (int i = 0; i < 5000; i++)
            {
                this.CustomDynamic(hey, "TestInt");
                this.CustomDynamic(hey, "TestInt2");
            }
            sw.Stop();

            long ms2 = sw.ElapsedMilliseconds;
            sw.Reset();

            //sw.Start();
            //for (int i = 0; i < 5000; i++)
            //{
            //    this.CustomDynamic2(hey, "TestInt");
            //    this.CustomDynamic2(hey, "TestInt2");
            //}
            //sw.Stop();

            //long ms3 = sw.ElapsedMilliseconds;
            //sw.Reset();
            sw.Start();
            for (int i = 0; i < 5000; i++)
            {
                this.Reflection(hey, "TestInt");
                this.Reflection(hey, "TestInt2");
            }
            sw.Stop();

            long ms5 = sw.ElapsedMilliseconds;
            sw.Reset();

            sw.Start();
            for (int i = 0; i < 5000; i++)
            {
                int testInt = hey.TestInt;
                int testInt2 = hey.TestInt2;
            }
            sw.Stop();

            long ms7 = sw.ElapsedMilliseconds;
            sw.Reset();

            sw.Start();
            for (int i = 0; i < 5000; i++)
            {
                this.Fasterflect(hey, "TestInt");
                this.Fasterflect(hey, "TestInt2");
            }
            sw.Stop();

            long ms8 = sw.ElapsedMilliseconds;
            sw.Reset();

            sw.Start();
            for (int i = 0; i < 5000; i++)
            {
                this.FasterflectWithCaching(hey, "TestInt");
                this.FasterflectWithCaching(hey, "TestInt2");
            }
            sw.Stop();

            long ms9 = sw.ElapsedMilliseconds;
            sw.Reset();

            sw.Start();
            for (int i = 0; i < 5000; i++)
            {
                this.FasterflectWithCaching2(hey, "TestInt");
                this.FasterflectWithCaching2(hey, "TestInt2");
            }
            sw.Stop();

            long ms10 = sw.ElapsedMilliseconds;
            sw.Reset();

            sw.Start();
            for (int i = 0; i < 50000; i++)
            {
                this.BetterReflection(hey, o => o.TestInt);
                this.BetterReflection(hey, o => o.TestInt2);
            }
            sw.Stop();

            long ms11a = sw.ElapsedMilliseconds;
            sw.Reset();

            sw.Start();
            IReadablePropertyInfo<Hey, int> propertyInfo1a = TypeInfoFactory.GetTypeInfo<Hey>().GetReadableProperty(o => o.TestInt);
            IReadablePropertyInfo<Hey, int> propertyInfo2a = TypeInfoFactory.GetTypeInfo<Hey>().GetReadableProperty(o => o.TestInt2);
            for (int i = 0; i < 500000; i++)
            {
                this.BetterReflectionWithCaching(hey, propertyInfo1a);
                this.BetterReflectionWithCaching(hey, propertyInfo2a);
            }
            sw.Stop();

            long ms12a = sw.ElapsedMilliseconds;
            sw.Reset();

            sw.Start();
            PropertyInfo pi1 = typeof(Hey).GetProperty("TestInt");
            PropertyInfo pi2 = typeof(Hey).GetProperty("TestInt2");
            for (int i = 0; i < 500000; i++)
            {
                int value1 = (int)pi1.GetValue(hey);
                int value2 = (int)pi2.GetValue(hey);
            }
            sw.Stop();

            long ms13 = sw.ElapsedMilliseconds;
            sw.Reset();

            sw.Start();
            for (int i = 0; i < 500000; i++)
            {
                int value1 = (int)typeof(Hey).GetProperty("TestInt").GetValue(hey);
                int value2 = (int)typeof(Hey).GetProperty("TestInt2").GetValue(hey);
            }
            sw.Stop();

            long ms14 = sw.ElapsedMilliseconds;
        }

        #endregion

        #region Methods

        private static Func<object, object> GetDelegate<T, TProperty>(MethodInfo m)
        {
            Func<T, TProperty> getMethod = (Func<T, TProperty>)Delegate.CreateDelegate(typeof(Func<T, TProperty>), m);
            return o => getMethod((T)o);
        }

        private TProperty BetterReflection<TProperty>(Hey o, Expression<Func<Hey, TProperty>> propertyExpression)
        {
            IReadablePropertyInfo<Hey, TProperty> p = TypeInfoFactory.GetTypeInfo<Hey>().GetReadableProperty(propertyExpression);
            return p.GetValue(o);
        }

        private TProperty BetterReflectionWithCaching<TProperty>(Hey o, IReadablePropertyInfo<Hey, TProperty> propertyInfo)
        {
            return propertyInfo.GetValue(o);
        }

        private int CustomDynamic(object o, string propertyName)
        {
            CallSite<Func<CallSite, object, object>> callSite;
            if (!this.callSiteByPropertyName.TryGetValue(propertyName, out callSite))
            {
                callSite = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, propertyName, typeof(Sandbox), new[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
                this.callSiteByPropertyName.Add(propertyName, callSite);
            }
            return (int)callSite.Target(callSite, o);
        }

        private int CustomDynamic2(object o, string propertyName)
        {
            CallSite<Func<CallSite, object, object>> callSite = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, propertyName, typeof(Sandbox), new[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
            return (int)callSite.Target(callSite, o);
        }

        private int CustomDynamic3(object o, string propertyName)
        {
            Func<object, object> d;
            if (!this.delegatesByPropertyName.TryGetValue(propertyName, out d))
            {
                MethodInfo m = o.GetType().GetProperty(propertyName).GetGetMethod();
                MethodInfo getDelegateMethod = typeof(Sandbox).GetMethod("GetDelegate", BindingFlags.Static | BindingFlags.NonPublic).MakeGenericMethod(o.GetType(), m.ReturnType);
                d = (Func<object, object>)getDelegateMethod.Invoke(null, new object[] { m });
                this.delegatesByPropertyName.Add(propertyName, d);
            }
            return (int)d(o);
        }

        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1118:ParameterMustNotSpanMultipleLines", Justification = "Reviewed. Suppression is OK here.")]
        private int CustomDynamic4(object o, string propertyName)
        {
            Func<object, object> d = this.delegatesByPropertyName2.GetOrAdd(propertyName, (Func<string, Func<object, object>>)(n =>
                {
                    MethodInfo m = o.GetType().GetProperty(propertyName).GetGetMethod();
                    MethodInfo getDelegateMethod = typeof(Sandbox).GetMethod("GetDelegate", BindingFlags.Static | BindingFlags.NonPublic).MakeGenericMethod(o.GetType(), m.ReturnType);
                    return (Func<object, object>)getDelegateMethod.Invoke(null, new object[] { m });
                }));
            return (int)d(o);
        }

        private int Dynamic(object o)
        {
            dynamic d = o;
            return d.TestInt;
        }

        private int Dynamic2(object o)
        {
            dynamic d = o;
            return d.TestInt2;
        }

        private int Fasterflect(object o, string propertyName)
        {
            PropertyInfo p = typeof(Hey).GetProperty(propertyName);
            return (int)p.DelegateForGetPropertyValue()(o);
        }

        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1118:ParameterMustNotSpanMultipleLines", Justification = "Reviewed. Suppression is OK here.")]
        private int FasterflectWithCaching(object o, string propertyName)
        {
            MemberGetter d;
            if (!this.fasterflectDelegatesByPropertyName.TryGetValue(propertyName, out d))
            {
                PropertyInfo p = typeof(Hey).GetProperty(propertyName);
                d = p.DelegateForGetPropertyValue();
                this.fasterflectDelegatesByPropertyName.Add(propertyName, d);
            }
            return (int)d(o);
        }

        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1118:ParameterMustNotSpanMultipleLines", Justification = "Reviewed. Suppression is OK here.")]
        private int FasterflectWithCaching2(object o, string propertyName)
        {
            MemberGetter d = this.fasterflectDelegatesByPropertyName2.GetOrAdd(propertyName, (Func<string, MemberGetter>)(n =>
                {
                    PropertyInfo p = typeof(Hey).GetProperty(propertyName);
                    return p.DelegateForGetPropertyValue();
                }));
            return (int)d(o);
        }

        private int Reflection(object o, string propertyName)
        {
            PropertyInfo p = typeof(Hey).GetProperty(propertyName);
            return (int)p.GetValue(o);
        }

        #endregion

        public class Hey
        {
            private int writeOnlyProperty;

            #region Public Properties

            public int TestInt { get; set; }

            public int TestInt2 { get; set; }

            public int ReadOnlyProperty
            {
                get
                {
                    return 7;
                }
            }

            public int WriteOnlyProperty
            {
                set
                {
                    this.writeOnlyProperty = value;
                }
            }

            #endregion

            #region Public Methods and Operators

            public int GetWriteOnlyProperty()
            {
                return this.writeOnlyProperty;
            }

            public void Clear()
            {
                this.TestInt = 0;
            }

            public string Get()
            {
                return this.TestInt.ToString(CultureInfo.InvariantCulture);
            }

            public string Get(string prefix)
            {
                return prefix + this.TestInt.ToString(CultureInfo.InvariantCulture);
            }

            public string Get(string prefix, string suffix)
            {
                return this.Get(prefix) + suffix;
            }

            public string Get(string prefix, DateTime d)
            {
                return this.Get(prefix) + d.Year.ToString(CultureInfo.InvariantCulture).Substring(2);
            }

            public int GetAsInteger()
            {
                return this.TestInt;
            }

            public string GetWithLongFormat(string prefix, DateTime d)
            {
                return this.Get(prefix) + d.Year;
            }

            public void Set(int v)
            {
                this.TestInt = v;
            }

            public void Set(int v, int v2)
            {
                this.TestInt = v + v2;
            }

            public void Set(int v, DateTime d)
            {
                this.TestInt = v + d.Year;
            }

            public void SetWithSubtract(int v, DateTime d)
            {
                this.TestInt = v - d.Year;
            }

            #endregion
        }

        public class TestGeneric<T>
        {
        }
    }
}