using System;
using XCommon.Util;

namespace XCommon.Patterns.Specification.Validation.Extensions
{
    public static class SpecificationUtil
    {
        public static SpecificationList<TEntity> AndIsEmail<TEntity>(this SpecificationList<TEntity> specification, Func<TEntity, string> selector, string message, params object[] args)
            => specification.AndIsEmail(selector, c => true, false, message, args);

        public static SpecificationList<TEntity> AndIsEmail<TEntity>(this SpecificationList<TEntity> specification, Func<TEntity, string> selector, bool condition, bool stopIfInvalid, string message, params object[] args)
            => specification.AndIsEmail(selector, c => condition, stopIfInvalid, message, args);

        public static SpecificationList<TEntity> AndIsEmail<TEntity>(this SpecificationList<TEntity> specification, Func<TEntity, string> selector, Func<TEntity, bool> condition, bool stopIfInvalid, string message, params object[] args)
        {
            return specification.AndRegexValid(selector, LibraryRegex.Email, condition, stopIfInvalid, message, args);            
        }
        
        public static SpecificationList<TEntity> AndIsUrl<TEntity>(this SpecificationList<TEntity> specification, Func<TEntity, string> selector, string message, params object[] args)
            => specification.AndIsUrl(selector, c => true, false, message, args);

        public static SpecificationList<TEntity> AndIsUrl<TEntity>(this SpecificationList<TEntity> specification, Func<TEntity, string> selector, bool condition, bool stopIfInvalid, string message, params object[] args)
            => specification.AndIsUrl(selector, c => condition, stopIfInvalid, message, args);

        public static SpecificationList<TEntity> AndIsUrl<TEntity>(this SpecificationList<TEntity> specification, Func<TEntity, string> selector, Func<TEntity, bool> condition, bool stopIfInvalid, string message, params object[] args)
        {
            return specification.AndRegexValid(selector, LibraryRegex.URL, condition, stopIfInvalid, message, args);
        }
    }
}
