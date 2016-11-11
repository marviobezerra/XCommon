using System;

namespace XCommon.Patterns.Specification.Validation
{
    internal class SpecificationItem<TEntity>
    {
        internal ISpecificationValidation<TEntity> Specification { get; set; }

        internal bool StopIfInvalid { get; set; }

        internal Func<TEntity, bool> Condition { get; set; }
    }
}
