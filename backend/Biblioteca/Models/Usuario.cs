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

        #region Construtor

        public Usuario() { }

        public Usuario(int id, string nome, string email, string senha, int roleId)
            : this(nome, email, senha, roleId)
        {
            Id = id;           
        } 

        public Usuario(string nome, string email, string senha, int roleId)
        {            
            Nome = nome;
            Email = email;
            Senha = senha;
            RoleId = roleId;
        }

        #endregion
    }
}
