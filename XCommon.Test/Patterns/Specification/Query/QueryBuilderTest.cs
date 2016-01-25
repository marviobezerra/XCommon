using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using XCommon.Patterns.Specification.Query;
using XCommon.Test.Patterns.Specification.Helper;
using Xunit;

namespace XCommon.Test.Patterns.Specification.Query
{
	public class QueryBuilderTest
	{
		#region DataProvider
		//ncrunch: no coverage start
		[ExcludeFromCodeCoverage]
		public static IEnumerable<object[]> GetListFilter()
		{
			List<PersonsEntity> list = new List<PersonsEntity>
			{
				new PersonsEntity { Id = 1, Name = "A", Email = "A" },
				new PersonsEntity { Id = 2, Name = "B", Email = "B" },
				new PersonsEntity { Id = 3, Name = "C", Email = "C" },
				new PersonsEntity { Id = 4, Name = "C", Email = "A" }
			};

			PersonFilter filter = new PersonFilter { };

			yield return new object[] { list, filter };
		}

		//ncrunch: no coverage end
		#endregion

		[Theory, MemberData(nameof(GetListFilter))]
		[Trait("Patterns Specification Query", "OrderBy")]
		public void Query_OrderyBy_Id(List<PersonsEntity> list, PersonFilter filter)
		{
			int expectedId = 1;

			var query = new QueryBuilder<PersonsEntity, PersonFilter>()
				.OrderBy(c => c.Name)
				.Build(list, filter);

			Assert.Equal(expectedId, query.FirstOrDefault().Id);
		}

		[Theory, MemberData(nameof(GetListFilter))]
		[Trait("Patterns Specification Query", "OrderBy")]
		public void Query_OrderyByDesc_Id(List<PersonsEntity> list, PersonFilter filter)
		{
			int expectedId = 4;

			var query = new QueryBuilder<PersonsEntity, PersonFilter>()
				.OrderByDesc(c => c.Id)
				.Build(list, filter);

			Assert.Equal(expectedId, query.FirstOrDefault().Id);
		}

		[Theory, MemberData(nameof(GetListFilter))]
		[Trait("Patterns Specification Query", "OrderBy")]
		public void Query_OrderyByDesc_Mix(List<PersonsEntity> list, PersonFilter filter)
		{
			int expectedId = 4;

			var query = new QueryBuilder<PersonsEntity, PersonFilter>()
				.OrderByDesc(c => c.Name)
				.OrderBy(c => c.Email)
				.Build(list, filter);

			Assert.Equal(expectedId, query.FirstOrDefault().Id);
		}

		[Theory, MemberData(nameof(GetListFilter))]
		[Trait("Patterns Specification Query", "OrderBy")]
		public void Query_OrderyBy_Condition_False(List<PersonsEntity> list, PersonFilter filter)
		{
			int expectedId = 1;

			var query = new QueryBuilder<PersonsEntity, PersonFilter>()
				.OrderBy(c => c.Email, c => false)
				.OrderBy(c => c.Name)
				.Build(list, filter);

			Assert.Equal(expectedId, query.FirstOrDefault().Id);
		}

		[Theory, MemberData(nameof(GetListFilter))]
		[Trait("Patterns Specification Query", "OrderBy")]
		public void Query_OrderyBy_Condition_True(List<PersonsEntity> list, PersonFilter filter)
		{
			int expectedId = 1;

			var query = new QueryBuilder<PersonsEntity, PersonFilter>()
				.OrderBy(c => c.Email, c => true)
				.OrderBy(c => c.Name)
				.Build(list, filter);

			Assert.Equal(expectedId, query.FirstOrDefault().Id);
		}

		[Theory, MemberData(nameof(GetListFilter))]
		[Trait("Patterns Specification Query", "OrderBy")]
		public void Query_OrderyByDesc_Condition_False(List<PersonsEntity> list, PersonFilter filter)
		{
			int expectedId = 3;

			var query = new QueryBuilder<PersonsEntity, PersonFilter>()
				.OrderByDesc(c => c.Name, c => false)
				.OrderByDesc(c => c.Email)
				.Build(list, filter);

			Assert.Equal(expectedId, query.FirstOrDefault().Id);
		}

		[Theory, MemberData(nameof(GetListFilter))]
		[Trait("Patterns Specification Query", "OrderBy")]
		public void Query_OrderyByDesc_Condition_True(List<PersonsEntity> list, PersonFilter filter)
		{
			int expectedId = 3;

			var query = new QueryBuilder<PersonsEntity, PersonFilter>()
				.OrderByDesc(c => c.Name, c => true)
				.OrderByDesc(c => c.Email)
				.Build(list, filter);

			Assert.Equal(expectedId, query.FirstOrDefault().Id);
		}

