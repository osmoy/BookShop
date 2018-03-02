using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moy.BookShop.Model
{    
    public class SearchDetail
    {
        public Guid Id { get; set; }
        public string KeyWords { get; set; }
        public DateTime SearchTime { get; set; }
    }
}