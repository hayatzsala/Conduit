using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Db
{
    public interface IUserRepositry
{
        public  Task<bool> CreateUser(User user);
        public  Task<User> GetUser(string Email);
        public void updateUserData(User UserTable);
        public Task<User> GetUserById(Guid id);
        public  Task<bool> DeleteUser(Guid id);
}
}
