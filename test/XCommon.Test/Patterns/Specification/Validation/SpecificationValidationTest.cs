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
        public void DefaultSpecification(PersonEntity data, bool valid, string message)
        {
            var validation = new DefaultSpecificationValidation();
            var result = validation.IsSatisfiedBy(data);

            valid.Should().Be(result, message);
        }

        [Theory(DisplayName = "Default Specification (With execute)")]
		[Trait("Patterns", "Specification - Validation")]
		[MemberData(nameof(SpecificationValidationDataSource.DefaultDataSource), MemberType = typeof(SpecificationValidationDataSource))]
        public void DefaultSpecificationWithExecute(PersonEntity data, bool valid, string message)
        {
            var validation = new DefaultSpecificationValidation();
            var execute = new Execute();
            var result = validation.IsSatisfiedBy(data, execute);

            valid.Should().Be(result, message);
            valid.Should().Be(!execute.HasErro, message);
        }

        [Theory(DisplayName = "Complete Specification")]
		[Trait("Patterns", "Specification - Validation")]
		[MemberData(nameof(SpecificationValidationDataSource.CompleteDataSource), MemberType = typeof(SpecificationValidationDataSource))]
        public void CompleteSpecification(PersonEntity data, bool valid, string message)
        {
            var validation = new CompleteSpecificationValidation();
            var result = validation.IsSatisfiedBy(data);

            valid.Should().Be(result, message);
        }

        [Theory(DisplayName = "Complete Specification (With execute)")]
		[Trait("Patterns", "Specification - Validation")]
		[MemberData(nameof(SpecificationValidationDataSource.CompleteDataSource), MemberType = typeof(SpecificationValidationDataSource))]
        public void CompleteSpecificationWithExecute(PersonEntity data, bool valid, string message)
        {
            var validation = new CompleteSpecificationValidation();
            var execute = new Execute();
            var result = validation.IsSatisfiedBy(data, execute);

            valid.Should().Be(result, message);
            valid.Should().Be(!execute.HasErro, message);
        }

        [Theory(DisplayName = "Complex Specification")]
		[Trait("Patterns", "Specification - Validation")]
		[MemberData(nameof(SpecificationValidationDataSource.ComplexDataSource), MemberType = typeof(SpecificationValidationDataSource))]
        public void ComplexSpecification(PersonEntity data, bool valid, string message)
        {
            var validation = new ComplexSpecificationValidation();
            var result = validation.IsSatisfiedBy(data);

            valid.Should().Be(result, message);
        }

        [Theory(DisplayName = "Complex Specification (With execute)")]
		[Trait("Patterns", "Specification - Validation")]
		[MemberData(nameof(SpecificationValidationDataSource.ComplexDataSource), MemberType = typeof(SpecificationValidationDataSource))]
        public void ComplexSpecificationWithExecute(PersonEntity data, bool valid, string message)
        {
            var validation = new ComplexSpecificationValidation();
            var execute = new Execute();
            var result = validation.IsSatisfiedBy(data, execute);

            valid.Should().Be(result, message);
            valid.Should().Be(!execute.HasErro, message);
        }
    }
}
