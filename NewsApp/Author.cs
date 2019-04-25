using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp
{
   public class Author
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string FullName { get; set; }

        public ICollection<Article> Articles { get; set; }
    }
}
