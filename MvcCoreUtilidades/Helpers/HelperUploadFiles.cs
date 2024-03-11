using System.IO;
using System.Net.Mail;

namespace MvcCoreUtilidades.Helpers
{
    public class HelperUploadFiles
    {
        private HelperPathProvider helperPathProvider;

        public HelperUploadFiles(HelperPathProvider helperPathProvider)
        {
            this.helperPathProvider = helperPathProvider;
        }

        public async Task<string> UploadFileAsync (IFormFile file, Folders folder)
        {
            string fileName = file.FileName;
            //RECUPERAMOS LA RUTA DE NUESTRO FICHERO CON EL HELPER
            string path = this.helperPathProvider.MapPath(fileName, folder);
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return path;
        }
    }
}
