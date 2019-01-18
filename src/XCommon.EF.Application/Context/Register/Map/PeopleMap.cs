using Microsoft.EntityFrameworkCore;

namespace XCommon.EF.Application.Context.Register.Map
{
	class PeopleMap
	{
		internal static void Map(ModelBuilder modelBuilder, bool unitTest)
		{
			modelBuilder.Entity<People>(entity =>
			{
				entity.HasKey(e => e.IdPerson);
				
				if (!unitTest)
					entity.ToTable("People", "Register");
				else
					entity.ToTable("RegisterPeople");

				entity.Property(e => e.IdPerson)
					.IsRequired()
					.ValueGeneratedNever();

				entity.Property(e => e.Name)
					.IsRequired()
					.HasMaxLength(300);

				entity.Property(e => e.Email)
					.IsRequired()
					.HasMaxLength(300);

				entity.Property(e => e.Gender)
					.IsRequired();

				entity.Property(e => e.Birthday);

				entity.Property(e => e.Culture)
					.IsRequired()
					.HasMaxLength(6);

				entity.Property(e => e.TimeZone)
					.IsRequired();

				entity.Property(e => e.About);

				entity.Property(e => e.ImageProfile);

				entity.Property(e => e.ImageCover);

				entity.Property(e => e.CreatedAt)
					.IsRequired();

				entity.Property(e => e.ChangedAt)
					.IsRequired();

			});
		}
	}
}
