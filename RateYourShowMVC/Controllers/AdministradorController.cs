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
using System.Net;
using System.IO;

namespace RateYourShowMVC.Controllers
{
    public class AdministradorController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Administrador
        public ActionResult CadastroPersonalidade()
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

            if (usu.TipoUsuario != TipoUsuario.Administrado)
            {
                return RedirectToAction("Index", "LandingPage");
            }

            ViewBag.Sexo = new SelectList(Enum.GetValues(typeof(Sexo)), usu.Sexo);

            return View();
        }
        [HttpPost]
        public ActionResult CadastroPersonalidade([Bind(Include = "Nome,Datadenascimento,Nacionalidade")] Equipe equipe, HttpPostedFileBase arq)
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

            if (equipe.Nome.Length < 3 || equipe.Nome.Length > 200)
            {
                ModelState.AddModelError("", "Nome deve ter entre 3 e 200 caractéres.");
                return View();
            }

            if (equipe.Nacionalidade.Length < 3 || equipe.Nacionalidade.Length > 200)
            {
                ModelState.AddModelError("", "Nacionalidade deve ter entre 3 e 200 caractéres.");
                return View();
            }

            if (arq == null)
            {
                ModelState.AddModelError("", "Imagem não pode ser nula.");
                return View();
            }

            string valor = "";

            if (arq != null)
            {
                Upload.CriarDiretorio();
                string nomearq = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(arq.FileName);
                valor = Upload.UploadArquivo(arq, nomearq);
                if (valor == "sucesso")
                {
                    equipe.Inativo = Inativo.Sim;
                    db.Equipe.Add(equipe);
                    db.SaveChanges();

                    Midia midia = new Midia
                    {
                        Link = nomearq,
                        EquipeId = equipe.EquipeId,
                        TipoMidiaId = 2
                    };

                    db.Midia.Add(midia);
                    db.SaveChanges();

                    return RedirectToAction("Personalidades", "Administrador");

                }
            }
            else
            {
                ModelState.AddModelError("", valor);
            }
            return View();
        }

        public ActionResult Series(int? id)
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

            if (usu.TipoUsuario != TipoUsuario.Administrado)
            {
                return RedirectToAction("Index", "LandingPage");
            }

            if (id != null)
            {
                Series ser = db.Serie.Find(id);

                if (ser.Inativo == Inativo.Sim)
                {
                    ser.Inativo = Inativo.Não;

                }
                else
                {
                    ser.Inativo = Inativo.Sim;
                }

                db.Entry(ser).State = EntityState.Modified;
                db.SaveChanges();
                return View(db.Serie.ToList());
            }

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

        public ActionResult Personalidades(int? id)
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

            if (usu.TipoUsuario != TipoUsuario.Administrado)
            {
                return RedirectToAction("Index", "LandingPage");
            }

            if (id != null)
            {
                Equipe equ = db.Equipe.Find(id);

                if (equ.Inativo == Inativo.Sim)
                {
                    equ.Inativo = Inativo.Não;

                }
                else
                {
                    equ.Inativo = Inativo.Sim;
                }

                db.Entry(equ).State = EntityState.Modified;
                db.SaveChanges();
                return View(db.Equipe.ToList());
            }

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

            if (cookie == null || (cookie == null || cookie.Value == ""))
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

            if (usu.TipoUsuario != TipoUsuario.Administrado)
            {
                return RedirectToAction("Index", "LandingPage");
            }

            return View(db.Usuario.ToList());
        }

        [HttpPost]
        public ActionResult Usuarios(string procurar, string button, string UsuarioId, string motivo)
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

            if (button == "buscar")
            {
                var pessoas = db.Usuario.Where(u => u.Nome.Contains(procurar));
                return View(pessoas.ToList());
            }
            else
            {
                Usuario usuario = db.Usuario.Find(Convert.ToInt32(UsuarioId));

                if (usuario.Bloqueado == Bloqueado.Não)
                {
                    usuario.Bloqueado = Bloqueado.Sim;
                    db.Entry(usuario).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    usuario.Bloqueado = Bloqueado.Não;
                    db.Entry(usuario).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Usuarios");
                }


                TempData["MSG"] = Funcoes.EnviarEmail(usuario.Email, "RYS - Sua conta foi bloqueada", motivo + "<br /> <b>Caso você tenha sido bloqueado irregularmente entre em contato com nossa equipe respondendo esse e-mail e iremos analisar sua situação.<br /> Att, Equipe RYS</b>");

                return RedirectToAction("Usuarios");
            }
        }

        public ActionResult EditarUsuario(int? id)
        {
            HttpCookie cookie = Request.Cookies.Get("UsuId");

            ViewBag.Publicacao = db.Publicacao.ToList();

            if ((cookie == null || cookie.Value == ""))
            {
                return RedirectToAction("Index", "Home");
            }


            ViewBag.Amizade = db.Amizade.ToList();
            ViewBag.Pessoa = db.Usuario.ToList();

            Usuario usu = db.Usuario.Find(Convert.ToInt32(cookie.Value));
            ViewBag.Usuario = usu;

            if (usu.TipoUsuario != TipoUsuario.Administrado)
            {
                return RedirectToAction("Index", "LandingPage");
            }

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

        public ActionResult EditarPersonalidade(int? id)
        {
            HttpCookie cookie = Request.Cookies.Get("UsuId");

            if ((cookie == null || cookie.Value == ""))
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Amizade = db.Amizade.ToList();
            ViewBag.Pessoa = db.Usuario.ToList();

            Usuario usu = db.Usuario.Find(Convert.ToInt32(cookie.Value));
            ViewBag.Usuario = usu;

            if (usu.TipoUsuario != TipoUsuario.Administrado)
            {
                return RedirectToAction("Index", "LandingPage");
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
            Equipe equipe = db.Equipe.Find(id);
            if (equipe == null)
            {
                return HttpNotFound();
            }

            return View(equipe);
        }

        [HttpPost]
        public ActionResult EditarPersonalidade(string Nome, int EquipeId, string Nacionalidade, DateTime Data)
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

            Equipe equipe = db.Equipe.Find(EquipeId);
            if (equipe == null)
            {
                return HttpNotFound();
            }

            equipe.Nome = Nome;
            equipe.Nacionalidade = Nacionalidade;
            equipe.Datadenascimento = Data;

            db.Entry(equipe).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("EditarPersonalidade");
        }

        public ActionResult EditarSerie(int? id)
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

            if (usu.TipoUsuario != TipoUsuario.Administrado)
            {
                return RedirectToAction("Index", "LandingPage");
            }

            ViewBag.Imagem = "default.jpg";

            if (mid != null)
            {
                ViewBag.Imagem = mid.Link;
            }
            ViewBag.Usuario = usu;

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

        public ActionResult AlterarDados(int? id)
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

            if (usu.TipoUsuario != TipoUsuario.Administrado)
            {
                return RedirectToAction("Index", "LandingPage");
            }

            ViewBag.Imagem = "default.jpg";

            if (mid != null)
            {
                ViewBag.Imagem = mid.Link;
            }
            ViewBag.Usuario = usu;

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
        public ActionResult AlterarDados([Bind(Include = "SeriesId,Nome,Paisdeorigem,Descricao")] Series series)
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

            if (usu.TipoUsuario != TipoUsuario.Administrado)
            {
                return RedirectToAction("Index", "LandingPage");
            }

            ViewBag.Imagem = "default.jpg";

            if (mid != null)
            {
                ViewBag.Imagem = mid.Link;
            }
            ViewBag.Usuario = usu;

            Series serie = db.Serie.Find(series.SeriesId);

            if (series == null)
            {
                return HttpNotFound();
            }

            serie.Nome = series.Nome;
            serie.Paisdeorigem = series.Paisdeorigem;
            serie.Descricao = series.Descricao;

            db.Entry(serie).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Series", "Administrador");
        }

        public ActionResult AdicionarPersonagem(int? id)
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

            if (usu.TipoUsuario != TipoUsuario.Administrado)
            {
                return RedirectToAction("Index", "LandingPage");
            }

            ViewBag.Imagem = "default.jpg";

            if (mid != null)
            {
                ViewBag.Imagem = mid.Link;
            }
            ViewBag.Usuario = usu;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Series series = db.Serie.Find(id);
            if (series == null)
            {
                return HttpNotFound();
            }

            ViewBag.Equipe = db.Equipe.ToList();

            return View(series);
        }

        [HttpPost]
        public ActionResult AdicionarPersonagem([Bind(Include = "EquipeId,SeriesId,EquipeTipoId,Personagem")] SerieEquipe seriequipe, string button, string procurar)
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

            if (button == "buscar")
            {
                var equipes = db.Equipe.Where(u => u.Nome.Contains(procurar));
                return View(equipes.ToList());
            }
            else
            {
                SerieEquipe sequipe = new SerieEquipe
                {
                    SeriesId = seriequipe.SeriesId,
                    EquipeId = seriequipe.EquipeId,
                    EquipeTipoId = seriequipe.EquipeTipoId,
                    Personagem = seriequipe.Personagem
                };

                db.SerieEquipe.Add(sequipe);
                db.SaveChanges();

                return RedirectToAction("Series");
            }
        }

        public ActionResult RemoverPersonagem(int? id)
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

            if (usu.TipoUsuario != TipoUsuario.Administrado)
            {
                return RedirectToAction("Index", "LandingPage");
            }

            ViewBag.Imagem = "default.jpg";

            if (mid != null)
            {
                ViewBag.Imagem = mid.Link;
            }
            ViewBag.Usuario = usu;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Series series = db.Serie.Find(id);
            if (series == null)
            {
                return HttpNotFound();
            }

            ViewBag.Equipe = db.Equipe.ToList();

            return View(series);
        }

        public ActionResult CadastroSerie()
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

            if (usu.TipoUsuario != TipoUsuario.Administrado)
            {
                return RedirectToAction("Index", "LandingPage");
            }

            ViewBag.Imagem = "default.jpg";

            if (mid != null)
            {
                ViewBag.Imagem = mid.Link;
            }
            ViewBag.Usuario = usu;

            return View();
        }

        [HttpPost]
        public ActionResult RemoverPersonagem([Bind(Include = "EquipeId,SeriesId,EquipeTipoId,Personagem")] SerieEquipe seriequipe, string button, string procurar)
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

            if (button == "buscar")
            {
                var equipes = db.Equipe.Where(u => u.Nome.Contains(procurar));
                return View(equipes.ToList());
            }
            else
            {
                SerieEquipe sequipe = db.SerieEquipe.Find(seriequipe.SeriesId,seriequipe.EquipeId,seriequipe.EquipeTipoId,seriequipe.Personagem);

                db.SerieEquipe.Remove(sequipe);
                db.SaveChanges();

                return RedirectToAction("Series");
            }
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

            if ((cookie == null || cookie.Value == ""))
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Amizade = db.Amizade.ToList();
            ViewBag.Pessoa = db.Usuario.ToList();

            Usuario usu = db.Usuario.Find(Convert.ToInt32(cookie.Value));
            Midia mid = db.Midia.Where(t => t.UsuarioId == usu.UsuarioId).ToList().FirstOrDefault();

            if (usu.TipoUsuario != TipoUsuario.Administrado)
            {
                return RedirectToAction("Index", "LandingPage");
            }

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

        public ActionResult AdicionarEpisodio(int? id)
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

            if (usu.TipoUsuario != TipoUsuario.Administrado)
            {
                return RedirectToAction("Index", "LandingPage");
            }

            ViewBag.Imagem = "default.jpg";

            if (mid != null)
            {
                ViewBag.Imagem = mid.Link;
            }
            ViewBag.Usuario = usu;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Series series = db.Serie.Find(id);
            if (series == null)
            {
                return HttpNotFound();
            }

            ViewBag.Equipe = db.Equipe.ToList();

            return View(series);
        }

        [HttpPost]
        public ActionResult AdicionarEpisodio([Bind(Include = "EpisodioId,Numero,Titulo,Temporada,SeriesId")] Episodio episodio)
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

            if (usu.TipoUsuario != TipoUsuario.Administrado)
            {
                return RedirectToAction("Index", "LandingPage");
            }

            ViewBag.Imagem = "default.jpg";

            if (mid != null)
            {
                ViewBag.Imagem = mid.Link;
            }
            ViewBag.Usuario = usu;

            Series series = db.Serie.Find(episodio.SeriesId);
            if (series == null)
            {
                return HttpNotFound();
            }

            ViewBag.Equipe = db.Equipe.ToList();

            if (ModelState.IsValid)
            {
                db.Episodio.Add(episodio);
                db.SaveChanges();
            }

            return View(series);
        }
    }
}