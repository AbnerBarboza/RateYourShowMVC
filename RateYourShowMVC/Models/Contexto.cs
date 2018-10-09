namespace RateYourShowMVC.Models
{
    using MySql.Data.MySqlClient;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations.History;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;
    using System.Web;

    public class Contexto : DbContext
    {

        public Contexto() : base(nameOrConnectionString: "StringConexao") { }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

        }
        public virtual DbSet<Amizade> Amizade { get; set; }
        public virtual DbSet<Comentario> Comentario { get; set; }
        public virtual DbSet<Contato> Contato { get; set; }
        public virtual DbSet<Denuncia> Denuncia { get; set; }
        public virtual DbSet<Episodio> Episodio { get; set; }
        public virtual DbSet<Equipe> Equipe { get; set; }
        public virtual DbSet<Genero> Genero { get; set; }
        public virtual DbSet<Lista> Lista { get; set; }
        public virtual DbSet<Mensagem> Mensagem { get; set; }
        public virtual DbSet<Midia> Midia { get; set; }
        public virtual DbSet<Perguntas> Pergunta { get; set; }
        public virtual DbSet<Publicacao> Publicacao { get; set; }
        public virtual DbSet<Quizzes> Quizz { get; set; }
        public virtual DbSet<RecuperarSenha> RecuperarSenha { get; set; }
        public virtual DbSet<Requisicao> Requisicao { get; set; }
        public virtual DbSet<SerieEquipe> SerieEquipe { get; set; }
        public virtual DbSet<SerieGenero> SerieGenero { get; set; }
        public virtual DbSet<SerieLista> SerieLista { get; set; }
        public virtual DbSet<Tipocontato> TipoContato { get; set; }
        public virtual DbSet<Series> Serie { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Usuarioepisodio> UsuarioEpisodio { get; set; }
        public virtual DbSet<Usuariolista> UsuarioLista { get; set; }
        public virtual DbSet<Usuarioquizz> UsuarioQuizz { get; set; }
        public virtual DbSet<Usuarioserie> UsuarioSerie { get; set; }
    }
}