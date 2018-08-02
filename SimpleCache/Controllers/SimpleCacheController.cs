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
        private IMemoryCache _cache;

        public SimpleCacheController(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        // GET api/cache
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var actualDate = DateTime.Now.ToString();
            var cachedDate = string.Empty;
            var key = "cacheKey";

            if (!_cache.TryGetValue<string>(key, out cachedDate))
            {
                cachedDate = DateTime.Now.ToString();
                var cacheExpiration = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(5));
                _cache.Set<string>(key, cachedDate, cacheExpiration);
            }

            return new string[] { string.Concat("Data atual: ", actualDate), string.Concat("Data cache: ", cachedDate) };
        }
    }
}


