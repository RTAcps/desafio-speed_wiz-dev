using Biblioteca.Context;
using Biblioteca.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Biblioteca.Dados.Gerador
{
    public class GeradorDados
    {
        public static void InicializarDados(IServiceProvider provedorServico)
        {
            using var contexto = new BibliotecaDbContext(provedorServico.GetRequiredService<DbContextOptions<BibliotecaDbContext>>());
            if (contexto.Livros.Any())
            {
                return;
            }

            contexto.Livros.AddRange
                (
                    new Livro
                    {
                        Id = 1,
                        Descricao = "ASP.NET Core Web API",
                        ISBN = 26484548348,
                        AnoLancamento = 2010
                    },

                    new Livro
                    {
                        Id = 2,
                        Descricao = "C#",
                        ISBN = 37593437439,
                        AnoLancamento = 2017
                    }
                ); ;

            contexto.SaveChanges();
        }
    }
}
