using Microsoft.EntityFrameworkCore;

namespace XCommon.EF.Application.Context.Register.Map
{
	internal class UsersRolesMap
	{
		internal static void Map(ModelBuilder modelBuilder, bool unitTest)
		{
			modelBuilder.Entity<UsersRoles>(entity =>
			{
				entity.HasKey(e => e.IdUserRole);

				if (!unitTest)
					entity.ToTable("UsersRoles", "Register");
				else
					entity.ToTable("RegisterUsersRoles");

				entity.Property(e => e.IdUserRole)
					.IsRequired()
					.ValueGeneratedNever();

				entity.Property(e => e.IdUser)
					.IsRequired();

				entity.Property(e => e.Role)
					.IsRequired();

				entity
					.HasOne(d => d.Users)
					.WithMany(p => p.UsersRoles)
					.HasForeignKey(d => d.IdUser);
			});
		}
	}
}
