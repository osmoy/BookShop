using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moy.BookShop.Model
{
    [Serializable]
    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FilePath { get; set; }
        public short Status { get; set; }
        public string FileExt { get; set; }        
        public bool IsFlag { get; set; }

    }
}
