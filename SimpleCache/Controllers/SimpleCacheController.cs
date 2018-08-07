using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace SimpleCache.Controllers
{
    [Route("api/[controller]")]
    public class SimpleCacheController : Controller
    {
        private IMemoryCache _memoryCache;

        public SimpleCacheController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        // GET api/cache
        [HttpGet]
        public string Get()
        {
            var actualDate = DateTime.Now.ToString();
            var cachedDate = string.Empty;
            var key = "cacheKey";

            if (_memoryCache.TryGetValue<string>(key, out cachedDate))
            {
                return string.Concat("Data cache: ", cachedDate);
            }
            else
            {
                cachedDate = actualDate;
                var cacheExpiration = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(10));
                _memoryCache.Set<string>(key, cachedDate, cacheExpiration);
                return string.Concat("Data atual: ", actualDate);
            }
        }
    }
}


