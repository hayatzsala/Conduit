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
        public  Task<User> GetUserByEmail(string Email);
        public Task<bool> updateUserData(User UserTable, string Email);
        public Task<User> GetUserById(Guid id);
        public Task<IEnumerable<User>> GetAllUser();
        public Task<Guid> GetUserID(string Email);
}
}
