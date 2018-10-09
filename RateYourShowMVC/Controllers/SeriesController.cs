using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RateYourShowMVC.Models;

namespace RateYourShowMVC.Controllers
{
    public class SeriesController : Controller
    {
        private Contexto db = new Contexto();
        // GET: Series
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DashSeries()
        {
            HttpCookie cookie = Request.Cookies.Get("UsuId");

            Usuario usu = db.Usuario.Find(Convert.ToInt32(cookie.Value));
            ViewBag.Usuario = usu;

            Midia mid = db.Midia.Where(t => t.UsuarioId == usu.UsuarioId).ToList().FirstOrDefault();

            ViewBag.Imagem = "default.jpg";

            if (mid != null)
            {
                ViewBag.Imagem = mid.Link;
            }

            var serie = db.Serie;
            return View(serie.ToList());
        }
        public ActionResult Perfil(int? id)
        {
            HttpCookie cookie = Request.Cookies.Get("UsuId");

            Usuario usu = db.Usuario.Find(Convert.ToInt32(cookie.Value));
            ViewBag.Usuario = usu;

            Midia mid = db.Midia.Where(t => t.UsuarioId == usu.UsuarioId).ToList().FirstOrDefault();

            ViewBag.Imagem = "default.jpg";

            if (mid != null)
            {
                ViewBag.Imagem = mid.Link;
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Series series = db.Serie.Find(id);
            if (series == null)
            {
                return HttpNotFound();
            }
            return View(series);
        }

        public ActionResult Ranking()
        {
            HttpCookie cookie = Request.Cookies.Get("UsuId");

            Usuario usu = db.Usuario.Find(Convert.ToInt32(cookie.Value));
            ViewBag.Usuario = usu;

            Midia mid = db.Midia.Where(t => t.UsuarioId == usu.UsuarioId).ToList().FirstOrDefault();

            ViewBag.Imagem = "default.jpg";

            if (mid != null)
            {
                ViewBag.Imagem = mid.Link;
            }

            return View();
        }
        public ActionResult Personagem(int? id)
        {
            HttpCookie cookie = Request.Cookies.Get("UsuId");

            Usuario usu = db.Usuario.Find(Convert.ToInt32(cookie.Value));
            ViewBag.Usuario = usu;

            Midia mid = db.Midia.Where(t => t.UsuarioId == usu.UsuarioId).ToList().FirstOrDefault();

            ViewBag.Imagem = "default.jpg";

            if (mid != null)
            {
                ViewBag.Imagem = mid.Link;
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipe equipe = db.Equipe.Find(id);
            if (equipe == null)
            {
                return HttpNotFound();
            }

            return View(equipe);
        }
    }
}