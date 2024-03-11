using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace MvcCoreUtilidades.Controllers
{
    public class CachingController : Controller
    {
        private IMemoryCache memoryChache;

        public CachingController(IMemoryCache memoryChache)
        {
            this.memoryChache = memoryChache;
        }
        //PODEMOS INDICAR EL TIEMPO EN SEGUNDOS PARA QUE RESPONDA DE NUEVO AL ACTION
        [ResponseCache(Duration = 15, Location =ResponseCacheLocation.Client)]
        public IActionResult MemoriaDistribuida()
        {
            string fecha = DateTime.Now.ToLongDateString() + "--" + DateTime.Now.ToLongTimeString();
            ViewData["FECHA"] = fecha;
            return View();
        }
        public IActionResult MemoriaPersonalizada(int? tiempo)
        {   
            //LA PIRMERA VEZ NO RECIBIMOS TEIMPO
            //VAMOS A PONER UN TIEMPO DE 60 SEGUNDOS
            if(tiempo == null)
            {
                tiempo = 5;
            }
            string fecha = DateTime.Now.ToLongDateString() + "--" + DateTime.Now.ToLongTimeString();
            //PREGUNTAMOS SI EXISTE ALGO EN CACHE O NO EXISTE
            if(this.memoryChache.Get("FECHA") == null)
            {
                //NO EXISTE NADA EN CACHE TODAVIA
                //CREAMOS LAS OPCIONES PARA EL CACHE CON TIEMPO
                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(tiempo.Value));
                this.memoryChache.Set("FECHA", fecha, options);
                ViewData["MENSAJE"] = "Almacenando en cache";
                ViewData["FECHA"] = this.memoryChache.Get("FECHA");
            }
            else
            {
                //TENEMOS LA FECHA EN CACHE
                fecha = this.memoryChache.Get<string>("FECHA");
                ViewData["MENSAJE"] = "Recuperando de cache";
                ViewData["FECHA"] = fecha;
            }
            return View();
        }
    }
}
