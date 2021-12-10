using Biblioteca.Context;
using Biblioteca.InputModel;
using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Biblioteca.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly BibliotecaDbContext _bibliotecaDbContext;

        public UsuariosController(BibliotecaDbContext bibliotecaDbContext)
        {
            _bibliotecaDbContext = bibliotecaDbContext;
        }

        [HttpPost("cadastrar-administrador")]
        public async Task<IActionResult> CadastrarAdministrador(UsuarioInput dadosEntrada)
        {
            var administrador = new Usuario()
            {
                Nome = dadosEntrada.Nome,
                Email = dadosEntrada.Email,
                Senha = dadosEntrada.Senha,
                RoleId = dadosEntrada.RoleId,
                CriadoEm = DateTime.Now
            };

            await _bibliotecaDbContext.Usuarios.AddAsync(administrador);
            await _bibliotecaDbContext.SaveChangesAsync();

            return Ok(
                        new
                        {
                            administradorCriado = administrador.RoleId
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
                            administradorCriado = comum.RoleId
                        }
                    );
        }
    }
}
