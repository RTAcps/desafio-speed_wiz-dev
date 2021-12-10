using Biblioteca.Context;
using Biblioteca.InputModel;
using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class LivrosController : ControllerBase
    {
        private readonly BibliotecaDbContext _bibliotecaDbContext;

        public LivrosController(BibliotecaDbContext bibliotecaDbContext)
        {
            _bibliotecaDbContext = bibliotecaDbContext;
        }

        [HttpGet("filtrar-por-descricao")]
        public async Task<IActionResult> FiltrarPorDescricao(string descricao)
        {
            var livros = await _bibliotecaDbContext.Livros
                .Where(x => x.Descricao.Contains(descricao))
                .ToListAsync();

            if (livros.Any())
            {
                return Ok(livros);
            }

            return NotFound("Nenhum registro encontrado!");
        }

        [HttpGet("filtrar-por-isbn")]
        public async Task<IActionResult> FiltrarPorISBN(int isbn)
        {
            var livro = await _bibliotecaDbContext.Livros
                .Where(x => x.ISBN == isbn)
                .FirstOrDefaultAsync();

            if (livro == null)
            {
                return NotFound("Livro não existe no banco de dados!");
            }

            return Ok(livro);
        }

        [HttpGet("filtrar-por-ano-lancamento")]
        public async Task<IActionResult> FiltrarPorAnoLancamento(int anoLancamento)
        {
            var livros = await _bibliotecaDbContext.Livros
                .Where(x => x.AnoLancamento == anoLancamento)
                .ToListAsync();

            if (livros.Any())
            {
                return Ok(livros);
            }

            return NotFound("Nenhum livro encontrado!");
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar(AtualizarLivroInput dadosEntrada)
        {
            var livro = await _bibliotecaDbContext.Livros
                .Where(x => x.Id == dadosEntrada.Id)
                .FirstOrDefaultAsync();

            if (livro == null)
            {
                return NotFound("Livro não existe!");
            }

            livro.Descricao = dadosEntrada.Descricao;
            livro.ISBN = dadosEntrada.ISBN;
            livro.AnoLancamento = dadosEntrada.AnoLancamento;

            _bibliotecaDbContext.Livros.Update(livro);
            await _bibliotecaDbContext.SaveChangesAsync();

            return Ok(livro);
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarLivro(LivroInput dadosEntrada)
        {
            var livro = new Livro()
            {
                Descricao = dadosEntrada.Descricao,
                ISBN = dadosEntrada.ISBN,
                AnoLancamento = dadosEntrada.AnoLancamento,
                AutorId = dadosEntrada.AutorId
            };

            await _bibliotecaDbContext.Livros.AddAsync(livro);
            await _bibliotecaDbContext.SaveChangesAsync();

            return Ok(
                        new
                        {
                            success = true,
                            data = new
                            {
                                descricao = livro.Descricao,
                                isbn = livro.ISBN
                            }
                        }
                    );
        }

        [HttpDelete]
        public async Task<IActionResult> Deletar(int codigo)
        {
            var livro = await _bibliotecaDbContext.Livros
                .Where(x => x.Id == codigo)
                .FirstOrDefaultAsync();

            if (livro == null)
            {
                return NotFound("Resgistro não encontrado!");
            }

            _bibliotecaDbContext.Livros.Remove(livro);
            await _bibliotecaDbContext.SaveChangesAsync();

            return Ok("Registro deletado com sucesso!");
        }
    }
}
