using Core.Log.Exceptions;
using Core.Server.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Core.Server.Brokers
{
    public class CacheBroker
    {
        CacheItemPolicy _Policy;

        public ObjectCache Cache { get; set; }

        private CacheBroker()
        {
            Cache = MemoryCache.Default;
            _Policy = new CacheItemPolicy();
            _Policy.SlidingExpiration = ServerConfiguration.Configuration.CacheConfiguration.ExpirePeriod;
        }

        public void AddCache(string key, object value)
        {
            Cache.Set(key, value, _Policy);
        }

        public void AddCache<T>(string key, T value) where T : class
        {
            Cache.Set(key, value, _Policy);
        }

        public T GetCache<T>(string key) where T : class
        {
            T ret;

            ret = Cache.Get(key) as T;

            return ret;
        }

        public static CacheBroker Init()
        {
            CacheBroker ret = new CacheBroker();
            return ret;
        }
    }
}
