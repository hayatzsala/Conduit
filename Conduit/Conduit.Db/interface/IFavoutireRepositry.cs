using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Db
{
    public interface IFavouriteRepositry
{
        public Task<bool> AddFavourite(Guid userId, Guid articleId);
}
}
