﻿using Microsoft.AspNetCore.Mvc;
using MvcCoreUtilidades.Helpers;

namespace MvcCoreUtilidades.Controllers
{
    public class CifradosController : Controller
    {
        public IActionResult CifradoBasico()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CifradoBasico(string contenido, string resultado, string accion)
        {
            //CIFRAMOS EL CONTENIDO RECIBIDO
            string response = HelperCryptography.EncriptarTextoBasico(contenido);
            if (accion.ToLower() == "cifrar")
            {
                ViewData["TEXTOCIFRADO"] = response;
            } else if (accion.ToLower() == "comparar")
            {
                //COMPARAMOS LA RESPUESTA CIFRADA CON EL VALOR DE LA CAJA RESULTADO
                if(response != resultado)
                {
                    ViewData["MENSAJE"] = "Los valores no coinciden";
                }
                else
                {
                    ViewData["MENSAJE"] = "Los valores son iguales";
                }
            }

            return View();
        }
        public IActionResult CifradoEficiente()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CifradoEficiente(string contenido, string resultado, string accion)
        {   
            if (accion.ToLower() == "cifrar")
            {
                //CIFRAMOS GENERANOD UN NUEVO SALT
                string response = HelperCryptography.EncriptarContenido(contenido, false);
                ViewData["TEXTOCIFRADO"] = response;
            }else if (accion.ToLower() == "comparar")
            {
                string response = HelperCryptography.EncriptarContenido(contenido, true);
                if (response != resultado)
                {
                    ViewData["MENSAJE"] = "<h1 style='color:red'> Los datos NO coinciden </h1>";
                }
                else
                {
                    ViewData["MENSAJE"] = "<h1 style='color:green'> Los datos SI coinciden </h1>";
                }
            }
            return View();
        }
    }
}
