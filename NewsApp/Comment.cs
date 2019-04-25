using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp
{
   public class Comment
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string TextComment { get; set; }

        public Guid ArticleId { get; set; }
        public Article Article { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
             
    }
}
