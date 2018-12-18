using Microsoft.EntityFrameworkCore;

namespace XCommon.EF.Application.Context.Register.Map
{
	internal class UsersMap
	{
		internal static void Map(ModelBuilder modelBuilder, bool unitTest)
		{
			modelBuilder.Entity<Users>(entity =>
			{
				entity.HasKey(e => e.IdUser);

				if (!unitTest)
					entity.ToTable("Users", "Register");
				else
					entity.ToTable("RegisterUsers");

				entity.Property(e => e.IdUser)
					.IsRequired()
					.ValueGeneratedNever();

				entity.Property(e => e.IdPerson)
					.IsRequired();

				entity.Property(e => e.AccessFailedCount)
					.IsRequired();

				entity.Property(e => e.LockoutEnabled)
					.IsRequired();

				entity.Property(e => e.LockoutEnd);

				entity.Property(e => e.EmailConfirmed)
					.IsRequired();

				entity.Property(e => e.PhoneConfirmed)
					.IsRequired();

				entity.Property(e => e.PasswordHash);

				entity.Property(e => e.ProfileComplete)
					.IsRequired();

				entity
					.HasOne(d => d.People)
					.WithOne(p => p.Users)
					.HasForeignKey<People>(d => d.IdPerson);

			});
		}
	}
}
