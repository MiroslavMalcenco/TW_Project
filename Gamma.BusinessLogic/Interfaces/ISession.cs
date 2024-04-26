using Gamma.Domain.Entities.Response;
using Gamma.Domain.Entities.User;
using System.Collections.Generic;

namespace Gamma.BusinessLogic.Interfaces
{
    public interface ISession
    {
        IEnumerable<UDBModel> GetAll();
        ServiceResponse ValidateUserCredential(ULoginData user);
        ServiceResponse ChangePassword(UChangePasswordData password);
        ServiceResponse ValidateUserRegister(URegisterData newUser);
        CookieResponse GenCookie(string username);
        UserMinimal GetUserByCookie(string apiCookieValue);
        ServiceResponse EditProfileAction(UEditProfileData existingUser);
        UEditProfileData GetUserById(int userId);
    }
}