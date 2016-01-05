using XCommon.Extensions.String;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using XCommon.Application.Login;
using XCommon.Patterns.Repository.Executes;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace XCommon.Web.MVC.Controllers
{
    public class LoginExternalController<TLoginBusiness> : LoginController<TLoginBusiness>
        where TLoginBusiness : ILoginBusiness
    {
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectTo(LoginStatus.LoginSucess);
            }

            if (ModelState.IsValid)
            {
                ExternalLoginInfo info = await Authentication.GetExternalLoginInfoAsync();

                if (info == null)
                {
                    return RedirectTo(LoginStatus.LoginFailExternal);
                }
            }

            return View(model);
        }

        [AllowAnonymous]
        public virtual async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            ExternalLoginInfo loginInfo = await Authentication.GetExternalLoginInfoAsync();

            if (loginInfo == null)
                return RedirectTo(LoginStatus.LoginFailExternal);

            Execute<LoginPersonEntity> execute = LoginBusiness.ValidaUserNew(ParsePersonLogin(loginInfo, true));

            if (!execute.HasErro)
                DoLogin(execute.Entity, true);

            return RedirectTo(!execute.HasErro ? LoginStatus.LoginSucess : LoginStatus.LoginFailExternal);
        }

        protected virtual LoginPersonEntity ParsePersonLogin(ExternalLoginInfo loginInfo, bool external = false)
        {
            LoginPersonEntity retorno = new LoginPersonEntity
            {
                Key = null,
                Email = loginInfo.Email,
                Birthday = DateTime.Now,
                External = external,
                FullName = loginInfo.ExternalIdentity.Name,
                Provider = loginInfo.Login.LoginProvider,
                ProviderKey = loginInfo.Login.ProviderKey,
                Culture = "pt-BR",
            };

            if (loginInfo.Login.LoginProvider.IsNotEmpty() && loginInfo.Login.LoginProvider.ToLower() == "google")
            {
                ParseGoogleLogin(loginInfo, retorno);
            }

            return retorno;
        }

        private void ParseGoogleLogin(ExternalLoginInfo loginInfo, LoginPersonEntity retorno)
        {
            try
            {
                var googleGender = loginInfo.ExternalIdentity.Claims.FirstOrDefault(c => c.Type.Contains("urn:google:gender"));
                var googleLanguage = loginInfo.ExternalIdentity.Claims.FirstOrDefault(c => c.Type.Contains("urn:google:language"));
                var googlePlacesLived = loginInfo.ExternalIdentity.Claims.FirstOrDefault(c => c.Type.Contains("urn:google:placesLived"));

                // Try get "Gender"
                if (googleGender != null && googleGender.Value.IsNotEmpty())
                {
                    retorno.Male = googleGender.Value.Contains("male");
                }

                // Try get "Language" 
                if (googleLanguage != null && googleLanguage.Value.IsNotEmpty())
                {
                    retorno.Culture = googleLanguage.Value.ToLower().Contains("br")
                            ? "pt-BR"
                            : "en-US";
                }

                // Try get "Country"
                if (googlePlacesLived != null && googlePlacesLived.Value.IsNotEmpty())
                {
                    var cityName = Regex.Replace(googlePlacesLived.Value, ".*value\":.?\"|\".*|[\\[\\]\\{\\}]|[\\r\\n]", string.Empty);

                    retorno.CityName = googlePlacesLived == null
                        ? retorno.CityName
                        : cityName.Trim();
                }
            }
            finally
            {

            }
        }

        #region Helper
        private const string XsrfKey = "XsrfId";

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };

                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

        public class ExternalLoginConfirmationViewModel
        {
            public string Email { get; set; }
        }
        #endregion
    }
}
