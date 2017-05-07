using FluentAssertions;
using System.Collections.Generic;
using XCommon.CodeGeneratorV2;
using XCommon.CodeGeneratorV2.Core.DataBase;
using XCommon.CodeGeneratorV2.CSharp.Configuration;
using XCommon.Patterns.Ioc;
using Xunit;
using System.IO;
using System.Linq;
using System;

namespace XCommon.Test.CodeGenerator.DataBase
{
	public class DataBaseReadTest
	{
		public DataBaseReadTest()
		{
			var path = Directory.GetCurrentDirectory();

			var config = new GeneratorConfig
			{
				CSharp = new CSharpConfig
				{
					DataBase = new CSharpDataBaseConfig
					{
						ConnectionString = string.Format(@"Data Source=(LocalDB)\ProjectsV13;AttachDbFilename={0}\AppData\CodeGeneratorTest.mdf;Integrated Security=True;Connect Timeout=30;MultipleActiveResultSets=True", path),
						SchemaExclude = new List<string> { "dbo" }
					}
				}
			};

			var generator = new Generator(config);

			var dataBaseRead = Kernel.Resolve<IDataBaseRead>();
			DataBase = dataBaseRead.Read();
		}

		public List<DataBaseSchema> DataBase { get; set; }

		[Fact(DisplayName = "Null Check")]
		[Trait("CodeGenerator", "Database reader")]
		public void NullCheckTest()
		{
			DataBase.Should().NotBeNull("That is a valid database connection");
			DataBase.Should().HaveCount(2, "There are two schemas on test Database");
		}

		[Fact(DisplayName = "Check Common Schema")]
		[Trait("CodeGenerator", "Database reader")]
		public void CommonSchemaTest()
		{
			var schema = DataBase.FirstOrDefault(c => c.Name == "Common");

			schema.Should().NotBeNull("It should have a Common Schema");
			schema.Name.Should().Be("Common");
			schema.Tables.Should().HaveCount(3, "There are three table on Common schema");
		}

		[Fact(DisplayName = "Check Register Schema")]
		[Trait("CodeGenerator", "Database reader")]
		public void RegisterSchemaTest()
		{
			var schema = DataBase.FirstOrDefault(c => c.Name == "Register");

			schema.Should().NotBeNull("It should have a Register Schema");
			schema.Name.Should().Be("Register");
			schema.Tables.Should().HaveCount(2, "There are two table on Register schema");
		}

		[Fact(DisplayName = "Check People Table")]
		[Trait("CodeGenerator", "Database reader")]
		public void PeopleTableTest()
		{
			var schema = DataBase.FirstOrDefault(c => c.Name == "Register");
			var table = schema.Tables.FirstOrDefault(c => c.Name == "People");

			table.Should().NotBeNull("It should have a People table");
			table.Name.Should().Be("People");
			table.PKName.Should().Be("IdPerson");
		}

		[Fact(DisplayName = "Check People Table - Columns")]
		[Trait("CodeGenerator", "Database reader")]
		public void PeopleTableTestColumns()
		{
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
