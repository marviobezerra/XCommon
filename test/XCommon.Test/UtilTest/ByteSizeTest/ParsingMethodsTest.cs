using System;
using System.Globalization;
using XCommon.Util;
using Xunit;

namespace XCommon.Test.UtilTest.ByteSizeTest
{
	public class ParsingMethodsTest
	{
		[Fact]
		[Trait("Common", "ByteSize")]
		public void Parse()
		{
			var val = "1020KB";
			var expected = ByteSize.FromKiloBytes(1020);
			var result = ByteSize.Parse(val);

			Assert.Equal(expected, result);
		}

		[Fact]
		[Trait("Common", "ByteSize")]
		public void TryParse()
		{
			var val = "1020KB";
			var expected = ByteSize.FromKiloBytes(1020);
			var resultBool = ByteSize.TryParse(val, out var resultByteSize);

			Assert.True(resultBool);
			Assert.Equal(expected, resultByteSize);
		}

		[Fact]
		[Trait("Common", "ByteSize")]
		public void ParseDecimalMB()
		{
			var val = "100.5MB";
			var expected = ByteSize.FromMegaBytes(100.5);
			var result = ByteSize.Parse(val);

			Assert.Equal(expected, result);
		}

		[Fact]
		[Trait("Common", "ByteSize")]
		public void TryParseReturnsFalseOnBadValue()
		{
			var val = "Unexpected Value";
			var resultBool = ByteSize.TryParse(val, out var resultByteSize);

			Assert.False(resultBool);
			Assert.Equal(new ByteSize(), resultByteSize);
		}

		[Fact]
		[Trait("Common", "ByteSize")]
		public void TryParseReturnsFalseOnMissingMagnitude()
		{
			var val = "1000";
			var resultBool = ByteSize.TryParse(val, out var resultByteSize);

			Assert.False(resultBool);
			Assert.Equal(new ByteSize(), resultByteSize);
		}

		[Fact]
		[Trait("Common", "ByteSize")]
		public void TryParseReturnsFalseOnMissingValue()
		{
			var val = "KB";
			var resultBool = ByteSize.TryParse(val, out var resultByteSize);

			Assert.False(resultBool);
			Assert.Equal(new ByteSize(), resultByteSize);
		}

		[Fact]
		[Trait("Common", "ByteSize")]
		public void TryParseWorksWithLotsOfSpaces()
		{
			var val = " 100 KB ";
			var expected = ByteSize.FromKiloBytes(100);
			var result = ByteSize.Parse(val);

			Assert.Equal(expected, result);
		}

		[Fact]
		[Trait("Common", "ByteSize")]
		public void ParsePartialBits()
		{
			var val = "10.5b";

			Assert.Throws<FormatException>(() =>
			{
				ByteSize.Parse(val);
			});
		}


		[Fact]
		[Trait("Common", "ByteSize")]
		public void ParseThrowsOnInvalid()
		{
			var badValue = "Unexpected Value";

			Assert.Throws<FormatException>(() =>
			{
				ByteSize.Parse(badValue);
			});
		}

		[Fact]
		[Trait("Common", "ByteSize")]
		public void ParseThrowsOnNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
			{
				ByteSize.Parse(null);
			});
		}


		[Fact]
		[Trait("Common", "ByteSize")]
		public void ParseBits()
		{
			var val = "1b";
			var expected = ByteSize.FromBits(1);
			var result = ByteSize.Parse(val);

			Assert.Equal(expected, result);
		}

		[Fact]
		[Trait("Common", "ByteSize")]
		public void ParseBytes()
		{
			var val = "1B";
			var expected = ByteSize.FromBytes(1);
			var result = ByteSize.Parse(val);

			Assert.Equal(expected, result);
		}

		[Fact]
		[Trait("Common", "ByteSize")]
		public void ParseKB()
		{
			var val = "1020KB";
			var expected = ByteSize.FromKiloBytes(1020);
			var result = ByteSize.Parse(val);

			Assert.Equal(expected, result);
		}

		[Fact]
		[Trait("Common", "ByteSize")]
		public void ParseMB()
		{
			var val = "1000MB";
			var expected = ByteSize.FromMegaBytes(1000);
			var result = ByteSize.Parse(val);

			Assert.Equal(expected, result);
		}

		[Fact]
		[Trait("Common", "ByteSize")]
		public void ParseGB()
		{
			var val = "805GB";
			var expected = ByteSize.FromGigaBytes(805);
			var result = ByteSize.Parse(val);

			Assert.Equal(expected, result);
		}

		[Fact]
		[Trait("Common", "ByteSize")]
		public void ParseTB()
		{
			var val = "100TB";
			var expected = ByteSize.FromTeraBytes(100);
			var result = ByteSize.Parse(val);

			Assert.Equal(expected, result);
		}

		[Fact]
		[Trait("Common", "ByteSize")]
		public void ParsePB()
		{
			var val = "100PB";
			var expected = ByteSize.FromPetaBytes(100);
			var result = ByteSize.Parse(val);

			Assert.Equal(expected, result);
		}

		[Fact]
		[Trait("Common", "ByteSize")]
		public void ParseCultureNumberSeparator()
		{
			CultureInfo.CurrentCulture = new CultureInfo("de-DE");
			var val = "1.500,5 MB";
			var expected = ByteSize.FromMegaBytes(1500.5);
			var result = ByteSize.Parse(val);

			Assert.Equal(expected, result);
		}
	}
}