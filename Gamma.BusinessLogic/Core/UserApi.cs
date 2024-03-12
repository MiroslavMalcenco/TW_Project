using Gamma.BusinessLogic.DBModel;
using Gamma.Domain.Entities.Response;
using Gamma.Domain.Entities.Session;
using Gamma.Domain.Entities.User;
using Gamma.Helpers;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Gamma.BusinessLogic.Core
{
    public class UserApi
    {
        public ServiceResponse ReturnCredentialStatus(ULoginData user)
        {
            var hPass = LoginHelper.HashGen(user.Password);
            using (var db = new UserContext())
            {
                return new ServiceResponse { Status = false, StatusMessage = "Имя пользователя или пароль введены неверно" };
            }
        }
        public ServiceResponse ReturnChangedPassword(UChangePasswordData password)
        {
            using (var db = new UserContext())
            {
                try
                {
                    var user = db.Users.Find(password.Id);
                    if (user == null || user.Password != LoginHelper.HashGen(password.OldPassword)) return new ServiceResponse { Status = false, StatusMessage = "An error occurred!" };
                    user.Password = LoginHelper.HashGen(password.NewPassword);
                    db.SaveChanges();
                    return new ServiceResponse() { Status = true, StatusMessage = "Пароль успешно изменен" };
                }
                catch
                {
                    return new ServiceResponse { Status = false, StatusMessage = "Возникла ошибка" };
                }
            }
        }
        public ServiceResponse ReturnRegisterStatus(URegisterData newUser)
        {
            var response = new ServiceResponse();
            using (var db = new UserContext())
            {
                try
                {
                    var existingUser = db.Users.FirstOrDefault(u => u.Email == newUser.Email || u.UserName == newUser.UserName);
                    if (existingUser != null)
                    {
                        response.StatusMessage = "Пользователь с данной почтой уже существует";
                        response.Status = false;
                        return response;
                    }
                    var user = new UDBModel
                    {
                        FullName = newUser.FullName,
                        UserName = newUser.UserName,
                        Email = newUser.Email,
                        PhoneNumber = newUser.PhoneNumber,
                        Password = LoginHelper.HashGen(newUser.Password),
                        Terms = newUser.Terms,
                        RegisterIP = newUser.IP,
                        RegisterDateTime = DateTime.Now,
                        LoginDateTime = DateTime.Now,
                        AccessLevel = Domain.Enum.URole.USER,
                    };
                    using (var db2 = new UserContext())
                    {
                        db2.Users.Add(user);
                        db2.SaveChanges();
                    }
                    response.StatusMessage = "Пользователь успешно зарегистрирован";
                    response.Status = true;
                }
                catch (Exception ex)
                {
                    response.StatusMessage = "Возникла ошибка при регистрации";
                    response.Status = false;
                }
            }
            return response;
        }
        public CookieResponse CookieGeneratorAction(string username)
        {
            var apiCookie = new HttpCookie("X-KEY")
            {
                Value = CookieGenerator.Create(username)
            };
            using (var db = new SessionContext())
            {
                SDBModel curent;
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(username))
                {
                    curent = (from e in db.Sessions where e.Username == username select e).FirstOrDefault();
                }
                else
                {
                    curent = (from e in db.Sessions where e.Username == username select e).FirstOrDefault();
                }
                if (curent != null)
                {
                    curent.CookieString = apiCookie.Value;
                    curent.ExpireTime = DateTime.Now.AddMinutes(60);
                    using (var todo = new SessionContext())
                    {
                        todo.Entry(curent).State = EntityState.Modified;
                        todo.SaveChanges();
                    }
                }
                else
                {
                    db.Sessions.Add(new SDBModel
                    {
                        Username = username,
                        CookieString = apiCookie.Value,
                        ExpireTime = DateTime.Now.AddMinutes(60)
                    });
                    db.SaveChanges();
                }
            }
            return new CookieResponse
            {
                Cookie = apiCookie,
                Data = DateTime.Now
            };
        }
        internal UserMinimal UserCookie(string cookie)
        {
            SDBModel session;
            UDBModel currentUser;
            using (var db = new SessionContext())
            {
                session = db.Sessions.FirstOrDefault(s => s.CookieString == cookie && s.ExpireTime > DateTime.Now);
            }
            if (session == null) return null;
            using (var db = new UserContext())
            {
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(session.Username))
                {
                    currentUser = db.Users.FirstOrDefault(u => u.Email == session.Username);
                }
                else
                {
                    currentUser = db.Users.FirstOrDefault(u => u.UserName == session.Username);
                }
            }
            if (currentUser == null) return null;
            var userMinimal = new UserMinimal
            {
                Id = currentUser.Id,
                Username = currentUser.UserName,
                Email = currentUser.Email,
                Level = currentUser.AccessLevel
            };
            return userMinimal;
        }
        public ServiceResponse ReturnEditedProfile(UEditProfileData existingUser)
        {
            var response = new ServiceResponse();
            using (var db = new UserContext())
            {
                try
                {
                    var userToEdit = db.Users.Find(existingUser.Id);
                    if (userToEdit != null)
                    {
                        userToEdit.PhoneNumber = existingUser.PhoneNumber;
                        userToEdit.Email = existingUser.Email;
                        userToEdit.FullName = existingUser.FullName;
                        db.SaveChanges();
                        response.Status = true;
                        response.StatusMessage = "Профиль пользователя успешно изменен";
                    }
                    else
                    {
                        response.Status = false;
                        response.StatusMessage = "Возникла ошибка";
                    }
                }
                catch (Exception ex)
                {
                    response.Status = false;
                    response.StatusMessage = "Возникла ошибка";
                }
            }
            return response;
        }
        public UEditProfileData ReturnUserById(int userId)
        {
            using (var db = new UserContext())
            {
                var user = db.Users.Find(userId);
                if (user != null)
                {
                    var foundUser = new UEditProfileData()
                    {
                        UserName = user.UserName,
                        PhoneNumber = user.PhoneNumber,
                        Email = user.Email,
                        FullName = user.FullName,
                        Id = userId
                    };
                    return foundUser;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}