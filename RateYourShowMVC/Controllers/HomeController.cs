﻿using System;
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

namespace RateYourShowMVC.Controllers
{
    public class HomeController : Controller
    {
        private Contexto db = new Contexto();
        UsuarioServices usuarioServices = new UsuarioServices();

        public ActionResult Index()
        {
            return View();
        }

       
        public ActionResult Cadastro()
        {
            ViewBag.Cadastro = "";
            ViewBag.Login = "active";

            ViewBag.Sexo = new SelectList(Enum.GetValues(typeof(Sexo)));
            return View();
        }

        public ActionResult ReportarErro()
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

        [HttpPost]
        public ActionResult ReportarErro(string erro)
        {
            string batata = erro;
            HttpCookie cookie = Request.Cookies.Get("UsuId");

            Usuario usu = db.Usuario.Find(Convert.ToInt32(cookie.Value));
            ViewBag.Usuario = usu;

            Midia mid = db.Midia.Where(t => t.UsuarioId == usu.UsuarioId).ToList().FirstOrDefault();

            ViewBag.Imagem = "default.jpg";

            if (mid != null)
            {
                ViewBag.Imagem = mid.Link;
            }
            
            return RedirectToAction("Index","LandingPage");
        }

        [HttpPost]
        public ActionResult Cadastro([Bind(Include = "Nome,Email,Senha,DatadeNascimento,Sexo,ConfirmarSenha,TermosdeUso")] CadastroUsuario usuario, string button, string email, string senha)
        {
            ViewBag.Sexo = new SelectList(Enum.GetValues(typeof(Sexo)));

            if (button == "cadastrar")
            {
                ViewBag.Cadastro = "active";
                ViewBag.Login = "";

                ViewBag.Active = false;

                if (!usuario.TermosdeUso)
                {
                    ModelState.AddModelError("", "É Necessário aceitar os Termos de Uso.");
                    return View(usuario);
                }

                if (usuario.Nome.Length < 3 || usuario.Nome.Length > 250)
                {
                    ModelState.AddModelError("", "O nome do usuário deve conter entre 3 e 250 caractéres.");
                    return View(usuario);
                }
                if (usuario.Senha.Length < 6 || usuario.Senha.Length > 300)
                {
                    ModelState.AddModelError("", "A senha do usuário deve conter entre 6 e 300 caractéres.");
                    return View(usuario);
                }
                if (usuario.Senha != usuario.ConfirmarSenha)
                {
                    ModelState.AddModelError("", "Senhas não conhecidem.");
                    return View(usuario);
                }
                Usuario usu = new Usuario
                {
                    DatadeNascimento = usuario.DatadeNascimento,
                    Nome = usuario.Nome,
                    Senha = usuario.Senha,
                    TipoUsuario = TipoUsuario.Comum,
                    Bloqueado = Bloqueado.Não,
                    Inativo = Inativo.Não,
                    Sexo = usuario.Sexo,
                    Email = usuario.Email,
                    SerieNotificacoes = true,
                    EpiNotificacoes = true,
                    Spoiler = false
                };

                if (!usuarioServices.EmailWhere(usuario.Email))
                {
                    if (ModelState.IsValid)
                    {
                        db.Usuario.Add(usu);
                        db.SaveChanges();
                        TempData["MSG"] = Funcoes.EnviarEmail(usuario.Email, "Cadastro Rate Your Show", "<div style='background:rgb(245,245,245); width:760px; padding-bottom:30px'>" +
                        "			<h1 style='margin:0px; padding:16px 30px'>" +
                        "			<a href='#'><img src='https://imagizer.imageshack.com/v2/34x34q90/921/luCC0y.png' alt=''></a>" +
                        "			</h1>" +
                        "			<div style='padding:0px 30px'>" +
                        "				<img src='https://imagizer.imageshack.com/v2/700x280q90/921/0vhXWJ.jpg' alt='' style='vertical-align:top' width='700' height='280'>" +
                        "			</div>" +
                        "			<div style='background:rgb(255,255,255); padding:48px 30px 47px; margin:0px 30px 0px; width:640px;'>" +
                        "				<div style='color: rgb(150, 150, 150); line-height: 16px; padding-bottom: 17px; font-family: verdana, serif, 'EmojiFont'; font-size: 14px;'>	" +
                        "						<br />							" +
                        "						 Bem-vindo ao Rate Your Show, <span style='text-decoration: none;  color: rgb(255, 94, 58);'>" + usu.Nome + "!</span> " +
                        "						<p style ='color: rgb(150, 150, 150); margin-top: 15px; line-height: 20px; font-family: verdana, serif, 'EmojiFont'; font-size: 13px;''>Estamos felizes porque você agora faz parte da nossa comunidade que cresce cada vez mais!" +
                        "						Com o Rate Your Show, você pode manter um controle das suas séries e compartilha-las com outros membros da comunidade.</p>" +
                        "				</div>			" +
                        "				<div style='color: rgb(150, 150, 150); padding-top: 27px; font-family: verdana, serif, 'EmojiFont'; font-size: 13px;'>" +
                        "					Com amor, <a href='' style='text-decoration: none;  color: rgb(255, 94, 58); text-transform: uppercase; '>Rate Your Show</a>." +
                        "				</div>" +
                        "			</div>" +
                        "		</div>");
                        return RedirectToAction("Index");
                    }

                    return View();
                }
                else
                {
                    ModelState.AddModelError("", "O email informado já está cadastrado em nossa base.");
                    return View();
                }
            }
            else
            {
                ViewBag.Cadastro = "";
                ViewBag.Login = "active";
                ViewBag.Active = false;
                Usuario usu = db.Usuario.Where(t => t.Email == email).ToList().FirstOrDefault();
                if (usu != null)
                {
                    if (usu.Senha != senha)
                    {
                        ModelState.AddModelError("", "Senha Incorreta!");
                        return View();
                    }
                    string permissoes = "";
                    permissoes += usu.TipoUsuario;

                    permissoes = permissoes.Substring(0, permissoes.Length - 1);
                    FormsAuthentication.SetAuthCookie(usu.Nome, false);
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, usu.Email, DateTime.Now, DateTime.Now.AddMinutes(30), false, permissoes);
                    string hash = FormsAuthentication.Encrypt(ticket);
                    HttpCookie cookie = new HttpCookie("UsuId", usu.UsuarioId.ToString());
                    if (ticket.IsPersistent)
                        cookie.Expires = ticket.Expiration;
                    Response.Cookies.Add(cookie);
                    return RedirectToAction("Index", "LandingPage");
                }
                else
                {
                    ModelState.AddModelError("", "E-mail Não Cadastrado");
                    return View();
                }
            }
        }

