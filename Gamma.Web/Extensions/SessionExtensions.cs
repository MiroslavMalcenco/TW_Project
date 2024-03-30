using System.Web;
using Gamma.Domain.Entities.User;
using Gamma.Domain.Enum;

namespace WebApplication1.Extensions
{
    public static class SessionExtensions
    {
        public static UDBModel GetUser(this HttpSessionStateBase session)
        {
            return (UDBModel)session["__User"];
        }
        public static void ClearUser(this HttpSessionStateBase session)
        {
            session.Remove("__User");
        }
        public static void SetUser(this HttpSessionStateBase session, UDBModel user)
		{
			session["__User"] = user;
		}
        public static bool IsUserLoggedIn(this HttpSessionStateBase session)
        {
            return session.GetUser() != null;
        }
        public static bool UserHasRole(this HttpSessionStateBase session, URole role)
        {
            if (!session.IsUserLoggedIn())
                return false;
            var user = session.GetUser();
            return user.AccessLevel >= role;
        }
    }
}