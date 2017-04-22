using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moy.BookShop.Model
{
    public class Order
    {
       public string Id{get;set;} 
       public DateTime OrderDate{get;set;} 
       public int UserId{get;set;} 
       public decimal TotalPrice{get;set;} 
       public string ReceiverAddress{get;set;}
       public bool OrderState { get; set; }
    }
}
