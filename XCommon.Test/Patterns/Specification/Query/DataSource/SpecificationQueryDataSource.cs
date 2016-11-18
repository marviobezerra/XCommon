using System.Collections.Generic;
using XCommon.Extensions.Converters;
using XCommon.Patterns.Repository.Entity;
using XCommon.Test.Entity;
using XCommon.UnitTest;
using XCommon.Util;

namespace XCommon.Test.Patterns.Specification.Query.DataSource
{
    public static class SpecificationQueryDataSource
    {
        public static List<PersonEntity> PeopleList
        {
            get
            {
                List<PersonEntity> result = new List<PersonEntity>();

                result.Add(new PersonEntity { Action = EntityAction.None, Id = "01".ToGuid(), Name = "Marvio Andre Bezerra Silverio", Email = "marvio.bezerra@gmail.com", Age = 35 });
                result.Add(new PersonEntity { Action = EntityAction.None, Id = "02".ToGuid(), Name = "Maria Jane", Email = "maria.jane@gmail.com", Age = 60 });
                result.Add(new PersonEntity { Action = EntityAction.None, Id = "03".ToGuid(), Name = "Eveline Andrade", Email = "eveline@gmail.com", Age = 41 });
                result.Add(new PersonEntity { Action = EntityAction.None, Id = "04".ToGuid(), Name = "Jader Barbosa", Email = "jader@gmail.com", Age = 38 });
                result.Add(new PersonEntity { Action = EntityAction.None, Id = "05".ToGuid(), Name = "Joaquim Jose", Email = "joaquim@gmail.com", Age = 81 });
                result.Add(new PersonEntity { Action = EntityAction.None, Id = "06".ToGuid(), Name = "Evandro Neves", Email = "evandro@gmail.com", Age = 18 });
                result.Add(new PersonEntity { Action = EntityAction.None, Id = "07".ToGuid(), Name = "Kleiton Araujo", Email = "kleiton@gmail.com", Age = 21 });
                result.Add(new PersonEntity { Action = EntityAction.None, Id = "08".ToGuid(), Name = "Maria Albina", Email = "maria.albina@gmail.com", Age = 30 });
                result.Add(new PersonEntity { Action = EntityAction.None, Id = "09".ToGuid(), Name = "Maria Lucia", Email = "maria.lucia@gmail.com", Age = 61 });
                result.Add(new PersonEntity { Action = EntityAction.None, Id = "10".ToGuid(), Name = "Antonio Viana", Email = "antonio.viana@gmail.com", Age = 50 });

                result.Add(new PersonEntity { Action = EntityAction.None, Id = "11".ToGuid(), Name = "Jonhy Begood", Email = "jonhy.begood@gmail.com", Age = 48 });
                result.Add(new PersonEntity { Action = EntityAction.None, Id = "12".ToGuid(), Name = "Mary Ane", Email = "mary.ane@gmail.com", Age = 18 });
                result.Add(new PersonEntity { Action = EntityAction.None, Id = "13".ToGuid(), Name = "Sebastian Sho", Email = "sebastian.sho@gmail.com", Age = 21 });
                result.Add(new PersonEntity { Action = EntityAction.None, Id = "14".ToGuid(), Name = "Mary Angel", Email = "mary.angel@gmail.com", Age = 22 });
                result.Add(new PersonEntity { Action = EntityAction.None, Id = "15".ToGuid(), Name = "Lucas Abrel", Email = "lucas.abre@gmail.com", Age = 26 });
                result.Add(new PersonEntity { Action = EntityAction.None, Id = "16".ToGuid(), Name = "Lola Marques", Email = "lola.marques@gmail.com", Age = 29 });
                result.Add(new PersonEntity { Action = EntityAction.None, Id = "17".ToGuid(), Name = "Rafael Vaiana", Email = "rafael.viana@gmail.com", Age = 36 });
                result.Add(new PersonEntity { Action = EntityAction.None, Id = "18".ToGuid(), Name = "Jenyffer Liz", Email = "jenyffer.liz@gmail.com", Age = 95 });
                result.Add(new PersonEntity { Action = EntityAction.None, Id = "19".ToGuid(), Name = "Brenda Pires", Email = "brenda.pires@gmail.com", Age = 55 });
                result.Add(new PersonEntity { Action = EntityAction.None, Id = "20".ToGuid(), Name = "Joana Prado", Email = "joana.prado@gmail.com", Age = 40 });

                return result;
            }
        }

        public static IEnumerable<object[]> DefaultDataSource
        {
            get
            {
                DataList<PersonFilter, int> result = new DataList<PersonFilter, int>();


                return result.Cast();
            }
        }
    }
}
