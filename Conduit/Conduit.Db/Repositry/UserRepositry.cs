
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Db.Repositry
{
    public  class UserRepositry:IUserRepositry
    {
        private readonly ConduitContext _context;

        public UserRepositry(ConduitContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateUser(User user)
        {

            var userCreate = await _context.Users.AddAsync(

                 new User
                 {
                     UserId= user.UserId,
                     Age = user.Age,
                     Bio = user.Bio,
                     Email = user.Email,
                     Password = user.Password,
                     UserName = user.UserName,
                     Articles = user.Articles,
                     Comments = user.Comments,
                     Favourites= user.Favourites,
                 }

                 );

            return await Save();
        }

        public async Task<User> GetUser(string Email)
        {
            return await _context.Users.Where(s=>s.Email.Equals(Email)).SingleOrDefaultAsync();

        }


        public async void updateUserData(User UserTable)
        {
            var User = await GetUserById(UserTable.UserId);
            User.UserName = UserTable.UserName;
            User.Age = UserTable.Age;
            User.Bio = UserTable.Bio;
            User.Email = UserTable.Email;
            _context.SaveChanges();
        }


        public async Task<User> GetUserById(Guid id)
        {
            return await _context.Users.Where(s => s.UserId.Equals(id)).SingleOrDefaultAsync();
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            var User = await GetUserById(id);

            if (User != null)
            {
                _context.Remove(User);

            }
            return await Save();
        }


        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }


    }
}
