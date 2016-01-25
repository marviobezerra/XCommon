using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using XCommon.Patterns.Repository.Executes;
using XCommon.Patterns.Specification.Entity.Extensions;
using XCommon.Patterns.Specification.Entity.Implementation;
using XCommon.Test.Patterns.Specification.Helper;
using Xunit;

namespace XCommon.Test.Patterns.Specification.Entity
{
    public class AndIsNotEmptyTest
    {
		#region DataProvider
		//ncrunch: no coverage start
		[ExcludeFromCodeCoverage]
		public static IEnumerable<object[]> GetDateNotEmptyValues()
		{
			string validMessage = "Not Empty Value";
			string inValidMessage = "Empty Value";

			yield return new object[] { null, false, inValidMessage };
			yield return new object[] { (DateTime?)new DateTime(2015, 1, 1), true, validMessage };
		}

		public static IEnumerable<object[]> GetIntNotEmptyValues()
		{
			string validMessage = "Not Empty Value";
			string inValidMessage = "Empty Value";

			yield return new object[] { null, false, inValidMessage };
			yield return new object[] { (int?)1, true, validMessage };
		}

		public static IEnumerable<object[]> GetDecimalNotEmptyValues()
		{
			string validMessage = "Not Empty Value";
			string inValidMessage = "Empty Value";

			yield return new object[] { null, false, inValidMessage };
			yield return new object[] { (decimal?)1, true, validMessage };
		}
		//ncrunch: no coverage end
		#endregion

		[Theory, MemberData(nameof(GetIntNotEmptyValues))]
        [Trait("Patterns Specification Entity AndIsNotEmpty", "Int")]
        public void NotEmpty_Int_With_Execute(int? value, bool valid, string message)
        {
			Execute execute = new Execute();
            GenerictValueEntity<int?> entity = new GenerictValueEntity<int?>(value);

            SpecificationEntity<GenerictValueEntity<int?>> spec = new SpecificationEntity<GenerictValueEntity<int?>>()
                .AndIsNotEmpty(c => c.Value, message);

            var result = spec.IsSatisfiedBy(entity, execute);

            Assert.Equal(valid, result);
			Assert.Equal(valid, !execute.HasErro);

			if (!valid)
			{
				Assert.Equal(1, execute.Messages.Count);
				Assert.Equal(message, execute.Messages[0].Message);
			}
		}

		[Theory, MemberData(nameof(GetIntNotEmptyValues))]
		[Trait("Patterns Specification Entity AndIsNotEmpty", "Int")]
		public void NotEmpty_Int_Without_Execute(int? value, bool valid, string message)
		{
			GenerictValueEntity<int?> entity = new GenerictValueEntity<int?>(value);

			SpecificationEntity<GenerictValueEntity<int?>> spec = new SpecificationEntity<GenerictValueEntity<int?>>()
				.AndIsNotEmpty(c => c.Value);

			var result = spec.IsSatisfiedBy(entity);

			Assert.Equal(valid, result);
		}



		[Theory, MemberData(nameof(GetDecimalNotEmptyValues))]
		[Trait("Patterns Specification Entity AndIsNotEmpty", "Decimal")]
		public void NotEmpty_Decimal_With_Execute(decimal? value, bool valid, string message)
		{
			Execute execute = new Execute();
			GenerictValueEntity<decimal?> entity = new GenerictValueEntity<decimal?>(value);

			SpecificationEntity<GenerictValueEntity<decimal?>> spec = new SpecificationEntity<GenerictValueEntity<decimal?>>()
				.AndIsNotEmpty(c => c.Value, message);

			var result = spec.IsSatisfiedBy(entity, execute);

			Assert.Equal(valid, result);
			Assert.Equal(valid, !execute.HasErro);

			if (!valid)
			{
				Assert.Equal(1, execute.Messages.Count);
				Assert.Equal(message, execute.Messages[0].Message);
			}
		}

		[Theory, MemberData(nameof(GetDecimalNotEmptyValues))]
		[Trait("Patterns Specification Entity AndIsNotEmpty", "Decimal")]
		public void NotEmpty_Decimal_Without_Execute(decimal? value, bool valid, string message)
		{
			GenerictValueEntity<decimal?> entity = new GenerictValueEntity<decimal?>(value);

			SpecificationEntity<GenerictValueEntity<decimal?>> spec = new SpecificationEntity<GenerictValueEntity<decimal?>>()
				.AndIsNotEmpty(c => c.Value);

			var result = spec.IsSatisfiedBy(entity);

			Assert.Equal(valid, result);
		}

		[Theory, MemberData(nameof(GetDateNotEmptyValues))]
		[Trait("Patterns Specification Entity AndIsNotEmpty", "Date")]
		public void NotEmpty_Date_With_Execute(DateTime? value, bool valid, string message)
		{
			Execute execute = new Execute();
			GenerictValueEntity<DateTime?> entity = new GenerictValueEntity<DateTime?>(value);

			SpecificationEntity<GenerictValueEntity<DateTime?>> spec = new SpecificationEntity<GenerictValueEntity<DateTime?>>()
				.AndIsNotEmpty(c => c.Value, message);

			var result = spec.IsSatisfiedBy(entity, execute);

			Assert.Equal(valid, result);
			Assert.Equal(valid, !execute.HasErro);

			if (!valid)
			{
				Assert.Equal(1, execute.Messages.Count);
				Assert.Equal(message, execute.Messages[0].Message);
			}
		}

		[Theory, MemberData(nameof(GetDateNotEmptyValues))]
		[Trait("Patterns Specification Entity AndIsNotEmpty", "Date")]
		public void NotEmpty_Date_Without_Execute(DateTime? value, bool valid, string message)
        {
            GenerictValueEntity<DateTime?> entity = new GenerictValueEntity<DateTime?>(value);

            SpecificationEntity<GenerictValueEntity<DateTime?>> spec = new SpecificationEntity<GenerictValueEntity<DateTime?>>()
                .AndIsNotEmpty(c => c.Value);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(valid, result);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("Hello word!", true)]
        [Trait("Patterns Specification Entity AndIsNotEmpty", "String")]
        public void NotEmpty_String_Without_Execute(string value, bool valid)
        {
            GenerictValueEntity<string> entity = new GenerictValueEntity<string>(value);

            SpecificationEntity<GenerictValueEntity<string>> spec = new SpecificationEntity<GenerictValueEntity<string>>()
                .AndIsNotEmpty(c => c.Value);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(valid, result);
        }

		[Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        [Trait("Patterns Specification Entity AndIsNotEmpty", "Object")]
        public void NotEmpty_Object_Without_Execute(bool create, bool valid)
        {
            GenerictValueEntity<object> entity = create 
                ? new GenerictValueEntity<object>("X")
                : null;

            SpecificationEntity<GenerictValueEntity<object>> spec = new SpecificationEntity<GenerictValueEntity<object>>()
                .AndIsNotEmpty(c => c);

            var result = spec.IsSatisfiedBy(entity);

            Assert.Equal(valid, result);
        }
    }
}