		[Theory, MemberData(nameof(GetListFilter))]
		[Trait("Patterns Specification Query", "Take")]
		public void Query_Take_All(List<PersonsEntity> list, PersonFilter filter)
		{
			int expectedCount = 4;

			var query = new QueryBuilder<PersonsEntity, PersonFilter>()
				.Take(0, 10)
				.Build(list, filter);

			Assert.Equal(expectedCount, query.Count());
		}

		[Theory, MemberData(nameof(GetListFilter))]
		[Trait("Patterns Specification Query", "Take")]
		public void Query_Take_One(List<PersonsEntity> list, PersonFilter filter)
		{
			int expectedCount = 1;

			var query = new QueryBuilder<PersonsEntity, PersonFilter>()
				.Take(0, 1)
				.Build(list, filter);

			Assert.Equal(expectedCount, query.Count());
		}

		[Theory, MemberData(nameof(GetListFilter))]
		[Trait("Patterns Specification Query", "Take")]
		public void Query_Take_Negative(List<PersonsEntity> list, PersonFilter filter)
		{
			int expectedCount = 0;

			var query = new QueryBuilder<PersonsEntity, PersonFilter>()
				.Take(-1, -1)
				.Build(list, filter);

			Assert.Equal(expectedCount, query.Count());
		}

		[Theory, MemberData(nameof(GetListFilter))]
		[Trait("Patterns Specification Query", "And")]
		public void Query_And_Id(List<PersonsEntity> list, PersonFilter filter)
		{
			int expectedCount = 1;
			int expectedId = 1;

			var query = new QueryBuilder<PersonsEntity, PersonFilter>()
				.And(c => c.Id == 1)
				.Build(list, filter);

			Assert.Equal(expectedId, query.FirstOrDefault().Id);
			Assert.Equal(expectedCount, query.Count());
		}

		[Theory, MemberData(nameof(GetListFilter))]
		[Trait("Patterns Specification Query", "And")]
		public void Query_And_Like(List<PersonsEntity> list, PersonFilter filter)
		{
			int expectedCount = 1;
			int expectedId = 1;

			var query = new QueryBuilder<PersonsEntity, PersonFilter>()
				.And(c => c.Name.Contains("A"))
				.Build(list, filter);

			Assert.Equal(expectedId, query.FirstOrDefault().Id);
			Assert.Equal(expectedCount, query.Count());
		}

		[Theory, MemberData(nameof(GetListFilter))]
		[Trait("Patterns Specification Query", "And")]
		public void Query_And_Condtion_True(List<PersonsEntity> list, PersonFilter filter)
		{
			int expectedCount = 1;
			int expectedId = 1;

			var query = new QueryBuilder<PersonsEntity, PersonFilter>()
				.And(c => c.Name.Contains("A"), c => true)
				.Build(list, filter);

			Assert.Equal(expectedId, query.FirstOrDefault().Id);
			Assert.Equal(expectedCount, query.Count());
		}

		[Theory, MemberData(nameof(GetListFilter))]
		[Trait("Patterns Specification Query", "And")]
		public void Query_And_Condtion_False(List<PersonsEntity> list, PersonFilter filter)
		{
			int expectedCount = 4;
			int expectedId = 1;

			var query = new QueryBuilder<PersonsEntity, PersonFilter>()
				.And(c => c.Name.Contains("A"), c => false)
				.Build(list, filter);

			Assert.Equal(expectedId, query.FirstOrDefault().Id);
			Assert.Equal(expectedCount, query.Count());
		}

		[Theory, MemberData(nameof(GetListFilter))]
		[Trait("Patterns Specification Query", "Or")]
		public void Query_Or(List<PersonsEntity> list, PersonFilter filter)
		{
			int expectedCount = 2;

			var query = new QueryBuilder<PersonsEntity, PersonFilter>()
				.Or(c => c.Email == "C", c => c.Id == 1)
				.Build(list, filter);

			Assert.True(query.Any(c => c.Id == 1));
			Assert.True(query.Any(c => c.Email == "C"));
			Assert.Equal(expectedCount, query.Count());
		}

		[Theory, MemberData(nameof(GetListFilter))]
		[Trait("Patterns Specification Query", "Or")]
		public void Query_Or_Condition_True(List<PersonsEntity> list, PersonFilter filter)
		{
			int expectedCount = 4;

			var query = new QueryBuilder<PersonsEntity, PersonFilter>()
				.Or(c => c.Email == "C", c => c.Id == 1, c => false)
				.Build(list, filter);
			
			Assert.Equal(expectedCount, query.Count());
		}
	}
}
