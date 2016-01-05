using Microsoft.Owin.Security.OAuth;
using XCommon.Application.Login;
using XCommon.Patterns.Ioc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace XCommon.Web.WebApi.Controller
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public SimpleAuthorizationServerProvider()
        {
            Kernel.Resolve(this);
        }

        [Inject]
        public ILoginBusiness LoginBusiness { get; set; }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.Run(() =>
            {
                context.Validated();
            });
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            await Task.Run(() => 
            {
                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

                var execute = LoginBusiness.ValidaUser(new LoginEntity { User = context.UserName, Password = context.Password, RememberMe = true });
                
                if (execute.HasErro)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }

                DoLogin(context, execute.Entity.Key, execute.Entity.FirstName, true);
            });
        }

        protected virtual void DoLogin(OAuthGrantResourceOwnerCredentialsContext context, object userKey, string fullName, bool persistent)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                new[] { new Claim(ClaimTypes.Name, userKey.ToString()) },
                context.Options.AuthenticationType,
                ClaimTypes.Name,
                ClaimTypes.Role);

            identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", fullName));
            identity.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "XPet"));

            context.Validated(identity);
        }
    }
}
