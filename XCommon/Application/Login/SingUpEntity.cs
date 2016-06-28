using System;

namespace XCommon.Application.Login
{
	public class SingUpEntity
    {
        public Guid Key { get; set; }
        
        public string FullName { get; set; }
        
        public string SocialName { get; set; }
        
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string Culture { get; set; }
        
        public DateTime Birthday { get; set; }
        
        public bool Male { get; set; }
        
        public string Password { get; set; }
        
        public string Provider { get; set; }
        
        public string ProviderKey { get; set; }
        
        public bool External { get; set; }
    }
}
