using Biblioteca.Adaptadores;
using Biblioteca.Dados.Repositorio.Interfaces;
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

        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IJwtAuthGerenciador _jwtAuthGerenciador;

        #endregion

        #region Construtor

        public AuthController(IUsuarioRepositorio usuarioRepositorio,
                              IJwtAuthGerenciador jwtAuthGerenciador)
        {
            _usuarioRepositorio = usuarioRepositorio;
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
                var usuario = await _usuarioRepositorio
            .RecuperarPorCredenciais(jwtUsuarioCredenciais.Email, jwtUsuarioCredenciais.Senha);

                if (!ModelState.IsValid)
                {
                    return BadRequest("Parâmetros inválidos, verifique!");
                }                       

                if (usuario == null)
                {
                    return NotFound("Usuário não encontrado!");
                }

                var credenciais = new JwtCredenciais
                {
                    Email = usuario.Email,
                    Senha = usuario.Senha,
                    Role = usuario.Role.Nome
                };

                var response = new
                {
                    Status = "Sucesso",
                    Code = StatusCodes.Status200OK,
                    Data = _jwtAuthGerenciador.GerarToken(usuario.ParaJwtCredenciais())
                };

                return Ok(response);

            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Status = "Erro",
                    Code = StatusCodes.Status500InternalServerError,
                    Mensagem = e.Message
                });
            }
        }

        #endregion
        
    }
}
