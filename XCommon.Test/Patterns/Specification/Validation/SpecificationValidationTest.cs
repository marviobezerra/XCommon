using XCommon.Patterns.Repository.Executes;
using XCommon.Test.Patterns.Specification.Validation.Sample;
using Xunit;

namespace XCommon.Test.Patterns.Specification.Validation
{
    public class SpecificationValidationTest
    {
        [Fact(DisplayName = "Default entity validation")]
        public void DefaultEntityValidation()
        {
            PersonEntity person = new PersonEntity();

            PersonEmptyValidation validation = new PersonEmptyValidation();
            bool result = validation.IsSatisfiedBy(person);

            Assert.Equal(true, result);
        }

        [Fact(DisplayName = "Null entity validation")]
        public void NullEntityValidation()
        {
            PersonEmptyValidation validation = new PersonEmptyValidation();

            bool result = validation.IsSatisfiedBy(null);

            Assert.Equal(false, result);
        }


        [Fact(DisplayName = "Default entity validation (With execute)")]
        public void DefaultEntityValidationWithExecute()
        {
            PersonEntity person = new PersonEntity();
            Execute execute = new Execute();
            PersonEmptyValidation validation = new PersonEmptyValidation();

            bool result = validation.IsSatisfiedBy(person, execute);

            Assert.Equal(false, execute.HasErro);
            Assert.Equal(0, execute.Messages.Count);
            Assert.Equal(true, result);
        }

        [Fact(DisplayName = "Null entity validation (With execute)")]
        public void NullEntityValidationWithExecute()
        {
            PersonEmptyValidation validation = new PersonEmptyValidation();
            Execute execute = new Execute();

            bool result = validation.IsSatisfiedBy(null, execute);

            Assert.Equal(true, execute.HasErro);
            Assert.Equal(1, execute.Messages.Count);
            Assert.Equal(false, result);
        }

        [Fact(DisplayName = "String Is Not Empty Validation")]
        public void StringIsNotEmptyValidation()
        {
            PersonEntity person = new PersonEntity();
            PersonIsNotEmptyValidation validation = new PersonIsNotEmptyValidation();

            bool result = validation.IsSatisfiedBy(person);

            Assert.Equal(false, result);
        }

        [Fact(DisplayName = "String Is Not Empty Validation (With execute)")]
        public void StringIsNotEmptyValidationWithExecute()
        {
            PersonEntity person = new PersonEntity();
            PersonIsNotEmptyValidation validation = new PersonIsNotEmptyValidation();
            Execute execute = new Execute();

            bool result = validation.IsSatisfiedBy(person, execute);

            Assert.Equal(true, execute.HasErro);
            Assert.Equal(2, execute.Messages.Count);
            Assert.Equal(false, result);
        }

        [Fact(DisplayName = "Is Valid Validation")]
        public void IsValidValidation()
        {
            PersonEntity person = new PersonEntity();
            PersonIsValidValidation validation = new PersonIsValidValidation();

            bool result = validation.IsSatisfiedBy(person);

            Assert.Equal(false, result);
        }

        [Fact(DisplayName = "Is Valid Validation (With execute)")]
        public void IsValidValidationWithExecute()
        {
            PersonEntity person = new PersonEntity();
            PersonIsValidValidation validation = new PersonIsValidValidation();
            Execute execute = new Execute();

            bool result = validation.IsSatisfiedBy(person, execute);

            Assert.Equal(true, execute.HasErro);
            Assert.Equal(2, execute.Messages.Count);
            Assert.Equal(false, result);
        }
    }
}
