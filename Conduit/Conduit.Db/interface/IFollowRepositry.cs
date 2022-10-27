using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Db
{
    public interface IFollowRepositry
    {
        public Task<bool> followAfriend(Guid userId, Guid FriendId);
}
}
