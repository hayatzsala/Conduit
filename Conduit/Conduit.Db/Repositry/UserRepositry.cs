using Microsoft.EntityFrameworkCore;
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
                 }

                 );

            return await Save();
        }

        public async Task<User> GetUserByEmail(string Email)
        {
            return await _context.Users.Where(s=>s.Email.Equals(Email)).SingleOrDefaultAsync()??null;

        }
        public async Task<Guid> GetUserID(string Email)
        {

         var user = await _context.Users.Where(s => s.Email.Equals(Email)).SingleOrDefaultAsync();
          
            return user.UserId ==null? default(Guid):user.UserId;

        }
        public async Task<bool> updateUserData(User UserTable,string Email)
        {
            var User = await GetUserByEmail(Email);
            User.UserName = UserTable.UserName;
            User.Age = UserTable.Age;
            User.Bio = UserTable.Bio;
            User.Password = BCrypt.Net.BCrypt.HashPassword(UserTable.Password);
            var save = await Save();
            if (save)
            {
                return true;

            }
            return false;
        }


        public async Task<User> GetUserById(Guid id)
        {
            return await _context.Users.Where(s => s.UserId.Equals(id)).SingleOrDefaultAsync();
        }


        public async Task<IEnumerable<User>> GetAllUser()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }


    }
}
