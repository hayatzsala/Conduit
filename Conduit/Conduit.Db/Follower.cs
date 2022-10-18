using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Db
{
    public class Follower
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FollowerId { get; set; }
        public User Users { get; set; }


    }
}
