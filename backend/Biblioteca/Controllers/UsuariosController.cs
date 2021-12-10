using Biblioteca.Context;
using Biblioteca.InputModel;
using Biblioteca.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Biblioteca.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly BibliotecaDbContext _bibliotecaDbContext;

        public UsuariosController(BibliotecaDbContext bibliotecaDbContext)
        {
            _bibliotecaDbContext = bibliotecaDbContext;
        }

        [HttpPost("cadastrar-admin")]
        public async Task<IActionResult> CadastrarAdministrador(UsuarioInput dadosEntrada)
        {
            var admin = new Usuario()
            {
                Nome = dadosEntrada.Nome,
                Email = dadosEntrada.Email,
                Senha = dadosEntrada.Senha,
                RoleId = dadosEntrada.RoleId,
                CriadoEm = DateTime.Now
            };

            await _bibliotecaDbContext.Usuarios.AddAsync(admin);
            await _bibliotecaDbContext.SaveChangesAsync();

            return Ok(
                        new
                        {
                            Status = "Sucesso",
                            Code = StatusCodes.Status200OK,
                            adminCriado = admin.RoleId
                        }
                    );
        }

        [HttpPost("cadastrar-comum")]
        public async Task<IActionResult> CadastrarComum(UsuarioInput dadosEntrada)
        {
            var comum = new Usuario()
            {
                Nome = dadosEntrada.Nome,
                Email = dadosEntrada.Email,
                Senha = dadosEntrada.Senha,
                RoleId = dadosEntrada.RoleId,
                CriadoEm = DateTime.Now
            };

            await _bibliotecaDbContext.Usuarios.AddAsync(comum);
            await _bibliotecaDbContext.SaveChangesAsync();

            return Ok(
                        new
                        {
                            Status = "Sucesso",
                            Code = StatusCodes.Status200OK,
                            comumCriado = comum.RoleId
                        }
                    );
        }
    }
}
