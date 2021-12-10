using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Services.Auth.Jwt
{
    public class JwtUsuarioCredenciais
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }
    }
}
