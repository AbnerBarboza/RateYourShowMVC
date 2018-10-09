using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RateYourShowMVC.Models
{
    public enum TipoLista
    {
        colaborativa = 1,
        publica = 2
    }

    public enum Status
    {
        Pendente = 'P',
        Aceito = 'A',
        Rejeitado = 'R'
    }

    public enum Tipo
    {
        Requisicao = 'R',
        Sugestão = 'S'
    }

    public enum Seguindo
    {
        Sim = 'S',
        Não = 'N'
    }

    public enum Sexo
    {
        Masculino = 1,
        Feminino = 2,
        Outro = 3
    }

    public enum Inativo
    {
        Não = 'N',
        Sim = 'S'
    }

    public enum TipoUsuario
    {
        Administrado = 'A',
        Comum = 'C',
        Premium = 'P'
    }

    public enum Bloqueado
    {
        Não = 'N',
        Sim = 'S'
    }

}