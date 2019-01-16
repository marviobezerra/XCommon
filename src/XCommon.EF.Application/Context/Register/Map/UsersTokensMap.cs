using Microsoft.EntityFrameworkCore;
namespace XCommon.EF.Application.Context.Register.Map
{
	internal class UsersTokensMap
	{
		internal static void Map(ModelBuilder modelBuilder, bool unitTest)
		{
			modelBuilder.Entity<UsersTokens>(entity =>
			{
				entity.HasKey(e => e.IdUserToken);

				if (!unitTest)
					entity.ToTable("UsersTokes", "Register");
				else
					entity.ToTable("RegisterUsersTokes");

				entity.Property(e => e.IdUserToken)
					.IsRequired()
					.ValueGeneratedNever();

				entity.Property(e => e.IdUser)
					.IsRequired();

				entity.Property(e => e.Token)
					.IsRequired();

				entity.Property(e => e.TokenType)
					.IsRequired();

				entity.Property(e => e.CreatedAt)
					.IsRequired();

				entity.Property(e => e.ValidUntil)
					.IsRequired();

				entity
					.HasOne(d => d.Users)
					.WithMany(p => p.UsersTokens)
					.HasForeignKey(d => d.IdUser);
			});
		}
	}
}
