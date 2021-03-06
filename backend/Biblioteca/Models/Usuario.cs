using System;

namespace Biblioteca.Models
{
    public class Usuario
    {
        #region Propriedades

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime CriadoEm { get; set; }
        public Role Role { get; set; }
        public int RoleId { get; set; }

        #endregion        
    }
}
