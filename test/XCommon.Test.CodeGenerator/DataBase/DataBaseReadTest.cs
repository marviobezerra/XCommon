using FluentAssertions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using XCommon.CodeGeneratorV2;
using XCommon.CodeGeneratorV2.Core.DataBase;
using XCommon.CodeGeneratorV2.CSharp.Configuration;
using XCommon.Patterns.Ioc;
using XCommon.Test.CodeGenerator.DataBase.Fixture;
using Xunit;

namespace XCommon.Test.CodeGenerator.DataBase
{
	[Collection("Database collection")]
	public class DataBaseReadTest
	{
		public DatabaseFixture Fixture { get; set; }

		public DataBaseReadTest(DatabaseFixture fixture)
		{
			fixture.DBPath = string.Format(@"{0}\AppData\CodeGeneratorTest.mdf", Directory.GetCurrentDirectory());
			fixture.DBServer = @"(LocalDB)\ProjectsV13";

			Fixture = fixture;

			var config = new GeneratorConfig
			{
				CSharp = new CSharpConfig
				{
					DataBase = new CSharpDataBaseConfig
					{
						ConnectionString = string.Format(@"Data Source={0};AttachDbFilename={1};Integrated Security=True;Connect Timeout=30;MultipleActiveResultSets=True", Fixture.DBServer, Fixture.DBPath),
						SchemaExclude = new List<string> { "dbo" }
					}
				}
			};

			var generator = new Generator(config);

			var dataBaseRead = Kernel.Resolve<IDataBaseRead>();
			DataBase = dataBaseRead.Read();
		}

		private bool IsMyMachine
		{
			get
			{
				return System.Environment.MachineName.ToUpper() == "Brainiac".ToUpper();
			}
		}


		public List<DataBaseSchema> DataBase { get; set; }

		[SkippableFact(DisplayName = "Null Check")]
		[Trait("CodeGenerator", "Database reader")]
		public void NullCheckTest()
		{
			Skip.IfNot(IsMyMachine, "This test needs Azure Storage Emulator");

			DataBase.Should().NotBeNull("That is a valid database connection");
			DataBase.Should().HaveCount(2, "There are two schemas on test Database");
		}

		[SkippableFact(DisplayName = "Check Common Schema")]
		[Trait("CodeGenerator", "Database reader")]
		public void CommonSchemaTest()
		{
			Skip.IfNot(IsMyMachine, "This test needs Azure Storage Emulator");

			var schema = DataBase.FirstOrDefault(c => c.Name == "Common");

			schema.Should().NotBeNull("It should have a Common Schema");
			schema.Name.Should().Be("Common");
			schema.Tables.Should().HaveCount(3, "There are three table on Common schema");
		}

		[SkippableFact(DisplayName = "Check Register Schema")]
		[Trait("CodeGenerator", "Database reader")]
		public void RegisterSchemaTest()
		{
			Skip.IfNot(IsMyMachine, "This test needs Azure Storage Emulator");

			var schema = DataBase.FirstOrDefault(c => c.Name == "Register");

			schema.Should().NotBeNull("It should have a Register Schema");
			schema.Name.Should().Be("Register");
			schema.Tables.Should().HaveCount(2, "There are two table on Register schema");
		}

		[SkippableFact(DisplayName = "Check People Table")]
		[Trait("CodeGenerator", "Database reader")]
		public void PeopleTableTest()
		{
			Skip.IfNot(IsMyMachine, "This test needs Azure Storage Emulator");

			var schema = DataBase.FirstOrDefault(c => c.Name == "Register");
			var table = schema.Tables.FirstOrDefault(c => c.Name == "People");

			table.Should().NotBeNull("It should have a People table");
			table.Name.Should().Be("People");
			table.PKName.Should().Be("IdPerson");
		}

		[SkippableFact(DisplayName = "Check People Table - Columns")]
		[Trait("CodeGenerator", "Database reader")]
		public void PeopleTableTestColumns()
		{
			Skip.IfNot(IsMyMachine, "This test needs Azure Storage Emulator");

			var schema = DataBase.FirstOrDefault(c => c.Name == "Register");
			var columns = schema.Tables.FirstOrDefault(c => c.Name == "People").Columns;

			columns.Should().HaveCount(5, "There are five columns on People table");
			
			columns.Should().Contain(c => c.Name == "IdPerson" && c.Type == "Guid" && !c.Nullable && c.PK);
			columns.Should().Contain(c => c.Name == "Name" && c.Type == "string" && !c.Nullable && !c.PK);
			columns.Should().Contain(c => c.Name == "Email" && c.Type == "string" && !c.Nullable && !c.PK);
			columns.Should().Contain(c => c.Name == "MotherName" && c.Type == "string" && c.Nullable && !c.PK);
			columns.Should().Contain(c => c.Name == "BirthDate" && c.Type == "DateTime?" && c.Nullable && !c.PK);
		}
	}
}
