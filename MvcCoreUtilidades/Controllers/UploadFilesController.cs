using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc;
using MvcCoreUtilidades.Helpers;

namespace MvcCoreUtilidades.Controllers
{
    public class UploadFilesController : Controller
    {

        private IServer server;
        private HelperPathProvider helperPathProvider;

        public UploadFilesController(HelperPathProvider helperPathProvider, IServer server)
        {   
            this.server = server;
            this.helperPathProvider = helperPathProvider;
        }

        public IActionResult SubirFichero()
        {   
            //Este es de prueba para ver como va, el bueno esta en el helperpathprovider
            var addresses = server.Features.Get<IServerAddressesFeature>().Addresses;
            var url = addresses.FirstOrDefault();
            ViewData["PRUEBA"] =url;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SubirFichero(IFormFile fichero)
        {
            string path = this.helperPathProvider.MapPath(fichero.FileName, Folders.Uploads);
            //SUBIMOS EL FICHERO UTILIZANDO STREAM
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                //MEDIANTE IFORMFILE COPIAMOS EL CONTENIDO DEL FICHERO AL STREAM
                await fichero.CopyToAsync(stream);
            }
            ViewData["MENSAJE"] = "Fichero subido a: " + path;

            string urlPath = this.helperPathProvider.MapUrlPath(fichero.FileName, Folders.Uploads);
            ViewData["URL"] = urlPath;
            return View();
        }
    }
}
