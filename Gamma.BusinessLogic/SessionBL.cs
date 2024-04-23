using Gamma.BusinessLogic.Core;
using Gamma.BusinessLogic.DBModel;
using Gamma.BusinessLogic.Interfaces;
using Gamma.Domain.Entities.Response;
using Gamma.Domain.Entities.User;
using System.Collections.Generic;
using System.Linq;

namespace Gamma.BusinessLogic
{
    public class SessionBL : UserApi, ISession
    {
        private readonly UserContext _context;
        public SessionBL()
        {
            _context = new UserContext();
        }
        public SessionBL(UserContext context)
        {
            _context = context;
        }
        public IEnumerable<UDBModel> GetAll()
        {
            return _context.Users.ToList();
        }
        public ServiceResponse ValidateUserCredential(ULoginData data)
        {
            return ReturnCredentialStatus(data);
        }
        public ServiceResponse ChangePassword(UChangePasswordData password)
        {
            return ReturnChangedPassword(password);
        }
        public ServiceResponse ValidateUserRegister(URegisterData newUser)
        {
            return ReturnRegisterStatus(newUser);
        }
        public CookieResponse GenCookie(string username)
        {
            return CookieGeneratorAction(username);
        }
        public UserMinimal GetUserByCookie(string apiCookieValue)
        {
            return UserCookie(apiCookieValue);
        }
        public ServiceResponse EditProfileAction(UEditProfileData existingUser)
        {
            return ReturnEditedProfile(existingUser);
        }
        public UEditProfileData GetUserById (int userId)
        {
            return ReturnUserById(userId);
        }
    }
}