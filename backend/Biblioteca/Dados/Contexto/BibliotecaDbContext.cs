using Biblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Context
{
    public class BibliotecaDbContext : DbContext
    {
        #region DbSets

        public DbSet<Role> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Livro> Livros { get; set; }

        #endregion

        #region Contrutor

        public BibliotecaDbContext(DbContextOptions<BibliotecaDbContext> options) 
            : base(options) { }

        #endregion

        #region Metodos

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfigurationsFromAssembly(typeof(BibliotecaDbContext)
                .Assembly
                );

            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}
