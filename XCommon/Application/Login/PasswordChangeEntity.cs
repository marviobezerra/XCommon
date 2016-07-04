using System;

namespace XCommon.Application.Login
{
    public class PasswordChangeEntity
    {
        public Guid Key { get; set; }

        public string PasswordCurrent { get; set; }

        public string PasswordNew { get; set; }

        public string PasswordConfirm { get; set; }
    }
}
