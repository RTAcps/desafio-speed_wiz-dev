using System;

namespace Biblioteca.InputModel
{
    public class UsuarioInput
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int RoleId { get; set; }
    }
}
