using System;
using XCommon.Patterns.Repository.Entity;
using XCommon.Patterns.Repository.Executes;
using XCommon.Patterns.Specification.Validation;
using XCommon.Patterns.Specification.Validation.Extensions;

namespace XCommon.Test.Patterns.Specification.Validation.Sample
{
    public class ComplexSpecificationValidation : SpecificationValidation<PersonEntity>
    {
        public override bool IsSatisfiedBy(PersonEntity entity, Execute execute)
        {
            Specifications
                .AndMerge(ValidateNew(), c => c.Action == EntityAction.New)
                .AndMerge(ValidateUpdate(), c => c.Action == EntityAction.Update)                
                .AndMerge(ValidateDelete(), c => c.Action == EntityAction.Delete)                
                .AndIsNotEmpty(c => c.Name, "Person needs a Name")                
                .AndIsEmail(c => c.Email, "Person needs a valida email");

            return CheckSpecifications(entity, execute);
        }

        private SpecificationList<PersonEntity> ValidateDelete()
        {
            return NewSpecificationList()
                .AndIsValid(c => c.Id != Guid.Empty, "Delete person needs to have a valid ID");
        }

        private SpecificationList<PersonEntity> ValidateUpdate()
        {
            return NewSpecificationList()
                .AndIsValid(c => c.Age >= 21, "Update person needs to be older than 21");
        }

        private SpecificationList<PersonEntity> ValidateNew()
        {
            return NewSpecificationList()
                .AndIsValid(c => c.Age >= 18, "New person needs to be older than 18");
        }
    }
}
