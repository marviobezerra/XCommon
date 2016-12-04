using System;
using XCommon.Patterns.Repository.Entity;
using XCommon.Application.Executes;
using XCommon.Patterns.Specification.Validation;
using XCommon.Patterns.Specification.Validation.Extensions;
using XCommon.Patterns.Specification.Validation.Implementation;
using XCommon.Test.Entity;

namespace XCommon.Test.Patterns.Specification.Validation.Sample
{
    public class ComplexSpecificationValidation : SpecificationValidation<PersonEntity>
    {
        public override bool IsSatisfiedBy(PersonEntity entity, Execute execute)
        {
            var specifications = NewSpecificationList()
                .AndMerge(ValidateNew(), c => c.Action == EntityAction.New)
                .AndMerge(ValidateUpdate(), c => c.Action == EntityAction.Update)                
                .AndMerge(ValidateDelete(), c => c.Action == EntityAction.Delete)                
                .AndIsNotEmpty(c => c.Name, "Person needs a Name")                
                .AndIsEmail(c => c.Email, "Person needs a valida email");

            return CheckSpecifications(specifications, entity, execute);
        }

        private SpecificationList<PersonEntity> ValidateDelete()
        {
            return NewSpecificationList(false)
                .AndIsValid(c => c.Id != Guid.Empty, "Delete person needs to have a valid ID");
        }

        private SpecificationList<PersonEntity> ValidateUpdate()
        {
            return NewSpecificationList(false)
                .AndIsValid(c => c.Age >= 21, "Update person needs to be older than 21");
        }

        private SpecificationList<PersonEntity> ValidateNew()
        {
            return NewSpecificationList(false)
                .AndIsValid(c => c.Age >= 18, "New person needs to be older than 18");
        }
    }
}
