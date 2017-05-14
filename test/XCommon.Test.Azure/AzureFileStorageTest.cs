using FluentAssertions;
using System.Threading.Tasks;
using XCommon.Application;
using XCommon.Application.FileStorage;
using XCommon.Azure.Application.FileStorage.Implementations;
using XCommon.Patterns.Ioc;
using XCommon.Extensions.String;
using Xunit;

namespace XCommon.Test.Azure
{
	public class AzureFileStorageTest
	{
		public AzureFileStorageTest()
		{
			IApplicationSettings app = new ApplicationSettings
			{
				Production = false
			};

			Kernel.Map<IApplicationSettings>().To(app);
			Kernel.Map<IFileStorage>().To<AzureFileStorage>();

			SampleContent01 = "XCommon 01".ToByte();
			SampleContent02 = "XCommon 02".ToByte();
		}

		private byte[] SampleContent01 { get; set; }

		private byte[] SampleContent02 { get; set; }

		private bool IsMyMachine
		{
			get
			{
				return System.Environment.MachineName.ToUpper() == "Brainiac".ToUpper();
			}
		}

		[SkippableFact(DisplayName = "Save")]
		[Trait("Azure", "Blob Storage")]
		public async Task SaveFile()
		{
			Skip.IfNot(IsMyMachine, "This test needs Azure Storage Emulator");

			var container = "SaveFileOveride";
			var fileName = "TestFile.txt";
			var fileStorage = Kernel.Resolve<IFileStorage>();

			var result01 = await fileStorage.SaveAsync(container, fileName, SampleContent01);

			result01.Should().BeTrue();
		}

		[SkippableFact(DisplayName = "Save/Load - Overide")]
		[Trait("Azure", "Blob Storage")]
		public async Task SaveFileOveride()
		{
			Skip.IfNot(IsMyMachine, "This test needs Azure Storage Emulator");

			var container = "SaveFileOveride";
			var fileName = "TestFile.txt";
			var fileStorage = Kernel.Resolve<IFileStorage>();

			var result01 = await fileStorage.SaveAsync(container, fileName, SampleContent01);
			var result02 = await fileStorage.SaveAsync(container, fileName, SampleContent02);

			var file = await fileStorage.LoadAsync(container, fileName);

			result01.Should().BeTrue();
			result02.Should().BeTrue();
			file.Should().Equal(SampleContent02, "The last file is SampleContent02");
		}

		[SkippableFact(DisplayName = "Save/Load - Not Overide")]
		[Trait("Azure", "Blob Storage")]
		public async Task SaveFileNotOveride()
		{
			Skip.IfNot(IsMyMachine, "This test needs Azure Storage Emulator");

			var container = "SaveFileNotOveride";
			var fileName = "TestFile.txt";
			var fileStorage = Kernel.Resolve<IFileStorage>();

			await fileStorage.DeleteContainerAsync(container);

			var result01 = await fileStorage.SaveAsync(container, fileName, SampleContent01, false);
			var result02 = await fileStorage.SaveAsync(container, fileName, SampleContent02, false);

			var file = await fileStorage.LoadAsync(container, fileName);

			result01.Should().BeTrue("There is no file on this container");
			result02.Should().BeFalse("I should not override the previous file");

			file.Should().Equal(SampleContent01, "The expected file content is SampleContent01");
		}

		[SkippableFact(DisplayName = "Check if exists - False")]
		[Trait("Azure", "Blob Storage")]
		public async Task CheckExitsFalse()
		{
			Skip.IfNot(IsMyMachine, "This test needs Azure Storage Emulator");

			var container = "CheckExitsFalse";
			var fileName = "TestFile.txt";
			var fileStorage = Kernel.Resolve<IFileStorage>();

			var result01 = await fileStorage.ExistsAsync(container, fileName);

			result01.Should().BeFalse();
		}

		[SkippableFact(DisplayName = "Check if exists - True")]
		[Trait("Azure", "Blob Storage")]
		public async Task CheckExitsTrue()
		{
			Skip.IfNot(IsMyMachine, "This test needs Azure Storage Emulator");

			var container = "CheckExitsTrue";
			var fileName = "TestFile.txt";
			var fileStorage = Kernel.Resolve<IFileStorage>();

			var saveResult = await fileStorage.SaveAsync(container, fileName, SampleContent01);

			var result01 = await fileStorage.ExistsAsync(container, fileName);

			result01.Should().BeTrue($"There is a file {fileName} in the container {container}");
		}

		[SkippableFact(DisplayName = "Delete - True")]
		[Trait("Azure", "Blob Storage")]
		public async Task DeleteTrue()
		{
			Skip.IfNot(IsMyMachine, "This test needs Azure Storage Emulator");

			var container = "DeleteTrue";
			var fileName = "TestFile.txt";
			var fileStorage = Kernel.Resolve<IFileStorage>();

			var saveResult = await fileStorage.SaveAsync(container, fileName, SampleContent01);

			var result01 = await fileStorage.DeleteAsync(container, fileName);

			result01.Should().BeTrue($"There is a file to delete {fileName} in the container {container}");
		}

		[SkippableFact(DisplayName = "Delete - False")]
		[Trait("Azure", "Blob Storage")]
		public async Task DeleteFalse()
		{
			Skip.IfNot(IsMyMachine, "This test needs Azure Storage Emulator");

			var container = "CheckExitsFalse";
			var fileName = "TestFile.txt";
			var fileStorage = Kernel.Resolve<IFileStorage>();

			var result01 = await fileStorage.DeleteAsync(container, fileName);

			result01.Should().BeFalse();
		}
	}
}
