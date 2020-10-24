using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using twiterClone.DAL.Classes.UserClases;

namespace twitterClone.Entity.Interfaces
{
    public interface IAuthRepository
    {
        Task<UserClass> Register(UserClass user);
        Task<UserClass> Login(string UserName, string Password);
        Task<bool> UserNameExisist(string UserName);
        Task<bool> UserEmailExisist(string Email);
    }
}
