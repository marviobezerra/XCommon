using System;
using XCommon.Extensions.Converters;
using XCommon.Patterns.Repository.Entity;
using XCommon.Test.Entity;

namespace XCommon.Test.Patterns.Ioc.Sample
{
    public class TestClass02 : ITestClass
    {
        public TestClass02(int classKey)
        {
            ClassKey = classKey;
        }

        public int ClassKey { get; set; }

        public PersonEntity LastSavedPerson { get; private set; }

        public PersonEntity Load()
        {
            return new PersonEntity
            {
                Action = EntityAction.None,
                Id = "1".ToGuid(),
                Age = 18,
                Email = "marvio.bezerra@gmail.com",
                Name = "Márvio André"
            };
        }

        public bool Save(PersonEntity person)
        {
            LastSavedPerson = person;
            return true;
        }
    }
}
