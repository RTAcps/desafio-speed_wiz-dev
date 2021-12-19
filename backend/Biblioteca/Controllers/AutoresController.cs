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
    public class AutoresController : ControllerBase
    {
        #region Campos

        private readonly BibliotecaDbContext _bibliotecaDbContext;

        #endregion

        #region Construtor

        public AutoresController(BibliotecaDbContext bibliotecaDbContext)
        {
            _bibliotecaDbContext = bibliotecaDbContext;
        }

        #endregion

        #region Metodos

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ListarAutores()
        {
            var autores = await _bibliotecaDbContext.Autores.ToListAsync();

            if (autores.Any())
            {
                return Ok(new
                {
                    data = autores.Select(x =>
                    new
                    {
                        autorId = x.Id,
                        Nome = x.Nome,
                        Sobrenome = x.Sobrenome
                    })
                });
            }            
            
            return NotFound(new
            {
                Status = "Falha",
                Code = 404,
                Mensagem = "Não encontrado!"
            });
        }


        [HttpPost]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> CadastrarAutor(AutorInput dadosEntrada)
        {
            var autor = new Autor()
            {
                Nome = dadosEntrada.Nome,
                Sobrenome = dadosEntrada.Sobrenome,
                CriadoEm = DateTime.Now
            };

            await _bibliotecaDbContext.Autores.AddAsync(autor);
            await _bibliotecaDbContext.SaveChangesAsync();

            return Ok(new
            {
                Status = "Sucesso",
                Code = 201,
                Data = new
                {
                    NomeAutor = autor.Nome,
                    SobrenomeAutor = autor.Sobrenome
                }
            });
        }

        #endregion
    }
}
