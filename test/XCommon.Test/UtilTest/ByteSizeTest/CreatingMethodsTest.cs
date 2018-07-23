using XCommon.Util;
using Xunit;

namespace XCommon.Test.UtilTest.ByteSizeTest
{
	public class CreatingMethodsTest
	{
		[Fact]
		[Trait("Common", "ByteSize")]
		public void Constructor()
		{
			double byteSize = 1125899906842624;

			var result = new ByteSize(byteSize);

			Assert.Equal(byteSize * 8, result.Bits);
			Assert.Equal(byteSize, result.Bytes);
			Assert.Equal(byteSize / 1024, result.KiloBytes);
			Assert.Equal(byteSize / 1024 / 1024, result.MegaBytes);
			Assert.Equal(byteSize / 1024 / 1024 / 1024, result.GigaBytes);
			Assert.Equal(byteSize / 1024 / 1024 / 1024 / 1024, result.TeraBytes);
			Assert.Equal(1, result.PetaBytes);
		}

		[Fact]
		[Trait("Common", "ByteSize")]
		public void FromBitsMethod()
		{
			long value = 8;

			var result = ByteSize.FromBits(value);

			Assert.Equal(8, result.Bits);
			Assert.Equal(1, result.Bytes);
		}

		[Fact]
		[Trait("Common", "ByteSize")]
		public void FromBytesMethod()
		{
			double value = 1.5;

			var result = ByteSize.FromBytes(value);

			Assert.Equal(12, result.Bits);
			Assert.Equal(1.5, result.Bytes);
		}

		[Fact]
		[Trait("Common", "ByteSize")]
		public void FromKiloBytesMethod()
		{
			double value = 1.5;

			var result = ByteSize.FromKiloBytes(value);

			Assert.Equal(1536, result.Bytes);
			Assert.Equal(1.5, result.KiloBytes);
		}

		[Fact]
		[Trait("Common", "ByteSize")]
		public void FromMegaBytesMethod()
		{
			double value = 1.5;

			var result = ByteSize.FromMegaBytes(value);

			Assert.Equal(1572864, result.Bytes);
			Assert.Equal(1.5, result.MegaBytes);
		}

		[Fact]
		[Trait("Common", "ByteSize")]
		public void FromGigaBytesMethod()
		{
			double value = 1.5;

			var result = ByteSize.FromGigaBytes(value);

			Assert.Equal(1610612736, result.Bytes);
			Assert.Equal(1.5, result.GigaBytes);
		}

		[Fact]
		[Trait("Common", "ByteSize")]
		public void FromTeraBytesMethod()
		{
			double value = 1.5;

			var result = ByteSize.FromTeraBytes(value);

			Assert.Equal(1649267441664, result.Bytes);
			Assert.Equal(1.5, result.TeraBytes);
		}

		[Fact]
		[Trait("Common", "ByteSize")]
		public void FromPetaBytesMethod()
		{
			double value = 1.5;

			// Act
			var result = ByteSize.FromPetaBytes(value);

			Assert.Equal(1688849860263936, result.Bytes);
			Assert.Equal(1.5, result.PetaBytes);
		}
	}
}