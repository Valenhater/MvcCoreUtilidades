using Microsoft.AspNetCore.Mvc;
using MvcCoreUtilidades.Helpers;
using System.Net;
using System.Net.Mail;

namespace MvcCoreUtilidades.Controllers
{
    public class MailExampleController : Controller
    {   
        //NECESITAMOS RECUPERAR LAS CLAVES DE SETTINGS
        private HelperUploadFiles helperUploadFiles;
        private HelperMails helperMails;

        public MailExampleController(HelperMails helperMails, HelperUploadFiles helperUploadFiles)
        {
            this.helperUploadFiles = helperUploadFiles;
            this.helperMails = helperMails;
        }

        public IActionResult SendMail()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendMail(string para, string asunto, string mensaje, IFormFile file)
        {
            if (file != null)
            {
                string path = await helperUploadFiles.UploadFileAsync(file, Folders.Mails);
                await this.helperMails.SendMailsAsync(para, asunto, mensaje, path);
            }
            else
            {
                await helperMails.SendMailsAsync(para, asunto, mensaje);
            }      
            ViewData["MENSAJE"] = "Email enviado correctamente";
            return View();
        }
    }
}
