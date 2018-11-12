using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RateYourShowMVC.Models;
namespace RateYourShowMVC.Controllers
{
    public class LandingPageController : Controller
    {
        private Contexto db = new Contexto();

        // GET: LandingPage
        public ActionResult Index()
        {
            HttpCookie cookie = Request.Cookies.Get("UsuId");

            if (cookie.Value == "")
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Amizade = db.Amizade.ToList();
            ViewBag.Pessoa = db.Usuario.ToList();

            Usuario usu = db.Usuario.Find(Convert.ToInt32(cookie.Value));
            Midia mid = db.Midia.Where(t => t.UsuarioId == usu.UsuarioId).ToList().FirstOrDefault();

            ViewBag.Imagem = "default.jpg";

            if (mid != null)
            {
                ViewBag.Imagem = mid.Link;
            }


            ViewBag.Series = db.Serie.ToList();
            ViewBag.Amizades = db.Amizade.ToList();
            ViewBag.Usuario = usu;
            return View(db.Publicacao.ToList());
        }

        [HttpPost]
        public ActionResult Index(string texto)
        {
            HttpCookie cookie = Request.Cookies.Get("UsuId");

            ViewBag.Amizade = db.Amizade.ToList();
            ViewBag.Pessoa = db.Usuario.ToList();

            Usuario usu = db.Usuario.Find(Convert.ToInt32(cookie.Value));
            Midia mid = db.Midia.Where(t => t.UsuarioId == usu.UsuarioId).ToList().FirstOrDefault();

            ViewBag.Imagem = "default.jpg";

            if (mid != null)
            {
                ViewBag.Imagem = mid.Link;
            }




            ViewBag.Series = db.Serie.ToList();
            ViewBag.Amizades = db.Amizade.ToList();
            ViewBag.Usuario = usu;
            if (texto.Length > 255 || texto.Length < 3)
            {
                ModelState.AddModelError("", "O texto deve ter entre 3 e 255 caractéres.");
                return View(db.Publicacao.ToList());
            }

            Publicacao pub = new Publicacao
            {
                Descricao = texto,
                Data = DateTime.Now,
                UsuarioId = usu.UsuarioId,
                Inativo = Inativo.Não
            };

            if (ModelState.IsValid)
            {
                db.Publicacao.Add(pub);
                db.SaveChanges();
            }
            return RedirectToAction("Index", db.Publicacao.ToList());
        }
    }
}