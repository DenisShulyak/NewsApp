using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp
{
   public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Login { get; set; }// Login

        public string Password { get; set; }
        
    }
}
