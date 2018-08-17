using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using XCommon.EF.Patterns.Repository;
using XCommon.Patterns.Ioc;
using XCommon.Patterns.Specification.Query;
using XCommon.Patterns.Specification.Validation;
using XCommon.Test.EF.Sample;
using XCommon.Test.EF.Sample.Context;
using XCommon.Test.EF.Sample.DataSource;
using XCommon.Test.EF.Sample.Entity;
using XCommon.Test.EF.Sample.Query;
using XCommon.Test.EF.Sample.Validation;
using Xunit;

namespace XCommon.Test.EF
{
	public class AddressesBusinessTest
	{
		public AddressesBusinessTest()
		{
			Kernel.Map<ISpecificationQuery<Addresses, AddressesFilter>>().To<AddressesQuery>();
			Kernel.Map<ISpecificationQuery<People, PeopleFilter>>().To<PeopleQuery>();

			Kernel.Map<ISpecificationValidation<AddressesEntity>>().To<AddressesValidation>();
			Kernel.Map<ISpecificationValidation<PeopleEntity>>().To<PeopleValidation>();

			Kernel.Map<AddressesBusiness>().To<AddressesBusiness>();
			Kernel.Map<PeopleBusiness>().To<PeopleBusiness>();


		}

		[Fact]
		public async Task GetAllByFilter()
		{
			var options = RepositoryTestHelper.IsolateContext<SampleContext>();

			using (var db = new SampleContext(options))
			{
				db.People.AddRange(PeopleDataSource.DBPeopleDataSource);
				db.SaveChanges();
			}

			var peopleBusiness = Kernel.Resolve<PeopleBusiness>();
			var result = await peopleBusiness.GetByFilterAsync(new PeopleFilter { });
			result.Count.Should().Be(PeopleDataSource.DBPeopleDataSource.Count);
		}

		[Fact]
		public async Task GetFirstByFilter()
		{
			var options = RepositoryTestHelper.IsolateContext<SampleContext>();

			using (var db = new SampleContext(options))
			{
				db.People.AddRange(PeopleDataSource.DBPeopleDataSource);
				db.SaveChanges();
			}

			var peopleBusiness = Kernel.Resolve<PeopleBusiness>();
			var result = await peopleBusiness.GetByFilterAsync(new PeopleFilter { });
			result.Count.Should().Be(PeopleDataSource.DBPeopleDataSource.Count);
		}
	}
}
