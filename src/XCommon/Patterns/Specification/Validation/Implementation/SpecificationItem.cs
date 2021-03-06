﻿using System;

namespace XCommon.Patterns.Specification.Validation.Implementation
{
    public class SpecificationItem<TEntity>
    {
        public ISpecificationValidation<TEntity> Specification { get; set; }

        public bool StopIfInvalid { get; set; }

        public Func<TEntity, bool> Condition { get; set; }
    }
}
