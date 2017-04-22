using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moy.BookShop.DAL;

namespace Moy.BookShop.BLL
{
    public class CaptchaManager
    {
        public static void Add(int userId, string token)
        {
            new CaptchaService().Add(userId, token);
        }
    }
}
