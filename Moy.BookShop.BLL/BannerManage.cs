using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moy.BookShop.DAL;
using System.Text.RegularExpressions;
using Moy.BookShop.Common;

namespace Moy.BookShop.BLL
{
    public class BannerManage
    {
        public static bool IsBannerdWord(string msg)
        {
            IEnumerable<string> allBanner = null;

            if (CacheHelper.Get("bannerWord") == null)
            {
                allBanner = new BannerService().GetAllBanner();
                CacheHelper.Insert("bannerWord", allBanner);
            }
            string pattern = string.Join("|", CacheHelper.Get("bannerWord"));
            return Regex.IsMatch(msg, pattern);
        }
    }
}