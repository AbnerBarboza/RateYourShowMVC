using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RateYourShowMVC.Models;
using RateYourShowMVC.ViewsModel;

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

        [HttpPost]
        public ActionResult DashSeries(string procurar)
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

            var serie = db.Serie.Where(s => s.Nome.Contains(procurar));
            return View(serie.ToList());
        }

        public ActionResult DashAtores()
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

            var equipe = db.Equipe;
            return View(equipe.ToList());
        }

        [HttpPost]
        public ActionResult DashAtores(string procurar)
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

            var equipe = db.Equipe.Where(e => e.Nome.Contains(procurar));
            return View(equipe.ToList());
        }

        public ActionResult DashPessoas()
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

            var pessoas = db.Usuario;
            return View(pessoas.ToList());
        }

        [HttpPost]
        public ActionResult DashPessoas(string procurar)
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

            var pessoas = db.Usuario.Where(u => u.Nome.Contains(procurar));
            return View(pessoas.ToList());
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

            var rank = db.UsuarioSerie.GroupBy(g => g.Series.SeriesId).Select(s => new {
                Codigo = s.Key,
                Nome = s.Select(a => a.Series.Nome).FirstOrDefault(),
                Soma = s.Sum(a => a.Avaliacao) }).OrderByDescending(o => o.Soma).ToList();


            return View(db.UsuarioSerie.ToList());

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