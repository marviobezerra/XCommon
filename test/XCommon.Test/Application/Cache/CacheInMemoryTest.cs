using FluentAssertions;
using System;
using XCommon.Application.Cache;
using XCommon.Application.Cache.Implementations;
using XCommon.Extensions.Converters;
using XCommon.Patterns.Repository.Entity;
using XCommon.Test.Entity;
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
		[Trait("Application", "Cache")]
        public void GetUnexistentValueString()
        {
            var result = Cache.Get<string>();
            result.Should().Be(null);
        }

        [Fact(DisplayName = "Get (Unexistent value object)")]
		[Trait("Application", "Cache")]
		public void GetUnexistentValueObject()
        {
            var result = Cache.Get<PersonEntity>();
            result.Should().Be(null);
        }

        [Fact(DisplayName = "Get (Unexistent value int)")]
		[Trait("Application", "Cache")]
		public void GetUnexistentValueInt()
        {
            var result = Cache.Get<int>();
            result.Should().Be(0);
        }

        [Fact(DisplayName = "Put (Int value)")]
		[Trait("Application", "Cache")]
		public void PutValueInt()
        {
            Cache.Put(999);

            var result = Cache.Get<int>();

            result.Should().Be(999);
        }

        [Fact(DisplayName = "Put (String value)")]
		[Trait("Application", "Cache")]
		public void PutValueString()
        {
            Cache.Put("Foo");

            var result = Cache.Get<string>();

            result.Should().Be("Foo");
        }

        [Fact(DisplayName = "Put (Object value)")]
		[Trait("Application", "Cache")]
		public void PutValueObject()
        {
            var person = new PersonEntity
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
		[Trait("Application", "Cache")]
		public void PutValueObjectCheckReference()
        {
            var person = new PersonEntity
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

            var result = Cache.Get<PersonEntity>();

            result.Id.Should().Be(person.Id);
            result.Name.Should().Be(person.Name);
        }

        [Fact(DisplayName = "Put (Int value with key)")]
		[Trait("Application", "Cache")]
		public void PutValueWithKeyInt()
        {
            var key = "IntTest";
            Cache.Put(key, -999);

            var result = Cache.Get<int>(key);

            result.Should().Be(-999);
        }

        [Fact(DisplayName = "Put (String value with key)")]
		[Trait("Application", "Cache")]
		public void PutValueWithKeyString()
        {
            var key = "StringTest";

            Cache.Put(key, "Foo");

            var result = Cache.Get<string>(key);

            result.Should().Be("Foo");
        }

        [Fact(DisplayName = "Put (Object value with key)")]
		[Trait("Application", "Cache")]
		public void PutValueWithKeyObject()
        {
            var key = "ObjectTest";
            
            var person = new PersonEntity
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
		[Trait("Application", "Cache")]
		public void PutValueWithKeyObjectCheckReference()
        {
            var key = "ObjectRefTest";
            
            var person = new PersonEntity
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

            var result = Cache.Get<PersonEntity>(key);

            result.Id.Should().Be(person.Id);
            result.Name.Should().Be(person.Name);
        }

        [Fact(DisplayName = "Expire (Not expired dateTime)")]
		[Trait("Application", "Cache")]
		public void NotExpireCacheDateTime()
        {
            var key = "ObjectRefTest";

            var person = new PersonEntity
            {
                Action = EntityAction.None,
                Id = "1".ToGuid(),
                Age = 35,
                Name = "Marvio",
                Email = "1@2.com",
            };

            Cache.Put(key, person, DateTime.Now.AddDays(1));

            var result = Cache.Get<PersonEntity>(key);

            result.Should().Be(person);
        }

        [Fact(DisplayName = "Expire (Expired dateTime)")]
		[Trait("Application", "Cache")]
		public void ExpireCacheDateTime()
        {
            var key = "ObjectRefTest";

            var person = new PersonEntity
            {
                Action = EntityAction.None,
                Id = "1".ToGuid(),
                Age = 35,
                Name = "Marvio",
                Email = "1@2.com",
            };

            Cache.Put(key, person, DateTime.Now.AddMinutes(-1));

            var result = Cache.Get<PersonEntity>(key);

            result.Should().Be(null);
        }

        [Fact(DisplayName = "Expire (Not expired timespan)")]
		[Trait("Application", "Cache")]
		public void NotExpireCacheTimeSpan()
        {
            var key = "ObjectRefTest";

            var person = new PersonEntity
            {
                Action = EntityAction.None,
                Id = "1".ToGuid(),
                Age = 35,
                Name = "Marvio",
                Email = "1@2.com",
            };

            Cache.Put(key, person, TimeSpan.FromSeconds(3600));

            var result = Cache.Get<PersonEntity>(key);

            result.Should().Be(person);
        }

        [Fact(DisplayName = "Expire (Expired timespan)")]
		[Trait("Application", "Cache")]
		public void ExpireCacheTimeSpan()
        {
            var key = "ObjectRefTest";

            var person = new PersonEntity
            {
                Action = EntityAction.None,
                Id = "1".ToGuid(),
                Age = 35,
                Name = "Marvio",
                Email = "1@2.com",
            };

            Cache.Put(key, person, TimeSpan.FromMinutes(-10));

            var result = Cache.Get<PersonEntity>(key);

            result.Should().Be(null);
        }
    }
}
