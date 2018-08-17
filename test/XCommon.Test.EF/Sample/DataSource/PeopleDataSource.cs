using System;
using System.Collections.Generic;
using System.Linq;
using XCommon.Test.EF.Sample.Context;

namespace XCommon.Test.EF.Sample.DataSource
{
	public static class PeopleDataSource
    {
		public static List<People> DBPeopleDataSource
		{
			get
			{
				var result = new List<People>();

				foreach (var item in Enumerable.Range(1, 20))
				{
					result.Add(new People
					{
						IdPerson = Guid.NewGuid(),
						Email = $"person-{item}@gmail.com",
						Name = $"Person {item}"
					});
				}

				return result;
			}
		}
	}
}
