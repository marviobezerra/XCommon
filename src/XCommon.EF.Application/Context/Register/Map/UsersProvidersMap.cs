using Microsoft.EntityFrameworkCore;

namespace XCommon.EF.Application.Context.Register.Map
{
	internal class UsersProvidersMap
	{
		internal static void Map(ModelBuilder modelBuilder, bool unitTest)
		{
			modelBuilder.Entity<UsersProviders>(entity =>
			{
				entity.HasKey(e => e.IdUserProvide);

				if (!unitTest)
					entity.ToTable("UsersProviders", "Register");
				else
					entity.ToTable("RegisterUsersProviders");

				entity.Property(e => e.IdUserProvide)
					.IsRequired()
					.ValueGeneratedNever();

				entity.Property(e => e.IdUser)
					.IsRequired();

				entity.Property(e => e.Provider)
					.IsRequired();

				entity.Property(e => e.ProviderDefault)
					.IsRequired();

				entity.Property(e => e.ProviderToken)
					.IsRequired();

				entity.Property(e => e.ProviderUrlImage);

				entity.Property(e => e.ProviderUrlCover);

				entity
					.HasOne(d => d.Users)
					.WithMany(p => p.UsersProviders)
					.HasForeignKey(d => d.IdUser);
			});
		}
	}
}
