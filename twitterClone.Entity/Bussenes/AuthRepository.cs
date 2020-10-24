using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using twiterClone.DAL.Classes.UserClases;
using twiterClone.DAL.DataContext;
using twitterClone.Entity.Interfaces;
using Microsoft.EntityFrameworkCore;
using twiterClone.DAL.Enums;

namespace twitterClone.Entity.Bussenes
{
    public class AuthRepository : IAuthRepository
    {
        private readonly CloneDataContext context;

        public AuthRepository(CloneDataContext _context)
        {
            context = _context;
        }

        public async Task<UserClass> Register(UserClass user)
        {
            user.ObjectStatus = ObjectStatus.NonDeleted;
            user.Status = Status.Active;
            user.CreatedDate = DateTime.Now;
            user.CreatedBy = 0;
            user.LastUpdateBy = 0;
            user.LastUpdateDate = DateTime.Now;

            await context.AddAsync(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task<UserClass> Login(string UserName, string Password)
        {
            var user = await context.Users.FirstOrDefaultAsync(t => t.UserName == UserName && t.Password == Password);

            if (user == null)
                return null;
            return user;
        }

       

        public async Task<bool> UserEmailExisist(string Email)
        {
            if (await context.Users.AnyAsync(t => t.Email == Email))
                return true;
            return false;
        }

        public async Task<bool> UserNameExisist(string UserName)
        {
            if (await context.Users.AnyAsync(t => t.UserName == UserName))
                return true;
            return false;
        }
    }
}
