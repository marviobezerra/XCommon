using XCommon.Just4You.Business.Contract.Register;
using XCommon.Just4You.Business.Entity.Enumerators;
using XCommon.Just4You.Business.Entity.Register;
using XCommon.Just4You.Business.Entity.Register.Filter;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XCommon.Application;
using XCommon.Application.Authentication;
using XCommon.Application.Authentication.Entity;
using XCommon.Application.Executes;
using XCommon.Application.Settings;
using XCommon.Patterns.Ioc;
using XCommon.Patterns.Repository.Entity;
using XCommon.Patterns.Specification.Validation;
using XCommon.Util;
using XCommon.EF.Application.Register.Interface;
using XCommon.Application.Register.Entity.Filter;

namespace XCommon.EF.Application.Authentication
{
	public class LoginBusiness : : ILoginBusiness
	{
		#region Inject
		[Inject]
		private IPeopleBusiness PeopleBusiness { get; set; }

		[Inject]
		private IUsersBusiness UsersBusiness { get; set; }

		[Inject]
		private IUsersProvidersBusiness UsersProvidersBusiness { get; set; }

		[Inject]
		private ISpecificationValidation<SignInEntity> ValidateSignIn { get; set; }

		[Inject]
		private ISpecificationValidation<SignUpEntity> ValidateSignUp { get; set; }
		#endregion

		public LoginBusiness()
		{
			Kernel.Resolve(this);
		}

		public Task<Execute> ChangePasswordAsync(PasswordChangeEntity info)
		{
			throw new NotImplementedException();
		}

		public Task<Execute> ConfirmEmailAsync(Guid userKey, string token)
		{
			throw new NotImplementedException();
		}

		public async Task<Execute> ConfirmPhoneAsync(Guid userKey, string token)
		{
			var result = new Execute();
			var user = await UsersBusiness.GetByKeyAsync(userKey);

			return result;
		}

		public Task<Execute> RecoveryPasswordAsync(PasswordRecoveryEntity info)
		{
			throw new NotImplementedException();
		}

		public Task<Execute> RecoveryPasswordRequestTokenAsync(string email)
		{
			throw new NotImplementedException();
		}

		public Task<Execute> RecoveryPasswordValidateTokenAsync(Guid userKey, string token)
		{
			throw new NotImplementedException();
		}

		public async Task<Execute<TicketEntity>> SignInAsync(SignInEntity signIn)
		{
			var result = new Execute<TicketEntity>();

			if (!await ValidateSignIn.IsSatisfiedByAsync(signIn, result))
			{
				return result;
			}

			var person = await PeopleBusiness.GetFirstByFilterAsync(new PeopleFilter { Email = signIn.User });

			if (person == null)
			{
				result.AddMessage(ExecuteMessageType.Error, Resources.Messages.InvalidUserPassword);
				return result;
			}

			var user = await UsersBusiness.GetByKeyAsync(person.IdPerson);

			if (user == null)
			{
				result.AddMessage(ExecuteMessageType.Error, Resources.Messages.InvalidUserPassword);
				return result;
			}

			if (signIn.Provider == ProviderType.Local)
			{
				var passwordHash = Functions.GetMD5(signIn.Password);

				if (user.PasswordHash != passwordHash)
				{
					result.AddMessage(ExecuteMessageType.Error, Resources.Messages.InvalidUserPassword);
					return result;
				}

				result.Entity = new TicketEntity
				{
					Culture = person.Culture,
					Key = person.IdPerson,
					Name = person.Name,
					Roles = new List<string> { ((int)companyUser.Role).ToString() }
				};

				result.Entity.Values.Add("cp", company.IdCompany.ToString());

				return result;
			}

			var provider = await UsersProvidersBusiness.GetFirstByFilterAsync(new UsersProvidersFilter { IdUser = person.IdPerson, Provider = signIn.Provider });

			if (provider == null)
			{
				result.AddMessage(ExecuteMessageType.Error, Resources.Messages.InvalidUserPassword);
				return result;
			}

			result.Entity = new TicketEntity
			{
				Culture = person.Culture,
				Key = person.IdPerson,
				Name = person.Name,
				Roles = new List<string> { ((int)companyUser.Role).ToString() }
			};

			result.Entity.Values.Add("cp", company.IdCompany.ToString());

			return result;
		}

		public async Task<Execute<TicketEntity>> SignUpAsync(SignUpEntity signUp)
		{
			var result = new Execute<TicketEntity>();

			if (!await ValidateSignUp.IsSatisfiedByAsync(signUp, result))
			{
				return result;
			}

			var (person, user, provider, companyPerson) = await CastNewUser(signUp);

			using (var db = new Data.Just4YouContext())
			{
				using (var transaction = await db.Database.BeginTransactionAsync())
				{
					result.AddMessage(await PeopleBusiness.SaveAsync(person, db));
					result.AddMessage(await UsersBusiness.SaveAsync(user, db));
					result.AddMessage(await UsersProvidersBusiness.SaveAsync(provider, db));
					result.AddMessage(await CompaniesPeopleBusiness.SaveAsync(companyPerson, db));

					if (!result.HasErro)
					{
						transaction.Commit();
					}
				}
			}

			if (!result.HasErro)
			{
				result.Entity = new TicketEntity
				{
					Culture = person.Culture,
					Key = person.IdPerson,
					Name = person.Name,
					Roles = new List<string> { ((int)companyPerson.Role).ToString() }
				};
			}

			return result;
		}

		private async Task<(PeopleEntity person, UsersEntity user, UsersProvidersEntity provider, CompaniesPeopleEntity companyPerson)> CastNewUser(SignUpEntity signUp)
		{
			var company = await CompaniesBusiness.GetCurrentCompany();

			var person = new PeopleEntity
			{
				Action = EntityAction.New,
				IdPerson = Guid.NewGuid(),
				Name = signUp.Name,
				Email = signUp.Email,
				Birthday = signUp.BirthDay,
				Culture = signUp.Language,
				Gender = signUp.Male
				   ? GenderType.Male
				   : GenderType.Female,
				CreatedAt = DateTime.Now,
				ChangedAt = DateTime.Now,
				ImageCover = signUp.UrlCover,
				ImageProfile = signUp.UrlImage
			};

			var passwordHash = signUp.Provider == XCommon.Application.Authentication.Entity.ProviderType.Local
				? Functions.GetMD5(signUp.Password)
				: string.Empty;

			var user = new UsersEntity
			{
				Action = EntityAction.New,
				IdUser = person.IdPerson,
				IdPerson = person.IdPerson,
				AccessFailedCount = 0,
				EmailConfirmed = false,
				LockoutEnabled = false,
				PasswordHash = passwordHash,
				PhoneConfirmed = false,
				ProfileComplete = false
			};

			var userProvider = new UsersProvidersEntity
			{
				Action = EntityAction.New,
				IdUserProvide = Guid.NewGuid(),
				IdUser = person.IdPerson,
				Provider = signUp.Provider,
				ProviderDefault = true,
				ProviderToken = signUp.Token,
				ProviderUrlCover = signUp.UrlCover,
				ProviderUrlImage = signUp.UrlImage
			};

			var companyPerson = new CompaniesPeopleEntity
			{
				Action = EntityAction.New,
				IdCompanyPerson = Guid.NewGuid(),
				IdCompany = company.IdCompany,
				IdPerson = person.IdPerson,
				Role = RoleType.Anonymous
			};

			return (person, user, userProvider, companyPerson);
		}
	}
}
