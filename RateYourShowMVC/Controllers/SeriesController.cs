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

        public ActionResult DashSeries()
        {
            HttpCookie cookie = Request.Cookies.Get("UsuId");

            if (cookie.Value == "")
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Amizade = db.Amizade.ToList();
            ViewBag.Pessoa = db.Usuario.ToList();

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

        public ActionResult PerfilAmigo(int? id)
        {
            HttpCookie cookie = Request.Cookies.Get("UsuId");

            if (cookie.Value == "")
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Publicacao = db.Publicacao.ToList();


            ViewBag.Amizade = db.Amizade.ToList();
            ViewBag.Pessoa = db.Usuario.ToList();

            Usuario usu = db.Usuario.Find(Convert.ToInt32(cookie.Value));
            ViewBag.Usuario = usu;


            ViewBag.Convite = 0;

            Amizade ami = db.Amizade.Find(usu.UsuarioId, id);
            Amizade ami2 = db.Amizade.Find(id, usu.UsuarioId);

            if(ami != null)
            {
                if(ami.Status == Status.Aceito)
                {
                    ViewBag.Convite = 2;
                }
                if (ami.Status == Status.Pendente)
                {
                    ViewBag.Convite = 1;
                }
            }

            if(ami2 != null)
            {
                if(ami2.Status == Status.Aceito)
                {
                    ViewBag.Convite = 2;
                }
                if(ami2.Status == Status.Pendente)
                {
                    ViewBag.Convite = 3;
                }
            }

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

        [HttpPost]
        public ActionResult PerfilAmigo(int? id, string Convite)
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


            if(Convite == "1")
            {
                Amizade ami = new Amizade
                {
                    Datadeconvite = DateTime.Now,
                    Status = Status.Pendente,
                    UsuarioId1 = usu.UsuarioId,
                    UsuarioId2 = Convert.ToInt32(id),
                    Dataresposta = null
                };

                ViewBag.Convite = 1;

                db.Amizade.Add(ami);
                db.SaveChanges();
                return View(usuario);

            }
            if (Convite == "2")
            {
                Amizade ami = db.Amizade.Find(usu.UsuarioId, id);

                db.Amizade.Remove(ami);
                db.SaveChanges();
                ViewBag.Convite = 0;
                return View(usuario);
            }
            if (Convite == "3")
            {
                Amizade ami = db.Amizade.Find(usu.UsuarioId, id);
                Amizade ami2 = db.Amizade.Find(id, usu.UsuarioId);
                if(ami != null) { 
                db.Amizade.Remove(ami);
                db.SaveChanges();
                }
                else
                {
                    db.Amizade.Remove(ami2);
                    db.SaveChanges();
                }
                ViewBag.Convite = 0;

                return View(usuario);
            }
            if (Convite == "4")
            {
                Amizade ami = db.Amizade.Find(id,usu.UsuarioId);

                ami.Status = Status.Aceito;
                ami.Dataresposta = DateTime.Now;

                db.Entry(ami).State = EntityState.Modified;
                db.SaveChanges();

                ViewBag.Convite = 2;
                return View(usuario);
            }

            if (Convite == "5")
            {
                Amizade ami = db.Amizade.Find(id, usu.UsuarioId);

                db.Amizade.Remove(ami);
                db.SaveChanges();
                ViewBag.Convite = 0;
                return View(usuario);
            }

            return View(usuario);

        }

        [HttpPost]
        public ActionResult DashSeries(string procurar)
        {
            HttpCookie cookie = Request.Cookies.Get("UsuId");

            ViewBag.Amizade = db.Amizade.ToList();
            ViewBag.Pessoa = db.Usuario.ToList();

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

            if (cookie.Value == "")
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Amizade = db.Amizade.ToList();
            ViewBag.Pessoa = db.Usuario.ToList();

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

            ViewBag.Amizade = db.Amizade.ToList();
            ViewBag.Pessoa = db.Usuario.ToList();

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

            if (cookie.Value == "")
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Amizade = db.Amizade.ToList();
            ViewBag.Pessoa = db.Usuario.ToList();

            Usuario usu = db.Usuario.Find(Convert.ToInt32(cookie.Value));
            ViewBag.Usuario = usu;

            Midia mid = db.Midia.Where(t => t.UsuarioId == usu.UsuarioId).ToList().FirstOrDefault();

            ViewBag.Imagem = "default.jpg";

            if (mid != null)
            {
                ViewBag.Imagem = mid.Link;
            }

            var pessoas = db.Usuario.Where(u => u.Bloqueado != Bloqueado.Sim);
            return View(pessoas.ToList());
        }

        [HttpPost]
        public ActionResult DashPessoas(string procurar)
        {
            HttpCookie cookie = Request.Cookies.Get("UsuId");

            ViewBag.Amizade = db.Amizade.ToList();
            ViewBag.Pessoa = db.Usuario.ToList();

            Usuario usu = db.Usuario.Find(Convert.ToInt32(cookie.Value));
            ViewBag.Usuario = usu;

            Midia mid = db.Midia.Where(t => t.UsuarioId == usu.UsuarioId).ToList().FirstOrDefault();

            ViewBag.Imagem = "default.jpg";

            if (mid != null)
            {
                ViewBag.Imagem = mid.Link;
            }

            var pessoas = db.Usuario.Where(u => u.Nome.Contains(procurar) && u.Bloqueado != Bloqueado.Sim);
            return View(pessoas.ToList());
        }

        public ActionResult Perfil(int? id)
        {
            HttpCookie cookie = Request.Cookies.Get("UsuId");

            if (cookie.Value == "")
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Amizade = db.Amizade.ToList();
            ViewBag.Pessoa = db.Usuario.ToList();

            Usuario usu = db.Usuario.Find(Convert.ToInt32(cookie.Value));
            ViewBag.Usuario = usu;
            Usuarioserie uss = db.UsuarioSerie.Find(usu.UsuarioId, id);

            ViewBag.Seguir = 1;


            if (uss != null)
            {
                if (uss.Seguindo == Seguindo.Sim)
                {
                    ViewBag.Seguir = 0;
                }
            }

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

        [HttpPost]
        public ActionResult Perfil(int? id, string seguir)
        {
            HttpCookie cookie = Request.Cookies.Get("UsuId");

            ViewBag.Amizade = db.Amizade.ToList();
            ViewBag.Pessoa = db.Usuario.ToList();

            Usuario usu = db.Usuario.Find(Convert.ToInt32(cookie.Value));
            ViewBag.Usuario = usu;
            Usuarioserie uss = new Usuarioserie();

            uss = db.UsuarioSerie.Where(us => us.UsuarioId == usu.UsuarioId && us.SeriesId == id).FirstOrDefault();

            ViewBag.Seguir = 1;



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

            if (uss == null)
            {
                Usuarioserie use = new Usuarioserie
                {
                    UsuarioId = usu.UsuarioId,
                    SeriesId = Convert.ToInt32(id),
                    Spoiler = false,
                    Avaliacao = 0,
                    Datedeinicio = DateTime.Now,
                    Seguindo = Seguindo.Sim
                };

                db.UsuarioSerie.Add(use);
                db.SaveChanges();
            }
            else
            {
                db.UsuarioSerie.Remove(uss);
                db.SaveChanges();
            }

            if (uss != null)
            {
                if (uss.Seguindo == Seguindo.Sim)
                {
                    ViewBag.Seguir = 0;
                }
            }


            return RedirectToAction("Perfil", "Series");

        }

        public ActionResult Ranking()
        {
            HttpCookie cookie = Request.Cookies.Get("UsuId");

            if (cookie.Value == "")
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Amizade = db.Amizade.ToList();
            ViewBag.Pessoa = db.Usuario.ToList();

            Usuario usu = db.Usuario.Find(Convert.ToInt32(cookie.Value));
            ViewBag.Usuario = usu;

            Midia mid = db.Midia.Where(t => t.UsuarioId == usu.UsuarioId).ToList().FirstOrDefault();

            ViewBag.Imagem = "default.jpg";

            if (mid != null)
            {
                ViewBag.Imagem = mid.Link;
            }

            var rank = db.UsuarioSerie.GroupBy(g => g.Series.SeriesId).Select(s => new
            {
                Codigo = s.Key,
                Nome = s.Select(a => a.Series.Nome).FirstOrDefault(),
                Soma = s.Sum(a => a.Avaliacao)
            }).OrderByDescending(o => o.Soma).ToList();


            return View(db.UsuarioSerie.ToList());

        }
        public ActionResult Personagem(int? id)
        {
            HttpCookie cookie = Request.Cookies.Get("UsuId");

            if (cookie.Value == "")
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Amizade = db.Amizade.ToList();
            ViewBag.Pessoa = db.Usuario.ToList();

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