        public ActionResult RecuperarSenha()
        {
            ViewBag.Email = "active";
            ViewBag.Trocar = "";
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult RecuperarSenha(string email, string button, string codigo, string senha, string confirmarsenha)
        {

            if (button == "enviar")
            {
                int codigoUsu;
                codigoUsu = usuarioServices.GeraCodigo();
                Usuario usu = db.Usuario.Where(t => t.Email == email).ToList().FirstOrDefault();
                if (usu == null)
                {
                    ModelState.AddModelError("", "O email informado não está cadastrado em nossa base.");
                    ViewBag.Email = "active";
                    ViewBag.Trocar = "";
                    return View();
                }
                else
                {
                    RecuperarSenha rec = new RecuperarSenha
                    {
                        SenhaId = 0,
                        UsuarioId = usu.UsuarioId,
                        Vencimento = DateTime.Now.AddDays(1),
                        Valido = true,
                        Codigo = codigoUsu
                    };

                    if (ModelState.IsValid)
                    {
                        db.RecuperarSenha.Add(rec);
                        db.SaveChanges();
                        TempData["MSG"] = Funcoes.EnviarEmail(usu.Email, "RYS - Recuperar Senha", "<div style= 'background:rgb(245,245,245); width:760px; padding-bottom:30px '>" +
                    "			<h1 style= 'margin:0px; padding:16px 30px '>" +
                    "           <a href='# '><img src='https://imagizer.imageshack.com/v2/34x34q90/921/luCC0y.png' alt=''></a>" +
                    "			</h1>" +
                    "			<div style= 'padding:0px 30px '>" +
                    "				<img src='https://imagizer.imageshack.com/v2/700x280q90/923/T6Lqa5.jpg' alt= '' style= 'vertical-align:top 'width= '700' height= '280'>" +
                    "			</div>" +
                    "			<div style= 'background:rgb(255,255,255); padding:48px 30px 47px; margin:0px 30px 0px; width:640px; '>" +
                    "				<div style='color: rgb(0, 0, 0); line-height: 16px; padding-bottom: 17px; font-family: verdana, serif,  'EmojiFont '; font-size: 14px;'>	" +
                    "						<br />							" +
                    "						Aqui vai o código pra você mudar sua senha:" +
                    "				</div>" +
                    "				<div style='padding: 19px 20px;  border: 1px solid rgb(234, 234, 234); color: rgb(150, 150, 150); line-height: 20px; font-family: verdana, serif,  'EmojiFont '; font-size: 13px;'> " + codigoUsu.ToString() +
                    "				</div>" +
                    "				<div style='color: rgb(0, 0, 0); line-height: 16px; padding-top: 17px; font-family: verdana, serif,  'EmojiFont '; font-size: 14px;'>	" +
                    "						Agora é só entrar lá no rate your show e digitar esse código. Depois você cria a nova senha e tá feito" +
                    "				</div>" +
                    "				<div style='color: rgb(150, 150, 150); padding-top: 27px; font-family: verdana, serif,  'EmojiFont '; font-size: 13px;'>" +
                    "					Esse e-mail foi enviado por <a href= ' ' style= 'text-decoration: none;  color: rgb(255, 94, 58);   '>RATE YOUR SHOW</a>." +
                    "				</div>" +
                    "			</div>" +
                    "		</div>");
                        TempData["MSG"] = "success|E-mail enviado com sucesso";
                    }

                    ViewBag.Email = "";
                    ViewBag.Trocar = "active";

                    return View();
                }
            }
            else
            {
                ViewBag.Emai = "";
                ViewBag.Trocar = "active";
                int codigoUsu = Convert.ToInt32(codigo);
                RecuperarSenha rec = db.RecuperarSenha.Where(t => t.Codigo == codigoUsu).ToList().FirstOrDefault();
                if (rec == null)
                {
                    ModelState.AddModelError("", "Código inválido.");
                    return View();
                }
                else
                {
                    Usuario usu = db.Usuario.Find(rec.UsuarioId);

                    if (senha.Length < 6 || senha.Length > 300)
                    {
                        ModelState.AddModelError("", "A nova senha do usuário deve conter entre 6 e 300 caractéres.");
                        return View();
                    }

                    if (confirmarsenha != senha)
                    {
                        ModelState.AddModelError("", "Senhas não conhecidem.");
                        return View();
                    }

                    usu.Senha = senha;

                    if (rec.Vencimento < DateTime.Now || rec.Valido == false)
                    {
                        ModelState.AddModelError("", "Código expirado.");
                        return View();
                    }

                    rec.Valido = false;

                    if (ModelState.IsValid)
                    {
                        db.Entry(rec).State = EntityState.Modified;
                        db.SaveChanges();
                        db.Entry(usu).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Cadastro", "Home");
                    }
                    return View();

                }
            }
        }
    }
}