using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moy.BookShop.Model
{
    public class Cart
    {
        public int Id { get; set; }
        //public int UserId { get; set; }
        public User User { get; set; }
        //public int BookId { get; set; }
        public Book Book { get; set; }
        public int Quantit { get; set; }
    }
}