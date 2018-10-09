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

            Usuario usu = db.Usuario.Find(Convert.ToInt32(cookie.Value));
            Midia mid = db.Midia.Where(t => t.UsuarioId == usu.UsuarioId).ToList().FirstOrDefault();

            ViewBag.Imagem = "default.jpg";

            if (mid != null){
                ViewBag.Imagem = mid.Link;
            }


            ViewBag.Usuario = usu;
            return View();
        }
    }
}