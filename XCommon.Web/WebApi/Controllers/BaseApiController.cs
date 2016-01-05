using XCommon.Patterns.Ioc;
using XCommon.Patterns.Repository.Executes;
using System.Web.Http;

namespace XCommon.Web.WebApi.Controller
{
    [Authorize]
    public abstract class BaseApiController : ApiController
    {
        public BaseApiController()
        {
            Kernel.Resolve(this);
        }

        public bool IsAuthenticated
        {
            get
            {
                return User.Identity.IsAuthenticated;
            }
        }

        public ExecuteUser UserSession
        {
            get
            {
                ExecuteUser retorno;

                if (IsAuthenticated)
                    retorno = new ExecuteUser { Key = (object)User.Identity.Name };
                else
                    retorno = null;

                return retorno;
            }
        }
    }
}
