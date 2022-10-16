using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Db
{
    public class Favourite
    {
        public int FavouriteId { get; set; }
        public int UserId { get; set; }
        public int ArticleId { get; set; }
        public Article Articles { get; set; }
        public User Users { get; set; }
    }
}
