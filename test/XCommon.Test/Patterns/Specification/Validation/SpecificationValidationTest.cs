using System.Threading.Tasks;
using FluentAssertions;
using XCommon.Application.Executes;
using XCommon.Test.Entity;
using XCommon.Test.Patterns.Specification.Validation.DataSource;
using XCommon.Test.Patterns.Specification.Validation.Sample;
using Xunit;

namespace XCommon.Test.Patterns.Specification.Validation
{
    public class SpecificationValidationTest
    {
        [Theory(DisplayName = "Default Specification")]
		[Trait("Patterns", "Specification - Validation")]
		[MemberData(nameof(SpecificationValidationDataSource.DefaultDataSource), MemberType = typeof(SpecificationValidationDataSource))]
        public async Task DefaultSpecification(PersonEntity data, bool valid, string message)
        {
            var validation = new DefaultSpecificationValidation();
            var result = await validation.IsSatisfiedByAsync(data);

            valid.Should().Be(result, message);
        }

        [Theory(DisplayName = "Default Specification (With execute)")]
		[Trait("Patterns", "Specification - Validation")]
		[MemberData(nameof(SpecificationValidationDataSource.DefaultDataSource), MemberType = typeof(SpecificationValidationDataSource))]
        public async Task DefaultSpecificationWithExecute(PersonEntity data, bool valid, string message)
        {
            var validation = new DefaultSpecificationValidation();
            var execute = new Execute();
            var result = await validation.IsSatisfiedByAsync(data, execute);

            valid.Should().Be(result, message);
            valid.Should().Be(!execute.HasErro, message);
        }

        [Theory(DisplayName = "Complete Specification")]
		[Trait("Patterns", "Specification - Validation")]
		[MemberData(nameof(SpecificationValidationDataSource.CompleteDataSource), MemberType = typeof(SpecificationValidationDataSource))]
        public async Task CompleteSpecification(PersonEntity data, bool valid, string message)
        {
            var validation = new CompleteSpecificationValidation();
            var result = await validation.IsSatisfiedByAsync(data);

            valid.Should().Be(result, message);
        }

        [Theory(DisplayName = "Complete Specification (With execute)")]
		[Trait("Patterns", "Specification - Validation")]
		[MemberData(nameof(SpecificationValidationDataSource.CompleteDataSource), MemberType = typeof(SpecificationValidationDataSource))]
        public async Task CompleteSpecificationWithExecute(PersonEntity data, bool valid, string message)
        {
            var validation = new CompleteSpecificationValidation();
            var execute = new Execute();
            var result = await validation.IsSatisfiedByAsync(data, execute);

            valid.Should().Be(result, message);
            valid.Should().Be(!execute.HasErro, message);
        }

        [Theory(DisplayName = "Complex Specification")]
		[Trait("Patterns", "Specification - Validation")]
		[MemberData(nameof(SpecificationValidationDataSource.ComplexDataSource), MemberType = typeof(SpecificationValidationDataSource))]
        public async Task ComplexSpecification(PersonEntity data, bool valid, string message)
        {
            var validation = new ComplexSpecificationValidation();
            var result = await validation.IsSatisfiedByAsync(data);

            valid.Should().Be(result, message);
        }

        [Theory(DisplayName = "Complex Specification (With execute)")]
		[Trait("Patterns", "Specification - Validation")]
		[MemberData(nameof(SpecificationValidationDataSource.ComplexDataSource), MemberType = typeof(SpecificationValidationDataSource))]
        public async Task ComplexSpecificationWithExecute(PersonEntity data, bool valid, string message)
        {
            var validation = new ComplexSpecificationValidation();
            var execute = new Execute();
            var result = await validation.IsSatisfiedByAsync(data, execute);

            valid.Should().Be(result, message);
            valid.Should().Be(!execute.HasErro, message);
        }
    }
}
