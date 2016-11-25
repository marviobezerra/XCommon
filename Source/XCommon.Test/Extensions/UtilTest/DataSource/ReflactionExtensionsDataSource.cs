using System;
using System.Collections.Generic;
using XCommon.Test.Extensions.UtilTest.Sample;
using XCommon.Util;

namespace XCommon.Test.Extensions.UtilTest.DataSource
{
    public static class ReflactionExtensionsDataSource
    {
        public static IEnumerable<object[]> IsBasedInDataSource
        {
            get
            {
                PairList<Type, Type, bool> result = new PairList<Type, Type, bool>();

                result.Add(typeof(IEnumerable<string>), typeof(List<string>), true, "IsValid IEnumerable<string> > List<string>");
                result.Add(typeof(IEnumerable<string>), typeof(List<string>), true, "IsValid IEnumerable<string> > List<string>");

                result.Add(typeof(int), typeof(Int32), true, "IsValid int > Int32");
                result.Add(typeof(long), typeof(Int64), true, "IsValid long > Int64");

                result.Add(typeof(int), typeof(DateTime), false, "IsInValid int > DateTime");
                result.Add(typeof(int), typeof(string), false, "IsInValid int > DateTime");
                
                result.Add(typeof(IWeapon), typeof(IWeapon), true, "IsValid IWeapon > IWeapon");
                result.Add(typeof(IWeapon), typeof(Sword), true, "IsValid IWeapon > Sword");
                result.Add(typeof(IWeapon), typeof(Rocket), true, "IsValid IWeapon > Rocket");
                result.Add(typeof(ISolder), typeof(Solder), true, "IsValid ISolder > Solder");

                result.Add(typeof(Rocket), typeof(Sword), false, "IsInvalid Rocket > Sword");
                result.Add(typeof(Solder), typeof(IWeapon), false, "IsInvalid Solder > IWeapon");
                result.Add(typeof(Sword), typeof(IWeapon), false, "IsInvalid Sword > IWeapon");

                result.Add(typeof(IComplexType<List<string>, Dictionary<decimal, IEnumerable<DateTime>>, Guid>), typeof(ComplexType<List<string>, Dictionary<decimal, IEnumerable<DateTime>>, Guid>), true, "IsValid IComplexType<List<string>, Dictionary<decimal, IEnumerable<DateTime>>, Guid> > ComplexType<List<string>, Dictionary<decimal, IEnumerable<DateTime>>, Guid>");
                result.Add(typeof(ComplexType<List<string>, Dictionary<decimal, IEnumerable<DateTime>>, Guid>), typeof(ComplexType), true, "IsValid ComplexType<List<string>, Dictionary<decimal, IEnumerable<DateTime>>, Guid> > ComplexType");
                result.Add(typeof(IComplexType<List<string>, Dictionary<decimal, IEnumerable<DateTime>>, Guid>), typeof(ComplexType), true, "IsValid IComplexType<List<string>, Dictionary<decimal, IEnumerable<DateTime>>, Guid> > ComplexType");

                result.Add(typeof(IComplexType<List<string>, Dictionary<int, IEnumerable<DateTime>>, Guid>), typeof(ComplexType), false, "IsValid IComplexType<List<string>, Dictionary<int, IEnumerable<DateTime>>, Guid> > ComplexType");

                return result.Cast();
            }
        }

        public static IEnumerable<object[]> GetClassNameDataSource
        {
            get
            {
                PairList<Type, string> result = new PairList<Type, string>();

                result.Add(typeof(IEnumerable<string>), "IEnumerable<string>");
                result.Add(typeof(List<string>), "List<string>");

                result.Add(typeof(int), "int");
                result.Add(typeof(long), "long");

                result.Add(typeof(string), "string");

                result.Add(typeof(IWeapon), "IWeapon");
                result.Add(typeof(Sword), "Sword");
                result.Add(typeof(Rocket), "Rocket");
                result.Add(typeof(ISolder), "ISolder");


                result.Add(typeof(IComplexType<List<string>, Dictionary<decimal, IEnumerable<DateTime>>, Guid>), "IComplexType<List<string>, Dictionary<decimal, IEnumerable<DateTime>>, Guid>");
                result.Add(typeof(ComplexType<List<string>, Dictionary<decimal, IEnumerable<DateTime>>, Guid>), "ComplexType<List<string>, Dictionary<decimal, IEnumerable<DateTime>>, Guid>");
                result.Add(typeof(ComplexType), "ComplexType");

                return result.Cast();
            }
        }

        public static IEnumerable<object[]> GetClassNameDataSourceNoPrimitives
        {
            get
            {
                PairList<Type, string> result = new PairList<Type, string>();

                result.Add(typeof(IEnumerable<string>), "IEnumerable<String>");
                result.Add(typeof(List<string>), "List<String>");

                result.Add(typeof(int), "Int32");
                result.Add(typeof(long), "Int64");

                result.Add(typeof(string), "String");

                result.Add(typeof(IWeapon), "IWeapon");
                result.Add(typeof(Sword), "Sword");
                result.Add(typeof(Rocket), "Rocket");
                result.Add(typeof(ISolder), "ISolder");


                result.Add(typeof(IComplexType<List<string>, Dictionary<decimal, IEnumerable<DateTime>>, Guid>), "IComplexType<List<String>, Dictionary<Decimal, IEnumerable<DateTime>>, Guid>");
                result.Add(typeof(ComplexType<List<string>, Dictionary<decimal, IEnumerable<DateTime>>, Guid>), "ComplexType<List<String>, Dictionary<Decimal, IEnumerable<DateTime>>, Guid>");
                result.Add(typeof(ComplexType), "ComplexType");

                return result.Cast();
            }
        }
    }
}
