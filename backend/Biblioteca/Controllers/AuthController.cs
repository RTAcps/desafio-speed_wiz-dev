using Biblioteca.Adaptadores;
using Biblioteca.Context;
using Biblioteca.Services.Auth.Jwt;
using Biblioteca.Services.Auth.Jwt.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Biblioteca.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthController : ControllerBase
    {
        #region Campos

        private readonly BibliotecaDbContext _bibliotecaDbContext;
        private readonly IJwtAuthGerenciador _jwtAuthGerenciador;

        #endregion

        #region Construtor

        public AuthController(BibliotecaDbContext bibliotecaDbContext,
                              IJwtAuthGerenciador jwtAuthGerenciador)
        {
            _bibliotecaDbContext = bibliotecaDbContext;
            _jwtAuthGerenciador = jwtAuthGerenciador;
        }

        #endregion

        #region Metodos

        [MapToApiVersion("1.0")]
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Autenticar([FromBody] JwtUsuarioCredenciais jwtUsuarioCredenciais)
        {
            try
            {                
                if (!ModelState.IsValid)
                {
                    return BadRequest(new
                    {
                        Status = "Falha",
                        Code = 400,
                        Data = "Parâmetros inválidos, verifique!"
                    });
                }

                var usuario = await _bibliotecaDbContext
                    .Usuarios.SingleOrDefaultAsync(x => x
                    .Email == jwtUsuarioCredenciais.Email && x
                    .Senha == jwtUsuarioCredenciais.Senha);

                if (usuario == null)
                {
                    return NotFound(new
                    {
                        Status = "Falha",
                        Code = 404,
                        Data = "Usuário não encontrado!"
                    });
                }                               
                
                return Ok(new
                {
                    Status = "Sucesso",
                    Code = StatusCodes.Status200OK,
                    Data = _jwtAuthGerenciador.GerarToken(usuario.ParaJwtCredenciais())
                });

            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Status = "Erro",
                    Code = 500,
                    Mensagem = e.Message
                });
            }
        }

        #endregion
        
    }
}
