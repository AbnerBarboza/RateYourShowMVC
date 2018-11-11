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
using System.Text.RegularExpressions;
using System.Net;

namespace RateYourShowMVC.Controllers
{
    public class AdministradorController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Administrador
        public ActionResult CadastroPersonalidade()
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

            return View();
        }

        public ActionResult Series()
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

            return View(db.Serie.ToList());
        }

        [HttpPost]
        public ActionResult Series(string procurar)
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

            var serie = db.Serie.Where(s => s.Nome.Contains(procurar));
            return View(serie.ToList());
        }

        public ActionResult Personalidades()
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

            return View(db.Equipe.ToList());
        }

        [HttpPost]
        public ActionResult Personalidades(string procurar)
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

            var equipe = db.Equipe.Where(e => e.Nome.Contains(procurar));
            return View(equipe.ToList());
        }

        public ActionResult Usuarios()
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

            return View(db.Usuario.ToList());
        }

        [HttpPost]
        public ActionResult Usuarios(string procurar)
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

            var pessoas = db.Usuario.Where(u => u.Nome.Contains(procurar));
            return View(pessoas.ToList());
        }

        public ActionResult EditarUsuario(int? id)
        {
            HttpCookie cookie = Request.Cookies.Get("UsuId");

            ViewBag.Publicacao = db.Publicacao.ToList();


            ViewBag.Amizade = db.Amizade.ToList();
            ViewBag.Pessoa = db.Usuario.ToList();

            Usuario usu = db.Usuario.Find(Convert.ToInt32(cookie.Value));
            ViewBag.Usuario = usu;


            ViewBag.Convite = 0;


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
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }

            ViewBag.UsuSeries = db.UsuarioSerie.Where(
                d => d.UsuarioId == usuario.UsuarioId).ToList().Select(
                s => s.SeriesId
                ).ToList();

            ViewBag.Series = db.Serie.ToList();



            return View(usuario);

        }

        public ActionResult EditarPersonalidade()
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

            return View();
        }

        public ActionResult EditarSerie()
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

            return View();
        }

        public ActionResult CadastroSerie()
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

            return View();
        }

        [HttpPost]
        public ActionResult CadastroSerie([Bind(Include = "Nome,Paisdeorigem,Descricao")] Series series)
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

            if (series.Nome.Length < 3 || series.Nome.Length > 200)
            {
                ModelState.AddModelError("", "O nome deve ter entre 3 e 200 caractéres.");
                return View(series);
            }

            if (series.Paisdeorigem.Length < 3 || series.Paisdeorigem.Length > 200)
            {
                ModelState.AddModelError("", "O país de origem deve ter entre 3 e 200 caractéres.");
                return View(series);
            }

            if (series.Descricao.Length < 10 || series.Descricao.Length > 1000)
            {
                ModelState.AddModelError("", "A descrição deve ter entre 10 e 1000 caractéres.");
                return View(series);
            }

            Series ser = new Series
            {
                Nome = series.Nome,
                Paisdeorigem = series.Paisdeorigem,
                Descricao = series.Descricao,
                QuizzesId = null

            };

            if (ModelState.IsValid)
            {
                db.Serie.Add(ser);
                db.SaveChanges();
                return RedirectToAction("CadastroSerie2");
            }

            return View();


        }

        public ActionResult CadastroSerie2()
        {
            HttpCookie cookie = Request.Cookies.Get("UsuId");

            ViewBag.Amizade = db.Amizade.ToList();
            ViewBag.Pessoa = db.Usuario.ToList();
            ViewBag.Genero = db.Genero.ToList();

            IEnumerable<RateYourShowMVC.Models.Genero> generos = db.Genero.ToList();


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
        public ActionResult CadastroSerie2([Bind(Include = "GeneroId")] IEnumerable<Genero> generos)
        {
            HttpCookie cookie = Request.Cookies.Get("UsuId");

            ViewBag.Amizade = db.Amizade.ToList();
            ViewBag.Pessoa = db.Usuario.ToList();

            int batata = generos.Count();

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
    }
}