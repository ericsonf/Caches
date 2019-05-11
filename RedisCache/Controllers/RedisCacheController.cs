using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace RedisCache.Controllers
{
    [Route("api/[controller]")]
    public class RedisCacheController : Controller
    {
        private readonly IDistributedCache _distributedCache;
    
        public RedisCacheController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        // GET api/RedisCache
        [HttpGet]
        public string Get()
        {
            var date = string.Empty;
            var key = "distributedcachekey";

            date = _distributedCache.GetString(key);
            if (!string.IsNullOrEmpty(date))
            {
                return string.Concat("Data cache: ", date);
            }
            else
            {
                date = DateTime.Now.ToString();
                var cacheExpiration = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(10));
                _distributedCache.SetString(key, date, cacheExpiration);
                return string.Concat("Data atual: ", date);
            }
        }
    }
}
