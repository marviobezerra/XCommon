using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XCommon.Application.Authentication;
using XCommon.Application.Authentication.Entity;
using XCommon.Application.Executes;
using XCommon.Entity.Register;
using XCommon.Entity.Register.Enumerators;
using XCommon.Entity.Register.Filter;
using XCommon.EF.Application.Register.Interface;
using XCommon.Patterns.Ioc;
using XCommon.Patterns.Repository.Entity;
using XCommon.Patterns.Specification.Validation;
using XCommon.Util;
using System.Linq;

namespace XCommon.EF.Application.Authentication
{
	public class LoginBusiness : ILoginBusiness
	{
		#region Inject
		[Inject]
		private ITicketManager TicketManager { get; set; }

		[Inject]
		private IPeopleBusiness PeopleBusiness { get; set; }

		[Inject]
		private IUsersBusiness UsersBusiness { get; set; }

		[Inject]
		private IUsersProvidersBusiness UsersProvidersBusiness { get; set; }

		[Inject]
		private IUsersRolesBusiness UsersRolesBusiness { get; set; }

		[Inject]
		private ISpecificationValidation<SignInEntity> ValidateSignIn { get; set; }

		[Inject]
		private ISpecificationValidation<SignUpEntity> ValidateSignUp { get; set; }
		#endregion

		public LoginBusiness()
		{
			Kernel.Resolve(this);
		}

		#region Public Methods
		public virtual async Task<Execute> ChangePasswordAsync(PasswordChangeEntity info)
		{
			var result = new Execute();
			var user = await UsersBusiness.GetByKeyAsync(TicketManager.UserKey);

			if (user == null)
			{
				result.AddError(Resources.Authentication.InvalidUserPassword);
				return result;
			}

			if (!await VerifyPasswordHashAsync(user, info.PasswordCurrent))
			{
				result.AddError(Resources.Authentication.PasswordNotMatch);
				return result;
			}

			if (info.PasswordNew != info.PasswordConfirm)
			{

			}

			user.Action = EntityAction.Update;
			user.PasswordHash = await GetPasswordHashAsync(user, info.PasswordNew);
			result.AddMessage(await UsersBusiness.SaveAsync(user));

			return result;
		}

		public virtual async Task<Execute> ConfirmEmailAsync(Guid userKey, string token)
		{
			throw new NotImplementedException();
		}

		public virtual async Task<Execute> ConfirmPhoneAsync(Guid userKey, string token)
		{
			var result = new Execute();
			var user = await UsersBusiness.GetByKeyAsync(userKey);

			return result;
		}

		public virtual async Task<Execute> RecoveryPasswordAsync(PasswordRecoveryEntity info)
		{
			throw new NotImplementedException();
		}

		public virtual async Task<Execute> RecoveryPasswordRequestTokenAsync(string email)
		{
			throw new NotImplementedException();
		}

		public virtual async Task<Execute> RecoveryPasswordValidateTokenAsync(Guid userKey, string token)
		{
			throw new NotImplementedException();
		}

