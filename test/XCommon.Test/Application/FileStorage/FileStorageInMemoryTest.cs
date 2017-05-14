using FluentAssertions;
using System.Threading.Tasks;
using XCommon.Application.FileStorage;
using XCommon.Application.FileStorage.Implementations;
using Xunit;

namespace XCommon.Test.Application.FileStorage
{
	public class FileStorageInMemoryTest
    {
        public FileStorageInMemoryTest()
        {
            FileStorage = new FileStorageInMemory();
            SampleContent = new byte[] { 1, 0, 1, 1, 1, 0 };
        }

        private byte[] SampleContent { get; set; }

        private IFileStorage FileStorage { get; set; }

        [Fact(DisplayName = "Save (Container root)")]
		[Trait("Application", "FileStorage")]
		public async Task SaveContainerRoot()
        {
            var result = await FileStorage.SaveAsync("Sample.tmp", SampleContent);

            result.Should().Be(true);
        }

        [Fact(DisplayName = "Save (Container root, with override)")]
		[Trait("Application", "FileStorage")]
		public async Task SaveContainerRootWithOveride()
        {
            var result01 = await FileStorage.SaveAsync("Sample.tmp", SampleContent);
            var result02 = await FileStorage.SaveAsync("Sample.tmp", SampleContent);

            result01.Should().Be(true);
            result02.Should().Be(true);
        }

        [Fact(DisplayName = "Save (Container root, without override)")]
		[Trait("Application", "FileStorage")]
		public async Task SaveContainerRootWithoutOveride()
        {
            var result01 = await FileStorage.SaveAsync("Sample.tmp", SampleContent, false);
            var result02 = await FileStorage.SaveAsync("Sample.tmp", SampleContent, false);

            result01.Should().Be(true);
            result02.Should().Be(false);
        }
    }
}
