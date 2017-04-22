using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moy.BookShop.Model
{
    public class Banner
    {
        public int Id { get; set; }
        public string WordPattern { get; set; }
        public bool IsForbid { get; set; }
        public bool IsMod { get; set; }
        public string ReplaceWord { get; set; }
    }
}
