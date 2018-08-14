using XCommon.Patterns.Repository;
using XCommon.Test.EF.Sample.Context;
using XCommon.Test.EF.Sample.Entity;

namespace XCommon.Test.EF.Sample
{
    public class PeopleBusiness : RepositoryEFBase<PeopleEntity, PeopleFilter, People, SampleContext>
	{
	}
}
