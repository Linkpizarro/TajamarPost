using EjemploTableStorage.Models;
using EjemploTableStorage.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EjemploTableStorage.Controllers
{
    public class VideojuegosController : Controller
    {
        RepositoryVideojuegos repo;
        public VideojuegosController()
        {
            this.repo = new RepositoryVideojuegos();
        }

        public ActionResult Index()
        {
            List<Videojuego> videojuegos = repo.MostrarVideojuegos();
            return View(videojuegos);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Videojuego videojuego)
        {
            repo.CrearVideojuego(videojuego.Estudio, videojuego.IdVideojuego
            ,videojuego.Nombre, videojuego.Creador, videojuego.Descripcion, 
            videojuego.FechaLanzamiento, videojuego.Precio, videojuego.Categorias);
            return RedirectToAction("Index");
        }

        public ActionResult Details(String partition, String row)
        {
            Videojuego videojuego = repo.BuscarVideojuego(partition, row);
            return View(videojuego);
        }

        public ActionResult Edit(String partition, String row)
        {
            Videojuego videojuego = repo.BuscarVideojuego(partition, row);
            return View(videojuego);
        }

        [HttpPost]
        public ActionResult Edit(Videojuego videojuego)
        {
            repo.ModificarVideojuego(videojuego.Estudio, videojuego.IdVideojuego
            , videojuego.Nombre, videojuego.Creador, videojuego.Descripcion,
            videojuego.FechaLanzamiento, videojuego.Precio, videojuego.Categorias);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(String partition, String row)
        {
            repo.EliminarVideojuego(partition, row);
            return RedirectToAction("Index");

        }

    }
}