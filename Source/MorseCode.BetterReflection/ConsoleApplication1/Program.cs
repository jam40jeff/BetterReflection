#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="MorseCode Software">
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

namespace ConsoleApplication1
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    using Fasterflect;

    using Microsoft.CSharp.RuntimeBinder;

    using MorseCode.BetterReflection;

    using Binder = Microsoft.CSharp.RuntimeBinder.Binder;

    internal class Program
    {
        #region Constants

        private const int Iterations = 1000000;

        #endregion

        #region Static Fields

        private static readonly Dictionary<string, CallSite<Func<CallSite, object, object>>> CallSiteByPropertyName =
            new Dictionary<string, CallSite<Func<CallSite, object, object>>>();

        private static readonly Dictionary<string, Func<object, object>> DelegatesByPropertyName =
            new Dictionary<string, Func<object, object>>();

        private static readonly ConcurrentDictionary<string, Func<object, object>> DelegatesByPropertyName2 =
            new ConcurrentDictionary<string, Func<object, object>>();

        private static readonly Dictionary<string, MemberGetter> FasterflectDelegatesByPropertyName =
            new Dictionary<string, MemberGetter>();

        private static readonly ConcurrentDictionary<string, MemberGetter> FasterflectDelegatesByPropertyName2 =
            new ConcurrentDictionary<string, MemberGetter>();

        private static readonly IPropertyValueGetterCache PropertyValueGetterCache =
            new PropertyValueGetterCache(
                new StaticReflectionHelper<PropertyValueGetterCache>(new StaticReflectionHelper()));

        private static readonly IStaticReflectionHelper<Hey> StaticReflectionHelper =
            new StaticReflectionHelper<Hey>(new StaticReflectionHelper());

        private static readonly IPropertyInfo<Hey, int> TestIntPropertyInfo =
            BetterReflectionManager.TypeInfoFactory.GetTypeInfo<Hey>().GetProperty(o => o.TestInt);

        private static readonly IPropertyInfo<Hey, int> TestIntPropertyInfo2 =
            BetterReflectionManager.TypeInfoFactory.GetTypeInfo<Hey>().GetProperty(o => o.TestInt2);

        #endregion

        #region Public Methods and Operators

        public static void TestMethod1()
        {
            Hey hey = new Hey();
            hey.TestInt = 5;
            hey.TestInt2 = 35;

            int v1a = Dynamic(hey);
            int v1b = Dynamic2(hey);
            int v2a = CustomDynamic(hey, "TestInt");
            int v2b = CustomDynamic(hey, "TestInt2");
            int v3a = CustomDynamic2(hey, "TestInt");
            int v3b = CustomDynamic2(hey, "TestInt2");
            int v4a = CustomDynamic3(hey, "TestInt");
            int v4b = CustomDynamic3(hey, "TestInt2");
            int v5a = CustomDynamic4(hey, "TestInt");
            int v5b = CustomDynamic4(hey, "TestInt2");
            int v6a = Reflection(hey, "TestInt");
            int v6b = Reflection(hey, "TestInt2");
            int v7a = hey.TestInt;
            int v7b = hey.TestInt2;
            int v8a = Fasterflect(hey, "TestInt");
            int v8b = Fasterflect(hey, "TestInt2");
            int v9a = FasterflectWithCaching(hey, "TestInt");
            int v9b = FasterflectWithCaching(hey, "TestInt2");
            int v10a = FasterflectWithCaching2(hey, "TestInt");
            int v10b = FasterflectWithCaching2(hey, "TestInt2");
            int v11a = BetterReflection(hey, o => o.TestInt);
            int v11b = BetterReflection(hey, o => o.TestInt2);
            int v12a = BetterReflection2a(hey);
            int v12b = BetterReflection2b(hey);
            int v13a = BetterReflection3(hey, "TestInt");
            int v13b = BetterReflection3(hey, "TestInt2");
            int v14a = ReflectionWithExpressions(hey, o => o.TestInt);
            int v14b = ReflectionWithExpressions(hey, o => o.TestInt2);

            Stopwatch sw = new Stopwatch();

            sw.Start();
            for (int i = 0; i < Iterations; i++)
            {
                CustomDynamic3(hey, "TestInt");
                CustomDynamic3(hey, "TestInt2");
            }
            sw.Stop();

            long ms6 = sw.ElapsedMilliseconds;
            sw.Reset();

            sw.Start();
            for (int i = 0; i < Iterations; i++)
            {
                CustomDynamic4(hey, "TestInt");
                CustomDynamic4(hey, "TestInt2");
            }
            sw.Stop();

            long ms4 = sw.ElapsedMilliseconds;
            sw.Reset();

            sw.Start();
            for (int i = 0; i < Iterations; i++)
            {
                Dynamic(hey);
                Dynamic2(hey);
            }
            sw.Stop();

            long ms1 = sw.ElapsedMilliseconds;
            sw.Reset();

            sw.Start();
            for (int i = 0; i < Iterations; i++)
            {
                CustomDynamic(hey, "TestInt");
                CustomDynamic(hey, "TestInt2");
            }
            sw.Stop();

            long ms2 = sw.ElapsedMilliseconds;
            sw.Reset();

            //sw.Start();
            //for (int i = 0; i < Iterations; i++)
            //{
            //    this.CustomDynamic2(hey, "TestInt");
            //    this.CustomDynamic2(hey, "TestInt2");
            //}
            //sw.Stop();

            //long ms3 = sw.ElapsedMilliseconds;
            //sw.Reset();
            sw.Start();
            for (int i = 0; i < Iterations; i++)
            {
                Reflection(hey, "TestInt");
                Reflection(hey, "TestInt2");
            }
            sw.Stop();

            long ms5 = sw.ElapsedMilliseconds;
            sw.Reset();

            sw.Start();
            for (int i = 0; i < Iterations; i++)
            {
                int testInt = hey.TestInt;
                int testInt2 = hey.TestInt2;
            }
            sw.Stop();

            long ms7 = sw.ElapsedMilliseconds;
            sw.Reset();

            sw.Start();
            for (int i = 0; i < Iterations; i++)
            {
                Fasterflect(hey, "TestInt");
                Fasterflect(hey, "TestInt2");
            }
            sw.Stop();

            long ms8 = sw.ElapsedMilliseconds;
            sw.Reset();

            sw.Start();
            for (int i = 0; i < Iterations; i++)
            {
                FasterflectWithCaching(hey, "TestInt");
                FasterflectWithCaching(hey, "TestInt2");
            }
            sw.Stop();

            long ms9 = sw.ElapsedMilliseconds;
            sw.Reset();

            sw.Start();
            for (int i = 0; i < Iterations; i++)
            {
                FasterflectWithCaching2(hey, "TestInt");
                FasterflectWithCaching2(hey, "TestInt2");
            }
            sw.Stop();

            long ms10 = sw.ElapsedMilliseconds;
            sw.Reset();

            sw.Start();
            Expression<Func<Hey, int>> e1 = o => o.TestInt;
            Expression<Func<Hey, int>> e2 = o => o.TestInt2;
            for (int i = 0; i < Iterations; i++)
            {
                BetterReflection(hey, e1);
                BetterReflection(hey, e2);
            }
            sw.Stop();

            long ms11 = sw.ElapsedMilliseconds;
            sw.Reset();

            sw.Start();
            for (int i = 0; i < Iterations; i++)
            {
                BetterReflection2a(hey);
                BetterReflection2b(hey);
            }
            sw.Stop();

            long ms12 = sw.ElapsedMilliseconds;
            sw.Reset();

            sw.Start();
            for (int i = 0; i < Iterations; i++)
            {
                BetterReflection3(hey, "TestInt");
                BetterReflection3(hey, "TestInt2");
            }
            sw.Stop();

            long ms13 = sw.ElapsedMilliseconds;
            sw.Reset();

            sw.Start();
            for (int i = 0; i < Iterations; i++)
            {
                ReflectionWithExpressions(hey, e1);
                ReflectionWithExpressions(hey, e2);
            }
            sw.Stop();

            long ms14 = sw.ElapsedMilliseconds;

            Console.WriteLine(v1a);
            Console.WriteLine(v1b);
            Console.WriteLine(v2a);
            Console.WriteLine(v2b);
            Console.WriteLine(v3a);
            Console.WriteLine(v3b);
            Console.WriteLine(v4a);
            Console.WriteLine(v4b);
            Console.WriteLine(v5a);
            Console.WriteLine(v5b);
            Console.WriteLine(v6a);
            Console.WriteLine(v6b);
            Console.WriteLine(v7a);
            Console.WriteLine(v7b);
            Console.WriteLine(v8a);
            Console.WriteLine(v8b);
            Console.WriteLine(v9a);
            Console.WriteLine(v9b);
            Console.WriteLine(v10a);
            Console.WriteLine(v10b);
            Console.WriteLine(v11a);
            Console.WriteLine(v11b);
            Console.WriteLine(v12a);
            Console.WriteLine(v12b);
            Console.WriteLine(v13a);
            Console.WriteLine(v13b);
            Console.WriteLine(v14a);
            Console.WriteLine(v14b);

            Console.WriteLine();

            Console.WriteLine("DLR: " + ms1);
            Console.WriteLine("Custom DLR: " + ms2);
            //Console.WriteLine(ms3);
            Console.WriteLine("Concurrent Delegate caching: " + ms4);
            Console.WriteLine("Reflection: " + ms5);
            Console.WriteLine("Delegate caching: " + ms6);
            Console.WriteLine("Direct Access: " + ms7);
            Console.WriteLine("Fasterflect: " + ms8);
            Console.WriteLine("Fasterflect with Caching: " + ms9);
            Console.WriteLine("Fasterflect with Concurrent Caching: " + ms10);
            Console.WriteLine("Better Reflection: " + ms11);
            Console.WriteLine("Better Reflection with Caching: " + ms12);
            Console.WriteLine("Better Reflection with Property Name: " + ms13);
            Console.WriteLine("Reflection with Expressions: " + ms14);

            Console.ReadKey();
        }

        #endregion

        #region Methods

        private static TProperty BetterReflection<TProperty>(Hey o, Expression<Func<Hey, TProperty>> propertyExpression)
        {
            return BetterReflectionManager.TypeInfoFactory.GetTypeInfo<Hey>().GetProperty(propertyExpression).GetValue(o);
        }

        private static int BetterReflection2a(Hey o)
        {
            return TestIntPropertyInfo.GetValue(o);
        }

        private static int BetterReflection2b(Hey o)
        {
            return TestIntPropertyInfo2.GetValue(o);
        }

        private static int BetterReflection3(Hey o, string propertyName)
        {
            PropertyInfo propertyInfo = typeof(Hey).GetProperty(propertyName);
            return PropertyValueGetterCache.GetValue<Hey, int>(propertyInfo, o);
        }

        private static int CustomDynamic(object o, string propertyName)
        {
            CallSite<Func<CallSite, object, object>> callSite;
            if (!CallSiteByPropertyName.TryGetValue(propertyName, out callSite))
            {
                callSite =
                    CallSite<Func<CallSite, object, object>>.Create(
                        Binder.GetMember(
                            CSharpBinderFlags.None,
                            propertyName,
                            typeof(Program),
                            new[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
                CallSiteByPropertyName.Add(propertyName, callSite);
            }
            return (int)callSite.Target(callSite, o);
        }

        private static int CustomDynamic2(object o, string propertyName)
        {
            CallSite<Func<CallSite, object, object>> callSite =
                CallSite<Func<CallSite, object, object>>.Create(
                    Binder.GetMember(
                        CSharpBinderFlags.None,
                        propertyName,
                        typeof(Program),
                        new[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
            return (int)callSite.Target(callSite, o);
        }

        private static int CustomDynamic3(object o, string propertyName)
        {
            Func<object, object> d;
            if (!DelegatesByPropertyName.TryGetValue(propertyName, out d))
            {
                MethodInfo m = o.GetType().GetProperty(propertyName).GetGetMethod();
                MethodInfo getDelegateMethod =
                    typeof(Program).GetMethod("GetDelegate", BindingFlags.Static | BindingFlags.NonPublic)
                                   .MakeGenericMethod(o.GetType(), m.ReturnType);
                d = (Func<object, object>)getDelegateMethod.Invoke(null, new object[] { m });
                DelegatesByPropertyName.Add(propertyName, d);
            }
            return (int)d(o);
        }

        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1118:ParameterMustNotSpanMultipleLines",
            Justification = "Reviewed. Suppression is OK here.")]
        private static int CustomDynamic4(object o, string propertyName)
        {
            Func<object, object> d = DelegatesByPropertyName2.GetOrAdd(
                propertyName,
                (Func<string, Func<object, object>>)(n =>
                    {
                        MethodInfo m = o.GetType().GetProperty(propertyName).GetGetMethod();
                        MethodInfo getDelegateMethod =
                            typeof(Program).GetMethod("GetDelegate", BindingFlags.Static | BindingFlags.NonPublic)
                                           .MakeGenericMethod(o.GetType(), m.ReturnType);
                        return (Func<object, object>)getDelegateMethod.Invoke(null, new object[] { m });
                    }));
            return (int)d(o);
        }

        private static int Dynamic(object o)
        {
            dynamic d = o;
            return d.TestInt;
        }

        private static int Dynamic2(object o)
        {
            dynamic d = o;
            return d.TestInt2;
        }

        private static int Fasterflect(object o, string propertyName)
        {
            PropertyInfo p = typeof(Hey).GetProperty(propertyName);
            return (int)p.DelegateForGetPropertyValue()(o);
        }

        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1118:ParameterMustNotSpanMultipleLines",
            Justification = "Reviewed. Suppression is OK here.")]
        private static int FasterflectWithCaching(object o, string propertyName)
        {
            MemberGetter d;
            if (!FasterflectDelegatesByPropertyName.TryGetValue(propertyName, out d))
            {
                PropertyInfo p = typeof(Hey).GetProperty(propertyName);
                d = p.DelegateForGetPropertyValue();
                FasterflectDelegatesByPropertyName.Add(propertyName, d);
            }
            return (int)d(o);
        }

        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1118:ParameterMustNotSpanMultipleLines",
            Justification = "Reviewed. Suppression is OK here.")]
        private static int FasterflectWithCaching2(object o, string propertyName)
        {
            MemberGetter d = FasterflectDelegatesByPropertyName2.GetOrAdd(
                propertyName,
                (Func<string, MemberGetter>)(n =>
                    {
                        PropertyInfo p = typeof(Hey).GetProperty(propertyName);
                        return p.DelegateForGetPropertyValue();
                    }));
            return (int)d(o);
        }

        private static Func<object, object> GetDelegate<T, TProperty>(MethodInfo m)
        {
            Func<T, TProperty> getMethod = (Func<T, TProperty>)Delegate.CreateDelegate(typeof(Func<T, TProperty>), m);
            return o => getMethod((T)o);
        }

        private static void Main(string[] args)
        {
            TestMethod1();
        }

        private static int Reflection(object o, string propertyName)
        {
            PropertyInfo p = typeof(Hey).GetProperty(propertyName);
            return (int)p.GetValue(o);
        }

        private static TProperty ReflectionWithExpressions<TProperty>(
            Hey o, Expression<Func<Hey, TProperty>> propertyExpression)
        {
            PropertyInfo p = typeof(Hey).GetProperty(StaticReflectionHelper.GetMemberInfo(propertyExpression).Name);
            return (TProperty)p.GetValue(o);
        }

        #endregion

        public class Hey
        {
            #region Public Properties

            public int TestInt { get; set; }

            public int TestInt2 { get; set; }

            #endregion
        }
    }
}