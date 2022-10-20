using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Db
{
    public class Favourite
    {
        public Guid FavouriteId { get; set; }
        public Guid UserId { get; set; }
        public int ArticleId { get; set; }
    }
}
