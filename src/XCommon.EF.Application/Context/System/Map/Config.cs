using Microsoft.EntityFrameworkCore;

namespace XCommon.EF.Application.Context.System.Map
{
	internal class ConfigMap
	{
		internal static void Map(ModelBuilder modelBuilder, bool unitTest)
		{
			modelBuilder.Entity<Config>(entity =>
			{
				entity.HasKey(e => e.IdConfig);

				if (!unitTest)
					entity.ToTable("System", "Config");
				else
					entity.ToTable("SystemConfig");

				entity.Property(e => e.IdConfig)
					.IsRequired()
					.ValueGeneratedNever();

				entity.Property(e => e.ConfigKey)
					.IsRequired();

				entity.Property(e => e.Value)
					.IsRequired();

				entity.Property(e => e.Section);
			});
		}
	}
}
