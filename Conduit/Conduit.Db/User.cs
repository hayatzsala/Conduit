using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Db
{
    public class User
    {
        public Guid UserId { get; set; }
        public string  UserName { get; set; }
        public string  Email { get; set; }
        public string  Password { get; set; }
        public string  Bio { get; set; }
        public int  Age { get; set; }
    }
}