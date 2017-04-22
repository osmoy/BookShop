using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace Moy.BookShop.Common
{
    public class CacheHelper
    {       
        public static void Insert(string key, object value)
        {
            Cache cache = HttpRuntime.Cache;
            cache.Insert(key, value);
        }
        
        public static void Delete(string key)
        {
            Cache cache = HttpRuntime.Cache;
            cache.Remove(key);
        }
        
        public static object Get(string key)
        {
            Cache cache = HttpRuntime.Cache;
            return cache[key];
        }
    }
}
