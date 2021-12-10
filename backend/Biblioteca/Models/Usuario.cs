using System;

namespace Biblioteca.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime CriadoEm { get; set; }
        public Role Role { get; internal set; }
        public int RoleId { get; set; }
    }
}
