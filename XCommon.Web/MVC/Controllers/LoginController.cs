using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using XCommon.Application.Login;
using XCommon.Patterns.Ioc;
using XCommon.Patterns.Repository.Executes;
using XCommon.Web.MVC.Filters;
using System.Security.Claims;
using System.Web.Mvc;

namespace XCommon.Web.MVC.Controllers
{
    public abstract class LoginController<TLoginBusiness> : BaseController
        where TLoginBusiness : ILoginBusiness
    {
        [Inject]
        protected TLoginBusiness LoginBusiness { get; set; }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Login(LoginEntity login)
        {
            Execute<LoginPersonEntity> result = LoginBusiness.ValidaUser(login);

            if (!result.HasErro)
            {
                DoLogin(result.Entity, login.RememberMe);
                return RedirectTo(LoginStatus.LoginSucess);
            }

            ViewBag.LoginStatus = LoginStatus.LoginFail;
            return RedirectTo(LoginStatus.LoginFail);
        }

        [HttpPost]
        [AllowAnonymous]
        [JsonValidateAntiForgeryToken]
        public virtual JsonResult LoginAjax(LoginEntity login)
        {
            Execute<LoginPersonEntity> result = LoginBusiness.ValidaUser(login);

            if (!result.HasErro)
                DoLogin(result.Entity, login.RememberMe);

            return Json(new Execute(result));
        }

        [HttpPost]
        [Authorize]
        [JsonValidateAntiForgeryToken]
        public virtual JsonResult ChangePasswordAjax(LoginChangePasswordEntity info)
        {
            info.Key = UserSession.Key;
            Execute result = LoginBusiness.ChangePassword(info);
            return Json(result);
        }

        [AllowAnonymous]
        [JsonValidateAntiForgeryToken]
        [HttpPost]
        public virtual JsonResult RegisterNew(LoginPersonEntity user)
        {

            var execute = LoginBusiness.ValidaUserNew(user);

            if (!execute.HasErro)
                DoLogin(execute.Entity, true);

            return Json(execute);
        }

        [AllowAnonymous]
        [JsonValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult EmailExists(string email)
        {
            return Json(LoginBusiness.EmailExists(email), JsonRequestBehavior.DenyGet);
        }

        [AllowAnonymous]
        public virtual ActionResult Logout()
        {
            Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectTo(LoginStatus.LogOutSucess);
        }

        protected virtual void DoLogin(LoginPersonEntity user, bool persistent)
        {
            DoLogin(user.Key, user.FirstName, persistent);
        }

        protected virtual void DoLogin(object userKey, string fullName, bool persistent)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                new[] { new Claim(ClaimTypes.Name, userKey.ToString()) },
                DefaultAuthenticationTypes.ApplicationCookie,
                ClaimTypes.Name,
                ClaimTypes.Role);

            identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", fullName));
            identity.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "XPet"));

            Authentication.SignIn(new AuthenticationProperties
            {
                IsPersistent = persistent
            }, identity);
        }

        protected virtual ActionResult RedirectTo(LoginStatus loginStatus)
        {
            throw new System.NotImplementedException();
        }
    }
}
