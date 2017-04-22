using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moy.BookShop.Model
{
    public class User
    {
        public int Id { get; set; }
        public string LoginId { get; set; }
        public string LoginPwd { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public DateTime? Birthday { get; set; } 
        public UserRole UserRole { get; set; }
        public UserState UserState { get; set; }
        public string RegisterIp { get; set; }
        public DateTime RegisterTime { get; set; }
    }
}
