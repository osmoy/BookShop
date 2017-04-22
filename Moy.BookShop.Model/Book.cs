using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moy.BookShop.Model
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        ///出版社
        public Publisher Publisher { get; set; }        
        public DateTime PublishDate { get; set; }
        public string ISBN { get; set; }
        public decimal UnitPrice { get; set; }
        public string ContentDescription { get; set; }
        ///图书目录
        public string TOC { get; set; }
        public Category Category { get; set; }        
        public int Clicks { get; set; }
    }
}