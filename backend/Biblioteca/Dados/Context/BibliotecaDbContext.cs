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
        public BibliotecaDbContext(DbContextOptions<BibliotecaDbContext> options) : base(options) { }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>()
                .HasData(
                    new Role(1, "Admnistrador"),
                    new Role(2, "Comum")
                );

            modelBuilder.Entity<Usuario>()
                .HasData(
                    new Usuario(1, "Rodrigo", "rodrigo@gmail.com", "Test@123", 1),
                    new Usuario(2, "Junior", "junior@gmail.com", "Test@123", 2)
                );
        }
    }
}
