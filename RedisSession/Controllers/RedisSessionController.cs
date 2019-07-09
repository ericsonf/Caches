using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace RedisSession.Controllers
{
    [Route("api/[controller]")]
    public class RedisSessionController : Controller
    {
        // GET api/RedisSession
        [HttpGet]
        public string Get()
        {
            var key = "sessioncachekey";
            var storedValue = HttpContext.Session.GetString(key);
            
            if (string.IsNullOrEmpty(storedValue))
            {
                storedValue = "Fulano";
                HttpContext.Session.SetString(key, storedValue);
            }

             return string.Concat("Olá ", storedValue, " sua sessão foi criada com sucesso.");
        }

        // GET api/RedisSession/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            var key = "sessioncachekey";
            var storedValue = HttpContext.Session.GetString(key);
            
            if (string.IsNullOrEmpty(storedValue))
            {
                return string.Concat("Você não esta autorizado a ver o conteúdo dessa página.");
            }

            return string.Concat("Olá ", storedValue, " seja bem vindo."); 
        }
    }
}
