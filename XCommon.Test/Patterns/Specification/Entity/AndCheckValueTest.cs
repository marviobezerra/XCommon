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
	public class AndCheckValueTest
	{
		#region DataProvider
		//ncrunch: no coverage start
		[ExcludeFromCodeCoverage]
		public static IEnumerable<object[]> GetIntBiggerThanValues()
		{
			string validMessage = "Value int is valid";
			string inValidMessage = "Value int is not valid";

			yield return new object[] { 0, 0, false, inValidMessage };
			yield return new object[] { 5, 6, false, inValidMessage };
			yield return new object[] { -1, 0, false, inValidMessage };
			yield return new object[] { -10, -1, false, inValidMessage };
			yield return new object[] { 1, 0, true, validMessage };
			yield return new object[] { 10, 0, true, validMessage };
			yield return new object[] { -1, -2, true, validMessage };
		}

		[ExcludeFromCodeCoverage]
		public static IEnumerable<object[]> GetDecimaBiggerThanlValues()
		{
			string validMessage = "Value decimal is valid";
			string inValidMessage = "Value decimal is not valid bigger than";

			yield return new object[] { 0, 0, false, inValidMessage };
			yield return new object[] { 0, 0.1, false, inValidMessage };
			yield return new object[] { 0.001, 0.002, false, inValidMessage };
			yield return new object[] { -0.002, -0.002, false, inValidMessage };
			yield return new object[] { -1, 0, false, inValidMessage };
			yield return new object[] { -1, -1, false, inValidMessage };
			yield return new object[] { 1, 0, true, validMessage };
			yield return new object[] { 10, 9.999, true, validMessage };
			yield return new object[] { 0.1, 0, true, validMessage };
			yield return new object[] { 0.002, 0.001, true, validMessage };
		}

		[ExcludeFromCodeCoverage]
		public static IEnumerable<object[]> GetDateBiggerThanlValues()
		{
			string validMessage = "Value DateTime is not valid";
			string inValidMessage = "Value DateTime is valid bigger than";

			yield return new object[] { "2015-01-01", "2015-01-01", false, false, inValidMessage };
			yield return new object[] { "2015-01-01 00:00:00", "2015-01-01 00:00:01", false, false, inValidMessage };
			yield return new object[] { "2015-01-01 00:00:01", "2015-01-01 00:00:00", false, true, inValidMessage };
			yield return new object[] { "2015-01-02 00:00:00", "2015-01-01 00:00:00", true, false, validMessage };
		}

		[ExcludeFromCodeCoverage]
		public static IEnumerable<object[]> GetIntLessThanValues()
		{
			string validMessage = "Value int valid less than";
			string inValidMessage = "Value int not valid less than";

			yield return new object[] { 0, 0, false, inValidMessage };
			yield return new object[] { 6, 5, false, inValidMessage };
			yield return new object[] { 0, -1, false, inValidMessage };
			yield return new object[] { -1, -1, false, inValidMessage };
			yield return new object[] { -1, -10, false, inValidMessage };
			yield return new object[] { 0, 1, true, validMessage };
			yield return new object[] { 0, 10, true, validMessage };
			yield return new object[] { -2, -1, true, validMessage };
		}

		[ExcludeFromCodeCoverage]
		public static IEnumerable<object[]> GetDecimalLessThanValues()
		{
			string validMessage = "Value Decimal not valid less than";
			string inValidMessage = "Value Decimal not valid less than";

			yield return new object[] { 0, 0, false, inValidMessage };
			yield return new object[] { 0.1, 0, false, inValidMessage };
			yield return new object[] { 0.002, 0.001, false, inValidMessage };
			yield return new object[] { -0.001, -0.001, false, inValidMessage };
			yield return new object[] { -0, -1, false, inValidMessage };
			yield return new object[] { -1, -1, false, inValidMessage };
			yield return new object[] { 0, 1, true, validMessage };
			yield return new object[] { 9.999, 100, true, validMessage };
			yield return new object[] { 0, 0.1, true, validMessage };
			yield return new object[] { 0.001, 0.002, true, validMessage };
		}

		[ExcludeFromCodeCoverage]
		public static IEnumerable<object[]> GetDateLessThanValues()
		{
			string validMessage = "Value Decimal not valid less than";
			string inValidMessage = "Value Decimal not valid less than";

			yield return new object[] { "2015-01-01", "2015-01-01", false, false, inValidMessage };
			yield return new object[] { "2015-01-01 00:00:01", "2015-01-01 00:00:00", false, false, inValidMessage };
			yield return new object[] { "2015-01-01 00:00:00", "2015-01-01 00:00:01", false, true, inValidMessage };
			yield return new object[] { "2015-01-01 00:00:00", "2015-01-01 00:00:01", true, false, validMessage };
		}

		[ExcludeFromCodeCoverage]
		public static IEnumerable<object[]> GetIntInRangeValues()
		{
			string validMessage = "Value int in range";
			string inValidMessage = "Value int not in range";

			yield return new object[] { 1, 0, 0, false, inValidMessage };
			yield return new object[] { 0, 1, 1, false, inValidMessage };
			yield return new object[] { 2, -1, -10, false, inValidMessage };
			yield return new object[] { -2, -1, 0, false, inValidMessage };
			yield return new object[] { 0, 0, 1, true, validMessage };
			yield return new object[] { -1, -1, 0, true, validMessage };
			yield return new object[] { -1, -1, -1, true, validMessage };
			yield return new object[] { 1, 1, 1, true, validMessage };
		}

		[ExcludeFromCodeCoverage]
		public static IEnumerable<object[]> GetDecimalInRangeValues()
		{
			string validMessage = "Value Decimal in ragen";
			string inValidMessage = "Value Decimal not in range";

			yield return new object[] { 0.1, 0, 0, false, inValidMessage };
			yield return new object[] { 0.001, 0, 0, false, inValidMessage };
			yield return new object[] { 2, -1, -10, false, inValidMessage };
			yield return new object[] { -0.001, 0, 1, false, inValidMessage };
			yield return new object[] { 0, 0, 1, true, validMessage };
			yield return new object[] { -0.001, -0.001, 0, true, validMessage };
			yield return new object[] { -1, -1, -1, true, validMessage };
			yield return new object[] { 1, 1, 1, true, validMessage };
		}

		[ExcludeFromCodeCoverage]
		public static IEnumerable<object[]> GetDateInRangeValues()
		{
			string validMessage = "Value Decimal in range";
			string inValidMessage = "Value Decimal not in range";
			
			yield return new object[] {"2015-01-01 00:00:01", "2015-01-01 00:00:00", "2015-01-01 00:00:00", false, false, inValidMessage };
			yield return new object[] {"2015-01-01 00:00:00", "2015-01-01 00:00:01", "2015-01-01 00:00:01", false, false, inValidMessage };
			yield return new object[] {"2015-01-01 00:00:00", "2015-01-01 23:59:50", "2015-01-01 00:00:00", true, true, validMessage };
			yield return new object[] {"2015-01-01", "2015-01-01", "2015-01-01", true, false, validMessage };
			yield return new object[] {"2015-06-01", "2015-01-01", "2015-12-31", true, false, validMessage };
		}

		//ncrunch: no coverage end
		#endregion

		[Theory, MemberData(nameof(GetIntBiggerThanValues))]
		[Trait("Patterns Specification Entity AndCheckValue", "BiggerThan Int")]
		public void BiggerThan_Int_Without_Execute(int value, int compare, bool valid, string message)
		{
			BiggerThanEntity<int> entity = new BiggerThanEntity<int>(value, compare);

			var spec = new SpecificationEntity<BiggerThanEntity<int>>()
				.AndIsBiggerThan(c => c.Value, c => c.Compare);

			var result = spec.IsSatisfiedBy(entity);

			Assert.Equal(valid, result);
		}

		[Theory, MemberData(nameof(GetIntBiggerThanValues))]
		[Trait("Patterns Specification Entity AndCheckValue", "BiggerThan Int")]
		public void BiggerThan_Int_With_Execute(int value, int compare, bool valid, string message)
		{
			Execute execute = new Execute();

			BiggerThanEntity<int> entity = new BiggerThanEntity<int>(value, compare);

			var spec = new SpecificationEntity<BiggerThanEntity<int>>()
				.AndIsBiggerThan(c => c.Value, c => c.Compare, message);

			var result = spec.IsSatisfiedBy(entity, execute);

			Assert.Equal(valid, result);
			Assert.Equal(valid, !execute.HasErro);

			if (!valid)
			{
				Assert.Equal(1, execute.Messages.Count);
				Assert.Equal(message, execute.Messages[0].Message);
			}
		}

		[Theory, MemberData(nameof(GetDecimaBiggerThanlValues))]
		[Trait("Patterns Specification Entity AndCheckValue", "BiggerThan Decimal")]
		public void BiggerThan_Decimal_Without_Execute(decimal value, decimal compare, bool valid, string message)
		{
			BiggerThanEntity<decimal> entity = new BiggerThanEntity<decimal>(value, compare);

			var spec = new SpecificationEntity<BiggerThanEntity<decimal>>()
				.AndIsBiggerThan(c => c.Value, c => c.Compare);

			var result = spec.IsSatisfiedBy(entity);

			Assert.Equal(valid, result);
		}

		[Theory, MemberData(nameof(GetDecimaBiggerThanlValues))]
		[Trait("Patterns Specification Entity AndCheckValue", "BiggerThan Decimal")]
		public void BiggerThan_Decimal_With_Execute(decimal value, decimal compare, bool valid, string message)
		{
			Execute execute = new Execute();
			BiggerThanEntity<decimal> entity = new BiggerThanEntity<decimal>(value, compare);

			var spec = new SpecificationEntity<BiggerThanEntity<decimal>>()
				.AndIsBiggerThan(c => c.Value, c => c.Compare, message);

			var result = spec.IsSatisfiedBy(entity, execute);

			Assert.Equal(valid, result); Assert.Equal(valid, result);
			Assert.Equal(valid, !execute.HasErro);

			if (!valid)
			{
				Assert.Equal(1, execute.Messages.Count);
				Assert.Equal(message, execute.Messages[0].Message);
			}
		}

		[Theory, MemberData(nameof(GetDateBiggerThanlValues))]
		[Trait("Patterns Specification Entity AndCheckValue", "BiggerThan Date")]
		public void BiggerThan_DateTime_Without_Execute(DateTime value, DateTime compare, bool valid, bool removeTime, string message)
		{
			BiggerThanEntity<DateTime> entity = new BiggerThanEntity<DateTime>(value, compare);

			var spec = new SpecificationEntity<BiggerThanEntity<DateTime>>()
				.AndIsBiggerThan(c => c.Value, c => c.Compare, removeTime);

			var result = spec.IsSatisfiedBy(entity);

			Assert.Equal(valid, result);
		}

		[Theory, MemberData(nameof(GetDateBiggerThanlValues))]
		[Trait("Patterns Specification Entity AndCheckValue", "BiggerThan Date")]
		public void BiggerThan_DateTime_With_Execute(DateTime value, DateTime compare, bool valid, bool removeTime, string message)
		{
			Execute execute = new Execute();
			BiggerThanEntity<DateTime> entity = new BiggerThanEntity<DateTime>(value, compare);

			var spec = new SpecificationEntity<BiggerThanEntity<DateTime>>()
				.AndIsBiggerThan(c => c.Value, c => c.Compare, true, message);

			var result = spec.IsSatisfiedBy(entity, execute);

			Assert.Equal(valid, result); Assert.Equal(valid, result);
			Assert.Equal(valid, !execute.HasErro);

			if (!valid)
			{
				Assert.Equal(1, execute.Messages.Count);
				Assert.Equal(message, execute.Messages[0].Message);
			}
		}

		[Theory, MemberData(nameof(GetIntLessThanValues))]
		[Trait("Patterns Specification Entity AndCheckValue", "LessThan Int")]
		public void LessThan_Int_Without_Execute(int value, int compare, bool valid, string message)
		{
			LessThanEntity<int> entity = new LessThanEntity<int>(value, compare);

			var spec = new SpecificationEntity<LessThanEntity<int>>()
				.AndIsLessThan(c => c.Value, c => c.Compare);

			var result = spec.IsSatisfiedBy(entity);

			Assert.Equal(valid, result);
		}

		[Theory, MemberData(nameof(GetIntLessThanValues))]
		[Trait("Patterns Specification Entity AndCheckValue", "LessThan Int")]
		public void LessThan_Int_With_Execute(int value, int compare, bool valid, string message)
		{
			Execute execute = new Execute();
			LessThanEntity<int> entity = new LessThanEntity<int>(value, compare);

			var spec = new SpecificationEntity<LessThanEntity<int>>()
				.AndIsLessThan(c => c.Value, c => c.Compare, message);

			var result = spec.IsSatisfiedBy(entity, execute);

			Assert.Equal(valid, result);
			Assert.Equal(valid, !execute.HasErro);

			if (!valid)
			{
				Assert.Equal(1, execute.Messages.Count);
				Assert.Equal(message, execute.Messages[0].Message);
			}
		}

		[Theory, MemberData(nameof(GetDecimalLessThanValues))]
		[Trait("Patterns Specification Entity AndCheckValue", "LessThan Decimal")]
		public void LessThan_Decimal_Without_Execute(decimal value, decimal compare, bool valid, string message)
		{
			LessThanEntity<decimal> entity = new LessThanEntity<decimal>(value, compare);

			var spec = new SpecificationEntity<LessThanEntity<decimal>>()
				.AndIsLessThan(c => c.Value, c => c.Compare);

			var result = spec.IsSatisfiedBy(entity);

			Assert.Equal(valid, result);
		}

		[Theory, MemberData(nameof(GetDecimalLessThanValues))]
		[Trait("Patterns Specification Entity AndCheckValue", "LessThan Decimal")]
		public void LessThan_Decimal_With_Execute(decimal value, decimal compare, bool valid, string message)
		{
			Execute execute = new Execute();
			LessThanEntity<decimal> entity = new LessThanEntity<decimal>(value, compare);

			var spec = new SpecificationEntity<LessThanEntity<decimal>>()
				.AndIsLessThan(c => c.Value, c => c.Compare, message);

			var result = spec.IsSatisfiedBy(entity, execute);

			Assert.Equal(valid, result);
			Assert.Equal(valid, !execute.HasErro);

			if (!valid)
			{
				Assert.Equal(1, execute.Messages.Count);
				Assert.Equal(message, execute.Messages[0].Message);
			}
		}

		[Theory, MemberData(nameof(GetDateLessThanValues))]
		[Trait("Patterns Specification Entity AndCheckValue", "LessThan DateTime")]
		public void LessThan_DateTime_Without_Execute(DateTime value, DateTime compare, bool valid, bool removeTime, string message)
		{
			LessThanEntity<DateTime> entity = new LessThanEntity<DateTime>(value, compare);

			var spec = new SpecificationEntity<LessThanEntity<DateTime>>()
				.AndIsLessThan(c => c.Value, c => c.Compare, removeTime);

			var result = spec.IsSatisfiedBy(entity);

			Assert.Equal(valid, result);
		}

		[Theory, MemberData(nameof(GetIntInRangeValues))]
		[Trait("Patterns Specification Entity AndCheckValue", "InRange Int")]
		public void InRange_Int_With_Execute(int value, int start, int end, bool valid, string message)
		{
			Execute execute = new Execute();
			InRangeEntity<int> entity = new InRangeEntity<int>(value, start, end);

			var spec = new SpecificationEntity<InRangeEntity<int>>()
				.AndIsInRange(c => c.Value, c => c.Start, c => c.End, message);

			var result = spec.IsSatisfiedBy(entity, execute);

			Assert.Equal(valid, result);
			Assert.Equal(valid, !execute.HasErro);

			if (!valid)
			{
				Assert.Equal(1, execute.Messages.Count);
				Assert.Equal(message, execute.Messages[0].Message);
			}
		}

		[Theory, MemberData(nameof(GetIntInRangeValues))]
		[Trait("Patterns Specification Entity AndCheckValue", "InRange Int")]
		public void InRange_Int_Without_Execute(int value, int start, int end, bool valid, string message)
		{
			InRangeEntity<int> entity = new InRangeEntity<int>(value, start, end);

			var spec = new SpecificationEntity<InRangeEntity<int>>()
				.AndIsInRange(c => c.Value, c => c.Start, c => c.End);

			var result = spec.IsSatisfiedBy(entity);

			Assert.Equal(valid, result);
		}

		[Theory, MemberData(nameof(GetDecimalInRangeValues))]
		[Trait("Patterns Specification Entity AndCheckValue", "InRange Decimal")]
		public void InRange_Decimal_With_Execute(decimal value, decimal start, decimal end, bool valid, string message)
		{
			Execute execute = new Execute();
			InRangeEntity<decimal> entity = new InRangeEntity<decimal>(value, start, end);

			var spec = new SpecificationEntity<InRangeEntity<decimal>>()
				.AndIsInRange(c => c.Value, c => c.Start, c => c.End, message);

			var result = spec.IsSatisfiedBy(entity, execute);

			Assert.Equal(valid, result);
			Assert.Equal(valid, !execute.HasErro);

			if (!valid)
			{
				Assert.Equal(1, execute.Messages.Count);
				Assert.Equal(message, execute.Messages[0].Message);
			}
		}

		[Theory, MemberData(nameof(GetDecimalInRangeValues))]
		[Trait("Patterns Specification Entity AndCheckValue", "InRange Decimal")]
		public void InRange_Decimal_Without_Execute(decimal value, decimal start, decimal end, bool valid, string message)
		{
			InRangeEntity<decimal> entity = new InRangeEntity<decimal>(value, start, end);

			var spec = new SpecificationEntity<InRangeEntity<decimal>>()
				.AndIsInRange(c => c.Value, c => c.Start, c => c.End);

			var result = spec.IsSatisfiedBy(entity);

			Assert.Equal(valid, result);
		}

		[Theory, MemberData(nameof(GetDateInRangeValues))]
		[Trait("Patterns Specification Entity AndCheckValue", "InRange DateTime")]
		public void InRange_DateTime_With_Execute(DateTime value, DateTime start, DateTime end, bool valid, bool removeTime, string message)
		{
			Execute execute = new Execute();
			InRangeEntity<DateTime> entity = new InRangeEntity<DateTime>(value, start, end);

			var spec = new SpecificationEntity<InRangeEntity<DateTime>>()
				.AndIsInRange(c => c.Value, c => c.Start, c => c.End, removeTime, message);

			var result = spec.IsSatisfiedBy(entity, execute);

			Assert.Equal(valid, result);
			Assert.Equal(valid, !execute.HasErro);

			if (!valid)
			{
				Assert.Equal(1, execute.Messages.Count);
				Assert.Equal(message, execute.Messages[0].Message);
			}
		}

		[Theory, MemberData(nameof(GetDateInRangeValues))]
		[Trait("Patterns Specification Entity AndCheckValue", "InRange DateTime")]
		public void InRange_DateTime_Without_Execute(DateTime value, DateTime start, DateTime end, bool valid, bool removeTime, string message)
		{
			InRangeEntity<DateTime> entity = new InRangeEntity<DateTime>(value, start, end);

			var spec = new SpecificationEntity<InRangeEntity<DateTime>>()
				.AndIsInRange(c => c.Value, c => c.Start, c => c.End, removeTime);

			var result = spec.IsSatisfiedBy(entity);

			Assert.Equal(valid, result);
		}

		[Theory]
		[InlineData(-10, -1, false)]
		[InlineData(1, 0, true)]
		[Trait("Patterns Specification Entity AndCheckValue", "Out of Box")]
		public void Int_BiggerThan_InValid_OutOfBox(int value, int compare, bool valid)
		{
			BiggerThanEntity<int> entity = new BiggerThanEntity<int>(0, 0);

			SpecificationEntity<BiggerThanEntity<int>> spec = new SpecificationEntity<BiggerThanEntity<int>>()
				.AndIsBiggerThan(c => value, c => compare);

			var result = spec.IsSatisfiedBy(entity);

			Assert.Equal(valid, result);
		}
	}
}
