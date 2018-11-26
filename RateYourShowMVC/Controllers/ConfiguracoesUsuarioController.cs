using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
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

            if ((cookie == null || cookie.Value == ""))
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
            ViewBag.Usuario = usu;

            ViewBag.Sexo = new SelectList(Enum.GetValues(typeof(Sexo)), usu.Sexo);

            return View();
        }

        [HttpPost]
        public ActionResult InformacoesPessoais([Bind(Include = "Nome,Email,Sexo")] Usuario usuario)
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

            if ((cookie == null || cookie.Value == ""))
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
            ViewBag.Usuario = usu;
            return View();
        }

        [HttpPost]
        public ActionResult InformacoesUso([Bind(Include = "SerieNotificacoes,EpiNotificacoes,Spoiler")] Usuario usuario)
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
            ViewBag.Usuario = usu;

            usu.SerieNotificacoes = usuario.SerieNotificacoes;
            usu.EpiNotificacoes = usuario.EpiNotificacoes;
            usu.Spoiler = usuario.Spoiler;

            db.Entry(usu).State = EntityState.Modified;
            db.SaveChanges();

            return View();
        }

        public ActionResult AlterarFoto()
        {
            HttpCookie cookie = Request.Cookies.Get("UsuId");

            if ((cookie == null || cookie.Value == ""))
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
            ViewBag.Usuario = usu;
            return View();
        }

        [HttpPost]
        public ActionResult AlterarFoto([Bind(Include = "Id,Nome")] Midia midia, HttpPostedFileBase arq)
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
            ViewBag.Usuario = usu;

            string valor = "";

            if (arq != null)
            {
                Upload.CriarDiretorio();
                string nomearq = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(arq.FileName);
                valor = Upload.UploadArquivo(arq, nomearq);
                if (valor == "sucesso")
                {
                    if (mid == null)
                    {
                        midia.Link = nomearq;
                        midia.UsuarioId = usu.UsuarioId;
                        midia.TipoMidiaId = 5;

                            db.Midia.Add(midia);
                            db.SaveChanges();

                        return RedirectToAction("Index", "LandingPage");

                    }
                    else
                    {
                        midia = mid;
                        midia.Link = nomearq;

                            db.Entry(midia).State = EntityState.Modified;
                            db.SaveChanges();

                        return RedirectToAction("Index", "LandingPage");

                    }
                }
                else
                {
                    ModelState.AddModelError("", valor);
                }
            }
            return View();
        }

        public ActionResult AlterarSenha()
        {
            HttpCookie cookie = Request.Cookies.Get("UsuId");

            if ((cookie == null || cookie.Value == ""))
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
            ViewBag.Usuario = usu;
            return View();
        }

        [HttpPost]
        public ActionResult AlterarSenha(string SenhaAtual, string SenhaNova, string ConfirmarSenha)
        {
            HttpCookie cookie = Request.Cookies.Get("UsuId");

            ViewBag.Amizade = db.Amizade.ToList();
            ViewBag.Pessoa = db.Usuario.ToList();

            Usuario usu = db.Usuario.Find(Convert.ToInt32(cookie.Value));
            Midia mid = db.Midia.Where(t => t.UsuarioId == usu.UsuarioId).ToList().FirstOrDefault();

            string Senha = "^(?=.*[0-9].*[0-9])(?=.*[a-z].*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=])(?=\\S+$).{8,200}$";

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

            if (!Regex.IsMatch(SenhaNova, Senha))
            {
                ModelState.AddModelError("", "A senha do usuário deve conter no minimo 8 caractéres, caractéres especiais, 1 número e 1 letra maiúscula");
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

        public ActionResult DesativarConta()
        {
            HttpCookie cookie = Request.Cookies.Get("UsuId");

            if ((cookie == null || cookie.Value == ""))
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
            ViewBag.Usuario = usu;

            ViewBag.Sexo = new SelectList(Enum.GetValues(typeof(Sexo)), usu.Sexo);

            return View();
        }

        [HttpPost]
        public ActionResult DesativarConta(string email, string senha)
        {
            HttpCookie cookie = Request.Cookies.Get("UsuId");

            if ((cookie == null || cookie.Value == ""))
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
            ViewBag.Usuario = usu;

            ViewBag.Sexo = new SelectList(Enum.GetValues(typeof(Sexo)), usu.Sexo);

            if (usu.Email == email && usu.Senha == senha)
            {
                usu.Inativo = Inativo.Sim;
                db.Entry(usu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Usuário ou senha incorretos!");
            }

            return View();
        }
    }
}