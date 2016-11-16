using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XCommon.Application.FileStorage;
using XCommon.Application.FileStorage.Implementations;
using Xunit;
using FluentAssertions;

namespace XCommon.Test.Application.FileStorage
{
    public class FileStorageInMemoryTest
    {
        public FileStorageInMemoryTest()
        {
            FileStorage = new FileStorageInMemory();
            SampleContent = new byte[] { 1, 0, 1, 1, 1, 0 };
        }

        public byte[] SampleContent { get; set; }

        private IFileStorage FileStorage { get; set; }

        [Fact(DisplayName = "Save (Container root)")]  
        public void SaveContainerRoot()
        {
            var result = FileStorage.Save("Sample.tmp", SampleContent);

            result.Should().Be(true);
        }

        [Fact(DisplayName = "Save (Container root, with override)")]
        public void SaveContainerRootWithOveride()
        {
            var result01 = FileStorage.Save("Sample.tmp", SampleContent);
            var result02 = FileStorage.Save("Sample.tmp", SampleContent);

            result01.Should().Be(true);
            result02.Should().Be(true);
        }

        [Fact(DisplayName = "Save (Container root, without override)")]
        public void SaveContainerRootWithoutOveride()
        {
            var result01 = FileStorage.Save("Sample.tmp", SampleContent, false);
            var result02 = FileStorage.Save("Sample.tmp", SampleContent, false);

            result01.Should().Be(true);
            result02.Should().Be(false);
        }
    }
}
