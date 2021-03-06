using XCommon.Entity.Register;
using XCommon.Entity.Register.Filter;
using XCommon.Patterns.Repository;

namespace XCommon.EF.Application.Register.Interface
{
	public interface IPeopleBusiness : IRepositoryEF<PeopleEntity, PeopleFilter>
	{
	}
}
