using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace MvcCoreUtilidades.Helpers
{   
    //AQUI DEBERIAMOS TENER TODA S LAS CARPETAS QUE DESEEMOS QUE NUESTROS CONTROLLER UTILICEN
    public enum Folders { Images=0, Facturas=1, Uploads=2, Temporal=3, Mails = 4 }
    public class HelperPathProvider
    {
        //NECESITAMOS ACCEDERAL SISTEMA DE ARCHIVOS DEL WEB SERVER(wwwroot)
        private IWebHostEnvironment hostEnvironment;
        private IServer server;

        public HelperPathProvider(IWebHostEnvironment hostEnvironment, IServer server)
        {
            this.server = server;
            this.hostEnvironment = hostEnvironment;
        }

        //METODO PRIVADO QUE NOS DEVUELVA LA CARPETA DEPENDIENDO DEL FOLDER
        private string GetFolderPath(Folders folder)
        {
            string carpeta = "";
            if (folder == Folders.Images)
            {
                carpeta = "images";
            }
            else if (folder == Folders.Facturas)
            {
                carpeta = "temp";
            }
            else if (folder == Folders.Uploads)
            {
                carpeta = "uploads";
            }
            else if (folder == Folders.Temporal)
            {
                carpeta = "temp";
            }
            else if (folder == Folders.Mails)
            {
                carpeta = "mails";
            }
            return carpeta;
        }

        public string MapPath(string fileName, Folders folder)
        {
            string carpeta = this.GetFolderPath(folder);                    
            string rootPath = this.hostEnvironment.WebRootPath;
            string path = Path.Combine(rootPath, carpeta, fileName);
            return path;
        }

        public string MapUrlPath(string fileName, Folders folder)
        {
            string carpeta = this.GetFolderPath(folder);
            var addresses = server.Features.Get<IServerAddressesFeature>().Addresses;
            string serverUrl = addresses.FirstOrDefault(); 
            string urlPath = serverUrl + "/" + carpeta + "/" + fileName;
            return urlPath;
        }
    }
}
