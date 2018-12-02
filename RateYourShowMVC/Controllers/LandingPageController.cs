using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RateYourShowMVC.Models;
using RateYourShowMVC.ViewsModel;
using RateYourShowMVC.Services;
using System.Web.Security;
using CryptSharp;
using System.Data.Entity;
using System.Text.RegularExpressions;
namespace RateYourShowMVC.Controllers
{
    public class LandingPageController : Controller
    {
        private Contexto db = new Contexto();

        // GET: LandingPage
        public ActionResult Index()
        {
            HttpCookie cookie = Request.Cookies.Get("UsuId");

            if ((cookie == null || cookie.Value == ""))
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Midia = db.Midia.ToList();
            ViewBag.Amizade = db.Amizade.ToList();
            ViewBag.Pessoa = db.Usuario.ToList();

            Usuario usu = db.Usuario.Find(Convert.ToInt32(cookie.Value));
            Midia mid = db.Midia.Where(t => t.UsuarioId == usu.UsuarioId).ToList().FirstOrDefault();

            ViewBag.Imagem = "default.jpg";

            if (mid != null)
            {
                ViewBag.Imagem = mid.Link;
            }

            ViewBag.Comentario = db.Comentario.ToList();
            ViewBag.Series = db.Serie.ToList();
            ViewBag.Amizades = db.Amizade.ToList();
            ViewBag.Usuario = usu;
            return View(db.Publicacao.ToList());
        }

        [HttpPost]
        public ActionResult Index(string texto, string botao, int? PublicacaoId, string Descricao, string Texto, string motivo)
        {
            HttpCookie cookie = Request.Cookies.Get("UsuId");

            ViewBag.Midia = db.Midia.ToList();
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
            ViewBag.Comentario = db.Comentario.ToList();

            if (botao == "Editar")
            {
                Publicacao publ = db.Publicacao.Find(PublicacaoId);
                publ.Descricao = Descricao;
                db.Entry(publ).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Editar"] = "ok";

                return RedirectToAction("Index", "LandingPage");

            }
            else if (botao == "Excluir")
            {
                Publicacao publ = db.Publicacao.Find(PublicacaoId);
                
                foreach(var com in db.Comentario.Where(c => c.PublicacaoId == publ.PublicacaoId))
                {
                    db.Comentario.Remove(com);
                }
                db.SaveChanges();

                db.Publicacao.Remove(publ);
                db.SaveChanges();
                TempData["Excluir"] = "ok";

                return RedirectToAction("Index", "LandingPage");
            }
            else if (botao == "Comentar")
            {
                Publicacao publ = db.Publicacao.Find(PublicacaoId);

                Comentario com = new Comentario();

                com.PublicacaoId = Convert.ToInt32(PublicacaoId);
                com.Texto = Texto;
                com.Inativo = Inativo.Não;
                com.UsuarioId = usu.UsuarioId;
                com.UsuarioId1 = Convert.ToInt32(publ.UsuarioId);
                com.Datadepublicacao = DateTime.Now;

                db.Comentario.Add(com);
                db.SaveChanges();
                TempData["Comentario"] = "ok";

                return RedirectToAction("Index", db.Publicacao.ToList());

            }else if (botao == "Denunciar")
            {
                TempData["MSG"] = Funcoes.EnviarEmail("rys.rateyourshow@gmail.com", "[DENUNCIA] - " + PublicacaoId,motivo);
                TempData["Denuncia"] = "ok";
                return RedirectToAction("Index", db.Publicacao.ToList());
            }

            if (texto.Length > 255 || texto.Length < 3)
            {
                ModelState.AddModelError("", "O texto deve ter entre 3 e 255 caractéres.");
                TempData["PostagemErro"] = "ok";
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
            TempData["Postagem"] = "ok";

            return RedirectToAction("Index", db.Publicacao.ToList());
        }
    }
}