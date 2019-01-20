using Microsoft.EntityFrameworkCore;

namespace XCommon.EF.Application.Context.System.Map
{
	internal class ConfigsMap
	{
		internal static void Map(ModelBuilder modelBuilder, bool unitTest)
		{
			modelBuilder.Entity<Configs>(entity =>
			{
				entity.HasKey(e => e.IdConfig);

				if (!unitTest)
					entity.ToTable("System", "Configs");
				else
					entity.ToTable("SystemConfigs");

				entity.Property(e => e.IdConfig)
					.IsRequired()
					.ValueGeneratedNever();

				entity.Property(e => e.ConfigKey)
					.IsRequired();

				entity.Property(e => e.Value);

				entity.Property(e => e.Module)
					.IsRequired();
			});
		}
	}
}
