using System;

namespace Gamma.Domain.Entities.User
{
    public class ULoginData
    {
        public string IP { get; set; }
        public DateTime LastLoginTime { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}