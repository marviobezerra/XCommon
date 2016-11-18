using FluentAssertions;
using XCommon.Patterns.Repository.Executes;
using XCommon.Test.Entity;
using XCommon.Test.Patterns.Specification.Validation.DataSource;
using XCommon.Test.Patterns.Specification.Validation.Sample;
using Xunit;

namespace XCommon.Test.Patterns.Specification.Validation
{
    public class SpecificationValidationTest
    {
        [Theory(DisplayName = "Default Specification")]
        [MemberData(nameof(SpecificationValidationDataSource.DefaultDataSource), MemberType = typeof(SpecificationValidationDataSource))]
        public void DefaultSpecification(PersonEntity data, bool valid, string message)
        {
            DefaultSpecificationValidation validation = new DefaultSpecificationValidation();
            bool result = validation.IsSatisfiedBy(data);

            valid.Should().Be(result, message);
        }

        [Theory(DisplayName = "Default Specification (With execute)")]
        [MemberData(nameof(SpecificationValidationDataSource.DefaultDataSource), MemberType = typeof(SpecificationValidationDataSource))]
        public void DefaultSpecificationWithExecute(PersonEntity data, bool valid, string message)
        {
            DefaultSpecificationValidation validation = new DefaultSpecificationValidation();
            Execute execute = new Execute();
            bool result = validation.IsSatisfiedBy(data, execute);

            valid.Should().Be(result, message);
            valid.Should().Be(!execute.HasErro, message);
        }

        [Theory(DisplayName = "Complete Specification")]
        [MemberData(nameof(SpecificationValidationDataSource.CompleteDataSource), MemberType = typeof(SpecificationValidationDataSource))]
        public void CompleteSpecification(PersonEntity data, bool valid, string message)
        {
            CompleteSpecificationValidation validation = new CompleteSpecificationValidation();
            bool result = validation.IsSatisfiedBy(data);

            valid.Should().Be(result, message);
        }

        [Theory(DisplayName = "Complete Specification (With execute)")]
        [MemberData(nameof(SpecificationValidationDataSource.CompleteDataSource), MemberType = typeof(SpecificationValidationDataSource))]
        public void CompleteSpecificationWithExecute(PersonEntity data, bool valid, string message)
        {
            CompleteSpecificationValidation validation = new CompleteSpecificationValidation();
            Execute execute = new Execute();
            bool result = validation.IsSatisfiedBy(data, execute);

            valid.Should().Be(result, message);
            valid.Should().Be(!execute.HasErro, message);
        }

        [Theory(DisplayName = "Complex Specification")]
        [MemberData(nameof(SpecificationValidationDataSource.ComplexDataSource), MemberType = typeof(SpecificationValidationDataSource))]
        public void ComplexSpecification(PersonEntity data, bool valid, string message)
        {
            ComplexSpecificationValidation validation = new ComplexSpecificationValidation();
            bool result = validation.IsSatisfiedBy(data);

            valid.Should().Be(result, message);
        }

        [Theory(DisplayName = "Complex Specification (With execute)")]
        [MemberData(nameof(SpecificationValidationDataSource.ComplexDataSource), MemberType = typeof(SpecificationValidationDataSource))]
        public void ComplexSpecificationWithExecute(PersonEntity data, bool valid, string message)
        {
            ComplexSpecificationValidation validation = new ComplexSpecificationValidation();
            Execute execute = new Execute();
            bool result = validation.IsSatisfiedBy(data, execute);

            valid.Should().Be(result, message);
            valid.Should().Be(!execute.HasErro, message);
        }
    }
}
