using Biblioteca.Context;
using Biblioteca.InputModel;
using Biblioteca.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class LivrosController : ControllerBase
    {
        #region Campos

        private readonly BibliotecaDbContext _bibliotecaDbContext;

        #endregion

        #region Construtor

        public LivrosController(BibliotecaDbContext bibliotecaDbContext)
        {
            _bibliotecaDbContext = bibliotecaDbContext;
        }

        #endregion

        #region Metodos

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ListarLivros()
        {
            var livros = await _bibliotecaDbContext.Livros
                                .Include(x => x.Autor)
                                .ToListAsync();
            return Ok(new
            {
                data = livros.Select(x =>
                                new
                                {
                                    Id = x.Id, 
                                    Descricao = x.Descricao,
                                    ISBN = x.ISBN,
                                    AnoLancamento = x.AnoLancamento,
                                    Autor = x.AutorId, 
                                    NomeAutor = x.Autor.Nome + " " + x.Autor.Sobrenome
                                })
            });
        }

        [HttpPut]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Atualizar(AtualizarLivroInput dadosEntrada)
        {
            var livro = await _bibliotecaDbContext.Livros
                .Where(x => x.Id == dadosEntrada.Id)
                .FirstOrDefaultAsync();

            if (livro == null)
            {
                return NotFound(new
                {
                    Status = "Falha",
                    Code = 404,
                    Data = "Não encontrado!"
                });
            }

            livro.Descricao = dadosEntrada.Descricao;
            livro.ISBN = dadosEntrada.ISBN;
            livro.AnoLancamento = dadosEntrada.AnoLancamento;

            _bibliotecaDbContext.Livros.Update(livro);
            await _bibliotecaDbContext.SaveChangesAsync();

            return Ok(new
            {
                Status = "Sucesso",
                Code = 200,
                Mensagem = new
                {
                    NomeLivro = livro.Descricao,
                    ISBN = livro.ISBN,
                    AnoLancamento = livro.AnoLancamento
                }
            });
        }

        [HttpPost]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> CadastrarLivro(LivroInput dadosEntrada)
        {
            var autor = await _bibliotecaDbContext.Autores
                .Where(x => x.Id == dadosEntrada.AutorId)
                .FirstOrDefaultAsync();
            
            if (autor == null)
            {
                return NotFound(
                                    new
                                    {
                                        Status = "Falha",
                                        Code = 404,
                                        Data = "Autor não encontrado!"
                                    }
                                );
            }

            var livro = new Livro()
            {
                Descricao = dadosEntrada.Descricao,
                ISBN = dadosEntrada.ISBN,
                AnoLancamento = dadosEntrada.AnoLancamento,
                CriadoEm = DateTime.Now,
                AutorId = dadosEntrada.AutorId
            };

            await _bibliotecaDbContext.Livros.AddAsync(livro);
            await _bibliotecaDbContext.SaveChangesAsync();

            return Ok(
                        new
                        {
                            Status = "Sucesso",
                            Code = 200,
                            Data = new
                            {
                                Descricao = livro.Descricao,
                                ISBN = livro.ISBN,
                                AnoLancamento = dadosEntrada.AnoLancamento
                            }
                        }
                    );
        }

        [HttpDelete]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Deletar(int codigo)
        {
            var livro = await _bibliotecaDbContext.Livros
                .Where(x => x.Id == codigo)
                .FirstOrDefaultAsync();

            if (livro == null)
            {
                return NotFound(new
                {
                    Status = "Falha",
                    Code = 404,
                    Data = "Não encontrado!"
                });
            }

            _bibliotecaDbContext.Livros.Remove(livro);
            await _bibliotecaDbContext.SaveChangesAsync();

            return Ok(new
            {
                Status = "Sucesso",
                Code = 201,
                Data = "Registro deletado com sucesso!"
            });        
        }

        #endregion
    }
}
