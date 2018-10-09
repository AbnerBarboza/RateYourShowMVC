using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CryptSharp;
namespace RateYourShowMVC.Models
{
    public static class Criptografia
    {
        public static string Codifica(string senha)
        {
            string crypt = CryptSharp.Crypter.Sha512.GenerateSalt();
            crypt = CryptSharp.Crypter.Sha512.Crypt(senha, crypt);
            return crypt;
        }

        public static bool Compara(string senha, string hash)
        {
            return Crypter.CheckPassword(senha, hash);
        }
    }
}