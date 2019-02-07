using EjemploCifrarDescifrarArchivo.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EjemploCifrarDescifrarArchivo.Controllers
{
    public class CrypticController : Controller
    {
        // GET: CifrarArchivo
        public ActionResult CifrarArchivo()
        {
            return View();
        }

        // POST: CifrarArchivo
        [HttpPost]
        public ActionResult CifrarArchivo(String texto, String publica, String privada, String archivo)
        {
            //Mapea la carpeta especificada.
            String rute = Server.MapPath("~/Files");
            //Juntamos la carpeta mapeada + el nombre del archivo + .txt
            String path = Path.Combine(rute, archivo + ".txt");
            //Guardamos la clave Pública en una sesión para no tener que volver a preguntarla ,
            //esta clave debería ir en una base de datos asociada a un cierto usuario,
            //para que solo la gente que quieras, pueda ver dicho contenido.
            Session["Publica"] = publica;
            //Llamamos a los metodos de Crypt para poder realizar el cifrado del archivo.
            Crypt.encryptToFile(texto, path, Crypt.EncodingPrivateKey(privada), Crypt.EncodingPublicKey(publica));
            //Redireccionamos a la View que permite descifrar el archivo.
            return RedirectToAction("DescifrarArchivo", "Cryptic");
        }
        // GET: DescifrarArchivo
        public ActionResult DescifrarArchivo()
        {
            return View();
        }
        // POST: DescifrarArchivo
        [HttpPost]
        public ActionResult DescifrarArchivo(String privada, String archivo)
        {
            String rute = Server.MapPath("~/Files");
            String path = Path.Combine(rute, archivo + ".txt");
            ViewBag.Text = Crypt.decryptFromFile(
                path,
                Crypt.EncodingPrivateKey(privada),
                Crypt.EncodingPublicKey(Session["Publica"].ToString())
                );
            return View();
        }
    }
}