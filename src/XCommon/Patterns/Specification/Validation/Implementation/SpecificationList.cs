﻿using System;
using System.Collections.Generic;

namespace XCommon.Patterns.Specification.Validation.Implementation
{
    public class SpecificationList<TEntity>
    {
        public SpecificationList()
        {
            Items = new List<SpecificationItem<TEntity>>();
        }

        internal List<SpecificationItem<TEntity>> Items { get; set; }

        public SpecificationList<TEntity> Add(ISpecificationValidation<TEntity> specification, bool stopIfInvalid = false)
            => Add(specification, c => true, stopIfInvalid);

        public SpecificationList<TEntity> Add(ISpecificationValidation<TEntity> specification, bool contidion, bool stopIfInvalid = false)
            => Add(specification, c => contidion, stopIfInvalid);

        public SpecificationList<TEntity> Add(ISpecificationValidation<TEntity> specification, Func<TEntity, bool> contidion, bool stopIfInvalid = false)
            => Add(new SpecificationItem<TEntity> { Specification = specification, StopIfInvalid = stopIfInvalid, Condition = contidion });

        public SpecificationList<TEntity> Add(SpecificationItem<TEntity> specification)
        {
            Items.Add(specification);
            return this;
        }
    }
}
