using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace XCommon.Application.Login
{
    public class LoginPersonEntity
    {
        public object Key { get; set; }
        
        public string FullName { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
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
        
        public bool MakeService { get; set; }
        
        public string CityName { get; set; }
    }
}
