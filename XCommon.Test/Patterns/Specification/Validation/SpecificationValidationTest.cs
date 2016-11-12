using FluentAssertions;
using XCommon.Patterns.Repository.Executes;
using XCommon.Test.Patterns.Specification.Validation.Sample;
using Xunit;

namespace XCommon.Test.Patterns.Specification.Validation
{
    public class SpecificationValidationTest
    {
        [Theory(DisplayName = "Default Specification")]
        [MemberData(nameof(PersonDataSource.DefaultDataSource), MemberType = typeof(PersonDataSource))]
        public void DefaultSpecification(PersonEntity data, bool valid, string message)
        {
            DefaultSpecificationValidation validation = new DefaultSpecificationValidation();
            bool result = validation.IsSatisfiedBy(data);

            valid.Should().Be(result, message);
        }

        [Theory(DisplayName = "Default Specification (With execute)")]
        [MemberData(nameof(PersonDataSource.DefaultDataSource), MemberType = typeof(PersonDataSource))]
        public void DefaultSpecificationWithExecute(PersonEntity data, bool valid, string message)
        {
            DefaultSpecificationValidation validation = new DefaultSpecificationValidation();
            Execute execute = new Execute();
            bool result = validation.IsSatisfiedBy(data, execute);

            valid.Should().Be(result, message);
            valid.Should().Be(!execute.HasErro, message);
        }

        [Theory(DisplayName = "Complete Specification")]
        [MemberData(nameof(PersonDataSource.CompleteDataSource), MemberType = typeof(PersonDataSource))]
        public void CompleteSpecification(PersonEntity data, bool valid, string message)
        {
            CompleteSpecificationValidation validation = new CompleteSpecificationValidation();
            bool result = validation.IsSatisfiedBy(data);

            valid.Should().Be(result, message);
        }

        [Theory(DisplayName = "Complete Specification (With execute)")]
        [MemberData(nameof(PersonDataSource.CompleteDataSource), MemberType = typeof(PersonDataSource))]
        public void CompleteSpecificationWithExecute(PersonEntity data, bool valid, string message)
        {
            CompleteSpecificationValidation validation = new CompleteSpecificationValidation();
            Execute execute = new Execute();
            bool result = validation.IsSatisfiedBy(data, execute);

            valid.Should().Be(result, message);
            valid.Should().Be(!execute.HasErro, message);
        }

        [Theory(DisplayName = "Complex Specification")]
        [MemberData(nameof(PersonDataSource.ComplexDataSource), MemberType = typeof(PersonDataSource))]
        public void ComplexSpecification(PersonEntity data, bool valid, string message)
        {
            ComplexSpecificationValidation validation = new ComplexSpecificationValidation();
            bool result = validation.IsSatisfiedBy(data);

            valid.Should().Be(result, message);
        }

        [Theory(DisplayName = "Complex Specification (With execute)")]
        [MemberData(nameof(PersonDataSource.ComplexDataSource), MemberType = typeof(PersonDataSource))]
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
