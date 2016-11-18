using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XCommon.Test.Patterns.Specification.Validation.Sample;
using XCommon.UnitTest;

namespace XCommon.Test.Patterns.Specification.Query.DataSource
{
    public static class SpecificationQueryDataSource
    {
        public static IEnumerable<object[]> DefaultDataSource
        {
            get
            {
                List<DataItem<PersonEntity>> result = new List<DataItem<PersonEntity>>();

                result.Add(new DataItem<PersonEntity>(null, false, "Null entity always is invalid"));
                result.Add(new DataItem<PersonEntity>(new PersonEntity(), true, "Default entity in this case is valid"));

                foreach (var item in result)
                {
                    yield return item.Cast();
                }
            }
        }
    }
}
