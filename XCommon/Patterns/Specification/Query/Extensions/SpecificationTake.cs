using XCommon.Patterns.Specification.Query.Implementation;

namespace XCommon.Patterns.Specification.Query.Extensions
{
    public static class SpecificationTake
    {
        public static SpecificationList<TEntity, TFilter> Take<TEntity, TFilter>(this SpecificationList<TEntity, TFilter> specification, int pageNumber, int pageSize)
        {
            specification.PageNumber = pageNumber;
            specification.PageSize = pageSize;
            return specification;
        }
    }
}
