using System;
using System.Collections.Generic;
using XCommon.Extensions.Converters;
using XCommon.Patterns.Repository.Entity;
using XCommon.Test.Patterns.Specification.Validation.Sample;
using XCommon.UnitTest;

namespace XCommon.Test.Patterns.Specification.Validation.DataSource
{
    public static class PersonDataSource
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

        public static IEnumerable<object[]> CompleteDataSource
        {
            get
            {
                List<DataItem<PersonEntity>> result = new List<DataItem<PersonEntity>>();

                result.Add(new DataItem<PersonEntity>(null, false, "Null entity"));
                result.Add(new DataItem<PersonEntity>(new PersonEntity(), false, "Default entity"));
                result.Add(new DataItem<PersonEntity>(new PersonEntity { Id = Guid.Empty, Age = 38, Name = "Marvio", Email = "1@2.com" }, false, "Invalid Id"));
                result.Add(new DataItem<PersonEntity>(new PersonEntity { Id = "1".ToGuid(), Age = -1, Name = "Marvio", Email = "1@2.com" }, false, "Invalid Age (less than zero)"));
                result.Add(new DataItem<PersonEntity>(new PersonEntity { Id = "1".ToGuid(), Age = 0, Name = "Marvio", Email = "1@2.com" }, false, "Invalid Age (equals 0)"));
                result.Add(new DataItem<PersonEntity>(new PersonEntity { Id = "1".ToGuid(), Age = 38, Name = string.Empty, Email = "1@2.com" }, false, "Invalid Name (string empty)"));
                result.Add(new DataItem<PersonEntity>(new PersonEntity { Id = "1".ToGuid(), Age = 38, Name = null, Email = "1@2.com" }, false, "Invalid Name (string null)"));
                result.Add(new DataItem<PersonEntity>(new PersonEntity { Id = "1".ToGuid(), Age = 38, Name = "Marvio", Email = "1@.com" }, false, "Invalid Email"));
                result.Add(new DataItem<PersonEntity>(new PersonEntity { Id = "1".ToGuid(), Age = 38, Name = "Marvio", Email = "marvio@gmail" }, false, "Invalid Email"));
                result.Add(new DataItem<PersonEntity>(new PersonEntity { Id = "1".ToGuid(), Age = 38, Name = "Marvio", Email = "@gmail.com" }, false, "Invalid Email"));
                result.Add(new DataItem<PersonEntity>(new PersonEntity { Id = "1".ToGuid(), Age = 38, Name = "Marvio", Email = "marvio@gmail.com" }, true, "Valid person"));
                result.Add(new DataItem<PersonEntity>(new PersonEntity { Id = "1".ToGuid(), Age = 38, Name = "Marvio", Email = "marvio@gmail.com.br" }, true, "Valid person"));

                foreach (var item in result)
                {
                    yield return item.Cast();
                }
            }
        }

        public static IEnumerable<object[]> ComplexDataSource
        {
            get
            {
                List<DataItem<PersonEntity>> result = new List<DataItem<PersonEntity>>();

                // New
                result.Add(new DataItem<PersonEntity>(new PersonEntity { Action = EntityAction.New, Id = "1".ToGuid(), Age = 15, Name = "Marvio", Email = "marvio@gmail.com.br" }, false, "Invalid, young person"));
                result.Add(new DataItem<PersonEntity>(new PersonEntity { Action = EntityAction.New, Id = "1".ToGuid(), Age = 18, Name = "Marvio", Email = "marvio@gmail.com.br" }, true, "Valid person"));

                // Update
                result.Add(new DataItem<PersonEntity>(new PersonEntity { Action = EntityAction.Update, Id = "1".ToGuid(), Age = 18, Name = "Marvio", Email = "marvio@gmail.com.br" }, false, "Invalid, young person"));
                result.Add(new DataItem<PersonEntity>(new PersonEntity { Action = EntityAction.Update, Id = "1".ToGuid(), Age = 21, Name = "Marvio", Email = "marvio@gmail.com.br" }, true, "Valid person"));

                // Delete
                result.Add(new DataItem<PersonEntity>(new PersonEntity { Action = EntityAction.Delete, Id = Guid.Empty, Age = 18, Name = "Marvio", Email = "marvio@gmail.com.br" }, false, "Invalid ID"));
                result.Add(new DataItem<PersonEntity>(new PersonEntity { Action = EntityAction.Delete, Id = "1".ToGuid(), Age = 21, Name = "Marvio", Email = "marvio@gmail.com.br" }, true, "Valid person"));

                // None
                result.Add(new DataItem<PersonEntity>(new PersonEntity { Action = EntityAction.None, Id = "1".ToGuid(), Age = 18, Name = null, Email = "marvio@gmail.com.br" }, false, "Invalid name"));
                result.Add(new DataItem<PersonEntity>(new PersonEntity { Action = EntityAction.None, Id = "1".ToGuid(), Age = 18, Name = string.Empty, Email = "marvio@gmail.com.br" }, false, "Invalid name"));
                result.Add(new DataItem<PersonEntity>(new PersonEntity { Action = EntityAction.None, Id = "1".ToGuid(), Age = 18, Name = "", Email = "marvio@gmail.com.br" }, false, "Invalid name"));
                result.Add(new DataItem<PersonEntity>(new PersonEntity { Action = EntityAction.None, Id = "1".ToGuid(), Age = 18, Name = "     ", Email = "marvio@gmail.com.br" }, false, "Invalid name"));

                result.Add(new DataItem<PersonEntity>(new PersonEntity { Action = EntityAction.None, Id = "1".ToGuid(), Age = 18, Name = "Marvio", Email = "@gmail.com.br" }, false, "Invalid email"));
                result.Add(new DataItem<PersonEntity>(new PersonEntity { Action = EntityAction.None, Id = "1".ToGuid(), Age = 18, Name = "Marvio", Email = "marvio@gmail" }, false, "Invalid email"));
                result.Add(new DataItem<PersonEntity>(new PersonEntity { Action = EntityAction.None, Id = "1".ToGuid(), Age = 18, Name = "Marvio", Email = null }, false, "Invalid email"));
                result.Add(new DataItem<PersonEntity>(new PersonEntity { Action = EntityAction.None, Id = "1".ToGuid(), Age = 18, Name = "Marvio", Email = string.Empty }, false, "Invalid email"));
                result.Add(new DataItem<PersonEntity>(new PersonEntity { Action = EntityAction.None, Id = "1".ToGuid(), Age = 18, Name = "Marvio", Email = "" }, false, "Invalid email"));
                result.Add(new DataItem<PersonEntity>(new PersonEntity { Action = EntityAction.None, Id = "1".ToGuid(), Age = 18, Name = "Marvio", Email = "          " }, false, "Invalid email"));

                result.Add(new DataItem<PersonEntity>(new PersonEntity { Action = EntityAction.None, Id = "1".ToGuid(), Age = 18, Name = "Marvio", Email = "marvio@gmail.com.br" }, true, "Valid person"));


                foreach (var item in result)
                {
                    yield return item.Cast();
                }
            }
        }
    }
}
