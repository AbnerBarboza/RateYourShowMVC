using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RateYourShowMVC.Models;

namespace RateYourShowMVC.Services
{
    public class UsuarioServices
    {
        private Contexto db = new Contexto();

        public Boolean EmailWhere(string email)
        {

            Usuario usu = db.Usuario.Where(t => t.Email == email).ToList().FirstOrDefault();
            if (usu != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GeraCodigo ()
        {
            RecuperarSenha rec = new RecuperarSenha();
            int codigo;

            do
            {
                Random random = new Random();
                codigo = random.Next(100000000, 999999999);
                rec = db.RecuperarSenha.Where(t => t.SenhaId == codigo).ToList().FirstOrDefault();
            } while (rec != null);

            return codigo;

        }
    }
}