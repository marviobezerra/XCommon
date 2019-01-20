using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using XCommon.Application.Settings;
using XCommon.EF.Application.Context.Register;
using XCommon.EF.Application.Context.Register.Map;
using XCommon.EF.Application.Context.System;
using XCommon.EF.Application.Context.System.Map;
using XCommon.Patterns.Ioc;

namespace XCommon.EF.Application.Context
{
	public class XCommonDbContext : DbContext
	{
		protected virtual IApplicationSettings AppSettings => Kernel.Resolve<IApplicationSettings>();

		public virtual DbSet<People> People { get; set; }

		public virtual DbSet<Users> Users { get; set; }

		public virtual DbSet<UsersProviders> UsersProviders { get; set; }

		public Configs Configs { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			if (AppSettings.UnitTest)
			{
				options
					.UseInMemoryDatabase("XCommonDbContext")
					.ConfigureWarnings(config => config.Ignore(InMemoryEventId.TransactionIgnoredWarning));

				return;
			}

			options.UseSqlServer(AppSettings.DataBaseConnectionString);

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			PeopleMap.Map(modelBuilder, AppSettings.UnitTest);
			UsersMap.Map(modelBuilder, AppSettings.UnitTest);
			UsersProvidersMap.Map(modelBuilder, AppSettings.UnitTest);
			UsersRolesMap.Map(modelBuilder, AppSettings.UnitTest);
			UsersTokensMap.Map(modelBuilder, AppSettings.UnitTest);

			ConfigsMap.Map(modelBuilder, AppSettings.UnitTest);
		}
	}
}
