using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RateYourShowMVC.Models;
namespace RateYourShowMVC.Controllers
{
    public class ConfiguracoesUsuarioController : Controller
    {
        private Contexto db = new Contexto();

        // GET: ConfiguracoesUsuario
        public ActionResult InformacoesPessoais()
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

        [HttpPost]
        public ActionResult InformacoesPessoais([Bind(Include = "Nome,Email,Sexo")] Usuario usuario)
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

            if (usuario.Nome.Length < 3 || usuario.Nome.Length > 250)
            {
                ModelState.AddModelError("", "O nome do usuário deve conter entre 3 e 250 caractéres.");
                return View();
            }

            usu.Nome = usuario.Nome;
            usu.Sexo = usuario.Sexo;
            usu.Email = usuario.Email;


            db.Entry(usu).State = EntityState.Modified;
            db.SaveChanges();

            return View();
        }

        public ActionResult InformacoesUso()
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
            return View();
        }

        [HttpPost]
        public ActionResult InformacoesUso([Bind(Include = "SerieNotificacoes,EpiNotificacoes,Spoiler")] Usuario usuario)
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

            usu.SerieNotificacoes = usuario.SerieNotificacoes;
            usu.EpiNotificacoes = usuario.EpiNotificacoes;
            usu.Spoiler = usuario.Spoiler;

            db.Entry(usu).State = EntityState.Modified;
            db.SaveChanges();

            return View();
        }

        public ActionResult AlterarSenha()
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
            return View();
        }

        [HttpPost]
        public ActionResult AlterarSenha(string SenhaAtual, string SenhaNova, string ConfirmarSenha)
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

            if (SenhaAtual != usu.Senha)
            {
                ModelState.AddModelError("", "Senha incorreta.");
                return View();
            }

            if (SenhaNova != ConfirmarSenha)
            {
                ModelState.AddModelError("", "Senhas não conhecidem!");
                return View();
            }

            usu.Senha = SenhaNova;

            if (ModelState.IsValid)
            {
                db.Entry(usu).State = EntityState.Modified;
                db.SaveChanges();
            }

            return View();
        }
    }
}