#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTest1.cs" company="MorseCode Software">
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

namespace MorseCode.BetterReflection.Tests
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
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Binder = Microsoft.CSharp.RuntimeBinder.Binder;

    [TestClass]
    public class UnitTest1
    {
        #region Fields

        private readonly Dictionary<string, CallSite<Func<CallSite, object, object>>> callSiteByPropertyName =
            new Dictionary<string, CallSite<Func<CallSite, object, object>>>();

        private readonly Dictionary<string, Func<object, object>> delegatesByPropertyName =
            new Dictionary<string, Func<object, object>>();

        private readonly ConcurrentDictionary<string, Func<object, object>> delegatesByPropertyName2 =
            new ConcurrentDictionary<string, Func<object, object>>();

        private readonly Dictionary<string, MemberGetter> fasterflectDelegatesByPropertyName =
            new Dictionary<string, MemberGetter>();

        private readonly ConcurrentDictionary<string, MemberGetter> fasterflectDelegatesByPropertyName2 =
            new ConcurrentDictionary<string, MemberGetter>();

        #endregion

        #region Public Methods and Operators

        [TestMethod]
        public void TestMethod1()
        {
            Hey hey = new Hey();
            hey.TestInt = 5;
            hey.TestInt2 = 35;

            int v1a = this.Dynamic(hey);
            int v1b = this.Dynamic2(hey);
            int v2a = this.CustomDynamic(hey, "TestInt");
            int v2b = this.CustomDynamic(hey, "TestInt2");
            int v3a = this.CustomDynamic2(hey, "TestInt");
            int v3b = this.CustomDynamic2(hey, "TestInt2");
            int v4a = this.CustomDynamic3(hey, "TestInt");
            int v4b = this.CustomDynamic3(hey, "TestInt2");
            int v5a = this.CustomDynamic4(hey, "TestInt");
            int v5b = this.CustomDynamic4(hey, "TestInt2");
            int v6a = this.Reflection(hey, "TestInt");
            int v6b = this.Reflection(hey, "TestInt2");
            int v7a = hey.TestInt;
            int v7b = hey.TestInt2;
            int v8a = this.Fasterflect(hey, "TestInt");
            int v8b = this.Fasterflect(hey, "TestInt2");
            int v9a = this.FasterflectWithCaching(hey, "TestInt");
            int v9b = this.FasterflectWithCaching(hey, "TestInt2");
            int v10a = this.FasterflectWithCaching2(hey, "TestInt");
            int v10b = this.FasterflectWithCaching2(hey, "TestInt2");
            int v11a = this.BetterReflection(hey, o => o.TestInt);
            int v11b = this.BetterReflection(hey, o => o.TestInt2);
            Stopwatch sw = new Stopwatch();

            sw.Start();
            for (int i = 0; i < 5000000; i++)
            {
                this.CustomDynamic3(hey, "TestInt");
                this.CustomDynamic3(hey, "TestInt2");
            }
            sw.Stop();

            long ms6 = sw.ElapsedMilliseconds;
            sw.Reset();

            sw.Start();
            for (int i = 0; i < 5000000; i++)
            {
                this.CustomDynamic4(hey, "TestInt");
                this.CustomDynamic4(hey, "TestInt2");
            }
            sw.Stop();

            long ms4 = sw.ElapsedMilliseconds;
            sw.Reset();

            sw.Start();
            for (int i = 0; i < 5000000; i++)
            {
                this.Dynamic(hey);
                this.Dynamic2(hey);
            }
            sw.Stop();

            long ms1 = sw.ElapsedMilliseconds;
            sw.Reset();

            sw.Start();
            for (int i = 0; i < 5000000; i++)
            {
                this.CustomDynamic(hey, "TestInt");
                this.CustomDynamic(hey, "TestInt2");
            }
            sw.Stop();

            long ms2 = sw.ElapsedMilliseconds;
            sw.Reset();

            //sw.Start();
            //for (int i = 0; i < 5000000; i++)
            //{
            //    this.CustomDynamic2(hey, "TestInt");
            //    this.CustomDynamic2(hey, "TestInt2");
            //}
            //sw.Stop();

            //long ms3 = sw.ElapsedMilliseconds;
            //sw.Reset();
            sw.Start();
            for (int i = 0; i < 5000000; i++)
            {
                this.Reflection(hey, "TestInt");
                this.Reflection(hey, "TestInt2");
            }
            sw.Stop();

            long ms5 = sw.ElapsedMilliseconds;
            sw.Reset();

            sw.Start();
            for (int i = 0; i < 5000000; i++)
            {
                int testInt = hey.TestInt;
                int testInt2 = hey.TestInt2;
            }
            sw.Stop();

            long ms7 = sw.ElapsedMilliseconds;
            sw.Reset();

            sw.Start();
            for (int i = 0; i < 5000000; i++)
            {
                this.Fasterflect(hey, "TestInt");
                this.Fasterflect(hey, "TestInt2");
            }
            sw.Stop();

            long ms8 = sw.ElapsedMilliseconds;
            sw.Reset();

            sw.Start();
            for (int i = 0; i < 5000000; i++)
            {
                this.FasterflectWithCaching(hey, "TestInt");
                this.FasterflectWithCaching(hey, "TestInt2");
            }
            sw.Stop();

            long ms9 = sw.ElapsedMilliseconds;
            sw.Reset();

            sw.Start();
            for (int i = 0; i < 5000000; i++)
            {
                this.FasterflectWithCaching2(hey, "TestInt");
                this.FasterflectWithCaching2(hey, "TestInt2");
            }
            sw.Stop();

            long ms10 = sw.ElapsedMilliseconds;
            sw.Reset();

            sw.Start();
            for (int i = 0; i < 5000000; i++)
            {
                this.BetterReflection(hey, o => o.TestInt);
                this.BetterReflection(hey, o => o.TestInt2);
            }
            sw.Stop();

            long ms11 = sw.ElapsedMilliseconds;
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
            IPropertyInfo<Hey, TProperty> p =
                BetterReflectionManager.TypeInfoFactory.GetTypeInfo<Hey>().GetProperty(propertyExpression);
            return p.GetValue(o);
        }

        private int CustomDynamic(object o, string propertyName)
        {
            CallSite<Func<CallSite, object, object>> callSite;
            if (!this.callSiteByPropertyName.TryGetValue(propertyName, out callSite))
            {
                callSite =
                    CallSite<Func<CallSite, object, object>>.Create(
                        Binder.GetMember(
                            CSharpBinderFlags.None,
                            propertyName,
                            typeof(UnitTest1),
                            new[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
                this.callSiteByPropertyName.Add(propertyName, callSite);
            }
            return (int)callSite.Target(callSite, o);
        }

        private int CustomDynamic2(object o, string propertyName)
        {
            CallSite<Func<CallSite, object, object>> callSite =
                CallSite<Func<CallSite, object, object>>.Create(
                    Binder.GetMember(
                        CSharpBinderFlags.None,
                        propertyName,
                        typeof(UnitTest1),
                        new[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
            return (int)callSite.Target(callSite, o);
        }

        private int CustomDynamic3(object o, string propertyName)
        {
            Func<object, object> d;
            if (!this.delegatesByPropertyName.TryGetValue(propertyName, out d))
            {
                MethodInfo m = o.GetType().GetProperty(propertyName).GetGetMethod();
                MethodInfo getDelegateMethod =
                    typeof(UnitTest1).GetMethod("GetDelegate", BindingFlags.Static | BindingFlags.NonPublic)
                                     .MakeGenericMethod(o.GetType(), m.ReturnType);
                d = (Func<object, object>)getDelegateMethod.Invoke(null, new object[] { m });
                this.delegatesByPropertyName.Add(propertyName, d);
            }
            return (int)d(o);
        }

        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1118:ParameterMustNotSpanMultipleLines",
            Justification = "Reviewed. Suppression is OK here.")]
        private int CustomDynamic4(object o, string propertyName)
        {
            Func<object, object> d = this.delegatesByPropertyName2.GetOrAdd(
                propertyName,
                (Func<string, Func<object, object>>)(n =>
                    {
                        MethodInfo m = o.GetType().GetProperty(propertyName).GetGetMethod();
                        MethodInfo getDelegateMethod =
                            typeof(UnitTest1).GetMethod("GetDelegate", BindingFlags.Static | BindingFlags.NonPublic)
                                             .MakeGenericMethod(o.GetType(), m.ReturnType);
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

        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1118:ParameterMustNotSpanMultipleLines",
            Justification = "Reviewed. Suppression is OK here.")]
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

        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1118:ParameterMustNotSpanMultipleLines",
            Justification = "Reviewed. Suppression is OK here.")]
        private int FasterflectWithCaching2(object o, string propertyName)
        {
            MemberGetter d = this.fasterflectDelegatesByPropertyName2.GetOrAdd(
                propertyName,
                (Func<string, MemberGetter>)(n =>
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
            #region Public Properties

            public int TestInt { get; set; }

            public int TestInt2 { get; set; }

            #endregion
        }
    }
}