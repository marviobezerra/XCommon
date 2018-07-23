using System.Globalization;
using XCommon.Util;
using Xunit;

namespace XCommon.Test.UtilTest.ByteSizeTest
{
	public class ToStringMethodTest
	{
		[Fact]
		[Trait("Common", "ByteSize")]
		public void ReturnsLargestMetricSuffix()
		{
			var value = ByteSize.FromKiloBytes(10.5);
			var result = value.ToString();
			Assert.Equal(10.5.ToString("0.0 KB"), result);
		}

		[Fact]
		[Trait("Common", "ByteSize")]
		public void ReturnsDefaultNumberFormat()
		{
			var value = ByteSize.FromKiloBytes(10.5);
			var result = value.ToString("KB");
			Assert.Equal(10.5.ToString("0.0 KB"), result);
		}

		[Fact]
		[Trait("Common", "ByteSize")]
		public void ReturnsProvidedNumberFormat()
		{
			var value = ByteSize.FromKiloBytes(10.1234);
			var result = value.ToString("#.#### KB");
			Assert.Equal(10.1234.ToString("0.0000 KB"), result);
		}

		[Fact]
		[Trait("Common", "ByteSize")]
		public void ReturnsBits()
		{
			var value = ByteSize.FromBits(10);
			var result = value.ToString("##.#### b");
			Assert.Equal("10 b", result);
		}

		[Fact]
		[Trait("Common", "ByteSize")]
		public void ReturnsBytes()
		{
			var value = ByteSize.FromBytes(10);
			var result = value.ToString("##.#### B");
			Assert.Equal("10 B", result);
		}

		[Fact]
		[Trait("Common", "ByteSize")]
		public void ReturnsKiloBytes()
		{
			var value = ByteSize.FromKiloBytes(10);
			var result = value.ToString("##.#### KB");
			Assert.Equal("10 KB", result);
		}

		[Fact]
		[Trait("Common", "ByteSize")]
		public void ReturnsMegaBytes()
		{
			var value = ByteSize.FromMegaBytes(10);
			var result = value.ToString("##.#### MB");
			Assert.Equal("10 MB", result);
		}

		[Fact]
		[Trait("Common", "ByteSize")]
		public void ReturnsGigaBytes()
		{
			var value = ByteSize.FromGigaBytes(10);
			var result = value.ToString("##.#### GB");
			Assert.Equal("10 GB", result);
		}

		[Fact]
		[Trait("Common", "ByteSize")]
		public void ReturnsTeraBytes()
		{
			var value = ByteSize.FromTeraBytes(10);
			var result = value.ToString("##.#### TB");
			Assert.Equal("10 TB", result);
		}

		[Fact]
		[Trait("Common", "ByteSize")]
		public void ReturnsPetaBytes()
		{
			var value = ByteSize.FromPetaBytes(10);
			var result = value.ToString("##.#### PB");
			Assert.Equal("10 PB", result);
		}

		[Fact]
		[Trait("Common", "ByteSize")]
		public void ReturnsSelectedFormat()
		{
			var value = ByteSize.FromTeraBytes(10);
			var result = value.ToString("0.0 TB");
			Assert.Equal(10.ToString("0.0 TB"), result);
		}

		[Fact]
		[Trait("Common", "ByteSize")]
		public void ReturnsLargestMetricPrefixLargerThanZero()
		{
			var value = ByteSize.FromMegaBytes(.5);
			var result = value.ToString("#.#");
			Assert.Equal("512 KB", result);
		}

		[Fact]
		[Trait("Common", "ByteSize")]
		public void ReturnsLargestMetricPrefixLargerThanZeroForNegativeValues()
		{
			var value = ByteSize.FromMegaBytes(-.5);
			var result = value.ToString("#.#");
			Assert.Equal("-512 KB", result);
		}

		[Fact]
		[Trait("Common", "ByteSize")]
		public void ReturnsLargestMetricSuffixUsingCurrentCulture()
		{
			var originalCulture = CultureInfo.CurrentCulture;
			CultureInfo.CurrentCulture = new CultureInfo("fr-FR");

			var value = ByteSize.FromKiloBytes(10000);
			var result = value.ToString();
			Assert.Equal("9,77 MB", result);

			CultureInfo.CurrentCulture = originalCulture;
		}

		[Fact]
		[Trait("Common", "ByteSize")]
		public void ReturnsLargestMetricSuffixUsingSpecifiedCulture()
		{
			var value = ByteSize.FromKiloBytes(10000);
			var result = value.ToString("#.#", new CultureInfo("fr-FR"));
			Assert.Equal("9,8 MB", result);
		}

		[Fact]
		[Trait("Common", "ByteSize")]
		public void ReturnsCultureSpecificFormat()
		{
			var value = ByteSize.FromKiloBytes(10.5);
			var deCulture = new CultureInfo("de-DE");
			var result = value.ToString("0.0 KB", deCulture);
			Assert.Equal(10.5.ToString("0.0 KB", deCulture), result);
		}

		[Fact]
		[Trait("Common", "ByteSize")]
		public void ReturnsZeroBits()
		{
			var value = ByteSize.FromBits(0);
			var result = value.ToString();
			Assert.Equal("0 b", result);
		}
	}
}