		public virtual async Task<Execute<TicketEntity>> SignInAsync(SignInEntity signIn)
		{
			var result = new Execute<TicketEntity>();

			if (!await ValidateSignIn.IsSatisfiedByAsync(signIn, result))
			{
				return result;
			}

			var person = await PeopleBusiness.GetFirstByFilterAsync(new PeopleFilter { Email = signIn.User });

			if (person == null)
			{
				result.AddMessage(ExecuteMessageType.Error, Resources.Authentication.InvalidUserPassword);
				return result;
			}

			var user = await UsersBusiness.GetByKeyAsync(person.IdPerson);

			if (user == null)
			{
				result.AddMessage(ExecuteMessageType.Error, Resources.Authentication.InvalidUserPassword);
				return result;
			}

			if (signIn.Provider == ProviderType.Local)
			{
				if (!await VerifyPasswordHashAsync(user, signIn.Password))
				{
					result.AddMessage(ExecuteMessageType.Error, Resources.Authentication.InvalidUserPassword);
					return result;
				}

				result.Entity = new TicketEntity
				{
					Culture = person.Culture,
					Key = person.IdPerson,
					Name = person.Name,
					Roles = await GetUserRolesAsync(user)
				};

				await SetTicketCustomValuesAsync(result.Entity);

				return result;
			}

			var provider = await UsersProvidersBusiness.GetFirstByFilterAsync(new UsersProvidersFilter { IdUser = person.IdPerson, Provider = signIn.Provider });

			if (provider == null)
			{
				result.AddMessage(ExecuteMessageType.Error, Resources.Authentication.InvalidUserPassword);
				return result;
			}

			result.Entity = new TicketEntity
			{
				Culture = person.Culture,
				Key = person.IdPerson,
				Name = person.Name,
				Roles = await GetUserRolesAsync(user)
			};

			await SetTicketCustomValuesAsync(result.Entity);

			return result;
		}

		public async Task<Execute<TicketEntity>> SignUpAsync(SignUpEntity signUp)
		{
			var result = new Execute<TicketEntity>();

			if (!await ValidateSignUp.IsSatisfiedByAsync(signUp, result))
			{
				return result;
			}

			var (person, user, provider) = await CastNewUser(signUp);

			using (var db = new Context.XCommonDbContext())
			{
				using (var transaction = await db.Database.BeginTransactionAsync())
				{
					result.AddMessage(await PeopleBusiness.SaveAsync(person, db));
					result.AddMessage(await UsersBusiness.SaveAsync(user, db));
					result.AddMessage(await UsersProvidersBusiness.SaveAsync(provider, db));

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
					Roles = await GetUserRolesAsync(user)
				};
			}

			return result;
		}
		#endregion

		#region Protected
		protected virtual async Task SetTicketCustomValuesAsync(TicketEntity ticket)
		{
			await Task.Yield();
		}

		protected virtual async Task<List<string>> GetUserRolesAsync(UsersEntity user)
		{
			var result = await UsersRolesBusiness.GetByFilterAsync(new UsersRolesFilter { IdUser = user.IdUser });
			return result.Select(c => c.Role).ToList();
		}

		protected virtual async Task<string> GetPasswordHashAsync(UsersEntity user, string password)
		{
			return await Task.FromResult(Hash.GetMD5(password));
		}

		protected virtual async Task<bool> VerifyPasswordHashAsync(UsersEntity user, string input)
		{
			return await Task.FromResult(Hash.VerifyMD5(input, user.PasswordHash));
		}

		protected virtual async Task<(PeopleEntity person, UsersEntity user, UsersProvidersEntity provider)> CastNewUser(SignUpEntity signUp)
		{
			var person = new PeopleEntity
			{
				Action = EntityAction.New,
				IdPerson = Guid.NewGuid(),
				Name = signUp.Name,
				Email = signUp.Email,
				Birthday = signUp.BirthDay,
				Culture = signUp.Language,
				Gender = signUp.Male ? GenderType.Male : GenderType.Female,
				CreatedAt = DateTime.Now,
				ChangedAt = DateTime.Now,
				ImageCover = signUp.UrlCover,
				ImageProfile = signUp.UrlImage
			};

			var user = new UsersEntity
			{
				Action = EntityAction.New,
				IdUser = person.IdPerson,
				IdPerson = person.IdPerson,
				AccessFailedCount = 0,
				EmailConfirmed = false,
				LockoutEnabled = false,
				PasswordHash = string.Empty,
				PhoneConfirmed = false,
				ProfileComplete = false
			};

			if (signUp.Provider == ProviderType.Local)
			{
				user.PasswordHash = await GetPasswordHashAsync(user, signUp.Password);
			}

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

			return await Task.FromResult((person, user, userProvider));
		}
		#endregion
	}
}
