using Gamma.Domain.Enum;

namespace Gamma.Web.Models
{
    public class UserData
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public URole AccessLevel { get; set; }
    }
}