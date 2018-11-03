using RateYourShowMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RateYourShowMVC.Controllers
{
    public class AdministradorController : Controller
    {
        private Contexto db = new Contexto();
        
        // GET: Administrador
        public ActionResult CadastroPersonalidade()
        {
            HttpCookie cookie = Request.Cookies.Get("UsuId");

            Usuario usu = db.Usuario.Find(Convert.ToInt32(cookie.Value));
            Midia mid = db.Midia.Where(t => t.UsuarioId == usu.UsuarioId).ToList().FirstOrDefault();

            ViewBag.Imagem = "default.jpg";

            if (mid != null)
            {
                ViewBag.Imagem = mid.Link;
            }
            ViewBag.Usuario = usu;

            ViewBag.Sexo = new SelectList(Enum.GetValues(typeof(Sexo)), usu.Sexo);

            return View();
        }

        public ActionResult Series()
        {
            HttpCookie cookie = Request.Cookies.Get("UsuId");

            Usuario usu = db.Usuario.Find(Convert.ToInt32(cookie.Value));
            Midia mid = db.Midia.Where(t => t.UsuarioId == usu.UsuarioId).ToList().FirstOrDefault();

            ViewBag.Imagem = "default.jpg";

            if (mid != null)
            {
                ViewBag.Imagem = mid.Link;
            }
            ViewBag.Usuario = usu;

            ViewBag.Sexo = new SelectList(Enum.GetValues(typeof(Sexo)), usu.Sexo);

            return View();
        }
        public ActionResult Personalidades()
        {
            HttpCookie cookie = Request.Cookies.Get("UsuId");

            Usuario usu = db.Usuario.Find(Convert.ToInt32(cookie.Value));
            Midia mid = db.Midia.Where(t => t.UsuarioId == usu.UsuarioId).ToList().FirstOrDefault();

            ViewBag.Imagem = "default.jpg";

            if (mid != null)
            {
                ViewBag.Imagem = mid.Link;
            }
            ViewBag.Usuario = usu;

            ViewBag.Sexo = new SelectList(Enum.GetValues(typeof(Sexo)), usu.Sexo);

            return View();
        }

        public ActionResult Usuarios()
        {
            HttpCookie cookie = Request.Cookies.Get("UsuId");

            Usuario usu = db.Usuario.Find(Convert.ToInt32(cookie.Value));
            Midia mid = db.Midia.Where(t => t.UsuarioId == usu.UsuarioId).ToList().FirstOrDefault();

            ViewBag.Imagem = "default.jpg";

            if (mid != null)
            {
                ViewBag.Imagem = mid.Link;
            }
            ViewBag.Usuario = usu;

            ViewBag.Sexo = new SelectList(Enum.GetValues(typeof(Sexo)), usu.Sexo);

            return View();
        }

        public ActionResult EditarPersonalidade()
        {
            HttpCookie cookie = Request.Cookies.Get("UsuId");

            Usuario usu = db.Usuario.Find(Convert.ToInt32(cookie.Value));
            Midia mid = db.Midia.Where(t => t.UsuarioId == usu.UsuarioId).ToList().FirstOrDefault();

            ViewBag.Imagem = "default.jpg";

            if (mid != null)
            {
                ViewBag.Imagem = mid.Link;
            }
            ViewBag.Usuario = usu;

            ViewBag.Sexo = new SelectList(Enum.GetValues(typeof(Sexo)), usu.Sexo);

            return View();
        }

        public ActionResult EditarSerie()
        {
            HttpCookie cookie = Request.Cookies.Get("UsuId");

            Usuario usu = db.Usuario.Find(Convert.ToInt32(cookie.Value));
            Midia mid = db.Midia.Where(t => t.UsuarioId == usu.UsuarioId).ToList().FirstOrDefault();

            ViewBag.Imagem = "default.jpg";

            if (mid != null)
            {
                ViewBag.Imagem = mid.Link;
            }
            ViewBag.Usuario = usu;

            ViewBag.Sexo = new SelectList(Enum.GetValues(typeof(Sexo)), usu.Sexo);

            return View();
        }

        public ActionResult CadastroSerie()
        {
            HttpCookie cookie = Request.Cookies.Get("UsuId");

            Usuario usu = db.Usuario.Find(Convert.ToInt32(cookie.Value));
            Midia mid = db.Midia.Where(t => t.UsuarioId == usu.UsuarioId).ToList().FirstOrDefault();

            ViewBag.Imagem = "default.jpg";

            if (mid != null)
            {
                ViewBag.Imagem = mid.Link;
            }
            ViewBag.Usuario = usu;

            ViewBag.Sexo = new SelectList(Enum.GetValues(typeof(Sexo)), usu.Sexo);

            return View();
        }



    }
}