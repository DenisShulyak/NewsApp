using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NewsApp
{
   public class Article //Статья
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string NewsTitle { get; set; } // Название новости
        public string TextNews { get; set; }  // Само содержание статьи
        

        public Guid AuthorId { get; set; }
        public Author Author { get; set; }
             
    }
}