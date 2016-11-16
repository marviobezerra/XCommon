using FluentAssertions;
using System;
using XCommon.Application.Cache;
using XCommon.Application.Cache.Implementations;
using XCommon.Extensions.Converters;
using XCommon.Patterns.Repository.Entity;
using XCommon.Test.Patterns.Specification.Validation.Sample;
using Xunit;

namespace XCommon.Test.Application.Cache
{
    public class CacheInMemoryTest
    {
        public CacheInMemoryTest()
        {
            Cache = new CacheInMemory();
        }

        public ICache Cache { get; set; }

        [Fact(DisplayName = "Get (Unexistent value string)")]
        public void GetUnexistentValueString()
        {
            var result = Cache.Get<string>();
            result.Should().Be(null);
        }

        [Fact(DisplayName = "Get (Unexistent value object)")]
        public void GetUnexistentValueObject()
        {
            var result = Cache.Get<PersonEntity>();
            result.Should().Be(null);
        }

        [Fact(DisplayName = "Get (Unexistent value int)")]
        public void GetUnexistentValueInt()
        {
            var result = Cache.Get<int>();
            result.Should().Be(0);
        }

        [Fact(DisplayName = "Put (Int value)")]
        public void PutValueInt()
        {
            Cache.Put(999);

            var result = Cache.Get<int>();

            result.Should().Be(999);
        }

        [Fact(DisplayName = "Put (String value)")]
        public void PutValueString()
        {
            Cache.Put("Foo");

            var result = Cache.Get<string>();

            result.Should().Be("Foo");
        }

        [Fact(DisplayName = "Put (Object value)")]
        public void PutValueObject()
        {
            PersonEntity person = new PersonEntity
            {
                Action = EntityAction.None,
                Id = "1".ToGuid(),
                Age = 35,
                Name = "Marvio",
                Email = "1@2.com",
            };

            Cache.Put(person);

            var result = Cache.Get<PersonEntity>();

            result.Should().Be(person);
        }

        [Fact(DisplayName = "Put (Object value, checking reference)")]
        public void PutValueObjectCheckReference()
        {
            PersonEntity person = new PersonEntity
            {
                Action = EntityAction.None,
                Id = "1".ToGuid(),
                Age = 35,
                Name = "Marvio",
                Email = "1@2.com",
            };

            Cache.Put(person);

            person.Id = "2".ToGuid();
            person.Name = "Marvio Andre";

            PersonEntity result = Cache.Get<PersonEntity>();

            result.Id.Should().Be(person.Id);
            result.Name.Should().Be(person.Name);
        }

        [Fact(DisplayName = "Put (Int value with key)")]
        public void PutValueWithKeyInt()
        {
            string key = "IntTest";
            Cache.Put(key, -999);

            var result = Cache.Get<int>(key);

            result.Should().Be(-999);
        }

        [Fact(DisplayName = "Put (String value with key)")]
        public void PutValueWithKeyString()
        {
            string key = "StringTest";

            Cache.Put(key, "Foo");

            var result = Cache.Get<string>(key);

            result.Should().Be("Foo");
        }

        [Fact(DisplayName = "Put (Object value with key)")]
        public void PutValueWithKeyObject()
        {
            string key = "ObjectTest";
            
            PersonEntity person = new PersonEntity
            {
                Action = EntityAction.None,
                Id = "1".ToGuid(),
                Age = 35,
                Name = "Marvio",
                Email = "1@2.com",
            };

            Cache.Put(key, person);

            var result = Cache.Get<PersonEntity>(key);

            result.Should().Be(person);
        }

        [Fact(DisplayName = "Put (Object value, checking reference with key)")]
        public void PutValueWithKeyObjectCheckReference()
        {
            string key = "ObjectRefTest";
            
            PersonEntity person = new PersonEntity
            {
                Action = EntityAction.None,
                Id = "1".ToGuid(),
                Age = 35,
                Name = "Marvio",
                Email = "1@2.com",
            };

            Cache.Put(key, person);

            person.Id = "2".ToGuid();
            person.Name = "Marvio Andre";

            PersonEntity result = Cache.Get<PersonEntity>(key);

            result.Id.Should().Be(person.Id);
            result.Name.Should().Be(person.Name);
        }

        [Fact(DisplayName = "Expire (Not expired dateTime)")]
        public void NotExpireCacheDateTime()
        {
            string key = "ObjectRefTest";

            PersonEntity person = new PersonEntity
            {
                Action = EntityAction.None,
                Id = "1".ToGuid(),
                Age = 35,
                Name = "Marvio",
                Email = "1@2.com",
            };

            Cache.Put(key, person, DateTime.Now.AddDays(1));

            PersonEntity result = Cache.Get<PersonEntity>(key);

            result.Should().Be(person);
        }

        [Fact(DisplayName = "Expire (Expired dateTime)")]
        public void ExpireCacheDateTime()
        {
            string key = "ObjectRefTest";

            PersonEntity person = new PersonEntity
            {
                Action = EntityAction.None,
                Id = "1".ToGuid(),
                Age = 35,
                Name = "Marvio",
                Email = "1@2.com",
            };

            Cache.Put(key, person, DateTime.Now.AddMinutes(-1));

            PersonEntity result = Cache.Get<PersonEntity>(key);

            result.Should().Be(null);
        }

        [Fact(DisplayName = "Expire (Not expired timespan)")]
        public void NotExpireCacheTimeSpan()
        {
            string key = "ObjectRefTest";

            PersonEntity person = new PersonEntity
            {
                Action = EntityAction.None,
                Id = "1".ToGuid(),
                Age = 35,
                Name = "Marvio",
                Email = "1@2.com",
            };

            Cache.Put(key, person, TimeSpan.FromSeconds(3600));

            PersonEntity result = Cache.Get<PersonEntity>(key);

            result.Should().Be(person);
        }

        [Fact(DisplayName = "Expire (Expired timespan)")]
        public void ExpireCacheTimeSpan()
        {
            string key = "ObjectRefTest";

            PersonEntity person = new PersonEntity
            {
                Action = EntityAction.None,
                Id = "1".ToGuid(),
                Age = 35,
                Name = "Marvio",
                Email = "1@2.com",
            };

            Cache.Put(key, person, TimeSpan.FromMinutes(-10));

            PersonEntity result = Cache.Get<PersonEntity>(key);

            result.Should().Be(null);
        }
    }
}
