using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace XCommon.Test.EF.Sample.Context
{
	public class SampleContext : DbContext
	{
		public SampleContext()
		{
		}

		public SampleContext(DbContextOptions<SampleContext> options)
			: base(options)
		{
		}

		public DbSet<Addresses> Addresses { get; set; }

		public DbSet<People> People { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			if (!options.IsConfigured)
			{
				options
					.UseInMemoryDatabase("XCommonSampleContext")
					.ConfigureWarnings(config => config.Ignore(InMemoryEventId.TransactionIgnoredWarning));
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Addresses>(entity =>
			{

				entity.HasKey(e => e.IdAddress);

				entity.Property(e => e.IdAddress)
				   .IsRequired()
				   .ValueGeneratedNever();

				entity.Property(e => e.IdPerson)
				   .IsRequired();

				entity.Property(e => e.StreetName)
					.IsRequired()
					.HasMaxLength(100);

				entity.Property(e => e.PostalCode)
					.IsRequired()
					.HasMaxLength(100);

				entity
					.HasOne(d => d.People)
					.WithMany(p => p.Addresses)
					.HasForeignKey(d => d.IdPerson);
			});

			modelBuilder.Entity<People>(entity =>
			{

				entity.HasKey(e => e.IdPerson);

				entity.Property(e => e.IdPerson)
				   .IsRequired()
				   .ValueGeneratedNever();

				entity.Property(e => e.Name)
					.IsRequired()
					.HasMaxLength(100);

				entity.Property(e => e.Email)
					.IsRequired()
					.HasMaxLength(100);

				entity
					.HasMany(d => d.Addresses)
					.WithOne(p => p.People)
					.HasForeignKey(d => d.IdPerson);
			});
		}
	}
}
