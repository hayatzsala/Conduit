using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Db.Repositry
{
    public class FollowRepositry:IFollowRepositry
    {
        private readonly ConduitContext _context;

        public FollowRepositry(ConduitContext context)
        {
            _context = context;
        }

        public async Task<bool> followAfriend(Guid userId,Guid FriendId)
        {
            await _context.Followers.AddAsync(

               new Follower
               {
                  UserId = userId,
                  FollowerId= FriendId

               });
            return await Save();

        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

    }
}
