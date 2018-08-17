using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using XCommon.Patterns.Ioc;

namespace XCommon.EF.Patterns.Repository
{
	public static class RepositoryTestHelper
	{
		public static DbContextOptions<TContext> IsolateContext<TContext>()
			where TContext : DbContext => IsolateContext<TContext>(Guid.NewGuid().ToString());

		public static DbContextOptions<TContext> IsolateContext<TContext>(string isolationName)
			where TContext : DbContext
		{
			var options = new DbContextOptionsBuilder<TContext>()
				.UseInMemoryDatabase(isolationName)
				.ConfigureWarnings(config => config.Ignore(InMemoryEventId.TransactionIgnoredWarning))
				.Options;

			Kernel.Map<DbContextOptions<TContext>>().To(options);

			return options;
		}
	}
}
