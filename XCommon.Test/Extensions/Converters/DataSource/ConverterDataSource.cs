using System.Collections.Generic;
using XCommon.Test.Extensions.Converters.Sample;
using XCommon.UnitTest;
using XCommon.Util;

namespace XCommon.Test.Extensions.Converters.DataSource
{
    public static class ConverterDataSource
    {
        public static IEnumerable<object[]> PersonConverterDataSource
        {
            get
            {
                List<DataItem<Pair<PersonAEntity, PersonBEntity>>> result = new List<DataItem<Pair<PersonAEntity, PersonBEntity>>>();

                result.Add(new DataItem<Pair<PersonAEntity, PersonBEntity>>(new Pair<PersonAEntity, PersonBEntity>(), false, "Null value"));

                foreach (var item in result)
                {
                    yield return item.Cast();
                }
            }
        }
    }
}
