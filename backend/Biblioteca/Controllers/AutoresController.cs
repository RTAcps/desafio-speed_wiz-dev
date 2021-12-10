using Biblioteca.Context;
using Biblioteca.InputModel;
using Biblioteca.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.Controllers
{   
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AutoresController : ControllerBase
    {
        private readonly BibliotecaDbContext _bibliotecaDbContext;

        public AutoresController(BibliotecaDbContext bibliotecaDbContext)
        {
            _bibliotecaDbContext = bibliotecaDbContext;
        }

        [HttpGet("filtrar-por-id")]
        public async Task<IActionResult> FiltrarPorId(string id)
        {
            var autores = await _bibliotecaDbContext.Autores
                .Where(x => x.Nome.Contains(id))
                .ToListAsync();

            if (autores.Any())
            {
                return Ok(new
                {
                    Status = "Sucesso",
                    Code = StatusCodes.Status200OK,
                    Mensagem = autores
                });
            }

            return NotFound(new
            {
                Status = "Falha",
                Code = StatusCodes.Status400BadRequest,
                Mensagem = "Não encontrado"
            });
        }

        [HttpGet("filtrar-por-nome")]
        public async Task<IActionResult> FiltrarPorNome(string nome)
        {
            var autores = await _bibliotecaDbContext.Autores
                .Where(x => x.Nome.Contains(nome))
                .ToListAsync();

            if (autores.Any())
            {
                return Ok(new
                {
                    Status = "Sucesso",
                    Code = StatusCodes.Status200OK,
                    Mensagem = autores
                });
            }

            return NotFound(new
            {
                Status = "Falha",
                Code = StatusCodes.Status400BadRequest,
                Mensagem = "Não encontrado"
            });
        }        
        
        [HttpGet("filtrar-por-sobrenome")]
        public async Task<IActionResult> FiltrarPorSobrenome(string sobrenome)
        {
            var autores = await _bibliotecaDbContext.Autores
                .Where(x => x.Nome.Contains(sobrenome))
                .ToListAsync();

            if (autores.Any())
            {
                return Ok(new
                {
                    Status = "Sucesso",
                    Code = StatusCodes.Status200OK,
                    Mensagem = autores
                });
            }

            return NotFound(new
            {
                Status = "Falha",
                Code = StatusCodes.Status400BadRequest,
                Mensagem = "Não encontrado"
            });
        }

        [HttpGet("listar-todos")]
        public async Task<IActionResult> Listar()
        {
            return Ok(await _bibliotecaDbContext.Autores.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarAutor(AutorInput dadosEntrada)
        {
            var autor = new Autor()
            {
                Nome = dadosEntrada.Nome,
                Sobrenome = dadosEntrada.Sobrenome
            };

            await _bibliotecaDbContext.Autores.AddAsync(autor);
            await _bibliotecaDbContext.SaveChangesAsync();

            return Ok(new
            {
                Status = "Sucesso",
                Code = StatusCodes.Status200OK,
                Mensagem = autor
            });
        }
    }
}
