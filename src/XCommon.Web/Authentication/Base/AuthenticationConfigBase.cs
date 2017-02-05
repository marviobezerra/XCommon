namespace XCommon.Web.Authentication.Base
{
    public abstract class AuthenticationConfigBase
    {
        public AuthenticationConfigBase(AuthenticationType type)
        {
            Type = type;
        }

        public AuthenticationType Type { get; private set; }
    }
}
