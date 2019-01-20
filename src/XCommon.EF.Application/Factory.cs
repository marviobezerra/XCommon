using XCommon.Application.Authentication;
using XCommon.Application.Authentication.Entity;
using XCommon.EF.Application.Authentication;
using XCommon.EF.Application.Authentication.Validation;
using XCommon.EF.Application.Context.Register;
using XCommon.EF.Application.Context.System;
using XCommon.EF.Application.Register.Implementation;
using XCommon.EF.Application.Register.Implementation.Query;
using XCommon.EF.Application.Register.Implementation.Validate;
using XCommon.EF.Application.Register.Interface;
using XCommon.EF.Application.System.Implementation;
using XCommon.EF.Application.System.Implementation.Query;
using XCommon.EF.Application.System.Implementation.Validate;
using XCommon.EF.Application.System.Interface;
using XCommon.Entity.Register;
using XCommon.Entity.Register.Filter;
using XCommon.Entity.System;
using XCommon.Entity.System.Filter;
using XCommon.Patterns.Ioc;
using XCommon.Patterns.Specification.Query;
using XCommon.Patterns.Specification.Validation;

namespace XCommon.EF.Application
{
	public static class Factory
	{
		public static void Do()
		{
			Repository();
			Query();
			Validation();
			Others();
		}

		private static void Validation()
		{
			Kernel.Map<ISpecificationValidation<PeopleEntity>>().To<PeopleValidate>();
			Kernel.Map<ISpecificationValidation<UsersEntity>>().To<UsersValidate>();
			Kernel.Map<ISpecificationValidation<UsersProvidersEntity>>().To<UsersProvidersValidate>();
			Kernel.Map<ISpecificationValidation<UsersRolesEntity>>().To<UsersRolesValidate>();
			Kernel.Map<ISpecificationValidation<UsersTokensEntity>>().To<UsersTokensValidate>();
			Kernel.Map<ISpecificationValidation<SignUpEntity>>().To<SignUpValidate>();
			Kernel.Map<ISpecificationValidation<SignInEntity>>().To<SignInValidate>();
			Kernel.Map<ISpecificationValidation<ConfigsEntity>>().To<ConfigsValidate>();
		}

		private static void Query()
		{
			Kernel.Map<ISpecificationQuery<People, PeopleFilter>>().To<PeopleQuery>();
			Kernel.Map<ISpecificationQuery<Users, UsersFilter>>().To<UsersQuery>();
			Kernel.Map<ISpecificationQuery<UsersProviders, UsersProvidersFilter>>().To<UsersProvidersQuery>();
			Kernel.Map<ISpecificationQuery<UsersRoles, UsersRolesFilter>>().To<UsersRolesQuery>();
			Kernel.Map<ISpecificationQuery<UsersTokens, UsersTokensFilter>>().To<UsersTokensQuery>();

			Kernel.Map<ISpecificationQuery<Configs, ConfigsFilter>>().To<ConfigsQuery>();
		}

		private static void Repository()
		{
			Kernel.Map<IPeopleBusiness>().To<PeopleBusiness>();
			Kernel.Map<IUsersBusiness>().To<UsersBusiness>();
			Kernel.Map<IUsersProvidersBusiness>().To<UsersProvidersBusiness>();
			Kernel.Map<IUsersRolesBusiness>().To<UsersRolesBusiness>();
			Kernel.Map<IUsersTokensBusiness>().To<UsersTokensBusiness>();

			Kernel.Map<IConfigsBusiness>().To<ConfigsBusiness>();			
		}

		private static void Others()
		{
			Kernel.Map<ILoginBusiness>().To<LoginBusiness>();
			Kernel.Map<IPasswordPolicesBusiness>().To<PasswordPolicesBusiness>();
		}
	}
}
